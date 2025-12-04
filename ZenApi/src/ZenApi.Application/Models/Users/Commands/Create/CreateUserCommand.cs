using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces;
using ZenApi.Application.Common.Mappings;
using ZenApi.Application.Dtos.Businesses;
using ZenApi.Domain.Entities;
using ZenApi.Domain.Enums;

namespace ZenApi.Application.Models.Users.Commands.Create
{
    public record CreateUserCommand : IRequest<int>, IMapTo<User>
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
        public required string FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Phone { get; init; }
        public required UserRole Role { get; init; }

        public bool Active { get; init; } = true;

        public required int ProvinceId { get; init; }
        public CreateBusinessDto? Business { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISecurityService _securityService;

        public CreateUserCommandHandler(IApplicationDbContext context, IMapper mapper, ISecurityService securityService)
        {
            _context = context;
            _mapper = mapper;
            _securityService = securityService;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = _securityService.HashPassword(request.Password);

            var user = _mapper.Map<User>(request);
            user.Password = hashedPassword;

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            if (request.Role == UserRole.Business && request.Business is not null)
            {
                var business = _mapper.Map<Business>(request.Business);

                business.UserId = user.Id;
                business.ProvinceId = request.ProvinceId;
                business.IsActive = true;
                business.SimultaneousBookings = 1;
                business.Name = request.Business.Name;
                business.Keyword1 = request.Business.Keyword1;
                business.Keyword2 = request.Business.Keyword2;
                business.Keyword3 = request.Business.Keyword3;

                business.BusinessCategories ??= new List<BusinessCategory>();
                business.Availabilities ??= new List<Availability>();

                if (request.Business.CategoryIds != null && request.Business.CategoryIds.Length > 0)
                {
                    var validCategoryIds = await _context.Categories
                        .Where(c => request.Business.CategoryIds.Contains(c.Id))
                        .Select(c => c.Id)
                        .ToListAsync(cancellationToken);

                    if (validCategoryIds.Count != request.Business.CategoryIds.Length)
                        throw new Exception("Some provided categories do not exist.");

                    foreach (var categoryId in validCategoryIds)
                    {
                        business.BusinessCategories.Add(new BusinessCategory
                        {
                            CategoryId = categoryId
                        });
                    }
                }

                _context.Businesses.Add(business);

                for (int day = 0; day <= 6; day++)
                {
                    business.Availabilities.Add(new Availability
                    {
                        DayOfWeek = day,
                        IsActive = false,
                        BusinessId = business.Id
                    });
                }

                await _context.SaveChangesAsync(cancellationToken);


                user.BusinessId = business.Id;

                await _context.SaveChangesAsync(cancellationToken);
            }

            return user.Id;
        }
    }
}
