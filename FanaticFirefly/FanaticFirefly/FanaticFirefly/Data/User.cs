using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanaticFirefly.Data
{
    public class User
    {
        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string userName { get; set; }
        public int defaultProfileId { get; set; }
        public string email { get; set; }
        public string joinedOn { get; set; }
        public string country { get; set; }
        public string phone { get; set; }       
    }
}
