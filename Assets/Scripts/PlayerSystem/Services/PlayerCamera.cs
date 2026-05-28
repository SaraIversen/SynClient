using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance;

    [SerializeField] private Player _player;
    [SerializeField] private float _sensitivity = 100f;
    [SerializeField] private float _clampAngle = 85f;
    //public Transform target; // ThirdPerson

    private float _verticalRotation;
    private float _horizontalRotation;

    private void Awake()
    {
        Singleton.Initialize(ref Instance, this);
    }

    private void Start()
    {
        _verticalRotation = transform.localEulerAngles.x;
        _horizontalRotation = _player.transform.eulerAngles.y;

        ToggleCursorMode();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursorMode();
        }

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Look();
        }
        Debug.DrawRay(transform.position, transform.forward * 2, Color.red);
    }

    private void Look()
    {
        float _mouseVertical = -Input.GetAxis("Mouse Y");
        float _mouseHorizontal = Input.GetAxis("Mouse X");

        _verticalRotation += _mouseVertical * _sensitivity * Time.deltaTime;
        _horizontalRotation += _mouseHorizontal * _sensitivity * Time.deltaTime;

        _verticalRotation = Mathf.Clamp(_verticalRotation, -_clampAngle, _clampAngle);
        //transform.RotateAround(target.position, transform.right, _mouseVertical * sensitivity * Time.deltaTime); // ThirdPerson
        transform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f); // FirstPerson
        _player.transform.rotation = Quaternion.Euler(0f, _horizontalRotation, 0f);
    }

    public void SetCameraRotation(Vector2 dir)
    {
        _verticalRotation = dir.x;
        _horizontalRotation = dir.y;
    }

    private void ToggleCursorMode()
    {
        Cursor.visible = !Cursor.visible;

        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
