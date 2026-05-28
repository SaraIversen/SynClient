using System.Threading;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public static ClientManager Instance;

    #region Instance Fields
    private Client _client;

    [SerializeField] private string _ip = "127.0.0.1";
    [SerializeField] private int _tcpPort = 5000;
    [SerializeField] private int _udpPort = 5001;

    private CancellationTokenSource _cts;
    private CancellationToken _clientToken;
    #endregion

    #region Methods
    private void Awake()
    {
        Singleton.Initialize(ref Instance, this);
    }

    public void ConnectToServer()
    {
        _cts = new CancellationTokenSource();
        _clientToken = _cts.Token;

        _client = new Client(_ip, _tcpPort, _udpPort);
        _ = _client.ConnectToServerAsync(_clientToken);
    }

    void OnDestroy()
    {
        Debug.Log("CLIENT: Shutting down");

        _cts?.Cancel();
        Client.Disconnect();
        _cts?.Dispose();
    }
    #endregion
}
