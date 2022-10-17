using System.Collections.Generic;
using System.Linq;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Domain;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Mapper
{
    public static class RequestTargetProfile
    {
        public static RequestTargetDto GetRequestTargetDto(this RequestTarget domain)
        {
            return new RequestTargetDto()
            {
                Id = domain.Id,
                Title = domain.Title,
                RequestOrgs = domain.RequestTargetRequestOrgs.Select(m => m.RequestOrgId).ToList(),
                Labels = domain.RequestTargetLabels.Select(m => m.LabelId).ToList(),
                Subjects = domain.RequestTargetSubjects.Select(m => m.SubjectId).ToList()
            };
        }
        public static RequestTargetGrid GetRequestTargetGrid(this RequestTargetQuery domain)
        {
            return new RequestTargetGrid()
            {
                Id = domain.Id,
                Title = domain.Title,
                RequestOrgs =
                    domain.RequestOrgList
                        .Select(z => z.Title).ToList(),
                Subjects = domain.SubjectList.Select(z => z.Title).ToList(),
                Labels = domain.LabelList.Select(z => z.Title).ToList()
            };
        }
    }
}