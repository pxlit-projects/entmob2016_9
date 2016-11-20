using EuphoricElephant.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace EuphoricElephant.Messaging
{
    public class NavigationMessage
    {
        public ViewType Type { get; private set; }
        public Frame Frame { get; private set; }

        public NavigationMessage(ViewType type, Frame frame)
        {
            Type = type;
            Frame = frame;
        }
    }
}
