using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject _go_deadOverlay;

    [SerializeField] private TextMeshProUGUI _txt_roundTripTime;
    [SerializeField] private TextMeshProUGUI _txt_username;

    [SerializeField] private Slider _healthbar;

    private void Awake()
    {
        Singleton.Initialize(ref Instance, this);
    }

    public void UpdateLatency(double newRRT)
    {
        _txt_roundTripTime.text = $"{newRRT} ms";
    }

    public void SetUsernameLocal(string username)
    {
        _txt_username.text = username;
    }

    public void SetUsernameRemote(TextMeshProUGUI txt_username, string username)
    {
        txt_username.text = username;
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
