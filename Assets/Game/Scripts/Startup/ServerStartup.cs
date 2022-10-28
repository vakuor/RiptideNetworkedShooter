#if SERVER_ONLY
using System.Threading.Tasks;
using Game.Scripts.UI.Screens;
using UnityEngine;
using Zenject;

public class ServerStartup : IInitializable
{
	private ScreenHandler _screenHandler;

	[Inject]
	public void Construct(ScreenHandler screenHandler)
	{
		_screenHandler = screenHandler;
	}

	public async void Initialize()
	{
		Debug.Log("Server started");
		LoadingScreen loadingScreen = _screenHandler.GetScreen<LoadingScreen>();
		loadingScreen.Init(Task.Delay(500),
				() => _screenHandler.GetScreen<ServerScreen>().Show());
		await loadingScreen.Load();
	}
}
#endif