using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using MozaeekCore.Domain;
using MozaeekCore.Enum;

namespace MozaeekCore.Persistense.EF.Repository
{
    public class TechnicianRepository : ITechnicianRepository
    {
        private readonly CoreDomainContext _context;

        public TechnicianRepository(CoreDomainContext context)
        {
            _context = context;
        }
        public void Add(Technician technician)
        {
            _context.Technicians.Add(technician);
        }

        public Task<Technician> FindWithSubject(long id)
        {
            return _context.Technicians
                .Where(m => m.Id == id)
                .Include(m => m.TechnicianSubjects)
               .FirstOrDefaultAsync();
        }

        public Task<Technician> FindWithPoint(long id)
        {
            return _context.Technicians
                .Where(m => m.Id == id)
                .Include(m => m.TechnicianPoints)
                .FirstOrDefaultAsync();
        }

        public Task<Technician> FindWithContactInfo(long id)
        {
            return _context.Technicians
                .Where(m => m.Id == id)
                .Include(m => m.TechnicianContactInfo)
                .FirstOrDefaultAsync();
        }

        public Task<Technician> FindWithEducationInfo(long id)
        {
            return _context.Technicians
                .Where(m => m.Id == id)
                .Include(m => m.TechnicianEducationalInfo)
                .FirstOrDefaultAsync();
        }

        public Task<Technician> FindWithPersonalInfo(long id)
        {
            return _context.Technicians
                .Where(m => m.Id == id)
                .Include(m => m.TechnicianPersonalInfo)
                .FirstOrDefaultAsync();
        }

        public Task<Technician> FindWithAttachment(long id)
        {
            return _context.Technicians
                .Where(m => m.Id == id)
                .Include(m => m.TechnicianAttachments)
                .FirstOrDefaultAsync();
        }

        public Task<Technician> FindWithRequest(long id)
        {
            return _context.Technicians
                .Where(m => m.Id == id)
                 .Include(m => m.TechnicianRequests)
                .FirstOrDefaultAsync();
        }

        public async Task ReplacePersonalPhoto(long id, TechnicianAttachment attachment)
        {
            var technician = await FindWithAttachment(id);
            var personalPhoto =
                technician.TechnicianAttachments.FirstOrDefault(m => m.AttachmentType == AttachmentType.PersonalPhoto);
            if (personalPhoto != null)
            {
                personalPhoto.AttachmentType = attachment.AttachmentType;
                personalPhoto.FileExtention = attachment.FileExtention;
                personalPhoto.FileName = attachment.FileName;
                personalPhoto.Source = attachment.Source;
            }
            else
            {
                if (technician.TechnicianAttachments == null)
                {
                    technician.TechnicianAttachments = new List<TechnicianAttachment>();
                }
                technician.TechnicianAttachments.Add(attachment);
            }
        }


    }
}
