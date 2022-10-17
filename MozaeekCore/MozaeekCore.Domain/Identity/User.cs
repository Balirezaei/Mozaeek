using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.Domain.Identity
{
    public class User: BasicInfo
    {
        public User()
        {

        }
        public User(long id)
        {
            Id = id;
        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }      
        public string EMail { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public string LastExpiredToken { get; set; }
    }

}
