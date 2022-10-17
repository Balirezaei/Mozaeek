using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.Domain
{
    public interface ITechnicianRepository
    {
        void Add(Technician technician);
        void Update(Technician technician);
        Task<Technician> FindWithSubject(long id);
        Task<Technician> FindWithPoint(long id);
        Task<Technician> FindWithContactInfo(long id);
        Task<Technician> FindWithEducationInfo(long id);
        Task<Technician> FindWithPersonalInfo(long id);
        Task<Technician> FindWithAttachment(long id);
        Task<Technician> Find(long id);
        Task<Technician> FindWithRequest(long id);
        Task ReplacePersonalPhoto(long id, TechnicianAttachment attachment);
    }
}
