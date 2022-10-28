using System;
using RiptideNetworking;

namespace GameShared.Messages
{
	public class DeSpawnMessage : NetworkMessage
	{
		public ushort OwnerId { get; private set; }

		public DeSpawnMessage(ushort ownerId) : base(MessageSendMode.reliable, MessageEnums.DeSpawn)
		{
			OwnerId = ownerId;
		}

		public DeSpawnMessage(Message message) : base(MessageSendMode.reliable, MessageEnums.DeSpawn)
		{
			OwnerId = message.GetUShort();
		}

		protected override void PushVariablesToMessage(Message message)
		{
			message.Add(OwnerId);
		}
	}
}