using UnityEngine;

namespace Game.Scripts
{
	public interface IPrefabCreator
	{
		T Instantiate<T>() where T : Component;
		T InstantiateAt<T>(Transform parent) where T : Component;
	}
}