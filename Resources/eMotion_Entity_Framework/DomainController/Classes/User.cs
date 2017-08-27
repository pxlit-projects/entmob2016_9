using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMotion.DomainClasses.Classes
{
    [Table("user")]
    public class User
    {
        [Key]
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

        public virtual ICollection<Profile> profiles { get; set; }
    }
}
