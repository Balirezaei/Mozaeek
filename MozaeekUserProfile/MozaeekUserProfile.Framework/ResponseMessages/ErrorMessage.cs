using System.Collections.Generic;

namespace MozaeekUserProfile.Core.Core.ResponseMessages
{
    public class ErrorMessage
    {
        public string Message { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
    }
}
