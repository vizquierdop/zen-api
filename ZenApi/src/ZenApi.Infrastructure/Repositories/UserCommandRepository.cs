using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Domain.Entities;
using ZenApi.Infrastructure.Persistence;

namespace ZenApi.Infrastructure.Repositories
{
    public class UserCommandRepository : IUserCommandRepository
    {
        private readonly ApplicationDbContext _context;

        public UserCommandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AttachBusinessAsync(int userId, int businessId, CancellationToken cancellationToken)
        {
            var user = await _context.UsersDomain.FindAsync(new object[] { userId }, cancellationToken);
            if (user is null)
                throw new Exception($"User {userId} not found.");

            user.BusinessId = businessId;
            _context.UsersDomain.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> CreateAsync(User entity, CancellationToken cancellationToken)
        {
            await _context.UsersDomain.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task<int> CreateUserWithBusinessAsync(User user, Business business, CancellationToken cancellationToken)
        {
            {
                // Transaction for user + business creation
                await using var tx = await _context.Database.BeginTransactionAsync(cancellationToken);

                // Create user
                await _context.UsersDomain.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                // Set business.UserId and create business
                business.UserId = user.Id;
                await _context.Businesses.AddAsync(business, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken); // business.Id populated

                // Create default availabilities
                if (business.Availabilities == null || business.Availabilities.Count == 0)
                {
                    for (int day = 0; day <= 6; day++)
                    {
                        var availability = new Availability
                        {
                            DayOfWeek = day,
                            IsActive = false,
                            BusinessId = business.Id
                        };
                        _context.Availabilities.Add(availability);
                    }
                    await _context.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    // If the Business instance already had Availability items with BusinessId=0,
                    // set BusinessId and add them.
                    foreach (var a in business.Availabilities)
                    {
                        if (a.BusinessId == 0)
                            a.BusinessId = business.Id;
                        _context.Availabilities.Add(a);
                    }
                    await _context.SaveChangesAsync(cancellationToken);
                }

                // If BusinessCategories are present they should reference CategoryId only;
                //    ensure BusinessId set and save (business.BusinessCategories may already be in business object)
                if (business.BusinessCategories != null && business.BusinessCategories.Count > 0)
                {
                    foreach (var bc in business.BusinessCategories)
                    {
                        bc.BusinessId = business.Id;
                        _context.BusinessCategories.Add(bc);
                    }
                    await _context.SaveChangesAsync(cancellationToken);
                }

                // Attach business id to user and update
                user.BusinessId = business.Id;
                _context.UsersDomain.Update(user);
                await _context.SaveChangesAsync(cancellationToken);

                await tx.CommitAsync(cancellationToken);

                return user.Id;
            }
        }

        public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.UsersDomain
                .Include(x => x.Business)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(User entity, CancellationToken cancellationToken)
        {
            _context.UsersDomain.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
