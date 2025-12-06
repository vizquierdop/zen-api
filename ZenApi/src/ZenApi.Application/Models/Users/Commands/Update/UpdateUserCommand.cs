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
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Models.Users.Commands.Update
{
    public record UpdateUserCommand : IRequest, IMapTo<User>
    {
        public required int Id { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public bool? IsActive { get; init; }
        public string? Phone { get; init; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserCommandRepository _repository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserCommandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity is null)
                throw new Exception("User not found");

            _mapper.Map(request, entity);

            await _repository.UpdateAsync(entity, cancellationToken);
        }
    }
}
