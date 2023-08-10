using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Template.Messages
{
    public class PrintDebugMessage : ValueChangedMessage<string>
    {
        public PrintDebugMessage(string message) : base(message)
        { 
        }
    }
}
