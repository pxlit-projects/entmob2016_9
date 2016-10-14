using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMotion.DomainClasses.Classes
{
    [Table("profile")]
    public class Profile
    {
        
        [Key]
        public int ProfileId { get; set; }
        public int UserId { get; set; }
        public int CommandId { get; set; }
        public string ProfileName { get; set; }

        public int ActId { get; set; }
        
        public virtual Action action { get; set; }

        public virtual User user { get; set; }

        public virtual Command command { get; set; }
        
        
    }
}
