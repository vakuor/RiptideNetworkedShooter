using RiptideNetworking;

namespace Game.Scripts.MessageHandlers
{
	public interface IMessageSender
	{
		public void SendMessageToAll(Message message, bool shouldRelease = true);
	}
}