using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ClientUDP
{
    public UdpClient Socket { get; private set; }
    public IPEndPoint EndPoint;

    private CancellationToken _cancellationToken;

    public ClientUDP(string ip, int udpPort, CancellationToken cancellationToken)
    {
        EndPoint = new IPEndPoint(IPAddress.Parse(ip), udpPort);

        Socket = new UdpClient();

        _cancellationToken = cancellationToken;
    }

    public void Connect()
    {
        Socket.Connect(EndPoint); // Connecting the ip and port to the socket right away, means it does not need to be set with every udpSend.

        using (Packet _packet = new Packet())
        {
            _ = SendDataAsync(_packet);
        }

        _ = UDPReceiveLoop(_cancellationToken); // Begin UDP receive loop

        if (!HeartBeat.IsPinging)
        {
            _ = HeartBeat.PingLoopAsync(_cancellationToken);
        }
    }

    /// <summary>Sends data to the client via UDP.</summary>
    /// <param name="_packet">The packet to send.</param>
    public async Task SendDataAsync(Packet packet)
    {
        try
        {
            packet.InsertInt(ClientManager.Client.ClientId); // Insert the client's ID at the start of the packet
            if (Socket != null)
            {
                await Socket.SendAsync(packet.ToArray(), packet.Length()).ConfigureAwait(false);
            }
        }
        catch (Exception _ex)
        {
            Debug.Log($"Error sending data to server via UDP: {_ex}");
            Disconnect();
        }
    }


    /// <summary>Receives incoming UDP data.</summary>
    private async Task UDPReceiveLoop(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                UdpReceiveResult result = await Socket.ReceiveAsync().ConfigureAwait(false);
                byte[] data = result.Buffer;

                if (data.Length < 4)
                {
                    ClientManager.Client.Disconnect();
                    return;
                }

                HandleData(data);
            }
        }
        catch
        {
            Disconnect();
        }
    }

    /// <summary>Prepares received data to be used by the appropriate packet handler methods.</summary>
    /// <param name="_data">The recieved data.</param>
    private void HandleData(byte[] data)
    {
        using (Packet packet = new Packet(data))
        {
            int _packetLength = packet.ReadInt();
            data = packet.ReadBytes(_packetLength);
        }

        ThreadManager.ExecuteOnMainThread(() =>
        {
            using (Packet packet = new Packet(data))
            {
                int packetId = packet.ReadInt();
                PacketRouter.PacketHandlers[packetId](packet); // Call appropriate method to handle the packet
            }
        });
    }

    /// <summary>Disconnects from the server and cleans up the UDP connection.</summary>
    private void Disconnect()
    {
        EndPoint = null;
        Socket = null;

        ClientManager.Client.Disconnect();
    }
}
