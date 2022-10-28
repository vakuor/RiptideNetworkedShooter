using RiptideNetworking;

namespace GameShared.Messages
{
	public class ChatMessage : NetworkMessage
	{
		public string Text { get; private set; }

		public ChatMessage(string text) : base(MessageSendMode.reliable, MessageEnums.ChatMail)
		{
			Text = text;
		}

		public ChatMessage(Message message) : base(MessageSendMode.reliable, MessageEnums.ChatMail)
		{
			Text = message.GetString();
		}

		protected override void PushVariablesToMessage(Message message)
		{
			message.Add(Text);
		}
	}
}