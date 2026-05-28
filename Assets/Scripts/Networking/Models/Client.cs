using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Client
{
    public static int ClientId { get; set; }

    public static ClientTCP TCP { get; private set; }
    public static ClientUDP UDP { get; private set; }

    public static bool IsConnected { get; private set; }


    private string _ip;
    private int _tcpPort;
    private int _udpPort;

    private TcpClient _tcpClient;


    public Client(string ip, int tcpPort, int udpPort) 
    {
        _ip = ip;
        _tcpPort = tcpPort;
        _udpPort = udpPort;

        _tcpClient = new TcpClient();

        PacketRouter.InitializeClientData();
    }  


    public async Task ConnectToServerAsync(CancellationToken cancellationToken)
    {
        Debug.Log("CLIENT: Trying to connect to server...");

        await _tcpClient.ConnectAsync(_ip, _tcpPort);

        TCP = new ClientTCP(_tcpClient); // TCP
        UDP = new ClientUDP(_ip, _udpPort, cancellationToken); // UDP

        IsConnected = true;

        Debug.Log($"CLIENT: Client connected to server at {_ip}:{_tcpPort}");

        while (IsConnected && !cancellationToken.IsCancellationRequested)
        {
            await TCP.TCPReceiveLoop(cancellationToken);
        }
    }

    /// <summary>Disconnects from the server and stops all network traffic.</summary>
    public static void Disconnect()
    {
        if (IsConnected)
        {
            IsConnected = false;

            TCP.Socket.Dispose();
            UDP.Socket.Dispose();

            ThreadManager.ExecuteOnMainThread(() =>
            {
                EnemyManager.Instance.DestroyAllEnemies();
                PlayerManager.Instance.DestroyAllPlayers();

                StartMenu.Instance.EnableStartMenu(true);
            });

            Debug.Log("Disconnected from server.");
        }
    }
}
