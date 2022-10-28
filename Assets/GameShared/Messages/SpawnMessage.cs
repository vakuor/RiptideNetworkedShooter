using RiptideNetworking;

namespace GameShared.Messages
{
	public class SpawnMessage : NetworkMessage
	{
		public ushort OwnerId { get; private set; }
		public string PlayerName { get; private set; }

		public SpawnMessage(ushort ownerId, string playerName) : base(MessageSendMode.reliable, MessageEnums.Spawn)
		{
			OwnerId = ownerId;
			PlayerName = playerName;
		}

		public SpawnMessage(Message message) : base(MessageSendMode.reliable, MessageEnums.Spawn)
		{
			OwnerId = message.GetUShort();
			PlayerName = message.GetString();
		}

		protected override void PushVariablesToMessage(Message message)
		{
			message.Add(OwnerId);
			message.Add(PlayerName);
		}
	}
}