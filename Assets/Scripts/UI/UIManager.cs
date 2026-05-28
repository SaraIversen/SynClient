using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject _go_deadOverlay;

    [SerializeField] private TextMeshProUGUI _txt_ping;
    [SerializeField] private TextMeshProUGUI _txt_username;

    [SerializeField] private Slider _healthbar;

    private void Awake()
    {
        Singleton.Initialize(ref Instance, this);
    }

    public void UpdatePing(double newPing)
    {
        _txt_ping.text = $"Ping: {newPing} ms";
    }

    public void SetUsername(string username)
    {
        _txt_username.text = username;
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        _healthbar.value = currentHealth / maxHealth;
    }

    public void ShowDeadOverlay(bool show)
    {
        _go_deadOverlay.SetActive(show);
    }
}
