using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int Id;
    public string Username;

    public float CurrentHealth;
    public float MaxHealth = 100f;

    [SerializeField] private Slider _healthbar;

    public bool IsDead => CurrentHealth <= 0;

    public int itemCount = 0;

    public void Initialize(int id, string username)
    {
        Id = id;
        Username = username;
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
        if (Client.ClientId == Id)
        {
            UIManager.Instance.SetHealth(CurrentHealth, MaxHealth);
        }
        else
        {
            _healthbar.value = CurrentHealth / MaxHealth;
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
        UIManager.Instance.ShowDeadOverlay(true);
    }

    public void Respawn(Vector3 respawnPos, Vector3 respawnRotationEulerAngles)
    {
        transform.position = respawnPos;
        Debug.Log(respawnRotationEulerAngles);
        PlayerCamera.Instance.SetCameraRotation(respawnRotationEulerAngles);

        gameObject.SetActive(true);
        UIManager.Instance.ShowDeadOverlay(false);
        SetHealth(MaxHealth);
    }
}
