using MozaeekUserProfile.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekUserProfile.ApplicationService.Services.SenderServices
{
    public class SmsCodeSenderService : ICodeSenderService
    {
        private readonly IOtpSenderService otpSenderService;

        public SmsCodeSenderService(IOtpSenderService otpSenderService)
        {
            this.otpSenderService = otpSenderService;
        }
        public Task Send(string code, string destination)
        {
            return otpSenderService.SendOtp(destination, code);
        }
    }
}
