using AutoMapper;
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
    public class ReservationCommandRepository : IReservationCommandRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationCommandRepository(ApplicationDbContext context, IConfigurationProvider mapper)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Reservation entity, CancellationToken cancellationToken)
        {
            await _context.Reservations.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _context.Reservations
                .FindAsync(new object[] { id }, cancellationToken);

            if (entity is null)
                throw new Exception("Reservation not found");

            _context.Reservations.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Reservation?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            // TODO Check this is ok
            return await _context.Reservations
                .Include(x => x.Service)
                .Include(x => x.Service.Business)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Reservation entity, CancellationToken cancellationToken)
        {
            _context.Reservations.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
