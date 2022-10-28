#if SERVER_ONLY
using RiptideNetworking;

namespace Game.Scripts.NetworkBase.MessageHandlers
{
	public abstract class ServerMessageHandler
	{
		public abstract void Handle(object sender, ServerMessageReceivedEventArgs e);
	}
}
#endif