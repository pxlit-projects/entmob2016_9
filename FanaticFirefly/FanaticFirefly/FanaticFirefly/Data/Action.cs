using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanaticFirefly.Data
{
    class Action
    {
        public int ActId { get; set; }
        public string action { get; set; }

        public Action(int ActId, string action)
        {
            this.ActId = ActId;
            this.action = action;
        }
    }
}
