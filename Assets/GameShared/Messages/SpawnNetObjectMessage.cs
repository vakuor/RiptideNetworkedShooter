using RiptideNetworking;
using UnityEngine;

namespace GameShared.Messages
{
	public class SpawnNetObjectMessage : NetworkMessage
	{
		public ushort NetObjectId { get; private set; }
		public ushort OwnerId { get; private set; }
		public NetObjectType NetObjectType { get; private set; }
		public Vector3 SpawnPosition { get; private set; }
		public Quaternion SpawnRotation { get; private set; }

		public SpawnNetObjectMessage(ushort netObjectId, ushort ownerId, NetObjectType netObjectType, Vector3 spawnPosition, Quaternion spawnRotation) : base(MessageSendMode.reliable, MessageEnums.SpawnNetObject)
		{
			NetObjectId = netObjectId;
			OwnerId = ownerId;
			NetObjectType = netObjectType;
			SpawnPosition = spawnPosition;
			SpawnRotation = spawnRotation;
		}

		public SpawnNetObjectMessage(Message message) : base(MessageSendMode.reliable, MessageEnums.SpawnNetObject)
		{
			NetObjectId = message.GetUShort();
			OwnerId = message.GetUShort();
			NetObjectType = (NetObjectType)message.GetUShort();
			SpawnPosition = message.GetVector3();
			SpawnRotation = message.GetQuaternion();
		}

		protected override void PushVariablesToMessage(Message message)
		{
			message.Add(NetObjectId);
			message.Add(OwnerId);
			message.Add((ushort)NetObjectType);
			message.Add(SpawnPosition);
			message.Add(SpawnRotation);
		}
	}
}