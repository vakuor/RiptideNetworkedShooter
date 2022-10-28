using System;
using System.Threading.Tasks;

namespace Game.Scripts.UI.Screens
{
	public class LoadingScreen : BaseScreen
	{
		private Action _onLoadingEnded;
		private Task _job;

		public void Init(Task job, Action onLoadingEnded)
		{
			_job = job;
			_onLoadingEnded = onLoadingEnded;
		}

		public async Task Load()
		{
			Show();
			await _job;
			_onLoadingEnded.Invoke();
			Hide();
		}
	}
}
