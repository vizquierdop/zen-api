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

namespace ZenApi.Application.Models.OfferedServices.Commands.Update
{
    public record UpdateOfferedServiceCommand : IRequest, IMapTo<OfferedService>
    {
        public required int Id { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public int? Duration { get; init; }
        public int? Price { get; init; }
        public bool? IsActive { get; init; }
    }

    public class UpdateOfferedServiceCommandHandler : IRequestHandler<UpdateOfferedServiceCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateOfferedServiceCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle(UpdateOfferedServiceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.OfferedServices
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)
                throw new Exception("Offered service not found");

            _mapper.Map(request, entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
