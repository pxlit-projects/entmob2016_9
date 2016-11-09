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
        public string action { get; set; }
        public virtual Profile profile { get; set; }
    }
}
