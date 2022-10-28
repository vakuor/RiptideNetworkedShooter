#if SERVER_ONLY
using GameShared;
using Zenject;
#endif
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Screens
{
	public class ServerScreen : BaseScreen
	{
		[SerializeField]
		private ushort _maxPlayerCount = DefaultSettings.DefaultMaxClientCount;
		[SerializeField]
		private TMP_InputField _portField;
		[SerializeField]
		private Button _connectBtn;

#if SERVER_ONLY
		private ServerInstance _serverInstance;
		private SceneHandler _sceneHandler;

		[Inject]
		public void Construct(ServerInstance serverInstance, SceneHandler sceneHandler)
		{
			_serverInstance = serverInstance;
			_sceneHandler = sceneHandler;
			_connectBtn.onClick.AddListener(OpenConnection);
		}

		private void OpenConnection()
		{
			if (string.IsNullOrWhiteSpace(_portField.text))
			{
				_portField.text = DefaultSettings.DefaultPort.ToString();
			}
			_sceneHandler.StartScene("DefaultMap");
			_serverInstance.Launch(ushort.Parse(_portField.text), _maxPlayerCount);
			Hide();
		}
#endif
	}
}