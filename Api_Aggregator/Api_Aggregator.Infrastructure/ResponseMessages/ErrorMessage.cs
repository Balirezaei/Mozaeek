using System.Collections.Generic;

namespace Api_Aggregator.Infrastructure.ResponseMessages
{
    public class ErrorMessage
    {
        public string Message { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
    }
}
