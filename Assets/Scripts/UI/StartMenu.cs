using TMPro;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public static StartMenu Instance;

    [SerializeField] private GameObject _go_startMenu;
    [SerializeField] private GameObject _go_gameOverlay;

    [SerializeField] private TMP_InputField _inputField_username;

    [SerializeField] private TextMeshProUGUI _txt_errorMessage;

    public void Awake()
    {
        Singleton.Initialize(ref Instance, this);

        EnableStartMenu();
    }

    public void EnableStartMenu(bool disconnected = false)
    {
        _go_gameOverlay.SetActive(false);

        _inputField_username.text = string.Empty;
        _inputField_username.interactable = true;

        if (disconnected)
        {
            _txt_errorMessage.text = "Disconnected from server. Please try again";
            _txt_errorMessage.gameObject.SetActive(true);
        }
        else
        {
            _txt_errorMessage.gameObject.SetActive(false);
        }

        _go_startMenu.SetActive(true);
    }

    public void Button_Connect()
    {
        _inputField_username.interactable = false;
        ClientManager.Instance.ConnectToServer();
    }

    public void ConnectedToServer()
    {
        _go_startMenu.SetActive(false);
        _go_gameOverlay.SetActive(true);
    }

    public string GetUsername()
    {
        // Validate username? Characters between 2 and 16?

        string username = _inputField_username.text;
        UIManager.Instance.SetUsernameLocal(username);
        return username;
    }
}
