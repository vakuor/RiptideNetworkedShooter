#if CLIENT_ONLY
using Game.Scripts.Gameplay.Player;
using Game.Scripts.NetworkBase.MessageHandlers.Server;
using GameShared;
using GameShared.Messages;
using RiptideNetworking;
using UnityEngine;

namespace Game.Scripts.MessageHandlers
{
	public class ClientPositionMessageHandler : ClientMessageHandler
	{
		private ClientInstance _clientInstance;
		private Players _players;
		private NetObjectSpawner _objectSpawner;

		public ClientPositionMessageHandler(ClientInstance clientInstance, SceneHandler sceneHandler, NetObjectSpawner objectSpawner)
		{
			_clientInstance = clientInstance;
			_players = sceneHandler.Players;
			_objectSpawner = objectSpawner;
		}

		public override void Handle(object sender, ClientMessageReceivedEventArgs e)
		{
			PositionSyncMessage msg = new(e.Message);
//			Debug.Log(msg.Position);
			var id = _objectSpawner.SpawnableObjects[msg.ObjectNetId].OwnerId;
			_players.PlayerMotors[id].Move(msg.Position, msg.Rotation,
					msg.ObjectNetId == _clientInstance.ClientId);
		}
	}
}
#endif