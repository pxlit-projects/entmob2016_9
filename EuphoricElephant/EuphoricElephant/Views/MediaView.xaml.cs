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
    public sealed partial class MediaView : Page
    {
        public MediaView()
        {
            Window.Current.SizeChanged += Current_SizeChanged;
            Init();
        }

        private void Init()
        {
            this.InitializeComponent();
            TrackScrollView.Height = Window.Current.Bounds.Height - 73;
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            TrackScrollView.Height = Window.Current.Bounds.Height - 73;
        }
    }
}
