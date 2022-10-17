using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.TechnicianProfileConsistensyService.Context;
using MozaeekCore.TechnicianProfileConsistensyService.Model;

namespace MozaeekCore.TechnicianProfileConsistensyService.Service
{
    public interface ITechnicianSynchronizeService
    {
        Task SaveIfNotExist(Tecnician tecnician);
    }

    public class TechnicianSynchronizeService : ITechnicianSynchronizeService
    {
        private readonly TechnicianProfileContext _context;

        public TechnicianSynchronizeService(TechnicianProfileContext context)
        {
            _context = context;
        }

        public async Task SaveIfNotExist(Tecnician tecnician)
        {
            var preSaved = await _context.Tecnician.Where(m => m.TechnicianId == tecnician.TechnicianId).FirstOrDefaultAsync();
            if (preSaved != null)
            {
                preSaved.FirstName = tecnician.FirstName;
                preSaved.LastName = tecnician.LastName;
                preSaved.MobileNumber = tecnician.MobileNumber;
                preSaved.NationalCode = tecnician.NationalCode;
            }
            else
            {
                await _context.Tecnician.AddAsync(tecnician);
            }
            await _context.SaveChangesAsync();
        }
    }
}