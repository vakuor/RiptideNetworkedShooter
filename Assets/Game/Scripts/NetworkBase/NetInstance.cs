using System;
using Game.Scripts.Startup;
using Zenject;

namespace Game.Scripts
{
	public abstract class NetInstance : IInitializable, IFixedTickable, IDisposable
	{
		protected GameModeHandler _gameModeHandler;

		protected abstract void SetGameMode(GameModeHandler gameModeHandler);
		protected abstract void ResetGameMode();
		
		public abstract void Initialize();
		public abstract void FixedTick();
		public abstract void Dispose();
	}
}