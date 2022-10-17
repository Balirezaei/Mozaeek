using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.Request.Events;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Mapper
{
    public static class RequestProfile
    {
        public static RequestDto GetRequestDto(this Domain.Request request)
        {
            return new RequestDto()
            {
                Title = request.RequestTarget.Title + " " + request.RequestAct.Title
            };
        }

        public static RequestDependency GetRequestDependency(this RequestNessesity domain)
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

        public static RequestDependency GetRequestDependency(this RequestDocument domain)
        {
            return new RequestDependency()
            {
                Description = domain.Description,
                Priority = domain.Priority,
            };
        }
        public static RequestDependency GetRequestDependency(this RequestQualification domain)
        {
            return new RequestDependency()
            {
                Description = domain.Description,
                Priority = domain.Priority,
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

    }
}