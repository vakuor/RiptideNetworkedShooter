using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Scripts
{
	public class PrefabCreator : IPrefabCreator
	{
		private readonly DiContainer _container;
		private readonly List<Component> _prefabs;

		public PrefabCreator(DiContainer container, List<Component> prefabs)
		{
			_container = container;
			_prefabs = prefabs;
		}

		public T Instantiate<T>() where T : Component
		{
			T prefab = _prefabs.OfType<T>().Single();
			return _container.InstantiatePrefabForComponent<T>(prefab);
		}

		public T InstantiateAt<T>(Transform parent) where T : Component
		{
			T prefab = _prefabs.OfType<T>().Single();
			return _container.InstantiatePrefabForComponent<T>(prefab, parent);
		}
	}
}