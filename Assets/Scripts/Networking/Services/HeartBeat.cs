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

        try
        {
            while (Client.IsConnected && !token.IsCancellationRequested)
            {
                LastTimestamp = Stopwatch.GetTimestamp();
                ClientSend.Ping();

                await Task.Delay(3000, token); // Every 3 second.

                LastTimestamp = 0;
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("CLIENT: Ping loop cancelled");
        }
    }
}
