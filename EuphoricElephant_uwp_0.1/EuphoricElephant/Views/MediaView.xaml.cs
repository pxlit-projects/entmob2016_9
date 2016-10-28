using EuphoricElephant.Interfaces;
using EuphoricElephant.Services;
using EuphoricElephant.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace EuphoricElephant.Views
{
    public sealed partial class MediaView : Page, IMediaView
    {
        public MediaView()
        {
            this.InitializeComponent();
            (DataContext as MediaViewModel).View = this as IMediaView;        
        }

        public void DrawOnCanvas(byte[] data)
        {
            foreach(var v in AudioCanvas.Children)
            {
                AudioCanvas.Children.Remove(v);
            }

            Polygon wave = new Polygon
            {
                Fill = new SolidColorBrush(Colors.LightSkyBlue),
                StrokeThickness = 2,
                Stroke = new SolidColorBrush(Colors.DarkSlateBlue)
            };

            wave.Points = AudioVisualizationService.GetWavePoints(data, AudioCanvas);

            AudioCanvas.Children.Add(wave);
        }
    }
}
