using RiptideNetworking;

namespace GameShared.Messages
{
	public class PingMessage : NetworkMessage
	{
		private readonly ushort _id;

		public PingMessage(ushort id) : base(MessageSendMode.unreliable, MessageEnums.Ping)
		{
			_id = id;
		}

		protected override void PushVariablesToMessage(Message message)
		{
			message.Add(_id);
		}
	}
}