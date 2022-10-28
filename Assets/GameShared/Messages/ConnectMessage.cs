using RiptideNetworking;

namespace GameShared.Messages
{
	public class ConnectMessage : NetworkMessage
	{
		public string PlayerName { get; private set; }
		public uint PlayerGlobalId { get; private set; }

		public ConnectMessage(string playerName, uint playerGlobalId) : base(MessageSendMode.reliable, MessageEnums.Connect)
		{
			PlayerName = playerName;
			PlayerGlobalId = playerGlobalId;
		}

		public ConnectMessage(Message message) : base(MessageSendMode.reliable, MessageEnums.Connect)
		{
			PlayerName = message.GetString();
			PlayerGlobalId = message.GetUInt();
		}

		protected override void PushVariablesToMessage(Message message)
		{
			message.Add(PlayerName);
			message.Add(PlayerGlobalId);
		}
	}
}