using System.Linq;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.Request.Events;

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
                ParentId = domain.ParentId
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
                Id = domain.Id
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

        
    }
}