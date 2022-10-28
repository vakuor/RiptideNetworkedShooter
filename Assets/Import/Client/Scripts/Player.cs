
// This file is provided under The MIT License as part of RiptideNetworking.
// Copyright (c) 2021 Tom Weiland
// For additional information please see the included LICENSE.md file or view it on GitHub: https://github.com/tom-weiland/RiptideNetworking/blob/main/LICENSE.md

using RiptideNetworking;
using System.Collections.Generic;
using UnityEngine;

namespace RiptideDemos.RudpTransport.Unity.ExampleClient
{
    public class Player : MonoBehaviour
    {
        public static Dictionary<ushort, Player> List { get; } = new Dictionary<ushort, Player>();

        public ushort Id { get; private set; }
        public string Username { get; private set; }

        public void Move(Vector3 newPosition, Vector3 forward)
        {
            transform.position = newPosition;

            if (Id != NetworkManager.Singleton.Client.Id) // Don't overwrite local player's forward direction to avoid noticeable rotational snapping
                transform.forward = forward;
        }

        private void OnDestroy()
        {
            List.Remove(Id);
        }

        public static void Spawn(ushort id, string username, Vector3 position)
        {
            Player player;
            if (id == NetworkManager.Singleton.Client.Id)
                player = Instantiate(NetworkManager.Singleton.LocalPlayerPrefab, position, Quaternion.identity).GetComponent<Player>();
            else
                player = Instantiate(NetworkManager.Singleton.PlayerPrefab, position, Quaternion.identity).GetComponent<Player>();

            player.name = $"Player {id} ({username})";
            player.Id = id;
            player.Username = username;
            List.Add(player.Id, player);
        }

        #region Messages
        [MessageHandler((ushort)ServerToClientId.spawnPlayer)]
        private static void SpawnPlayer(Message message)
        {
            Spawn(message.GetUShort(), message.GetString(), message.GetVector3());
        }

        [MessageHandler((ushort)ServerToClientId.playerMovement)]
        private static void PlayerMovement(Message message)
        {
            ushort playerId = message.GetUShort();
            if (List.TryGetValue(playerId, out Player player))
                player.Move(message.GetVector3(), message.GetVector3());
        }
        #endregion
    }
}
