#if SERVER_ONLY
using RiptideNetworking;
using UnityEngine;

namespace Game.Scripts.NetworkBase.MessageHandlers
{
	public class ServerPingMessageHandler : ServerMessageHandler
	{
		public override void Handle(object sender, ServerMessageReceivedEventArgs e)
		{
			Debug.Log("ping msg id: " + e.MessageId);
			Debug.Log("sender: " + sender);
		}
	}
}
#endif