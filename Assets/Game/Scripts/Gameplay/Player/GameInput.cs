#if CLIENT_ONLY
using GameShared.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay.Player
{
	public class GameInput : ITickable, IFixedTickable //TODO: remove tickable from using outside of map gameplay
	{
		public const int InputSize = 5;
		private readonly Vector3 _rotateValue = new(0, 60, 0);
		private PlayerInput PlayerActualInput { get; }
		public PlayerInput PlayerCachedInput { get; private set; }

		public GameInput()
		{
			PlayerActualInput = new PlayerInput(InputSize);
			PlayerCachedInput = PlayerActualInput;
		}

		public void Tick() // 1
		{
			//Debug.Log("1: GameInputTick");
			// Sample inputs every frame and store them until they're sent. This ensures no inputs are missed because they happened between FixedUpdate calls
			if (Input.GetKey(KeyCode.W))
				PlayerActualInput.Inputs[0] = true;

			if (Input.GetKey(KeyCode.S))
				PlayerActualInput.Inputs[1] = true;

			if (Input.GetKey(KeyCode.A))
				PlayerActualInput.Inputs[2] = true;

			if (Input.GetKey(KeyCode.D))
				PlayerActualInput.Inputs[3] = true;

			if (Input.GetKey(KeyCode.Space))
				PlayerActualInput.Inputs[4] = true;

			if (Input.GetKey(KeyCode.E))
			{
				PlayerActualInput.LookDirection += (_rotateValue * Time.deltaTime);
			}
			
			if (Input.GetKey(KeyCode.Q))
				PlayerActualInput.LookDirection -= (_rotateValue * Time.deltaTime);

			PlayerCachedInput = PlayerActualInput;
		}

		public void FixedTick() // 4
		{
			//Debug.Log("4: GameInputFixedTick");
			float temp = PlayerActualInput.LookDirection.y;
			PlayerActualInput.Reset();
			PlayerActualInput.LookDirection += new Vector3(0, temp, 0);
		}
	}
}
#endif