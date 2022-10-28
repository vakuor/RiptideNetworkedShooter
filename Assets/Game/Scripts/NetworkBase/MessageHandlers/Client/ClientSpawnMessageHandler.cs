#if CLIENT_ONLY
using System;
using Game.Scripts.Gameplay.Player;
using Game.Scripts.NetworkBase.MessageHandlers.Server;
using GameShared;
using GameShared.Messages;
using RiptideNetworking;
using UnityEngine;

namespace Game.Scripts.MessageHandlers
{
	public class ClientSpawnMessageHandler : ClientMessageHandler
	{
		private ClientInstance _clientInstance;
		private Players _players;

		public ClientSpawnMessageHandler(ClientInstance clientInstance, SceneHandler sceneHandler)
		{
			_clientInstance = clientInstance;
			_players = sceneHandler.Players;
		}

		public override void Handle(object sender, ClientMessageReceivedEventArgs e)
		{
			throw new Exception("Not used");
			SpawnNetObjectMessage msg = new(e.Message);
			//PlayerMotor playerMotor = _players.Spawn(msg.OwnerId, msg.NetObjectId, Vector3.up); //TODO: position from map
			if (_clientInstance.ClientId == msg.OwnerId)
			{
				Debug.Log("SpawnSelf");
				//Object.Destroy(playerMotor._model);//TODO: remove
			}
			else
			{
				Debug.Log("Spawn " + msg.OwnerId);
				//Object.Destroy(playerMotor._camera);//TODO: remove
			}
		}
	}
}
#endif