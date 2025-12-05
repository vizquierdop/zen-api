using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces;
using ZenApi.Application.Common.Mappings;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Models.OfferedServices.Commands.Create
{
    public record CreateOfferedServiceCommand : IRequest<int>, IMapTo<OfferedService>
    {
        public required string Name { get; init; }
        public string? Description { get; init; }
        public int? Duration { get; init; }
        public required int Price { get; init; }
        public required int BusinessId { get; init; }



        public class CreateOfferedServiceCommandHandler : IRequestHandler<CreateOfferedServiceCommand, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CreateOfferedServiceCommandHandler(IApplicationDbContext context, IMapper mapper)
            { 
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(CreateOfferedServiceCommand request, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<OfferedService>(request);
                entity.IsActive = true;

                if (request.Duration is null || !request.Duration.HasValue)
                {
                    entity.Duration = 0;
                }

                await _context.OfferedServices.AddAsync(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
