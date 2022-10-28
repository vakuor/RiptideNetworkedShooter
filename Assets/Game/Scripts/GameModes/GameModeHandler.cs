#if CLIENT_ONLY
using System;
#endif
using System.Collections.Generic;
using Game.Scripts.Gameplay.Player;
using GameShared;
using RiptideNetworking;

namespace Game.Scripts.Startup
{
	public abstract class GameModeHandler
	{
#if SERVER_ONLY
		protected ServerInstance _serverInstance;
		protected readonly UserStorage _userStorage;
		protected GameModeHandler(ServerInstance serverInstance)
		{
			_serverInstance = serverInstance;
			_userStorage = new UserStorage();
		}

		public abstract void ServerOnClientConnected(object sender, ServerClientConnectedEventArgs e);
		public abstract void ServerOnClientDisconnected(object sender, ClientDisconnectedEventArgs e);
		public abstract void ServerMessageReceived(object sender, ServerMessageReceivedEventArgs e);
#elif CLIENT_ONLY
		protected ClientInstance _clientInstance;
		protected GameModeHandler(ClientInstance clientInstance)
		{
			_clientInstance = clientInstance;
		}
		
		public abstract void ClientOnConnected(object sender, EventArgs e);
		public abstract void ClientOnConnectionFailed(object sender, EventArgs e);
		public abstract void ClientOnClientConnected(object sender, ClientConnectedEventArgs e);
		public abstract void ClientOnClientDisconnected(object sender, ClientDisconnectedEventArgs e);
		public abstract void ClientOnDisconnected(object sender, EventArgs e);
		public abstract void ClientMessageReceived(object sender, ClientMessageReceivedEventArgs e);
#endif
	}
}