using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanaticFirefly.Data
{
    public class Profile
    {
        public int profileId { get; set; }
        public int userId { get; set; }
        public string profileName { get; set; }
        public string pairings { get; set; }

        public Profile(int profileId, int userId, string profileName, string pairings)
        {
            this.profileId = profileId;
            this.userId = userId;
            this.profileName = profileName;
            this.pairings = pairings;
        }
    }
}
