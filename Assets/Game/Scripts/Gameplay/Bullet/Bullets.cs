using System.Collections.Generic;
using System.Linq;
using GameShared.Data;
using UnityEngine;

namespace Game.Scripts.Gameplay.Player
{
	public class Bullets
	{
		public readonly Dictionary<ushort, BulletMotor> BulletMotors;
		private readonly IPrefabCreator _prefabCreator;

		public Bullets(IPrefabCreator prefabCreator)
		{
			BulletMotors = new Dictionary<ushort, BulletMotor>();
			_prefabCreator = prefabCreator;
		}

		public void Clear()
		{
			ushort[] list = BulletMotors.Keys.ToArray();
			foreach (ushort motor in list)
			{
				DeSpawn(motor);
			}
		}

		public BulletMotor Spawn(ushort bulletId, Vector3 position, Quaternion rotation)
		{
			BulletMotor motor = _prefabCreator.Instantiate<BulletMotor>();
			motor.transform.SetPositionAndRotation(position, Quaternion.identity);
			BulletMotors.Add(bulletId, motor);
			return motor;
		}

		public bool IsMotorExists(ushort bulletId)
		{
			return BulletMotors.ContainsKey(bulletId);
		}

		public void DeSpawn(ushort bulletId)
		{
			var motorExists = BulletMotors.TryGetValue(bulletId, out BulletMotor motor);
			if (!motorExists)
			{
				return;
			}
			BulletMotors.Remove(bulletId);
		}
	}
}