using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance;

    public static Dictionary<int, Projectile> _projectiles = new Dictionary<int, Projectile>();

    [SerializeField] private GameObject _projectilePrefab;

    private void Awake()
    {
        Singleton.Initialize(ref Instance, this);
    }


    public bool GetProjectile(int projectileId, out Projectile projectile)
    {
        return _projectiles.TryGetValue(projectileId, out projectile);
    }

    /// <summary>Spawns a projectile.</summary>
    /// <param name="itemId">The projectile's Id.</param>
    /// <param name="position">The projectile's spawn position.</param>
    public void SpawnProjectile(int projectileId, Vector3 movementDirection, Vector3 position)
    {
        Projectile projectile = Instantiate(_projectilePrefab, position, Quaternion.LookRotation(movementDirection)).GetComponent<Projectile>();
        projectile.Initialize(projectileId);
        _projectiles.Add(projectileId, projectile);
    }

    public void DestroyProjectile(int projectileId)
    {
        if (!_projectiles.TryGetValue(projectileId, out Projectile projectile))
        {
            Debug.Log("Could not find projectile to destroy!");
            return;
        }

        _projectiles.Remove(projectileId);
        Destroy(projectile.gameObject);
    }
}
