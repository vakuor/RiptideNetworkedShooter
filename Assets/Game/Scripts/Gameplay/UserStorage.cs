using System;
using System.Collections.Generic;

namespace GameShared
{
	public class UserStorage
	{
		protected readonly Dictionary<ushort, User> _userStorage;
		public event Action<ushort, User> OnAdded;
		public event Action<ushort, User> OnRemoved;
		public UserStorage()
		{
			_userStorage = new Dictionary<ushort, User>();
		}

		public void Add(ushort clientId, User user)
		{
			_userStorage.Add(clientId, user);
			OnAdded?.Invoke(clientId, user);
		}

		public User Remove(ushort clientId)
		{
			var user = _userStorage[clientId];
			_userStorage.Remove(clientId);
			OnRemoved?.Invoke(clientId, user);
			return user;
		}

		public bool ContainsKey(ushort clientId)
		{
			return _userStorage.ContainsKey(clientId);
		}

		public bool ContainsValue(User user)
		{
			return _userStorage.ContainsValue(user);
		}
	}
}