using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Debug = UnityEngine.Debug;

public static class HeartBeat
{
    public static bool IsPinging = false;
    public static long LastTimestamp = 0;

    public static async Task PingLoopAsync(CancellationToken token)
    {
        IsPinging = true;
        Debug.Log("Ping loop started");

        try
        {
            while (ClientManager.Client.IsConnected && !token.IsCancellationRequested)
            {
                LastTimestamp = Stopwatch.GetTimestamp();
                ClientSend.Ping();

                await Task.Delay(5000, token).ConfigureAwait(false); // Every 5 second.

                LastTimestamp = 0;
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("CLIENT: Ping loop cancelled");
        }
    }
}
