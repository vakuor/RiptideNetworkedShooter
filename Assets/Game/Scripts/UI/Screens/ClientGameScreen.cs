using GameShared;
using GameShared.Messages;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.UI.Screens
{
	public class ClientGameScreen : BaseScreen
	{
		[SerializeField]
		private Button _stopGameBtn;
		[SerializeField]
		private Button _spawnBtn;

#if CLIENT_ONLY
		private ClientInstance _clientInstance;
		private SceneHandler _sceneHandler;
		private ScreenHandler _screenHandler;

		[Inject]
		public void Construct(ClientInstance clientInstance, SceneHandler sceneHandler, ScreenHandler screenHandler)
		{
			_clientInstance = clientInstance;
			_sceneHandler = sceneHandler;
			_screenHandler = screenHandler;
			_stopGameBtn.onClick.RemoveAllListeners();
			_stopGameBtn.onClick.AddListener(StopConnection);
			_spawnBtn.onClick.RemoveAllListeners();
			_spawnBtn.onClick.AddListener(AskForSpawn);
		}

		private void StopConnection()
		{
			_sceneHandler.StopScene();
			_clientInstance.Disconnect();
			Hide();
			_screenHandler.GetScreen<MainMenuScreen>().Show();
		}

		private void AskForSpawn()
		{
			_clientInstance.SendMessage(new SpawnRequestMessage().GetMessage());
		}
#endif
	}
}
