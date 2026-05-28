using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Id { get; private set; }

    [SerializeField] private GameObject _explosionPrefab;

    public void Initialize(int _id)
    {
        Id = _id;
    }

    public void Explode(Vector3 _position)
    {
        transform.position = _position;
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        ProjectileManager.Instance.DestroyProjectile(Id);
    }
}
