namespace GameShared.Messages
{
	public enum MessageEnums : ushort
	{
		Connect = 0,
		ChatMail = 1,
		Ping = 2,
		ServerInfo = 3,
		Spawn = 4,
		SpawnRequest = 5,
		Input = 6,
		DeSpawn = 7,
		Position = 8,
		SpawnBullet = 9,
		DeSpawnBullet = 10,
		PositionSync = 11,
		SpawnNetObject = 12,
	}
}
