using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Id { get; private set; }

    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private GameObject _explosionPrefab;

    public void Initialize(int _id, Vector3 initialForce)
    {
        Id = _id;
        _rigidBody.AddForce(initialForce);
    }

    public void Explode(Vector3 _position)
    {
        transform.position = _position;
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        ProjectileManager.Instance.DestroyProjectile(Id);
    }
}
