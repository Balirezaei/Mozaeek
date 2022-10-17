using System;

namespace MozaeekTechnicianProfile
{
    public class InvalidInputException:ApplicationException
    {
        public InvalidInputException()
        {

        }
        public InvalidInputException(string message)
            :base(message)
        {

        }
    }
}
