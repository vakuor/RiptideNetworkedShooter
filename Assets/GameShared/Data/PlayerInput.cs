using UnityEngine;

namespace GameShared.Data
{
	public class PlayerInput
	{
		public bool[] Inputs { get; set; }
		public Vector3 LookDirection { get; set; }

		public PlayerInput(int inputsSize)
		{
			Inputs = new bool[inputsSize];
			LookDirection = Vector3.zero;
		}

		public PlayerInput(bool[] inputs, Vector3 lookDirection)
		{
			Inputs = inputs;
			LookDirection = lookDirection;
		}

		public override bool Equals(object obj)
		{
			PlayerInput other = (PlayerInput)obj;
			if (other == null)
			{
				return base.Equals(obj);
			}
			for (int i = 0; i < Inputs.Length; i++)
			{
				if (other.Inputs[i] != Inputs[i])
				{
					return false;
				}
			}
			return true;
		}

		public void Reset()
		{
			for (int i = 0; i < Inputs.Length; i++)
			{
				Inputs[i] = false;
			}
			LookDirection = Vector3.zero;
		}
	}
}