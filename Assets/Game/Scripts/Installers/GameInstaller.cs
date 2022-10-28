using Game.Scripts;
using Game.Scripts.Gameplay.Player;
using Game.Scripts.NetworkBase.MessageHandlers;
using GameShared;
using RiptideNetworking;
using Zenject;

public class GameInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<SceneHandler>().AsSingle();
		/*Container.Bind<Players>().AsSingle().WhenInjectedInto<SceneHandler>();*/
		Container.Bind<NetObjectSpawner>().AsSingle();
#if SERVER_ONLY
		var server = new ServerCopy();
		Container.BindInterfacesAndSelfTo<ServerCopy>().FromInstance(server);
		Container.BindInterfacesAndSelfTo<ServerInstance>().AsSingle().NonLazy();
		Container.BindInterfacesAndSelfTo<ServerStartup>().AsTransient().NonLazy();
		Container.Bind<ServerMessageHandlerProvider>().AsSingle().WithArguments(GetServerContainer());
#elif CLIENT_ONLY
		Container.BindInterfacesAndSelfTo<ClientInstance>().AsSingle();
		Container.BindInterfacesAndSelfTo<ClientStartup>().AsTransient();
		Container.BindInterfacesAndSelfTo<GameInput>().AsSingle();
		Container.BindInterfacesAndSelfTo<NetInput>().AsSingle();
		BindExecutionOrders();
		Container.Bind<ClientMessageHandlerProvider>().AsSingle().WithArguments(GetClientContainer());
#endif
	}
#if SERVER_ONLY
	private DiContainer GetServerContainer()
	{
		var cont = new DiContainer();
		cont.Bind<SceneHandler>().FromInstance(Container.Resolve<SceneHandler>()).AsSingle();
		cont.Bind<NetObjectSpawner>().FromInstance(Container.Resolve<NetObjectSpawner>()).AsSingle();
		cont.Bind<ServerInstance>().FromInstance(Container.Resolve<ServerInstance>()).AsSingle();
		return cont;
	}
#elif CLIENT_ONLY
	private DiContainer GetClientContainer()
	{
		var cont = new DiContainer();
		cont.Bind<SceneHandler>().FromInstance(Container.Resolve<SceneHandler>()).AsSingle();
		cont.Bind<NetObjectSpawner>().FromInstance(Container.Resolve<NetObjectSpawner>()).AsSingle();
		cont.Bind<ClientInstance>().FromInstance(Container.Resolve<ClientInstance>()).AsSingle();
		return cont;
	}

	private void BindExecutionOrders()
	{
		Container.BindTickableExecutionOrder<GameInput>(-40);
		Container.BindTickableExecutionOrder<NetInput>(-30);
		Container.BindFixedTickableExecutionOrder<NetInput>(-20);
		Container.BindFixedTickableExecutionOrder<GameInput>(-10);
	}
#endif
}