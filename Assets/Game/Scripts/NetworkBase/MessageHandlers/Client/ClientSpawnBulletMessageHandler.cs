#if CLIENT_ONLY
using Game.Scripts.Gameplay.Player;
using Game.Scripts.NetworkBase.MessageHandlers.Server;
using GameShared;
using GameShared.Messages;
using RiptideNetworking;
using UnityEngine;

namespace Game.Scripts.MessageHandlers
{
	public class ClientSpawnBulletMessageHandler : ClientMessageHandler
	{
		private ClientInstance _clientInstance;
		private Bullets _bullets;

		public ClientSpawnBulletMessageHandler(ClientInstance clientInstance, SceneHandler sceneHandler)
		{
			_clientInstance = clientInstance;
			_bullets = sceneHandler.Bullets;
		}

		public override void Handle(object sender, ClientMessageReceivedEventArgs e)
		{
			SpawnBulletMessage msg = new SpawnBulletMessage(e.Message);
			BulletMotor bulletMotor = _bullets.Spawn(msg.BulletId, msg.SpawnPosition, msg.SpawnRotation); //TODO: position from map
			bulletMotor.AddForce(80f); //TODO: move constant to another place
			//_bullets.DeSpawn(msg.BulletId);
		}
	}
}
#endif