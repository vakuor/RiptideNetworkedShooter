#if SERVER_ONLY
using System;
using Game.Scripts.MessageHandlers;
using GameShared;
using GameShared.Messages;
using Zenject;

namespace Game.Scripts.NetworkBase.MessageHandlers
{
	public class ServerMessageHandlerProvider : MessageHandlerProvider
	{
		public ServerMessageHandlerProvider(DiContainer container) : base(container)
		{
		}

		public ServerMessageHandler GetHandler(MessageEnums messageEnum)
		{
			switch (messageEnum)
			{
				case MessageEnums.Connect:
					return new ServerConnectMessageHandler();
				case MessageEnums.ChatMail:
					return new ServerChatMessageHandler();
				case MessageEnums.Ping:
					break;//return new ClientPingMessageHandler();
				case MessageEnums.ServerInfo:
					break;//return new ClientServerInfoMessageHandler(_diContainer.Resolve<SceneHandler>());
				case MessageEnums.SpawnRequest:
					return new ServerSpawnRequestMessageHandler(_diContainer.Resolve<ServerInstance>(), _diContainer.Resolve<SceneHandler>());
				case MessageEnums.Input:
					return new ServerInputMessageHandler(_diContainer.Resolve<SceneHandler>(),_diContainer.Resolve<ServerInstance>(), _diContainer.Resolve<NetObjectSpawner>());
				default:
					throw new ArgumentOutOfRangeException(nameof(messageEnum), messageEnum, null);
			}
			throw new ArgumentOutOfRangeException(nameof(messageEnum), messageEnum, null);
		}
	}
}
#endif