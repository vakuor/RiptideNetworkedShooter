#if CLIENT_ONLY
using Game.Scripts.NetworkBase.MessageHandlers.Server;
using GameShared;
using GameShared.Messages;
using RiptideNetworking;
using UnityEngine;

namespace Game.Scripts.MessageHandlers
{
	public class ClientServerInfoMessageHandler : ClientMessageHandler
	{
		public SceneHandler _sceneHandler;

		public ClientServerInfoMessageHandler(SceneHandler sceneHandler)
		{
			_sceneHandler = sceneHandler;
		}

		public override void Handle(object sender, ClientMessageReceivedEventArgs e)
		{
			var msg = new ServerInfoMessage(e.Message);
			Debug.Log("ServerInfoMessage: " + msg.MapName);
			_sceneHandler.StartScene(msg.MapName);
		}
	}
}
#endif