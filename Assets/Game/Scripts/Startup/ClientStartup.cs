#if CLIENT_ONLY
using System.Threading.Tasks;
using Game.Scripts;
using Game.Scripts.UI.Screens;
using UnityEngine;
using Zenject;

/**
 * This is start class for client-side app
 */

public class ClientStartup : IInitializable
{
	private ScreenHandler _screenHandler;

/**
 * Construct method called by DI.
 *
 * @param screenHandler Screen handler object should be injected by DI.
 */
 
	[Inject]
	public void Construct(ScreenHandler screenHandler)
	{
		_screenHandler = screenHandler;
	}

	public async void Initialize()
	{
		Debug.Log("Client started");
		LoadingScreen loadingScreen = _screenHandler.GetScreen<LoadingScreen>();
		loadingScreen.Init(Task.Delay(1000),
				() => _screenHandler.GetScreen<MainMenuScreen>());
		await loadingScreen.Load();
	}
}
#endif