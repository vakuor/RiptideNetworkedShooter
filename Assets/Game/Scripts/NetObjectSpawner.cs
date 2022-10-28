using System;
using System.Collections.Generic;
using Game.Scripts.Gameplay.Player;
using Game.Scripts.MessageHandlers;
using GameShared;
using GameShared.Messages;
using UnityEngine;

namespace Game.Scripts
{
	public class NetObjectSpawner
	{
		public ushort SpawnedCount { get; private set; }

		private readonly IPrefabCreator _prefabCreator;
		private readonly Dictionary<ushort, INetSpawnableObject> _spawnedObjects;
		public Dictionary<ushort, INetSpawnableObject> SpawnableObjects => _spawnedObjects;

#if CLIENT_ONLY
		public NetObjectSpawner(IPrefabCreator prefabCreator)
		{
			_prefabCreator = prefabCreator;
			_spawnedObjects = new Dictionary<ushort, INetSpawnableObject>();
		}
#elif SERVER_ONLY
		private readonly IMessageSender _messageSender;
		public NetObjectSpawner(IPrefabCreator prefabCreator, IMessageSender messageSender)
		{
			_prefabCreator = prefabCreator;
			_messageSender = messageSender;
			_spawnedObjects = new Dictionary<ushort, INetSpawnableObject>();
		}
#endif

		public T Spawn<T>(ushort netId, ushort ownerId = 0) where T : Component, INetSpawnableObject
		{
			T obj = _prefabCreator.Instantiate<T>();
			return AddToDictionary(netId, ownerId, obj);
		}

		public T SpawnAt<T>(Transform t, ushort netId, ushort ownerId = 0) where T : Component, INetSpawnableObject
		{
			T obj = _prefabCreator.InstantiateAt<T>(t);
			return AddToDictionary(netId, ownerId, obj);
		}

		public void DeSpawn(ushort netId)
		{
			bool objExists = _spawnedObjects.Remove(netId, out INetSpawnableObject value);
			if (!objExists)
			{
				throw new Exception("DeSpawn target not exists!");
			}
			
#if SERVER_ONLY
			value.PositionChanged -= PositionSync;
			//value.Dead -= DeadSync;
#endif
			value.DeSpawn();
		}

		private T AddToDictionary<T>(ushort netId, ushort ownerId, T obj) where T : Component, INetSpawnableObject
		{
			bool success = _spawnedObjects.TryAdd(netId, obj);
			if (!success)
			{
				throw new Exception("Unsuccessful netObject spawn");
			}
			obj.NetId = netId;
			obj.OwnerId = ownerId;
			SpawnedCount++;
#if SERVER_ONLY
			Debug.Log("add to sync: " + obj.NetId);
			obj.PositionChanged += PositionSync;
			obj.Dead += delegate(ushort @ushort) { DeadSync(@ushort); };
#endif
			return obj;
		}
		
#if SERVER_ONLY
		private void DeadSync(ushort arg1)
		{
			/*if (_sceneHandler.Players.IsMotorExists(arg1))
			{
				_sceneHandler.Players.DeSpawn(arg1);*/
			Debug.Log("dead "+arg1);
			Players.instance.DeSpawn(arg1);
				_messageSender.SendMessageToAll(new DeSpawnMessage(arg1).GetMessage());
			//}
		}
		private void PositionSync(ushort arg1, Vector3 arg2, Quaternion arg3)
		{
			_messageSender.SendMessageToAll(new PositionSyncMessage(arg1,
							arg2,
							arg3.eulerAngles).
					GetMessage());
		}
#endif
	}
}