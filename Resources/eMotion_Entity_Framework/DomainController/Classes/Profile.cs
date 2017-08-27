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
        public int profileId { get; set; }
        public int userId { get; set; }
        public string profileName { get; set; }
        public string pairings { get; set; }
    }
}
