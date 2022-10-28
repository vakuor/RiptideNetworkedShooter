using Game;
using Game.Scripts;
using Game.Scripts.Gameplay.Player;
using Game.Scripts.UI.Screens;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PrefabInstaller", menuName = "Installers/PrefabInstaller")]
public class PrefabInstaller : ScriptableObjectInstaller<PrefabInstaller>
{
	[SerializeField]
	private MainUiCanvas _mainUiCanvasPrefab;
	[SerializeField]
	private LoadingScreen _loadingScreenPrefab;
	[SerializeField]
	private MainMenuScreen _mainMenuScreen;
    [SerializeField]
    private ServerScreen _serverScreen;
	[SerializeField]
	private ClientGameScreen _clientGameScreen;
	[SerializeField]
	private PlayerMotor _playerMotor;
	[SerializeField]
	private BulletMotor _bulletMotor;

	public override void InstallBindings()
	{
		Container.Bind<ScreenHandler>().AsSingle();
		Container.Bind<IPrefabCreator>().To<PrefabCreator>().AsSingle();
		Container.Bind<Component>().FromInstance(_mainUiCanvasPrefab).WhenInjectedInto<IPrefabCreator>();
		Container.Bind<Component>().FromInstance(_loadingScreenPrefab).WhenInjectedInto<IPrefabCreator>();
		Container.Bind<Component>().FromInstance(_mainMenuScreen).WhenInjectedInto<IPrefabCreator>();
		Container.Bind<Component>().FromInstance(_serverScreen).WhenInjectedInto<IPrefabCreator>();
		Container.Bind<Component>().FromInstance(_clientGameScreen).WhenInjectedInto<IPrefabCreator>();
		Container.Bind<Component>().FromInstance(_playerMotor).WhenInjectedInto<IPrefabCreator>();
		Container.Bind<Component>().FromInstance(_bulletMotor).WhenInjectedInto<IPrefabCreator>();
	}
}