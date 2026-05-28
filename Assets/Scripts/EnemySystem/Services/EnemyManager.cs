using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public static Dictionary<int, Enemy> _enemies = new Dictionary<int, Enemy>();

    [SerializeField] private GameObject _enemyPrefab;

    private void Awake()
    {
        Singleton.Initialize(ref Instance, this);
    }


    public bool GetEnemy(int enemyId, out Enemy enemy)
    {
        return _enemies.TryGetValue(enemyId, out enemy);
    }

    /// <summary>Spawns an enemy.</summary>
    /// <param name="itemId">The enemy's Id.</param>
    /// <param name="position">The enemy's spawn position.</param>
    public void SpawnEnemy(int enemyId, Vector3 position)
    {
        Enemy enemy = Instantiate(_enemyPrefab, position, Quaternion.identity).GetComponent<Enemy>();
        enemy.Initialize(enemyId);
        _enemies.Add(enemyId, enemy);
    }

    public void DestroyEnemy(int enemyId)
    {
        if (!_enemies.TryGetValue(enemyId, out Enemy enemy))
        {
            Debug.Log("Could not find enemy to destroy!");
            return;
        }

        _enemies.Remove(enemyId);
        Destroy(enemy.gameObject);
    }

    public void DestroyAllEnemies()
    {
        foreach (Enemy enemy in _enemies.Values)
        {
            Destroy(enemy.gameObject);
        }

        _enemies.Clear();
    }
}
