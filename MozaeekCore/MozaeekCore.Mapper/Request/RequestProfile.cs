using System.Collections.Generic;
using System.Linq;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.Request.Events;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Mapper
{
    public static class RequestProfile
    {
        public static RequestDto GetRequestDto(this RequestQuery request)
        {
            return new RequestDto()
            {
                Id = request.Id,
                RequestTargetId = request.RequestTarget.Id,
                RequestActId = request.RequestAct.Id,
                Points = (request.Points ?? new List<PointQuery>()).Select(m => m.Id).ToList(),
                RequestOrgs = (request.RequestOrgs ?? new List<RequestOrgQuery>()).Select(m => m.Id).ToList(),
                DefiniteRequestOrgs = (request.DefiniteRequestOrgs ?? new List<DefiniteRequestOrgQuery>()).Select(m => m.Id).ToList(),

                RequestQualifications = (request.Qualifications ?? new List<RequestQueryDependency>())
                    .Select(m => new RequestQualificationDto(m.Id, m.Description, m.Priority)).ToList(),


                RequestNecessities = (request.Necessities ?? new List<RequestQueryDependency>())
                    .Select(m => new RequestNecessityDto(m.Id, m.Description, m.Priority)).ToList(),

                RequestActions = (request.Actions ?? new List<RequestQueryDependency>())
                    .Select(m => new RequestActionDto(m.Id, m.Description, m.Priority)).ToList(),

                ConnectionDtos = (request.ConnectedRequestTargets ?? new List<ConnectedRequestTarget>())
                    .Select(m => new RequestTargetConnectionDto(m.RequestTargetId, m.Title, m.Description)).ToList(),

                RequestStartDate = request.RequestStartDate,
                RequestExpiredDate = request.RequestExpiredDate,
                FullOnline = request.FullOnline,
                Summary = request.Description,
                Regulation = request.Regulation
            };
        }

        public static RequestDependency GetRequestDependency(this RequestNecessity domain)
        {
            return new RequestDependency()
            {
                Description = domain.Description,
                Priority = domain.Priority,
            };
        }

        public static RequestDependency GetRequestDependency(this RequestAction domain)
        {
            return new RequestDependency()
            {
                Description = domain.Description,
                Priority = domain.Priority,
            };
        }

        //public static RequestDependency GetRequestDependency(this RequestDocument domain)
        //{
        //    return new RequestDependency()
        //    {
        //        Description = domain.Description,
        //        Priority = domain.Priority,
        //    };
        //}
        public static RequestDependency GetRequestDependency(this RequestQualification domain)
        {
            return new RequestDependency()
            {
                Description = domain.Description,
                Priority = domain.Priority,
            };
        }
        public static RequestTargetConnectionEventDto GetRequestDependency(this RequestTargetConnection domain)
        {
            return new RequestTargetConnectionEventDto()
            {
                Description = domain.Description,
                RequestTargetId = domain.RequestTargetId,
            };
        }
        public static RequestGrid GetRequestGrid(this RequestQuery domain)
        {
            return new RequestGrid()
            {
                Title = domain.Title,
                Id = domain.Id
            };
        }

        public static SingleUserRequestDto GetSingleUserRequest(this RequestListGroupByRequestTarget dto)
        {
            if (dto == null)
            {
                return null;
            }
            return new SingleUserRequestDto()
            {
                Title = dto.Title,
                RequestQualifications = dto.RequestQualifications,
                Description = dto.Description,
                RequestNecessities = dto.RequestNecessities,
                RequestActions = dto.RequestActions,
                Id = dto.Id,
                RequestActs = dto.RequestActs,
                RequestActId = dto.RequestActId,
                Points = dto.Points,
                RequestPointId = dto.Points.Any() ? dto.Points.FirstOrDefault().Id : 0
            };
        }
    }
}