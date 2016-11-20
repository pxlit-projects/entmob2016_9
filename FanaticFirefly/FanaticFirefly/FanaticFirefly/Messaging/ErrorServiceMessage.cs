using FanaticFirefly.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanaticFirefly.Messaging
{
    public class ErrorServiceMessage
    {
        public string ErrorMessage;
        public ViewType Type;

        public ErrorServiceMessage(string errorMessage, ViewType type)
        {
            this.ErrorMessage = errorMessage;
            this.Type = type;
        }
    }
}
