#if CLIENT_ONLY
using RiptideNetworking;

namespace Game.Scripts.NetworkBase.MessageHandlers.Server
{
	public abstract class ClientMessageHandler
	{
		public abstract void Handle(object sender, ClientMessageReceivedEventArgs e);
	}
}
#endif