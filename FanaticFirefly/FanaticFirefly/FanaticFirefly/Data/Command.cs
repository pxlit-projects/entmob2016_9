using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanaticFirefly.Data
{
    class Command
    {
        public int CommandId { get; set; }
        public String command { get; set; }

        public Command(int CommandId, string command)
        {
            this.CommandId = CommandId;
            this.command = command;
        }
    }
}
