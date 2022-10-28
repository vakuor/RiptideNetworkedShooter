using UnityEngine;

namespace GameShared
{
	public class MapHandler
	{
		private readonly string _sceneName;
		private readonly ushort _roundsCount;
		private ushort _currentRound;

		public MapHandler(string sceneName, ushort roundsCount = 4)
		{
			_sceneName = sceneName;
			_roundsCount = roundsCount;
		}

		public void Start()
		{
			Debug.Log("Start of game");
			_currentRound = 1;
		}

		public void NextRound()
		{
			Debug.Log("NextRound call");
			if (_currentRound + 1 > _roundsCount)
			{
				End();
				return;
			}
			_currentRound++;
		}

		public void End()
		{
			Debug.Log("End of game");
		}
	}
}