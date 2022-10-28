using Game.Scripts;
using Game.Scripts.Gameplay.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameShared
{
	public class SceneHandler
	{
		public Players Players { get; private set; }
		public Bullets Bullets { get; private set; }
		public ushort NetObjectSpawnedCount => _netObjectSpawner.SpawnedCount;
		
		private string _loadedScene;
		private readonly NetObjectSpawner _netObjectSpawner;
		
		public SceneHandler(NetObjectSpawner netObjectSpawner)
		{
			_netObjectSpawner = netObjectSpawner;
		}

		public void StartScene(string sceneName)
		{
			if (!string.IsNullOrWhiteSpace(_loadedScene))
			{
				StopScene();
			}
			SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
			_loadedScene = sceneName;
			Players = new Players(_netObjectSpawner);
		}

		public void StopScene()
		{
			if (string.IsNullOrWhiteSpace(_loadedScene))
			{
				Debug.LogWarning("You are trying to stop scene but there is no loaded scene!");
				return;
			}
			Players.Clear();
			SceneManager.UnloadScene(_loadedScene);
			_loadedScene = null;
		}
	}
}