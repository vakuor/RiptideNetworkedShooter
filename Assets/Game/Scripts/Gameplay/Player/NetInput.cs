#if CLIENT_ONLY
using GameShared.Data;
using GameShared.Messages;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay.Player
{
	public class NetInput : IFixedTickable, ITickable
	{
		private ClientInstance _clientInstance;
		private GameInput _gameInput;
		private PlayerInput _playerInput;
		private PlayerInput _lastSendPlayerInput;

		public NetInput(ClientInstance clientInstance, GameInput gameInput)
		{
			_clientInstance = clientInstance;
			_gameInput = gameInput;
			_playerInput = _gameInput.PlayerCachedInput;
			//_lastSendPlayerInput = new PlayerInput(GameInput.InputSize);
		}

		public void Tick() // 2
		{
			//Debug.Log("2: NetInputTick");
			_playerInput = _gameInput.PlayerCachedInput;
		}

		public void FixedTick() // 3
		{
			//Debug.Log("3: NetInputFixedTick");
			if (_clientInstance.IsConnected/* && !_lastSendPlayerInput.Equals(_playerInput)*/) //TODO: and controlled
			{
				_clientInstance.SendMessage(new InputMessage(_playerInput).GetMessage());
				//_lastSendPlayerInput = _playerInput;
				//Debug.Log("3: MessageSent");
			}
		}
	}
}
#endif