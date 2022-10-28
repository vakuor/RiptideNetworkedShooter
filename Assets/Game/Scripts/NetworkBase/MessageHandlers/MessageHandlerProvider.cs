using Zenject;

namespace Game.Scripts.MessageHandlers
{
	public abstract class MessageHandlerProvider
	{
		protected DiContainer _diContainer;

		public MessageHandlerProvider(DiContainer container)
		{
			_diContainer = container;
		}
	}
}