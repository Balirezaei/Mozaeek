using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Persistense.MongoDb
{
    public interface ITechnicianQueryService
    {
        Task<List<TechnicianQuery>> Get();
        Task<TechnicianQuery> Get(long id);
        Task CreateTechnicianByPersonalInfo(TechnicianPersonalInfoParameter parameter);
        Task UpdateTechnicianByPersonalInfo(TechnicianPersonalInfoParameter parameter);
        Task Remove(long id);
        Task<List<TechnicianQuery>> GetByPredicate(Expression<Func<TechnicianQuery, bool>> predicate, PagingContract pagingContract);
        Task<List<TechnicianQuery>> GetByPredicate(Expression<Func<TechnicianQuery, bool>> predicate);
        Task<long> GetCount(Expression<Func<TechnicianQuery, bool>> predicate);
        Task AddTechnicianContactInfo(TechnicianContactInfoParameter parameter);
        Task AddTechnicianEducaionInfo(TechnicianEducationInfoParameter parameter);
        Task AddTechnicianRequests(TechnicianRequestsParameter parameter);
        Task AddTechnicianSubjects(TechnicianSubjectsParameter parameter);
        Task AddTechnicianPoints(TechnicianPointsParameter parameter);
    }

    public class TechnicianQueryService : ITechnicianQueryService
    {
        private readonly IMongoRepository _repository;
        public TechnicianQueryService(IMongoRepository repository)
        {
            _repository = repository;
        }

        public Task<List<TechnicianQuery>> Get()
        {
            return _repository.TechnicianQueryCollection.Find(tech => true).ToListAsync();
        }

        public Task<TechnicianQuery> Get(long id)
        {
            return _repository.TechnicianQueryCollection.Find(tech => tech.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateTechnicianByPersonalInfo(TechnicianPersonalInfoParameter parameter)
        {
            var preSave = await Get(parameter.TechnicianId);
            if (preSave!=null)
            {
                return;
            }
            var technicianQuery = new TechnicianQuery()
            {
                TechnicianType = parameter.TechnicianType,
                FirstName = parameter.FirstName,
                LastName = parameter.LastName,
                NationalCode = parameter.NationalCode,
                IdentityNumber = parameter.IdentityNumber,
                Id = parameter.TechnicianId,
                CreateDateTime = parameter.CreateDate,
                LastEventId = parameter.EventId,
                LastEventPublishDate = parameter.PublishEventDate
            };

            await _repository.TechnicianQueryCollection.InsertOneAsync(technicianQuery);
        }
        public async Task UpdateTechnicianByPersonalInfo(TechnicianPersonalInfoParameter parameter)
        {
            var eventualConsistensy = await Get(parameter.TechnicianId);
            if (eventualConsistensy == null)
            {
                return;
            }
            if (eventualConsistensy.LastEventPublishDate > parameter.PublishEventDate)
            {
                return;
            }
            var query = eventualConsistensy;
            query.TechnicianType = parameter.TechnicianType;
            query.FirstName = parameter.FirstName;
            query.LastName = parameter.LastName;
            query.NationalCode = parameter.NationalCode;
            query.IdentityNumber = parameter.IdentityNumber;
            query.LastEventPublishDate = parameter.PublishEventDate;
            query.LastEventId = parameter.EventId;
            await _repository.TechnicianQueryCollection.ReplaceOneAsync(m => m.Id == query.Id, query);
        }

        public Task Remove(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TechnicianQuery>> GetByPredicate(Expression<Func<TechnicianQuery, bool>> predicate, PagingContract pagingContract)
        {
            throw new NotImplementedException();
        }

        public Task<List<TechnicianQuery>> GetByPredicate(Expression<Func<TechnicianQuery, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetCount(Expression<Func<TechnicianQuery, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task AddTechnicianContactInfo(TechnicianContactInfoParameter parameter)
        {
            var eventualConsistensy = await Get(parameter.TechnicianId);
            if (eventualConsistensy == null)
            {
                return;
            }
            if (eventualConsistensy.LastEventPublishDate > parameter.PublishEventDate)
            {
                return;
            }
            var query = eventualConsistensy;
            query.ContactInfoPostalCode = parameter.PostalCode;
            query.ContactInfoAddress = parameter.Address;
            query.ContactInfoMobileNumber = parameter.MobileNumber;
            query.ContactInfoOfficeNumber = parameter.OfficeNumber;
            query.ContactInfoPhoneNumber = parameter.PhoneNumber;
            query.LastEventId = parameter.EventId;
            query.LastEventPublishDate = parameter.PublishEventDate;
          await  _repository.TechnicianQueryCollection.ReplaceOneAsync(m => m.Id == query.Id, query);
        }

        public async Task AddTechnicianEducaionInfo(TechnicianEducationInfoParameter parameter)
        {
            var eventualConsistensy = await Get(parameter.TechnicianId);
            if (eventualConsistensy == null)
            {
                return;
            }
            if (eventualConsistensy.LastEventPublishDate > parameter.PublishEventDate)
            {
                return;
            }
            var query = eventualConsistensy;
            query.EducationalFeildId = parameter.EducationFieldId;
            query.EducationalFeildTitle = parameter.EducationFieldTitle;
            query.EducationalGradeId = parameter.EducationGradeId;
            query.EducationalGradeTitle = parameter.EducationGradeTitle;
            query.LastEventId = parameter.EventId;
            query.LastEventPublishDate = parameter.PublishEventDate;
          await  _repository.TechnicianQueryCollection.ReplaceOneAsync(m => m.Id == query.Id, query);
        }

        public async Task AddTechnicianRequests(TechnicianRequestsParameter parameter)
        {
            var eventualConsistensy = await Get(parameter.TechnicianId);
            if (eventualConsistensy == null)
            {
                return;
            }
            if (eventualConsistensy.LastEventPublishDate > parameter.PublishEventDate)
            {
                return;
            }
            var query = eventualConsistensy;
            var filterBuilder = Builders<RequestQuery>.Filter;
            var filter = filterBuilder.In(doc => doc.Id, parameter.Requests);
            var requests = await _repository.RequestQueryCollection.Find(filter).ToListAsync();
            query.Requests = requests;
            query.LastEventId = parameter.EventId;
            query.LastEventPublishDate = parameter.PublishEventDate;
          await  _repository.TechnicianQueryCollection.ReplaceOneAsync(m => m.Id == query.Id, query);
        }

        public async Task AddTechnicianSubjects(TechnicianSubjectsParameter parameter)
        {
            try
            {
                var eventualConsistensy = await Get(parameter.TechnicianId);
                if (eventualConsistensy == null)
                {
                    return;
                }
                if (eventualConsistensy.LastEventPublishDate > parameter.PublishEventDate)
                {
                    return;
                }
                var query = eventualConsistensy;
                var filterBuilder = Builders<SubjectQuery>.Filter;
                var filter = filterBuilder.In(doc => doc.Id, parameter.Subjects);
                var subjects = await _repository.SubjectQueryCollection.Find(filter).ToListAsync();
                query.Subjects = subjects;
                query.LastEventId = parameter.EventId;
                query.LastEventPublishDate = parameter.PublishEventDate;
               await _repository.TechnicianQueryCollection.ReplaceOneAsync(m => m.Id == query.Id, query);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task AddTechnicianPoints(TechnicianPointsParameter parameter)
        {
            var eventualConsistensy = await Get(parameter.TechnicianId);
            if (eventualConsistensy == null)
            {
                return;
            }
            if (eventualConsistensy.LastEventPublishDate > parameter.PublishEventDate)
            {
                return;
            }
            var query = eventualConsistensy;
            var filterBuilder = Builders<PointQuery>.Filter;
            var filter = filterBuilder.In(doc => doc.Id, parameter.Points);
            var points = await _repository.PointQueryCollection.Find(filter).ToListAsync();
            query.Points = points;
            query.LastEventId = parameter.EventId;
            query.LastEventPublishDate = parameter.PublishEventDate;
          await  _repository.TechnicianQueryCollection.ReplaceOneAsync(m => m.Id == query.Id, query);
        }
    }
}