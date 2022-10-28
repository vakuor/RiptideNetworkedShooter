#if CLIENT_ONLY
using Game.Scripts.NetworkBase.MessageHandlers.Server;
using GameShared.Messages;
using RiptideNetworking;
using UnityEngine;

namespace Game.Scripts.MessageHandlers
{
	public class ClientChatMessageHandler : ClientMessageHandler
	{
		public override void Handle(object sender, ClientMessageReceivedEventArgs e)
		{
			Debug.Log("Chat message: " + new ChatMessage(e.Message).Text);
		}
	}
}
#endif