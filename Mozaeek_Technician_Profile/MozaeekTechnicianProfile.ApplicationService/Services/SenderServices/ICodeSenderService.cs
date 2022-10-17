using System.Threading.Tasks;

namespace MozaeekTechnicianProfile.ApplicationService.Services.SenderServices
{
    public interface ICodeSenderService
    {
        Task Send(string code, string destination);
    }
    public interface IOtpCodeGenerator
    {
        string Generate(int length);
    }
}
