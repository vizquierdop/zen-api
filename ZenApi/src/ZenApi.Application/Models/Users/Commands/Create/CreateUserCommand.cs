using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces;
using ZenApi.Application.Common.Interfaces.Repositories;
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
        private readonly IUserCommandRepository _users;
        private readonly ICategoryQueryRepository _categories;
        private readonly ISecurityService _securityService;

        public CreateUserCommandHandler(
            IUserCommandRepository users,
            ICategoryQueryRepository categories,
            ISecurityService securityService)
        {
            _users = users;
            _categories = categories;
            _securityService = securityService;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //var hashedPassword = _securityService.HashPassword(request.Password);

            var appUserId = await _securityService.CreateUserAsync(request.Email, request.Password, request.Role);

            var domainUser = new User
            {
                Id = appUserId,
                Email = request.Email,
                //Password = hashedPassword,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                IsActive = request.Active,
                Role = request.Role,
                ProvinceId = request.ProvinceId
            };

            if (request.Role != UserRole.Business || request.Business is null)
            {
                return await _users.CreateAsync(domainUser, cancellationToken);
            }

            var businessDto = request.Business;

            var business = new Business
            {
                Name = businessDto.Name,
                Address = businessDto.Address ?? "",
                ProvinceId = request.ProvinceId,
                Keyword1 = businessDto.Keyword1,
                Keyword2 = businessDto.Keyword2,
                Keyword3 = businessDto.Keyword3,
                Phone = request.Phone ?? "",
                IsActive = true,
                SimultaneousBookings = businessDto.SimultaneousBookings > 0
                    ? businessDto.SimultaneousBookings
                    : 1
            };

            if (businessDto.CategoryIds?.Length > 0)
            {
                var validIds = await _categories.GetValidIdsAsync(businessDto.CategoryIds, cancellationToken);

                business.BusinessCategories = validIds
                    .Select(id => new BusinessCategory { CategoryId = id })
                    .ToList();
            }

            var newUserId = await _users.CreateUserWithBusinessAsync(domainUser, business, cancellationToken);

            return newUserId;
        }
    }
}
