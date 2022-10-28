#if CLIENT_ONLY
using Game.Scripts.Gameplay.Player;
using Game.Scripts.NetworkBase.MessageHandlers.Server;
using GameShared;
using GameShared.Messages;
using RiptideNetworking;
using UnityEngine;

namespace Game.Scripts.MessageHandlers
{
	public class ClientDeSpawnMessageHandler : ClientMessageHandler
	{
		private ClientInstance _clientInstance;
		private Players _players;

		public ClientDeSpawnMessageHandler(ClientInstance clientInstance, SceneHandler sceneHandler)
		{
			_clientInstance = clientInstance;
			_players = sceneHandler.Players;
		}

		public override void Handle(object sender, ClientMessageReceivedEventArgs e)
		{
			DeSpawnMessage msg = new DeSpawnMessage(e.Message);
			_players.DeSpawn(msg.OwnerId); //TODO: position from map
			if (_clientInstance.ClientId == msg.OwnerId)
			{
				Debug.Log("DeSpawnSelf");
			}
			else
			{
				Debug.Log("DeSpawn " + msg.OwnerId);
			}
		}
	}
}
#endif