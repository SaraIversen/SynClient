using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int Id;
    public string Username;

    public float CurrentHealth;
    public float MaxHealth = 100f;

    [Space(10)]

    [SerializeField] private TextMeshProUGUI _txt_username;
    [SerializeField] private Slider _healthbar;

    public bool IsDead => CurrentHealth <= 0;


    public void Initialize(int id, string username)
    {
        Id = id;
        Username = username;
        CurrentHealth = MaxHealth;
        UpdateHealthbar();

        if (ClientManager.Client.ClientId != Id)
        {
            UIManager.Instance.SetUsernameRemote(_txt_username, username);
        }
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
        if (ClientManager.Client.ClientId == Id)
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

        if (ClientManager.Client.ClientId == Id)
        {
            UIManager.Instance.ShowDeadOverlay(true);
        }
    }

    public void Respawn(Vector3 respawnPos, Vector3 respawnRotationEulerAngles)
    {
        transform.position = respawnPos;

        if (ClientManager.Client.ClientId == Id)
        {
            PlayerCamera.Instance.SetCameraRotation(respawnRotationEulerAngles);
            UIManager.Instance.ShowDeadOverlay(false);
        }

        gameObject.SetActive(true);
        SetHealth(MaxHealth);
    }
}
