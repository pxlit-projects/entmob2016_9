using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMotion.DomainClasses.Classes
{
    [Table("command")]
    public class Command
    {
        [Key]
        public int commandId { get; set; }
        public String command { get; set; }

        public virtual ICollection<Profile> profiles { get; set; }
    }
}
