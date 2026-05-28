using System.Collections.Concurrent;
using UnityEngine;

public static class PacketRouter
{
    public delegate void PacketHandler(Packet _packet);
    public static ConcurrentDictionary<int, PacketHandler> PacketHandlers;


    /// <summary>Initializes all necessary client data.</summary>
    public static void InitializeClientData()
    {
        PacketHandlers = new ConcurrentDictionary<int, PacketHandler>()
        {
            [(int)PacketId.welcome] = ClientHandle.Welcome,
            [(int)PacketId.pong] = ClientHandle.Pong,
            [(int)PacketId.spawnPlayer] = ClientHandle.SpawnPlayer,
            [(int)PacketId.playerDisconnected] = ClientHandle.PlayerDisconnected,
            [(int)PacketId.playerPosition] = ClientHandle.PlayerPosition,
            [(int)PacketId.playerRotation] = ClientHandle.PlayerRotation,
            [(int)PacketId.playerHealth] = ClientHandle.PlayerHealth,
            [(int)PacketId.playerRespawned] = ClientHandle.PlayerRespawned,
            [(int)PacketId.spawnProjectile] = ClientHandle.SpawnProjectile,
            [(int)PacketId.projectilePosition] = ClientHandle.ProjectilePosition,
            [(int)PacketId.projectileExploded] = ClientHandle.ProjectileExploded,
            [(int)PacketId.spawnEnemy] = ClientHandle.SpawnEnemy,
            [(int)PacketId.enemyPosition] = ClientHandle.EnemyPosition,
            [(int)PacketId.enemyRotation] = ClientHandle.EnemyRotation,
            [(int)PacketId.enemyHealth] = ClientHandle.EnemyHealth,
            [(int)PacketId.enemyRespawned] = ClientHandle.EnemyRespawned,
        };
        Debug.Log("Initialized client packets.");
    }
}
