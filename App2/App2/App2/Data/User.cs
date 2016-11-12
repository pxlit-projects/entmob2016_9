using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Data
{
    class User
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string userName { get; set; }
        public int defaultProfileId { get; set; }

        public User(int id, string firstName, string lastName, string password, string userName, int defaultProfileId)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.password = password;
            this.userName = userName;
            this.defaultProfileId = defaultProfileId;
        }
    }
}
