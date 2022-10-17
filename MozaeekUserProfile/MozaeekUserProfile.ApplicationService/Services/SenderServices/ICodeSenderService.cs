using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekUserProfile.ApplicationService.Services.SenderServices
{
    public interface ICodeSenderService
    {
        Task Send(string code, string destination);
    }
}
