using System;
using System.Collections.Generic;

namespace Game.Scripts.UI.Screens
{
	public class ScreenHandler
	{
		private IPrefabCreator _prefabCreator;
		private MainUiCanvas _mainUiCanvas;
		private Dictionary<Type, BaseScreen> _screenLinks;

		public ScreenHandler(IPrefabCreator prefabCreator)
		{
			_prefabCreator = prefabCreator;
			_mainUiCanvas = _prefabCreator.Instantiate<MainUiCanvas>();
			_screenLinks = new Dictionary<Type, BaseScreen>();
		}

		public T GetScreen<T>() where T : BaseScreen
		{
			var type = typeof(T);
			if (_screenLinks.ContainsKey(type))
			{
				return (T)_screenLinks[type];
			}
			T res = _prefabCreator.InstantiateAt<T>(_mainUiCanvas.transform);
			_screenLinks.Add(type, res);
			return res;
		}
	}
}