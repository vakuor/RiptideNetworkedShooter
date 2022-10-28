#if SERVER_ONLY
using Game.Scripts.Gameplay.Player;
using Game.Scripts.NetworkBase.MessageHandlers;
using GameShared;
using GameShared.Messages;
using RiptideNetworking;
using UnityEngine;

namespace Game.Scripts.MessageHandlers
{
	public class ServerInputMessageHandler : ServerMessageHandler
	{
		private ServerInstance _serverInstance;
		private SceneHandler _sceneHandler;
		private NetObjectSpawner _netObjectSpawner;

		public ServerInputMessageHandler(SceneHandler sceneHandler, ServerInstance serverInstance, NetObjectSpawner netObjectSpawner)
		{
			_sceneHandler = sceneHandler;
			_serverInstance = serverInstance;
			_netObjectSpawner = netObjectSpawner;
		}

		public override void Handle(object sender, ServerMessageReceivedEventArgs e)
		{
			InputMessage msg = new InputMessage(e.Message);
			if (!_sceneHandler.Players.PlayerMotors.ContainsKey(e.FromClientId)) //TODO: костыль, remove
			{
				return;
			}
			_sceneHandler.Players.HandleInput(e.FromClientId, msg.Input);
			var p = _sceneHandler.Players.PlayerMotors[e.FromClientId];
			if (msg.Input.Inputs[4] && Time.time - p._lastShotTime > 0.5f)
			{
				p._lastShotTime = Time.time;
				var mes = new SpawnNetObjectMessage(_serverInstance.netidtemp, e.FromClientId, NetObjectType.Bullet,
						p._shotPivot.position, p._shotPivot.rotation);
				_serverInstance.SendMessageToAll(mes.GetMessage());
				_serverInstance.netidtemp++;
				BulletMotor bullet = _netObjectSpawner.Spawn<BulletMotor>(mes.NetObjectId, mes.OwnerId);
				bullet.Transform.SetPositionAndRotation(mes.SpawnPosition, mes.SpawnRotation);
				bullet.Push();
			}
//			DebugInput(e, msg);
		}

		private void DebugInput(ServerMessageReceivedEventArgs e, InputMessage msg)
		{
			Debug.Log("Input: " + e.FromClientId + " " + msg.Input.Inputs[0] + " " + msg.Input.Inputs[1] + " " + msg.Input.Inputs[2] + " " + msg.Input.Inputs[3] + " " + msg.Input.Inputs[4]);
		}
	}
}
#endif