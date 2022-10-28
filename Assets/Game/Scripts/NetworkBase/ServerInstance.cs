#if SERVER_ONLY
using System;
using Game.Scripts.MessageHandlers;
using Game.Scripts.NetworkBase.MessageHandlers;
using Game.Scripts.Startup;
using GameShared;
using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
	public class ServerInstance : NetInstance
	{
		private ServerCopy Server { get; set; }

		private ServerMessageHandlerProvider _serverMessageHandlerProvider;
		private SceneHandler _sceneHandler; //TODO: remove
		private IMessageSender _messageSender;

		public ushort netidtemp = 100; //TODO: костыль

		public ServerInstance(ServerMessageHandlerProvider serverMessageHandlerProvider, SceneHandler sceneHandler, ServerCopy server)
		{
			_serverMessageHandlerProvider = serverMessageHandlerProvider;
			_sceneHandler = sceneHandler;
			Server = server;
		}

		public override void Initialize()
		{
			Debug.Log("Server initialization");
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = 30;

#if UNITY_EDITOR
			RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
#else
            Console.Title = "Server";
            Console.Clear();
            Application.SetStackTraceLogType(UnityEngine.LogType.Log, StackTraceLogType.None);
            RiptideLogger.Initialize(Debug.Log, true);
#endif
		}

		public void SendMessage(Message message, ushort toClientId, bool shouldRelease = true) => Server.Send(message, toClientId, shouldRelease);
		public void SendMessageToAll(Message message, bool shouldRelease = true) => Server.SendToAll(message, shouldRelease);
		public void SendMessageToAll(Message message, ushort exceptToClientId, bool shouldRelease = true) => Server.SendToAll(message, exceptToClientId, shouldRelease);

		protected override void SetGameMode(GameModeHandler gameModeHandler)
		{
			ResetGameMode();
			_gameModeHandler = gameModeHandler;
			Server.ClientConnected += _gameModeHandler.ServerOnClientConnected;
			Server.ClientDisconnected += _gameModeHandler.ServerOnClientDisconnected;
			Server.MessageReceived += _gameModeHandler.ServerMessageReceived;
		}

		protected override void ResetGameMode()
		{
			if (_gameModeHandler != null)
			{
				Server.ClientConnected -= _gameModeHandler.ServerOnClientConnected;
				Server.ClientDisconnected -= _gameModeHandler.ServerOnClientDisconnected;
				Server.MessageReceived -= _gameModeHandler.ServerMessageReceived;
			}
			else
			{
				Debug.LogWarning("Reset GameMode called while GameMode was null!");
			}
			_gameModeHandler = null;
		}

		public override void FixedTick()
		{
			Server.Tick();
		}

		public void Launch(ushort port, ushort maxClientCount)
		{
			if (port == 0)
			{
				port = DefaultSettings.DefaultPort;
			}
			if (maxClientCount == 0)
			{
				maxClientCount = DefaultSettings.DefaultMaxClientCount;
			}
			SetGameMode(new DeathMatch(this, _serverMessageHandlerProvider, _sceneHandler));
			Server.Start(port, maxClientCount);
		}

		public override void Dispose()
		{
			Server.Stop();
			ResetGameMode();
		}

	}
}
#endif