using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace EuphoricElephant.Interfaces
{
    public interface IMediaView
    {
        void DrawOnCanvas(byte[] data);
    }
}
