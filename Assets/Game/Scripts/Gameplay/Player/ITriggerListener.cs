using UnityEngine;

namespace Game.Scripts.Gameplay.Player
{
	public interface ITriggerListener
	{
		public void OnTriggerEnter(Collider other);
	}
}