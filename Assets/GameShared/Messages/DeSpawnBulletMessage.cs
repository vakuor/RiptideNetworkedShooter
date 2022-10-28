using RiptideNetworking;
using UnityEngine;

namespace GameShared.Messages
{
	public class DeSpawnBulletMessage : NetworkMessage
	{
		public ushort BulletId { get; private set; }

		public DeSpawnBulletMessage(ushort bulletId) : base(MessageSendMode.reliable, MessageEnums.DeSpawnBullet)
		{
			BulletId = bulletId;
		}

		public DeSpawnBulletMessage(Message message) : base(MessageSendMode.reliable, MessageEnums.DeSpawnBullet)
		{
			BulletId = message.GetUShort();
		}

		protected override void PushVariablesToMessage(Message message)
		{
			message.Add(BulletId);
		}
	}
}