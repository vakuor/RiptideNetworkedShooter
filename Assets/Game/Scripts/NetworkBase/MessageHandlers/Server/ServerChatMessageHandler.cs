#if SERVER_ONLY
using Game.Scripts.NetworkBase.MessageHandlers;
using GameShared.Messages;
using RiptideNetworking;
using UnityEngine;

namespace Game.Scripts.MessageHandlers
{
	public class ServerChatMessageHandler : ServerMessageHandler
	{
		public override void Handle(object sender, ServerMessageReceivedEventArgs e)
		{
			Debug.Log("Chat: " + new ChatMessage(e.Message).Text);
		}
	}
}
#endif