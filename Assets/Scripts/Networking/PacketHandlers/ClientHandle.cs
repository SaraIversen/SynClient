using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class ClientHandle
{
    public static void Welcome(Packet packet)
    {
        string msg = packet.ReadString();
        ClientManager.Client.ClientId = packet.ReadInt();

        Debug.Log($"Message from server: {msg}");

        StartMenu.Instance.ConnectedToServer();
        ClientSend.WelcomeReceived();
        ClientManager.Client.UDP.Connect(); // Now that we have the client's id, connect UDP
    }

    public static void Pong(Packet packet)
    {
        long now = Stopwatch.GetTimestamp();
        double rrtMs = (now - HeartBeat.LastTimestamp) * 1000 / Stopwatch.Frequency;

        Debug.Log($"Latency: {rrtMs} ms");

        UIManager.Instance.UpdateLatency(rrtMs);
    }

    public static void SpawnPlayer(Packet packet)
    {
        int id = packet.ReadInt();
        string username = packet.ReadString();
        Vector3 position = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();

        PlayerManager.Instance.SpawnPlayer(id, username, position, rotation);
    }

    public static void PlayerDisconnected(Packet packet)
    {
        int id = packet.ReadInt();

        PlayerManager.Instance.DestroyPlayer(id);
    }

    public static void PlayerPosition(Packet packet)
    {
        int id = packet.ReadInt();
        Vector3 position = packet.ReadVector3();

        if (!PlayerManager.Instance.GetPlayer(id, out Player player)) return;
        player.transform.position = position;
    }

    public static void PlayerRotation(Packet packet)
    {
        int id = packet.ReadInt();
        Quaternion rotation = packet.ReadQuaternion();

        if (!PlayerManager.Instance.GetPlayer(id, out Player player)) return;
        player.transform.rotation = rotation;
    }

    public static void PlayerHealth(Packet packet)
    {
        int id = packet.ReadInt();
        float health = packet.ReadFloat();

        if (!PlayerManager.Instance.GetPlayer(id, out Player player)) return;
        player.SetHealth(health);
    }

    public static void PlayerRespawned(Packet packet)
    {
        int id = packet.ReadInt();
        Vector3 position = packet.ReadVector3();
        Vector3 rotationEulerAngles = packet.ReadVector3();

        if (!PlayerManager.Instance.GetPlayer(id, out Player player)) return;
        player.Respawn(position, rotationEulerAngles);
    }

    public static void SpawnProjectile(Packet packet)
    {
        int projectileId = packet.ReadInt();
        Vector3 position = packet.ReadVector3();
        Vector3 movementDirection = packet.ReadVector3();
        Vector3 initialForce = packet.ReadVector3();
        int thrownByPlayer = packet.ReadInt();

        ProjectileManager.Instance.SpawnProjectile(projectileId, movementDirection, initialForce, position);
    }

    public static void ProjectilePosition(Packet packet)
    {
        int projectileId = packet.ReadInt();
        Vector3 position = packet.ReadVector3();

        if (ProjectileManager.Instance.GetProjectile(projectileId, out Projectile projectile))
        {
            projectile.transform.position = position;
        }
    }

    public static void ProjectileExploded(Packet packet)
    {
        int projectileId = packet.ReadInt();
        Vector3 position = packet.ReadVector3();

        if (ProjectileManager.Instance.GetProjectile(projectileId, out Projectile projectile))
        {
            projectile.Explode(position);
        }
    }

    public static void SpawnEnemy(Packet packet)
    {
        int enemyId = packet.ReadInt();
        Vector3 position = packet.ReadVector3();

        EnemyManager.Instance.SpawnEnemy(enemyId, position);
    }

    public static void EnemyPosition(Packet packet)
    {
        int enemyId = packet.ReadInt();
        Vector3 position = packet.ReadVector3();

        if (EnemyManager.Instance.GetEnemy(enemyId, out Enemy enemy))
        {
            enemy.transform.position = position;
        }
    }

    public static void EnemyRotation(Packet packet)
    {
        int enemyId = packet.ReadInt();
        Quaternion rotation = packet.ReadQuaternion();

        if (EnemyManager.Instance.GetEnemy(enemyId, out Enemy enemy))
        {
            enemy.transform.rotation = rotation;
        }
    }

    public static void EnemyHealth(Packet packet)
    {
        int enemyId = packet.ReadInt();
        float health = packet.ReadFloat();

        if (EnemyManager.Instance.GetEnemy(enemyId, out Enemy enemy))
        {
            enemy.SetHealth(health);
        }
    }

    public static void EnemyRespawned(Packet packet)
    {
        int enemyId = packet.ReadInt();
        Vector3 position = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();

        if (EnemyManager.Instance.GetEnemy(enemyId, out Enemy enemy))
        {
            enemy.Respawn(position, rotation);
        }
    }
}
