using AutoMapper;
using MediatR;
using ZenApi.Application.Common.Interfaces;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Application.Common.Mappings;

namespace ZenApi.Application.Models.Businesses.Commands.Update
{
    public record UpdateBusinessCommand : IRequest
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public string? Description { get; init; }
        public required string Address { get; init; }
        public required int ProvinceId { get; init; }
        public string? Photo { get; init; }
        public string? Keyword1 { get; init; }
        public string? Keyword2 { get; init; }
        public string? Keyword3 { get; init; }
        public required string Phone { get; init; }
        public int SimultaneousBookings { get; init; }
        public bool? IsActive { get; init; }
        public string? InstagramUser { get; init; }
        public string? XUser { get; init; }
        public string? TikTokUser { get; init; }
        public string? FacebookUser { get; init; }
        public string? GoogleMaps { get; init; }
    }

    public class UpdateBusinessCommandHandler : IRequestHandler<UpdateBusinessCommand>
    {
        private readonly IBusinessCommandRepository _repository;
        private readonly IMapper _mapper;

        public UpdateBusinessCommandHandler(IBusinessCommandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateBusinessCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null)
                throw new Exception("Business not found");

            // Update allowed fields
            //entity.Name = request.Name;
            //entity.Description = request.Description;
            //entity.Address = request.Address;
            //entity.ProvinceId = request.ProvinceId;
            //entity.Photo = request.Photo;
            //entity.Keyword1 = request.Keyword1;
            //entity.Keyword2 = request.Keyword2;
            //entity.Keyword3 = request.Keyword3;
            //entity.Phone = request.Phone;
            //entity.SimultaneousBookings = request.SimultaneousBookings;
            //entity.IsActive = request.IsActive;
            //entity.InstagramUser = request.InstagramUser;
            //entity.XUser = request.XUser;
            //entity.TikTokUser = request.TikTokUser;
            //entity.FacebookUser = request.FacebookUser;
            //entity.GoogleMaps = request.GoogleMaps;

            _mapper.Map(request, entity);

            await _repository.UpdateAsync(entity, cancellationToken);
        }
    }
}