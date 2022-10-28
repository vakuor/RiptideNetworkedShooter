#if CLIENT_ONLY
using System;
using System.Collections.Generic;
using Game.Scripts.Gameplay.Player;
using Game.Scripts.NetworkBase.MessageHandlers.Server;
using GameShared;
using GameShared.Messages;
using RiptideNetworking;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.MessageHandlers
{
	public class ClientSpawnNetObjectMessageHandler : ClientMessageHandler
	{
		private ClientInstance _clientInstance;
		private Players _players;
		private NetObjectSpawner _netObjectSpawner;

		public ClientSpawnNetObjectMessageHandler(ClientInstance clientInstance, SceneHandler sceneHandler, NetObjectSpawner netObjectSpawner)
		{
			_clientInstance = clientInstance;
			_players = sceneHandler.Players;
			_netObjectSpawner = netObjectSpawner;
		}

		public override void Handle(object sender, ClientMessageReceivedEventArgs e)
		{
			SpawnNetObjectMessage msg = new(e.Message);
			switch (msg.NetObjectType)
			{
				case NetObjectType.PlayerMotor:
				{
					Debug.Log(msg.SpawnPosition);
					PlayerMotor motor = _players.Spawn(msg.OwnerId, msg.NetObjectId, msg.SpawnPosition, msg.SpawnRotation);
					if (_clientInstance.ClientId == msg.OwnerId)
					{
						Debug.Log("SpawnSelf");
						Object.Destroy(motor._model);//TODO: remove
					}
					else
					{
						Debug.Log("Spawn " + msg.OwnerId);
						Object.Destroy(motor._camera);//TODO: remove
					}
					break;
				}
				case NetObjectType.Bullet:
				{
					BulletMotor bullet = _netObjectSpawner.Spawn<BulletMotor>(msg.NetObjectId, msg.OwnerId);
					bullet.Transform.SetPositionAndRotation(msg.SpawnPosition, msg.SpawnRotation);
					bullet.Push();
					break;
				}
				default:
				{
					throw new Exception("Unknown netspawnhandler type");
				}
			}
		}
	}
}
#endif