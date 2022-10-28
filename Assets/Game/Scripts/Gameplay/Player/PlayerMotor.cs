using System;
using GameShared;
using GameShared.Data;
using UnityEngine;

namespace Game.Scripts.Gameplay.Player
{
	[RequireComponent(typeof(CharacterController))]
	public class PlayerMotor : MonoBehaviour, ITriggerListener, INetSpawnableObject
	{
		[SerializeField]
		public Camera _camera;
		[SerializeField]
		public GameObject _model;
		[SerializeField]
		public Transform _shotPivot;

		[SerializeField]
		public CharacterController controller;
		[SerializeField]
		private float gravity;
		[SerializeField]
		private float moveSpeed;
		[SerializeField]
		private float jumpSpeed;

		public bool[] Inputs { get; set; }
		private float yVelocity;

		public void HandleInput(PlayerInput input)
		{
			Inputs = input.Inputs;
//			Debug.Log(input.LookDirection);
			SetForwardDirection(input.LookDirection);
		}

		/**
		 * Main move method called for player movement.
		 *
		 * @param newPosition Position to move to.
		 */
 
		public void Move(Vector3 newPosition, Vector3 forward, bool ownedByClient)
		{
			/*controller.enabled = false;
			//controller.transform.position = newPosition;
			controller.enabled = true;*/
			controller.Move(newPosition - controller.transform.position);
			//if(!ownedByClient)
				SetForwardDirection(forward);
			/*var t = transform;
			t.position = newPosition;*/
			//if (Id != NetworkManager.Singleton.Client.Id) // Don't overwrite local player's forward direction to avoid noticeable rotational snapping
			//t.forward = forward;
		}

		private void OnValidate()
		{
			if (controller == null)
			{
				controller = GetComponent<CharacterController>();
			}
		}

		private void Start()
		{
			gravity *= Time.fixedDeltaTime * Time.fixedDeltaTime;
			moveSpeed *= Time.fixedDeltaTime;
			jumpSpeed *= Time.fixedDeltaTime;

			Inputs = new bool[5];
		}

		private void FixedUpdate()
		{
			Vector2 inputDirection = Vector2.zero;
			if (Inputs[0])
				inputDirection.y += 1;

			if (Inputs[1])
				inputDirection.y -= 1;

			if (Inputs[2])
				inputDirection.x -= 1;

			if (Inputs[3])
				inputDirection.x += 1;

			Move(inputDirection);
		}

		public float _lastShotTime;
		[SerializeField]
		private ushort _netId;

		private void Move(Vector2 inputDirection)
		{
			var objTransform = transform;
			Vector3 moveDirection = objTransform.right * inputDirection.x + objTransform.forward * inputDirection.y;
			moveDirection *= moveSpeed;

			if (controller.isGrounded)
			{
				yVelocity = 0f;
				if (Inputs[4])
					yVelocity = jumpSpeed;
			}
			yVelocity += gravity;

			moveDirection.y = yVelocity;
			controller.Move(moveDirection);
			PositionChanged?.Invoke(NetId, objTransform.position, objTransform.rotation);
		}

		public void SetForwardDirection(Vector3 forward)
		{
			//Debug.Log("forward " + forward);
			controller.transform.rotation = Quaternion.Euler(forward);
			/*Debug.Log("final " + controller.transform.rotation);
			Debug.Log("final2 " + Quaternion.Euler(Vector3.forward));*/
		}
		public void OnTriggerEnter(Collider other)
		{
#if SERVER_ONLY
			if (other.CompareTag("bullet"))
			{
				var bullet = other.GetComponent<BulletMotor>();
				Debug.LogError("bullet from: " + bullet.OwnerId + "to " + _netId +"net id" + bullet.NetId);
				Dead?.Invoke(OwnerId);
			}
#endif
		}
		public event Action<ushort> Dead;

		public ushort NetId
		{
			get => _netId;
			set => _netId = value;
		}

		public ushort OwnerId { get; set; }

		public void DeSpawn()
		{
			Destroy(gameObject);
		}

		public event Action<ushort, Vector3, Quaternion> PositionChanged;
	}
}