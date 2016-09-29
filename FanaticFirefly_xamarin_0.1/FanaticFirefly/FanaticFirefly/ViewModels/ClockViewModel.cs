using FanaticFirefly.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FanaticFirefly.ViewModels
{
    public class ClockViewModel : BaseModel
    {
        private DateTime time;
        public DateTime Time
        {
            get { return time; }
            set { SetProperty(ref time, value); }
        }

        public ClockViewModel()
        {
            Time = DateTime.Now;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Time = DateTime.Now;
                return true;
            });
        }
    }
}
