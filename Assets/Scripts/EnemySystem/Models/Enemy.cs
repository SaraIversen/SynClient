using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int Id;

    public float CurrentHealth;
    public float MaxHealth = 100f;

    [SerializeField] private Slider _healthbar;

    public bool IsDead => CurrentHealth <= 0;

    public void Initialize(int id)
    {
        Id = id;
        CurrentHealth = MaxHealth;
        UpdateHealthbar();
    }

    public void SetHealth(float health)
    {
        CurrentHealth = health;
        UpdateHealthbar();

        if (IsDead)
        {
            Die();
        }
    }

    public void UpdateHealthbar()
    {
        _healthbar.value = CurrentHealth / MaxHealth;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void Respawn(Vector3 respawnPos, Quaternion respawnRot)
    {
        transform.position = respawnPos;
        transform.rotation = respawnRot;

        gameObject.SetActive(true);
        SetHealth(MaxHealth);
    }
}
