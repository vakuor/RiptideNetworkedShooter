using GameShared;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.UI.Screens
{
	public class MainMenuScreen : BaseScreen
	{
		[SerializeField]
		private TMP_InputField _ipField;
		[SerializeField]
		private TMP_InputField _portField;
		[SerializeField]
		private Button _connectBtn;
#if CLIENT_ONLY
		private ClientInstance _clientInstance;
		private ScreenHandler _screenHandler;
		private SceneHandler _sceneHandler;

		[Inject]
		public void Construct(ClientInstance clientInstance, ScreenHandler screenHandler, SceneHandler sceneHandler)
		{
			_clientInstance = clientInstance;
			_screenHandler = screenHandler;
			_sceneHandler = sceneHandler;
			_connectBtn.onClick.RemoveAllListeners();
			_connectBtn.onClick.AddListener(TryConnect);
		}

		private void TryConnect()
		{
			if (string.IsNullOrWhiteSpace(_ipField.text))
			{
				_ipField.text = DefaultSettings.DefaultIp;
			}
			if (string.IsNullOrWhiteSpace(_portField.text))
			{
				_portField.text = DefaultSettings.DefaultPort.ToString();
			}
			_clientInstance.Connect(_ipField.text, ushort.Parse(_portField.text));
			_screenHandler.GetScreen<ClientGameScreen>().Show();
			Hide();
		}
#endif
	}
}