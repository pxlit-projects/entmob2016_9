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
        public int Actions { get; set; }
        public string ProfileName { get; set; }


        public virtual User user { get; set; }
        public virtual Command command { get; set; }
        public virtual Action action { get; set; }
        
    }
}
