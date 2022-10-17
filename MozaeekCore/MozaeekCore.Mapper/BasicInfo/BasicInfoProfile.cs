using System.Linq;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Identity;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.Request.Events;
using MozaeekCore.Domain.Identity;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Mapper
{
    public static class BasicInfoProfile
    {
        public static LabelDto GetLabelDto(this Label domain)
        {
            return new LabelDto()
            {
                Id = domain.Id,
                Title = domain.Title,
                ParentId = domain.ParentId
            };
        }
        public static LabelDto GetLabelDtoNoParent(this Label domain)
        {
            return new LabelDto()
            {
                Id = domain.Id,
                Title = domain.Title
            };
        }


        public static RequestOrgDto GetRequestOrgDto(this RequestOrg domain)
        {
            return new RequestOrgDto()
            {
                Id = domain.Id,
                Title = domain.Title,
                ParentId = domain.ParentId,
                Icon = domain.Icon
            };
        }

        public static RequestOrgDto GetRequestOrgDtoNoParent(this RequestOrg domain)
        {
            return new RequestOrgDto()
            {
                Id = domain.Id,
                Title = domain.Title
            };
        }

        public static RequestActDto GetRequestActDto(this RequestAct domain)
        {
            return new RequestActDto()
            {
                Id = domain.Id,
                Title = domain.Title,
            };
        }
        public static PointDto GetPointDto(this Point domain)
        {
            return new PointDto()
            {
                Id = domain.Id,
                Title = domain.Title,
                ParentId = domain.ParentId
            };
        }
        public static PointDto GetPointDtoNoParent(this Point domain)
        {
            return new PointDto()
            {
                Id = domain.Id,
                Title = domain.Title
            };
        }
        public static SubjectDto GetSubjectDto(this Subject domain)
        {
            return new SubjectDto()
            {
                Title = domain.Title,
                ParentId = domain.ParentId,
                Id = domain.Id,
                Icon = domain.Icon
            };
        }
        public static UserDto GetUserDto(this User domain)
        {
            return new UserDto()
            {
                Id = domain.Id,
                EMail = domain.EMail,
                FirstName = domain.FirstName,
                LastName = domain.LastName,
                NationalCode = domain.NationalCode,
                UserName = domain.UserName,
                Roles = domain.UserRoles.Select(x => x.Role).ToList()
            };
        }
        public static SubjectDto GetSubjectDtoNoParent(this Subject domain)
        {
            return new SubjectDto()
            {
                Title = domain.Title
            };
        }

        public static HierarchyObject GetHierarchyObject(this Subject domain)
        {
            return new HierarchyObject()
            {
                Title = domain.Title,
                ParentId = domain.ParentId,
                Id = domain.Id
            };
        }
        public static HierarchyObject GetHierarchyObject(this RequestOrg domain)
        {
            return new HierarchyObject()
            {
                Title = domain.Title,
                ParentId = domain.ParentId,
                Id = domain.Id
            };
        }
        public static HierarchyObject GetHierarchyObject(this Point domain)
        {
            return new HierarchyObject()
            {
                Title = domain.Title,
                ParentId = domain.ParentId,
                Id = domain.Id
            };
        }

        public static RequestOrgGrid GetRequestOrgGrid(this RequestOrgQuery domain)
        {
            return new RequestOrgGrid()
            {
                HasChild = domain.HasChild,
                Id = domain.Id,
                Title = domain.Title

            };
        }


        public static LabelGrid GetLabelGrid(this LabelQuery domain)
        {
            return new LabelGrid()
            {
                HasChild = domain.HasChild,
                Id = domain.Id,
                Title = domain.Title

            };
        }

        public static SubjectGrid GetSubjectGrid(this SubjectQuery domain)
        {
            return new SubjectGrid()
            {
                HasChild = domain.HasChild,
                Id = domain.Id,
                Title = domain.Title

            };
        }


        public static SubjectDto GetSubjectDto(this SubjectQuery domain)
        {
            return new SubjectDto()
            {
                Id = domain.Id,
                Title = domain.Title,
                ParentId = domain.ParentId
            };
        }
        public static PointGrid GetPointGrid(this PointQuery domain)
        {
            return new PointGrid()
            {
                HasChild = domain.HasChild,
                Id = domain.Id,
                Title = domain.Title

            };
        }

        public static PointDto GetPointDto(this PointQuery domain)
        {
            return new PointDto()
            {
                Id = domain.Id,
                Title = domain.Title,
                ParentId = domain.ParentId
            };
        }

        public static CreatePointCommandResult GetPointCommandResult(this Point domain)
        {
            return new CreatePointCommandResult 
            {
                Id = domain.Id,
                ParentId = domain.ParentId,
                Title = domain.Title 
            };
        }
        public static DefiniteRequestOrgDto GetDefiniteRequestOrgDto(this DefiniteRequestOrgQuery query)
        {
            return new DefiniteRequestOrgDto
            {
                Point = query.Point.GetPointDto(),
                Address = query.Address,
                PhoneNumber = query.PhoneNumber,
                Id = query.Id
            };
        }
    }
}