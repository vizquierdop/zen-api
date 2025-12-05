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

namespace ZenApi.Application.Models.Holidays.Commands.Delete
{
    public record DeleteHolidayCommand(int Id) : IRequest;

    public class DeleteHolidayCommandHandler : IRequestHandler<DeleteHolidayCommand>
    {
        private readonly IHolidayCommandRepository _repository;

        public DeleteHolidayCommandHandler(IHolidayCommandRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteHolidayCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id, cancellationToken);
            //var entity = await _context.Holidays
            //    .FindAsync(request.Id, cancellationToken);

            //if (entity is null)
            //    throw new Exception("Holiday not found");

            //_context.Holidays.Remove(entity);

            //await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
