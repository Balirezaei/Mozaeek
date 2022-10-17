using Microsoft.EntityFrameworkCore;
using MozaeekTechnicianProfile.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace MozaeekTechnicianProfile.Persistense.EF.Repository
{
    public class TechnicianRepository : ITechnicianRepository
    {
        private readonly MozaeekTechnicianProfileContext _dbContext;
        public TechnicianRepository(MozaeekTechnicianProfileContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task Create(Technician technician)
        {
            await _dbContext.Technicians.AddAsync(technician);
        }

        public Task<Technician> FindTechnicianByMobileNo(string mobileNo)
        {
            return _dbContext.Technicians.FirstOrDefaultAsync(m => m.PhoneNumber == mobileNo);
        }

        public Task<Technician> GetTechnicianById(long technicianId)
        {
            return _dbContext.Technicians.FirstOrDefaultAsync(m => m.Id == technicianId);
        }

        public void Update(Technician technician)
        {
            _dbContext.Technicians.Update(technician);
        }
    }
}
