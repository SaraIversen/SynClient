using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public static Dictionary<int, Player> _players = new Dictionary<int, Player>();

    [SerializeField] private GameObject _localPlayerPrefab;
    [SerializeField] private GameObject _remotePlayerPrefab;

    private void Awake()
    {
        Singleton.Initialize(ref Instance, this);
    }

    public bool GetPlayer(int id, out Player player)
    {
        return _players.TryGetValue(id, out player);
    }

    /// <summary>Spawns a player.</summary>
    /// <param name="id">The player's Id.</param>
    /// <param name="name">The player's name.</param>
    /// <param name="position">The player's starting position.</param>
    /// <param name="rotation">The player's starting rotation.</param>
    public void SpawnPlayer(int id, string username, Vector3 position, Quaternion rotation)
    {
        GameObject player;
        if (id == ClientManager.Client.ClientId)
        {
            player = Instantiate(_localPlayerPrefab, position, rotation);
        }
        else
        {
            player = Instantiate(_remotePlayerPrefab, position, rotation);
        }

        player.GetComponent<Player>().Initialize(id, username);
        _players.Add(id, player.GetComponent<Player>());
    }

    public void DestroyPlayer(int id)
    {
        if (!_players.TryGetValue(id, out Player player))
        {
            Debug.Log("Could not find player to destroy!");
            return;
        }

        _players.Remove(id);
        Destroy(player.gameObject);
    }

    public void DestroyAllPlayers()
    {
        foreach (Player player in _players.Values)
        {
            Destroy(player.gameObject);
        }

        _players.Clear();
    }
}
