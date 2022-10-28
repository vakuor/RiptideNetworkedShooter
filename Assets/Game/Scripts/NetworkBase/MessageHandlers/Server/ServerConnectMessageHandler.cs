#if SERVER_ONLY
using GameShared.Messages;
using RiptideNetworking;
using UnityEngine;

namespace Game.Scripts.NetworkBase.MessageHandlers
{
	public class ServerConnectMessageHandler : ServerMessageHandler
	{
		public override void Handle(object sender, ServerMessageReceivedEventArgs e)
		{
			var msg = new ConnectMessage(e.Message);
			Debug.Log("connect msg: " + msg.PlayerName + " " + msg.PlayerGlobalId);
		}
	}
}
#endif