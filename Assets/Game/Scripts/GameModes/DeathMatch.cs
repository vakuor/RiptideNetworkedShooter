using System;
using System.Collections.Generic;
using Game.Scripts.Gameplay.Player;
using Game.Scripts.NetworkBase.MessageHandlers;
using GameShared;
using GameShared.Messages;
using RiptideNetworking;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Startup
{
	public class DeathMatch : GameModeHandler
	{
#if SERVER_ONLY
		private ServerMessageHandlerProvider _serverMessageHandlerProvider;
		private SceneHandler _sceneHandler; //TODO: remove
		public DeathMatch(ServerInstance serverInstance, ServerMessageHandlerProvider serverMessageHandlerProvider, SceneHandler sceneHandler) : base(serverInstance)
		{
			_serverMessageHandlerProvider = serverMessageHandlerProvider;
			_sceneHandler = sceneHandler;
		}
		public override void ServerOnClientConnected(object sender, ServerClientConnectedEventArgs e)
		{
			ConnectMessage connectMessage = new ConnectMessage(e.ConnectMessage);
			Debug.Log("Client connected. ID: " + e.Client.Id + " Name: " + connectMessage.PlayerName + " GlobalId: " + connectMessage.PlayerGlobalId );
			_userStorage.Add(e.Client.Id, new User(connectMessage.PlayerGlobalId, connectMessage.PlayerName, e.Client.Id));
			_serverInstance.SendMessage(new ChatMessage($"Welcome {connectMessage.PlayerName}").GetMessage(), e.Client.Id);
			_serverInstance.SendMessage(new ServerInfoMessage("DefaultMap").GetMessage(), e.Client.Id);
			_serverInstance.SendMessageToAll(new ChatMessage(
					$"Player: {e.Client.Id} with name: {connectMessage.PlayerName} joined the game.").GetMessage(), e.Client.Id);
			foreach (KeyValuePair<ushort, PlayerMotor> motorPair in _sceneHandler.Players.PlayerMotors)
			{
				Transform transform;
				_serverInstance.SendMessage(new SpawnNetObjectMessage(motorPair.Value.NetId, motorPair.Key, NetObjectType.PlayerMotor, (transform = motorPair.Value.transform).position, transform.rotation).GetMessage(), e.Client.Id);
			}
		}

		public override void ServerOnClientDisconnected(object sender, ClientDisconnectedEventArgs e)
		{
			var user = _userStorage.Remove(e.Id);
			if (_sceneHandler.Players.IsMotorExists(e.Id))
			{
				_sceneHandler.Players.DeSpawn(user.ClientId);
				_serverInstance.SendMessageToAll(new DeSpawnMessage(user.ClientId).GetMessage());
			}
			_serverInstance.SendMessageToAll(new ChatMessage("Player: " + e.Id + " left the game.").GetMessage(), e.Id);
		}

		public override void ServerMessageReceived(object sender, ServerMessageReceivedEventArgs e)
		{
			_serverMessageHandlerProvider.GetHandler((MessageEnums)e.MessageId).Handle(sender, e);
		}
#elif CLIENT_ONLY
		private ClientMessageHandlerProvider _clientMessageHandlerProvider;

		public DeathMatch(ClientInstance clientInstance, ClientMessageHandlerProvider clientMessageHandlerProvider) :
				base(clientInstance)
		{
			_clientMessageHandlerProvider = clientMessageHandlerProvider;
		}

		public override void ClientOnConnected(object sender, EventArgs e)
		{
			Debug.Log("Connected successfully!");
		}

		public override void ClientOnConnectionFailed(object sender, EventArgs e)
		{
			Debug.Log("Not Connected successfully!");
		}

		public override void ClientOnClientConnected(object sender, ClientConnectedEventArgs e)
		{
			Debug.Log("Client with ID: " + e.Id + " connected successfully!");
		}

		public override void ClientOnClientDisconnected(object sender, ClientDisconnectedEventArgs e)
		{
			Debug.Log("Client with ID: " + e.Id + " disconnected!");
		}

		public override void ClientOnDisconnected(object sender, EventArgs e)
		{
			Debug.Log("Disconnected successfully!");
		}

		public override void ClientMessageReceived(object sender, ClientMessageReceivedEventArgs e)
		{
			_clientMessageHandlerProvider.GetHandler((MessageEnums)e.MessageId).Handle(sender, e);
		}
#endif
	}
}