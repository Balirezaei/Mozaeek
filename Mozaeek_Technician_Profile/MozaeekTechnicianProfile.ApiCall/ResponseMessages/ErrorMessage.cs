using System.Collections.Generic;

namespace MozaeekTechnicianProfile.ApiCall.ResponseMessages
{
    public class ErrorMessage
    {
        public string Message { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
    }
}
