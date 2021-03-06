﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Hubl.Core.Model;
using MessageRouter.Message;
using MessageRouter.Network;

namespace Hubl.Core.Messages
{
    [DataContract, Message(MessageGroups.System)]
    public class EchoMessage:IMessage
    {
        public EchoMessage(User sender)
        {
            Sender = sender;
        }

        [DataMember]
        public User Sender { get; set; }
    }
}
