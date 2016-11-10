using EuphoricElephant.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuphoricElephant.Messaging
{
    public class NavigationMessage
    {
        public ViewType Type { get; private set; }

        public NavigationMessage(ViewType type)
        {
            Type = type;
        }
    }
}
