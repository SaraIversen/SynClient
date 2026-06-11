using UnityEngine;

public static class ClientSend
{
    #region Send Methods
    /// <summary>Sends a packet to the server via TCP.</summary>
    /// <param name="packet">The packet to send to the sever.</param>
    private static void SendTCPData(Packet packet)
    {
        packet.WriteLength();
        _ = ClientManager.Client.TCP.SendDataAsync(packet);
    }

    /// <summary>Sends a packet to the server via UDP.</summary>
    /// <param name="_packet">The packet to send to the sever.</param>
    private static void SendUDPData(Packet packet)
    {
        packet.WriteLength();
        _ = ClientManager.Client.UDP.SendDataAsync(packet);
    }
    #endregion

    #region Packets
    /// <summary>Lets the server know that the welcome message was received.</summary>
    public static void WelcomeReceived()
    {
        using (Packet packet = new Packet((int)PacketId.welcomeReceived))
        {
            packet.Write(ClientManager.Client.ClientId);
            packet.Write(StartMenu.Instance.GetUsername());

            SendTCPData(packet);
        }
    }

    public static void Ping()
    {
        using (Packet packet = new Packet((int)PacketId.ping))
        {
            packet.Write(ClientManager.Client.ClientId);

            SendUDPData(packet);
        }
    }


    /// <summary>Sends player input to the server.</summary>
    /// <param name="_inputs"></param>
    public static void PlayerMovement(bool[] inputs, Player player)
    {
        using (Packet packet = new Packet((int)PacketId.playerMovement))
        {
            packet.Write(inputs.Length);
            foreach (bool _input in inputs)
            {
                packet.Write(_input);
            }
            packet.Write(player.transform.rotation);

            SendUDPData(packet);
        }
    }

    public static void PlayerShoot(Vector3 facing)
    {
        using (Packet packet = new Packet((int)PacketId.playerShoot))
        {
            packet.Write(facing);

            SendUDPData(packet);
        }
    }
    #endregion
}
