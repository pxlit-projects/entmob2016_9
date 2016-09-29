using EuphoricElephant.Data;
using EuphoricElephant.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuphoricElephant.ViewModels
{
    public class SensorTagViewModel : SensorTag
    {
        private List<string> devices;

        public List<string> Devices
        {
            get { return devices; }
            set { SetProperty(ref devices, value); }
        }

        public SensorTagViewModel()
        {
            Load();
        }

        private async void Load()
        {
            Devices = await SensorTagService.FindAllTagNames();
        }
    }
}
