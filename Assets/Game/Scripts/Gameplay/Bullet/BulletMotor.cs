using System;
using GameShared;
using UnityEngine;

namespace Game.Scripts.Gameplay.Player
{
	public class BulletMotor : MonoBehaviour, INetSpawnableObject
	{
		[SerializeField]
		private float _bulletForce = 20f;
		[SerializeField]
		private Rigidbody _rigidbody;
		[SerializeField]
		private Transform _transform;
		public Transform Transform => _transform;

		public void AddForce(float force)
		{
			_rigidbody.AddForce(transform.forward * force);
		}

		private void Start()
		{
			Destroy(gameObject, 5f);
		}

		public void Push()
		{
			AddForce(_bulletForce);
		}

		private void OnReset()
		{
			_rigidbody = GetComponent<Rigidbody>();
			if (_rigidbody == null)
			{
				Debug.LogError("No rigidbody on bullet motor!");
			}
		}

		public ushort NetId { get; set; }
		public ushort OwnerId { get; set; }
		public void DeSpawn()
		{
			Destroy(gameObject);
		}

		public event Action<ushort, Vector3, Quaternion> PositionChanged;
		public event Action<ushort> Dead;
	}
}