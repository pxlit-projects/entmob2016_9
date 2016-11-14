using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuphoricElephant.Data
{
    public class ProfileItem
    {
        public Action action { get; set; }
        public List<Command> command { get; set; }
        public int commandIndex { get; set; }
    }
}
