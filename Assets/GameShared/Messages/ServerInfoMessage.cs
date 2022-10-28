using RiptideNetworking;

namespace GameShared.Messages
{
	public class ServerInfoMessage : NetworkMessage
	{
		public string MapName { get; private set; }

		public ServerInfoMessage(string mapName) : base(MessageSendMode.reliable, MessageEnums.ServerInfo)
		{
			MapName = mapName;
		}

		public ServerInfoMessage(Message message) : base(MessageSendMode.reliable, MessageEnums.ServerInfo)
		{
			MapName = message.GetString();
		}

		protected override void PushVariablesToMessage(Message message)
		{
			message.Add(MapName);
		}
	}
}