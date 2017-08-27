using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMotion.DomainClasses.Classes
{
    [Table("action")]
    public class Action
    {
        [Key]
        public int actId { get; set; }
        public string action { get; set; }
        public virtual Profile profile { get; set; }
    }
}
