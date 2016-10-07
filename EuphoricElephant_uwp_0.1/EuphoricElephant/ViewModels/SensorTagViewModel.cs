using EuphoricElephant.Models;
using EuphoricElephant.Services;
using System.Collections.ObjectModel;
using Windows.Devices.Enumeration;
using X2CodingLab.SensorTag.Sensors;
using X2CodingLab.SensorTag;
using EuphoricElephant.Model;
using System;
using System.Diagnostics;
using Windows.UI.Core;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using EuphoricElephant.Custom;
using EuphoricElephant.Helpers;
using X2CodingLab.SensorTag.Exceptions;

namespace EuphoricElephant.ViewModels
{
    public class SensorTagViewModel : BaseModel
    {
        private ObservableCollection<DeviceInformation> devices;
        private SensorTag activeSensor;
        private DeviceInformation selectedValue;

        private string gyroX;
        private string gyroY;
        private string gyroZ;
        private string accelX;
        private string accelY;
        private string accelZ;
        private string magX;
        private string magY;
        private string magZ;

        private bool pressed;

        public string GyroX
        {
            get { return gyroX; }
            set { SetProperty(ref gyroX, value); }
        }

        public string GyroY
        {
            get { return gyroY; }
            set { SetProperty(ref gyroY, value); }
        }

        public string GyroZ
        {
            get { return gyroZ; }
            set { SetProperty(ref gyroZ, value); }
        }

        public string AccelX
        {
            get { return accelX; }
            set { SetProperty(ref accelX, value); }
        }
        public string AccelY
        {
            get { return accelY; }
            set { SetProperty(ref accelY, value); }
        }
        public string AccelZ
        {
            get { return accelZ; }
            set { SetProperty(ref accelZ, value); }
        }
        public string MagX
        {
            get { return magX; }
            set { SetProperty(ref magX, value); }
        }
        public string MagY
        {
            get { return magY; }
            set { SetProperty(ref magY, value); }
        }
        public string MagZ
        {
            get { return magZ; }
            set { SetProperty(ref magZ, value); }
        }
        public ObservableCollection<DeviceInformation> Devices
        {
            get { return devices; }
            set { SetProperty(ref devices, value); }
        }

        public SensorTag ActiveSensor
        {
            get { return activeSensor; }
            set { SetProperty(ref activeSensor, value); }
        }

        public DeviceInformation SelectedValue
        {
            get { return selectedValue; }
            set
            {
                SetProperty(ref selectedValue, value);
                ActiveSensor = new SensorTag
                {
                    Info = value
                };
                Load();
            }
        }

        public SensorTagViewModel()
        {
            Init();
        }

        private async void Init()
        {
            Devices = new ObservableCollection<DeviceInformation>(await SensorTagService.FindAllTagNames());          
        }

        private async void Load()
        {
            try
            {
                ActiveSensor.Accelerometer = new CustomAccelerometer();
                ActiveSensor.SimpleKey = new CustomSimpleKey();

                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                async () =>
                {
                    try
                    {

                        var c = (await GattUtils.GetDevicesOfService(String.Format(Constants.BASE_ID, "aa80")));

                        await ActiveSensor.Accelerometer.Initialize(c[0]);
                        await ActiveSensor.Accelerometer.CustomEnableSensor();

                        CustomSimpleKey SimpleKey = new CustomSimpleKey();

                        SimpleKey.SensorValueChanged += sk_sensorValueChanged;

                        var k = (await GattUtils.GetDevicesOfService(String.Format(Constants.SERVICE_ID, "ffe0")));

                        await SimpleKey.Initialize(k[0]);
                        await SimpleKey.EnableNotifications();



                        

                        
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        

        private async void acc_sensorValueChanged(object sender, SensorValueChangedEventArgs e)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                async () =>
                {

                    byte[] raw = e.RawData;

                    byte[] data = await ActiveSensor.Accelerometer.ReadValue();

                    Debug.WriteLine(raw.ToString() + " " + data.ToString());

                    //Value1 = raw.ToString();
                    //Value2 = data.ToString();
                });
        }

        private void foo(byte[] data)
        {
            //GyroX = (BitConverter.ToInt16(data, 0) * 1.0) / (65536 / 500) + " °/s";
            //GyroY = (BitConverter.ToInt16(data, 2) * 1.0) / (65536 / 500) + " °/s";
            //GyroZ = (BitConverter.ToInt16(data, 4) * 1.0) / (65536 / 500) + " °/s";

            //AccelX = (BitConverter.ToInt16(data, 5) * 1.0) / (32768 / 16) + " G";
            //AccelY = (BitConverter.ToInt16(data, 7) * 1.0) / (32768 / 16) + " G";
            //AccelZ = (BitConverter.ToInt16(data, 9) * 1.0) / (32768 / 16) + " G";

            //MagX = (BitConverter.ToInt16(data, 7) * 1.0)  + " uT";
            //MagY = (BitConverter.ToInt16(data, 9) * 1.0)  + " uT";
            //MagZ = (BitConverter.ToInt16(data, 11) * 1.0)  + " uT";

            Debug.WriteLine(String.Format("x:{0} - y:{1} - z:{2}", (BitConverter.ToInt16(data, 6) * 1.0) / (32768 / 16), (BitConverter.ToInt16(data, 8) * 1.0) / (32768 / 16), (BitConverter.ToInt16(data, 10) * 1.0) / (32768 / 16)));
        }

        private async void sk_sensorValueChanged(object sender, SensorValueChangedEventArgs e)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                async() =>
                {
                    byte[] raw = e.RawData;

                    while (Convert.ToBoolean(raw[0]))
                    {
                        byte[] data = await ActiveSensor.Accelerometer.ReadValue();

                        foo(data);
                    }
                }
                );
        }
    }
}
