using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Domain.Entities;
using ZenApi.Domain.Enums;

namespace ZenApi.Application.Models.Reservations.Queries.GetAll
{
    public static class ReservationQueryFilters
    {
        public static IQueryable<Reservation> CreateFilters(IQueryable<Reservation> query, ReservationSearchModel request)
        {
            if (request.BusinessId != null)
            {
                query = query.Where(x => x.Service.BusinessId.Equals(request.BusinessId));
            }
            
            if (request.UserId!= null)
            {
                query = query.Where(x => x.UserId.Equals(request.UserId));
            }

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x =>
                    (x.User != null && x.User.FirstName == request.Search) ||
                    (x.User != null && x.User.LastName == request.Search) ||
                    x.CustomerName == request.Search ||
                    x.CustomerEmail == request.Search ||
                    x.CustomerPhone == request.Search ||
                    (x.User != null && x.User.FirstName.Contains(request.Search)) ||
                    (x.User != null && !string.IsNullOrEmpty(x.User.LastName) && x.User.LastName.Contains(request.Search)) ||
                    (!string.IsNullOrEmpty(x.CustomerName) && x.CustomerName.Contains(request.Search)) ||
                    (!string.IsNullOrEmpty(x.CustomerEmail) && x.CustomerEmail.Contains(request.Search)) ||
                    (!string.IsNullOrEmpty(x.CustomerPhone) && x.CustomerPhone.Contains(request.Search))
                );
            }

            if (request.CustomerName != null)
            {
                query = query.Where(x =>
                    (x.User != null && $"{x.User.FirstName} {x.User.LastName}" == request.CustomerName) ||
                    (x.User != null && $"{x.User.FirstName} {x.User.LastName}".Contains(request.CustomerName)) ||
                    x.CustomerName == request.CustomerName ||
                    (!string.IsNullOrEmpty(x.CustomerName) && x.CustomerName.Contains(request.CustomerName))
                );
            }

            if (request.CustomerPhone != null)
            {
                query = query.Where(x =>
                    (x.User != null && x.User.Phone == request.CustomerPhone) ||
                    (x.User != null && !string.IsNullOrEmpty(x.User.Phone) && x.User.Phone.Contains(request.CustomerPhone)) ||
                    x.CustomerPhone == request.CustomerPhone ||
                    (!string.IsNullOrEmpty(x.CustomerPhone) && x.CustomerPhone.Contains(request.CustomerPhone))
                );
            }

            if (request.CustomerEmail != null)
            {
                query = query.Where(x =>
                    (x.User != null && x.User.Email == request.CustomerEmail) ||
                    (x.User != null && x.User.Email.Contains(request.CustomerEmail)) ||
                    x.CustomerEmail == request.CustomerEmail ||
                    (!string.IsNullOrEmpty(x.CustomerEmail) && x.CustomerEmail.Contains(request.CustomerEmail))
                );
            }

            if (request.StartDate.HasValue)
            {
                query = query.Where(x => x.Date >= request.StartDate.Value);
            }

            if (request.EndDate.HasValue)
            {
                query = query.Where(x => x.Date <= request.EndDate.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.StartTime))
            {
                query = query.Where(x => string.Compare(x.StartTime, request.StartTime) >= 0);
            }

            if (!string.IsNullOrWhiteSpace(request.EndTime))
            {
                query = query.Where(x => string.Compare(x.EndTime, request.EndTime) <= 0);
            }

            if (!string.IsNullOrWhiteSpace(request.StatusTypes))
            {
                var statuses = request.StatusTypes
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .ToList();

                var parsedStatuses = statuses
                    .Select(s => Enum.TryParse<ReservationStatus>(s, true, out var status) ? status : (ReservationStatus?)null)
                    .Where(s => s.HasValue)
                    .Select(s => s!.Value)
                    .ToList();

                if (parsedStatuses.Count > 0)
                {
                    query = query.Where(x => parsedStatuses.Contains(x.Status));
                }
            }

            if (!string.IsNullOrWhiteSpace(request.ServiceIds))
            {
                var ids = request.ServiceIds
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(id =>
                    {
                        return int.TryParse(id, out var value) ? value : (int?)null;
                    })
                    .Where(x => x.HasValue)
                    .Select(x => x!.Value)
                    .ToList();

                if (ids.Count > 0)
                {
                    query = query.Where(x => ids.Contains(x.ServiceId));
                }
            }

            return query;
        }
    }
}
