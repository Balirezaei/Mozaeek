using MozaeekCore.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.ApplicationService.Contract.Technician
{
    public class UpdateTechnicianVerificationStepCommand: Command
    {
        public long Id { get; set; }
        public bool? isFirstStepVerified { get; set; }
        public bool? isSecondStepVefied { get; set; }
    }
}
