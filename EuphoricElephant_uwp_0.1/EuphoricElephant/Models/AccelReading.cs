using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuphoricElephant.Models
{
    public class AccelReading : BaseModel
    {
        private byte x;
        private byte y;
        private byte z;

        private DateTimeOffset time;

        public byte X
        {
            get { return x; }
            set { SetProperty(ref x, value); }
        }

        public byte Y
        {
            get { return y; }
            set { SetProperty(ref y, value); }
        }

        public byte Z
        {
            get { return z; }
            set { SetProperty(ref z, value); }
        }

        public DateTimeOffset Time
        {
            get { return time; }
            set { SetProperty(ref time, value); }
        }
    }
}
