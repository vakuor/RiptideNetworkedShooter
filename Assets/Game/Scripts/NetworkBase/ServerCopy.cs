using Game.Scripts.MessageHandlers;
using RiptideNetworking;

namespace Game.Scripts
{
	public class ServerCopy : Server, IMessageSender
	{
		public void SendMessageToAll(Message message, bool shouldRelease = true)
		{
			SendToAll(message, shouldRelease);
		}
	}
}