using System;
using RiptideNetworking;

namespace GameShared.Messages
{
	public abstract class NetworkMessage
	{
		private MessageSendMode _sendMode;
		private Enum _id;

		protected NetworkMessage(MessageSendMode sendMode, Enum id)
		{
			_sendMode = sendMode;
			_id = id;
		}

		public virtual Message GetMessage()
		{
			var message = Message.Create(_sendMode, _id);
			PushVariablesToMessage(message);
			return message;
		}

		protected abstract void PushVariablesToMessage(Message message);
	}
}