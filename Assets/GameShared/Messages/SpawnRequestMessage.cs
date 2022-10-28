using RiptideNetworking;

namespace GameShared.Messages
{
	public class SpawnRequestMessage : NetworkMessage
	{

		public SpawnRequestMessage() : base(MessageSendMode.reliable, MessageEnums.SpawnRequest)
		{
		}

		protected override void PushVariablesToMessage(Message message)
		{
		}
	}
}