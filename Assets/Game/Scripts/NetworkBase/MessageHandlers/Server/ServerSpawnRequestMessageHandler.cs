#if SERVER_ONLY
using Game.Scripts.Gameplay.Player;
using Game.Scripts.NetworkBase.MessageHandlers;
using GameShared;
using GameShared.Messages;
using RiptideNetworking;
using UnityEngine;

namespace Game.Scripts.MessageHandlers
{
	public class ServerSpawnRequestMessageHandler : ServerMessageHandler
	{
		private ServerInstance _serverInstance;
		private SceneHandler _sceneHandler;

		public ServerSpawnRequestMessageHandler(ServerInstance serverInstance, SceneHandler sceneHandler)
		{
			_serverInstance = serverInstance;
			_sceneHandler = sceneHandler;
		}

		public override void Handle(object sender, ServerMessageReceivedEventArgs e)
		{
			Debug.Log($"SpawnRequest from: {e.FromClientId}");
			//TODO: spawn checks if we can spawn this man
			if (_sceneHandler.Players.IsMotorExists(e.FromClientId))
			{
				Debug.LogWarning("This client trying to spawn existing playermotor!");
				return;
			}
			_serverInstance.SendMessageToAll(new SpawnNetObjectMessage(_sceneHandler.NetObjectSpawnedCount, e.FromClientId, NetObjectType.PlayerMotor, Vector3.up*2, Quaternion.identity).GetMessage());
			PlayerMotor motor = _sceneHandler.Players.Spawn(e.FromClientId, _sceneHandler.NetObjectSpawnedCount, Vector3.up*2, Quaternion.identity); //TODO: position from map
			Object.Destroy(motor._camera);//TODO: remove
		}
	}
}
#endif