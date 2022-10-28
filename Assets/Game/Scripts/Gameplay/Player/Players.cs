using System;
using System.Collections.Generic;
using System.Linq;
using GameShared.Data;
using UnityEngine;

namespace Game.Scripts.Gameplay.Player
{
	public class Players
	{
		public readonly Dictionary<ushort, PlayerMotor> PlayerMotors;
		private readonly NetObjectSpawner _netObjectSpawner;

		public static Players instance;

		public Players(NetObjectSpawner netObjectSpawner)
		{
			PlayerMotors = new Dictionary<ushort, PlayerMotor>();
			_netObjectSpawner = netObjectSpawner;
			instance = this;
		}

		public void Clear()
		{
			var list = PlayerMotors.Keys.ToArray();
			for (int i = 0; i < list.Length; i++)
			{
				DeSpawn(list[i]);
			}
		}

		public void HandleInput(ushort playerId, PlayerInput playerInput)
		{
			//if (PlayerMotors.ContainsKey(playerId)) //TODO: костыль, remove
			{
//				Debug.Log(playerInput.LookDirection);
				PlayerMotors[playerId].HandleInput(playerInput);
			}
		}
		
		public bool IsMotorExists(ushort playerId)
		{
			return PlayerMotors.ContainsKey(playerId);
		}

		public PlayerMotor Spawn(ushort playerId, ushort netObjectId, Vector3 position, Quaternion rotation)
		{
			PlayerMotor motor = _netObjectSpawner.Spawn<PlayerMotor>(netObjectId, playerId);
			motor.controller.enabled = false;
			motor.controller.transform.SetPositionAndRotation(position, rotation);
			motor.controller.enabled = true;
			
			Debug.LogError("playerid to spawn: " + playerId);
			PlayerMotors.Add(playerId, motor);
			return motor;
		}

		public void DeSpawn(ushort playerId)
		{
			Debug.LogError("playerid to despawn: " + playerId);
			bool exists = PlayerMotors.Remove(playerId, out PlayerMotor motor);
			if (!exists)
			{
				throw new Exception("PlayerMotor not exists");
			}
			_netObjectSpawner.DeSpawn(motor.NetId);
		}
	}
}