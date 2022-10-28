using RiptideNetworking;
using UnityEngine;

namespace GameShared.Messages
{
	public class PositionSyncMessage : NetworkMessage
	{
		public ushort ObjectNetId { get; private set; }
		public Vector3 Position { get; private set; }
		public Vector3 Rotation { get; private set; }

		public PositionSyncMessage(ushort objectNetId, Vector3 position, Vector3 rotation) : base(MessageSendMode.reliable, MessageEnums.PositionSync)
		{
			ObjectNetId = objectNetId;
			Position = position;
			Rotation = rotation;
			//Debug.Log("send position: " + Position);
		}

		public PositionSyncMessage(Message message) : base(MessageSendMode.reliable, MessageEnums.PositionSync)
		{
			ObjectNetId = message.GetUShort();
			Position = message.GetVector3();
			Rotation = message.GetVector3();
			//Debug.Log("got position: " + Position);
		}

		protected override void PushVariablesToMessage(Message message)
		{
			message.Add(ObjectNetId);
			message.Add(Position);
			message.Add(Rotation);
		}
	}
}