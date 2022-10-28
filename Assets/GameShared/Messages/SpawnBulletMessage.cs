using RiptideNetworking;
using UnityEngine;

namespace GameShared.Messages
{
	public class SpawnBulletMessage : NetworkMessage
	{
		public ushort OwnerId { get; private set; }
		public ushort BulletId { get; private set; }
		public Vector3 SpawnPosition { get; private set; }
		public Quaternion SpawnRotation { get; private set; }

		public SpawnBulletMessage(ushort ownerId, ushort bulletId, Vector3 spawnPosition, Quaternion spawnRotation) : base(MessageSendMode.reliable, MessageEnums.SpawnBullet)
		{
			OwnerId = ownerId;
			BulletId = bulletId;
			SpawnPosition = spawnPosition;
			SpawnRotation = spawnRotation;
		}

		public SpawnBulletMessage(Message message) : base(MessageSendMode.reliable, MessageEnums.SpawnBullet)
		{
			OwnerId = message.GetUShort();
			BulletId = message.GetUShort();
			SpawnPosition = message.GetVector3();
			SpawnRotation = message.GetQuaternion();
		}

		protected override void PushVariablesToMessage(Message message)
		{
			message.Add(OwnerId);
			message.Add(BulletId);
			message.Add(SpawnPosition);
			message.Add(SpawnRotation);
		}
	}
}