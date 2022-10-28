namespace GameShared
{
	public class User
	{
		public readonly uint Guid;
		public readonly string Name;
		public readonly ushort ClientId;

		public User(uint guid, string name, ushort clientId)
		{
			Guid = guid;
			Name = name;
			ClientId = clientId;
		}
	}
}