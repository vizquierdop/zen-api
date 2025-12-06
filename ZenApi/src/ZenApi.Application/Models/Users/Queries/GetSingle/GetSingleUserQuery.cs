using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Application.Dtos.Users;

namespace ZenApi.Application.Models.Users.Queries.GetSingle
{
    public record GetSingleUserQuery(int Id) : IRequest<UserDto>;

    public class GetSingleUserQueryHandler : IRequestHandler<GetSingleUserQuery, UserDto>
    {
        private readonly IUserQueryRepository _repository;

        public GetSingleUserQueryHandler(IUserQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDto> Handle(GetSingleUserQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (dto == null)
                throw new KeyNotFoundException($"User with Id {request.Id} not found.");

            return dto;
        }
    }
}
