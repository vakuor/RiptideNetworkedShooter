#if CLIENT_ONLY
using System;
using Game.Scripts.NetworkBase.MessageHandlers;
using Game.Scripts.Startup;
using GameShared.Messages;
using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
	public class ClientInstance : NetInstance
	{
		private Client Client { get; set; }

		private ClientMessageHandlerProvider _clientMessageHandlerProvider;

		public ClientInstance(ClientMessageHandlerProvider clientMessageHandlerProvider)
		{
			_clientMessageHandlerProvider = clientMessageHandlerProvider;
		}

		public override void Initialize()
		{
			Debug.Log("Client initialization");
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = 60;

#if UNITY_EDITOR
			RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
#endif

			Client = new Client();
		}

		public bool IsConnected => Client.IsConnected;
		public ushort ClientId
		{
			get
			{
				if (Client.IsConnected)
				{
					return Client.Id;
				}
				throw new Exception("Trying to get ClientID while it is not connected");
			}
		}

		public void SendMessage(Message message, bool shouldRelease = true) => Client.Send(message, shouldRelease);

		public override void FixedTick()
		{
			Client.Tick();
		}

		public override void Dispose()
		{
			Disconnect();
		}

		public void Connect(string ip, ushort port)
		{
			SetGameMode(new DeathMatch(this, _clientMessageHandlerProvider));
			Client.Connect($"{ip}:{port}", 0, new ConnectMessage("MyNameIsJojo", 23).GetMessage());
		}

		public void Disconnect()
		{
			Client.Disconnect();
			ResetGameMode();
		}


		protected override void SetGameMode(GameModeHandler gameModeHandler)
		{
			ResetGameMode();
			_gameModeHandler = gameModeHandler;
			Client.Connected += _gameModeHandler.ClientOnConnected;
			Client.Disconnected += _gameModeHandler.ClientOnDisconnected;
			Client.ConnectionFailed += _gameModeHandler.ClientOnConnectionFailed;
			Client.ClientConnected += _gameModeHandler.ClientOnClientConnected;
			Client.ClientDisconnected += _gameModeHandler.ClientOnClientDisconnected;
			Client.MessageReceived += _gameModeHandler.ClientMessageReceived;
		}

		protected override void ResetGameMode()
		{
			if (_gameModeHandler != null)
			{
				Client.Connected -= _gameModeHandler.ClientOnConnected;
				Client.Disconnected -= _gameModeHandler.ClientOnDisconnected;
				Client.ConnectionFailed -= _gameModeHandler.ClientOnConnectionFailed;
				Client.ClientConnected -= _gameModeHandler.ClientOnClientConnected;
				Client.ClientDisconnected -= _gameModeHandler.ClientOnClientDisconnected;
				Client.MessageReceived -= _gameModeHandler.ClientMessageReceived;
			}
			else
			{
				Debug.LogWarning("Reset gamemode called while gamemode was null!");
			}
			_gameModeHandler = null;
		}
	}
}
#endif