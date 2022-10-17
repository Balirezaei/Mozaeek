using System;

namespace MozaeekUserProfile.Exception.Exceptions
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
