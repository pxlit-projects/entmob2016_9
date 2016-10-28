using EuphoricElephant.Custom;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        public static PointCollection GetWavePoints(byte[] input, Canvas canvas)
        {
            PointCollection points = new PointCollection(); 
            PointCollection upperLimits = new PointCollection();


            try
            {
                PointCollection lowerLimits = new PointCollection();

                int samplesPerPixel = 128;
                long startPosition = 0;

                int BORDER_WIDTH = 5;
                int width = (int)canvas.ActualWidth - (2 * BORDER_WIDTH);
                int height = ((int)canvas.ActualHeight - (2 * BORDER_WIDTH));

                WaveFormat waveFormat = new WaveFormat();
                CustomWaveStream waveStream = new CustomWaveStream(new MemoryStream(input), waveFormat);
                WaveChannel32 channelStream = new WaveChannel32(waveStream);

                int bytesPerSample = (waveStream.WaveFormat.BitsPerSample / 8) * channelStream.WaveFormat.Channels;

                float[] data = FloatArrayFromByteArray(input);

                int size = data.Length;
                waveStream.Position = 0;
                int bytesRead1;
                byte[] waveData1 = new byte[samplesPerPixel * bytesPerSample];
                waveStream.Position = startPosition + (width * bytesPerSample * samplesPerPixel);

                for (float x = 0; x < width; x++)
                {
                    short low = 0;
                    short high = 0;
                    bytesRead1 = waveStream.Read(waveData1, 0, samplesPerPixel * bytesPerSample);
                    if (bytesRead1 == 0)
                        break;
                    for (int n = 0; n < bytesRead1; n += 2)
                    {
                        short sample = BitConverter.ToInt16(waveData1, n);
                        if (sample < low) low = sample;
                        if (sample > high) high = sample;
                    }
                    float lowPercent = ((((float)low) - short.MinValue) / ushort.MaxValue);
                    float highPercent = ((((float)high) - short.MinValue) / ushort.MaxValue);
                    float lowValue = height * lowPercent;
                    float highValue = height * highPercent;

                    Point upV = new Point(x, highValue);
                    Point lowV = new Point(x, lowValue);

                    upperLimits.Add(upV);
                    lowerLimits.Add(lowV);
                }

                

                for (int p = lowerLimits.Count - 1; p >= 0; p--)
                {
                    upperLimits.Add(lowerLimits[p]);
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return upperLimits;
        }

        private static float[] FloatArrayFromByteArray(byte[] input)
        {
            float[] output = new float[input.Length / 4];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = BitConverter.ToSingle(input, i * 4);
            }
            return output;
        }
    }
}
