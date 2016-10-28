using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuphoricElephant.Data
{
    [Table("action")]
    public class Action
    {
        [Key]
        public int ActId { get; set; }
        public List<int> ActionList { get; set; }
        public int Act1 { get; set; }
        public int Act2 { get; set; }
        public int Act3 { get; set; }
        public int Act4 { get; set; }
        //Foreign key van profiles
        //[Required]
        //public int ProfileId { get; set; }
        public virtual Profile profile { get; set; }
    }
}
