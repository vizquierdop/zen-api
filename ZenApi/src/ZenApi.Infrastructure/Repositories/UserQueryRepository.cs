using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Application.Dtos.Users;
using ZenApi.Infrastructure.Persistence;

namespace ZenApi.Infrastructure.Repositories
{
    public class UserQueryRepository : IUserQueryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfigurationProvider _mapper;

        public UserQueryRepository(ApplicationDbContext context, IConfigurationProvider mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.UsersDomain
                .AsNoTracking()
                // TODO Maybe it is not necessary
                .Include(x => x.Business)
                .Include(x => x.Reservations)
                .Where(x => x.Id == id)
                .ProjectTo<UserDto>(_mapper)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
