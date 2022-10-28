#if CLIENT_ONLY
using System;
using System.Collections.Generic;
using Game.Scripts.MessageHandlers;
using Game.Scripts.NetworkBase.MessageHandlers.Server;
using GameShared;
using GameShared.Messages;
using Zenject;

namespace Game.Scripts.NetworkBase.MessageHandlers
{
	public class ClientMessageHandlerProvider : MessageHandlerProvider
	{
		//private Dictionary<MessageEnums, Type> _handlers;
		public ClientMessageHandlerProvider(DiContainer container) : base(container)
		{
			/*_handlers = new Dictionary<MessageEnums, Type>();
			_handlers.Add(MessageEnums.ChatMail, typeof(ClientChatMessageHandler));
			_handlers.Add(MessageEnums.ServerInfo, typeof(ClientServerInfoMessageHandler));
			_handlers.Add(MessageEnums.Spawn, typeof(ClientSpawnMessageHandler));
			_handlers.Add(MessageEnums.DeSpawn, typeof(ClientDeSpawnMessageHandler));
			_handlers.Add(MessageEnums.Position, typeof(ClientPositionMessageHandler));*/
			//register enum handler iun dictionary
		}

		public ClientMessageHandler GetHandler(MessageEnums messageEnum)
		{
			/*Type type = _handlers[messageEnum];
			return (ClientMessageHandler)_diContainer.Resolve(type);*/
			switch (messageEnum)
			{
				case MessageEnums.Connect:
					break;//return new ClientConnectMessageHandler();
				case MessageEnums.ChatMail:
					return new ClientChatMessageHandler();
				case MessageEnums.Ping:
					break;//return new ClientPingMessageHandler();
				case MessageEnums.ServerInfo:
					return new ClientServerInfoMessageHandler(_diContainer.Resolve<SceneHandler>());
				case MessageEnums.Spawn:
					return new ClientSpawnMessageHandler(_diContainer.Resolve<ClientInstance>(), _diContainer.Resolve<SceneHandler>());
				case MessageEnums.DeSpawn:
					return new ClientDeSpawnMessageHandler(_diContainer.Resolve<ClientInstance>(), _diContainer.Resolve<SceneHandler>());
				case MessageEnums.PositionSync:
					return new ClientPositionMessageHandler(_diContainer.Resolve<ClientInstance>(), _diContainer.Resolve<SceneHandler>(), _diContainer.Resolve<NetObjectSpawner>());
				case MessageEnums.SpawnNetObject:
					return new ClientSpawnNetObjectMessageHandler(_diContainer.Resolve<ClientInstance>(), _diContainer.Resolve<SceneHandler>(), _diContainer.Resolve<NetObjectSpawner>());
				default:
					throw new ArgumentOutOfRangeException(nameof(messageEnum), messageEnum, null);
			}
			throw new ArgumentOutOfRangeException(nameof(messageEnum), messageEnum, null);
		}
	}
}
#endif