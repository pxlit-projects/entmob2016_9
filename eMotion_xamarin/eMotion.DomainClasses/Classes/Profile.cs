using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMotion.DomainClasses.Classes
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public int UserId { get; set; }
        public int Commands { get; set; }
        public int actions { get; set; }
        public string ProfileName { get; set; }
    }
}
