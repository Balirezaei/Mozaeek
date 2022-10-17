using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.BasicInfo.Point;
using MozaeekCore.Common;
using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Domain;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Command.BasicInfo
{
    public class CreateSubjectsFromExcelCommandHandler : IBaseAsyncCommandHandler<CreateSubjectFromExcelCommand, List<SubjectDto>>
    {
        private readonly IGenericRepository<Domain.Subject> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubjectQueryService _SubjectQueryService;

        public CreateSubjectsFromExcelCommandHandler(IGenericRepository<Domain.Subject> repository, IUnitOfWork unitOfWork, ISubjectQueryService SubjectQueryService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _SubjectQueryService = SubjectQueryService;
        }
        public async Task<List<SubjectDto>> HandleAsync(CreateSubjectFromExcelCommand cmd)
        {
            var result = cmd.ExcelPath.ImportExcel();
            var SubjectList = result.Where(r => (string.IsNullOrEmpty(r.ParentCode) || r.ParentCode == "0")).Select(r =>
            {
                var children = GetAllSubjectWithChildren(r, result.ToList());

                return new Domain.Subject(0, r.Title, children);
            }).ToList();

            await _repository.AddRange(SubjectList);
            await _unitOfWork.CommitAsync();

            await SaveToQuery(SubjectList);

            return SubjectList.Select(p => p.GetSubjectDto()).ToList();
        }

        private List<Subject> GetAllSubjectWithChildren(ParentChildExcelDto parent, List<ParentChildExcelDto> list)
        {
            var result = new List<Subject>();
            list.Where(x => x.ParentCode == parent.Code).ToList().ForEach(z =>
            {
                result.Add(new Subject(0, z.Title, GetAllSubjectWithChildren(z, list)));
            });
            return result;
        }

        private async Task SaveToQuery(List<Subject> list)
        {
            var result = new List<SubjectQuery>();
            foreach (var p in list)
            {
                await _SubjectQueryService.Create(new QueryModel.SubjectQuery(p.Id, p.Title, null, p.ParentId, DateTime.Now, Guid.NewGuid()));
                if (p.SubSubjects != null && p.SubSubjects.Any())
                {
                    await SaveToQuery(p.SubSubjects.ToList());
                }
            }
        }
    }
}