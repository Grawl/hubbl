﻿using System;
using MessageRouter.Network;
using Sockets.Plugin;
using Sockets.Plugin.Abstractions;

namespace Hubl.Mobile.Network
{
	public class MobileMulticastClient: IMulticastClient
	{

		private readonly IUdpSocketMulticastClient _udpClient;

		private readonly MobileNetworkSettings _settings;

		public MobileMulticastClient (MobileNetworkSettings settings)
		{
			_settings = settings;

		    _udpClient = new UdpSocketMulticastClient {TTL = settings.TTL};
            _udpClient.MessageReceived = OnMessageReceived;

		}

	    private void OnMessageReceived(object sender, UdpSocketMessageReceivedEventArgs e)
	    {
            if(MessageReceived != null)
                MessageReceived(sender, new DatagramReceivedEventArgs(e.RemoteAddress, int.Parse(e.RemotePort), e.ByteData));
	    }

	    #region IMulticastClient implementation

		public event EventHandler<DatagramReceivedEventArgs> MessageReceived;

		public System.Threading.Tasks.Task JoinMulticastGroupAsync ()
		{
			return _udpClient.JoinMulticastGroupAsync (_settings.MulticastAdress, _settings.MulticastPort, _settings.Adapters);
		}

		public System.Threading.Tasks.Task DisconnectAsync ()
		{
			return _udpClient.DisconnectAsync ();
		}

		public System.Threading.Tasks.Task SendMulticastAsync (byte[] data)
		{
			return _udpClient.SendMulticastAsync (data);
		}

		#endregion

		#region IDisposable implementation

		public void Dispose ()
		{
			_udpClient.Dispose ();
		}

		#endregion


	}
}

