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

namespace ZenApi.Application.Models.Holidays.Commands.Create
{
    public class CreateHolidayCommand : IRequest<int>, IMapTo<Holiday>
    {
        public required DateTime StartDate { get; init; }
        public required DateTime EndDate { get; init; }
        public required int BusinessId { get; init; }

        public class CreateHolidayCommandHandler : IRequestHandler<CreateHolidayCommand, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CreateHolidayCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(CreateHolidayCommand request, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<Holiday>(request);

                await _context.Holidays.AddAsync(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }

    }
}
