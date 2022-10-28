using GameShared.Data;
using RiptideNetworking;
using UnityEngine;

namespace GameShared.Messages
{
	public class InputMessage : NetworkMessage
	{
		public PlayerInput Input { get; private set; }

		public InputMessage(PlayerInput input) : base(MessageSendMode.reliable, MessageEnums.Input)
		{
			Input = input;
		}

		public InputMessage(Message message) : base(MessageSendMode.reliable, MessageEnums.Input)
		{
			Input = new PlayerInput(message.GetBools(), message.GetVector3());
		}

		protected override void PushVariablesToMessage(Message message)
		{
			message.Add(Input.Inputs);
			message.Add(Input.LookDirection);
		}
	}
}