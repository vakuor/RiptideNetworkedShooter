using System;
using UnityEngine;

namespace GameShared
{
	public interface INetSpawnableObject
	{
		public ushort NetId { get; set; }
		public ushort OwnerId { get; set; }
		public void DeSpawn();

		public event Action<ushort, Vector3, Quaternion> PositionChanged;
		public event Action<ushort> Dead;
	}
}