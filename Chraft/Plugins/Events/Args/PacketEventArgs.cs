﻿#region C#raft License
// This file is part of C#raft. Copyright C#raft Team 
// 
// C#raft is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Chraft.Net;
using Chraft.Net.Packets;

namespace Chraft.Plugins.Events.Args
{
    public class PacketEventArgs : ChraftEventArgs
    {
        public virtual Packet Packet { get; set; }
        public virtual Socket Socket { get; set; }
        public virtual Client Client { get; set; }

        public PacketEventArgs(Packet Packet, Socket Socket, Client Client)
        {
            this.Socket = Socket;
            this.Client = Client;
            this.Packet = Packet;
        }
    }
    public class PacketSentEventArgs : PacketEventArgs
    {
        public PacketSentEventArgs(Packet Packet, Socket Socket, Client Client) : base(Packet, Socket, Client) { }
    }
    public class PacketRecevedEventArgs : PacketEventArgs
    {
        public PacketRecevedEventArgs(Packet Packet, Socket Socket, Client Client) : base(Packet, Socket, Client) { }
    }
}
