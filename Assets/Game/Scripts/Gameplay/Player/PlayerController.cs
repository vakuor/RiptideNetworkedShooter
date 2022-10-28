#if CLIENT_ONLY
using GameShared.Data;
using RiptideNetworking;
using UnityEngine;

namespace Game.Scripts.Gameplay.Player
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField]
		private Transform camTransform;

		private PlayerInput _playerInput;

		private void Awake()
		{
			_playerInput = new PlayerInput(5);
		}

		/*private void Update()
		{
			// Sample inputs every frame and store them until they're sent. This ensures no inputs are missed because they happened between FixedUpdate calls
			if (Input.GetKey(KeyCode.W))
				inputs[0] = true;

			if (Input.GetKey(KeyCode.S))
				inputs[1] = true;

			if (Input.GetKey(KeyCode.A))
				inputs[2] = true;

			if (Input.GetKey(KeyCode.D))
				inputs[3] = true;

			if (Input.GetKey(KeyCode.Space))
				inputs[4] = true;
		}

		private void FixedUpdate()
		{
			SendInput();

			// Reset input booleans
			for (int i = 0; i < inputs.Length; i++)
				inputs[i] = false;
		}

		private void SendInput()
		{
			Message message = Message.Create(MessageSendMode.unreliable, ClientToServerId.playerInput);
			message.Add(inputs, false);
			message.Add(camTransform.forward);
		}*/
	}
}
#endif