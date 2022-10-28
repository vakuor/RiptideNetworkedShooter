﻿
// This file is provided under The MIT License as part of RiptideNetworking.
// Copyright (c) 2021 Tom Weiland
// For additional information please see the included LICENSE.md file or view it on GitHub: https://github.com/tom-weiland/RiptideNetworking/blob/main/LICENSE.md

using RiptideNetworking;
using UnityEngine;

namespace RiptideDemos.RudpTransport.Unity.ExampleClient
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform camTransform;
        private bool[] inputs;

        private void Start()
        {
            inputs = new bool[5];
        }

        private void Update()
        {
            // Sample inputs every frame and store them until they're sent. This ensures no inputs are missed because they happened between FixedUpdate calls
            if (Input.GetKey(KeyCode.W))
                inputs[0] = true;

            if (Input.GetKey(KeyCode.S))
                inputs[1] = true;

            if (Input.GetKey(KeyCode.A))
                inputs[2] = true;

            if (Input.GetKey(KeyCode.D))
                inputs[3] = true;

            if (Input.GetKey(KeyCode.Space))
                inputs[4] = true;
        }

        private void FixedUpdate()
        {
            SendInput();

            // Reset input booleans
            for (int i = 0; i < inputs.Length; i++)
                inputs[i] = false;
        }

        #region Messages
        private void SendInput()
        {
            Message message = Message.Create(MessageSendMode.unreliable, ClientToServerId.playerInput);
            message.Add(inputs, false);
            message.Add(camTransform.forward);
            NetworkManager.Singleton.Client.Send(message);
        }
        #endregion
    }
}
