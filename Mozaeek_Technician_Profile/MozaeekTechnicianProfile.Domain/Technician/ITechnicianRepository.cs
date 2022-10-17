using System.Threading.Tasks;

namespace MozaeekTechnicianProfile.Domain
{
    public interface ITechnicianRepository
    {
        Task<Technician> FindTechnicianByMobileNo(string mobileNo);
        Task<Technician> GetTechnicianById(long technicianId);
        Task Create(Technician technician);
        void Update(Technician technician);
    }
}