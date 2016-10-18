using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace EuphoricElephant.Services
{
    public static class AudioVisualizationService
    {
        public static PointCollection GetWavePoints(byte[] data, Canvas canvas)
        {
            PointCollection points = null;

            PointCollection upperLimits = new PointCollection();
            PointCollection lowerLimits = new PointCollection();

            int BORDER_WIDTH = 5;
            double width = canvas.Width - (2 * BORDER_WIDTH);
            double height = canvas.Height - (2 * BORDER_WIDTH);

            int size = data.Length;

            for (int iPixel = 0; iPixel < width; iPixel++)
            {
                // determine start and end points within WAV
                int start = (int)((float)iPixel * ((float)size / (float)width));
                int end = (int)((float)(iPixel + 1) * ((float)size / (float)width));
                float min = float.MaxValue;
                float max = float.MinValue;
                for (int i = start; i < end; i++)
                {
                    float val = data[i];
                    min = val < min ? val : min;
                    max = val > max ? val : max;
                }
                int yMax = (int)(BORDER_WIDTH + height - (int)((max + 1) * .5 * height));
                int yMin = (int)(BORDER_WIDTH + height - (int)((min + 1) * .5 * height));

                Point up = new Point(iPixel + BORDER_WIDTH, yMax);
                Point low = new Point(iPixel + BORDER_WIDTH, yMin);

                upperLimits.Add(up);
                lowerLimits.Add(low);
            }

            points = upperLimits;

            for(int p = lowerLimits.Count -1; p >= 0; p--)
            {
                points.Add(lowerLimits[p]);
            }

            return points;
        }
    }
}
