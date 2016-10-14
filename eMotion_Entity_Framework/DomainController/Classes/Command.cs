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
        public int CommandId { get; set; }
        public int Command1 { get; set; }
        public int Command2 { get; set; }
        public int Command3 { get; set; }
        public int Command4 { get; set; }

        public virtual ICollection<Profile> profiles { get; set; }
    }
}
