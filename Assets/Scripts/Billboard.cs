using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform _cam;
    private bool _cameraFound = false;

    void Start()
    {
        if (_cameraFound) return;

        _cam = PlayerCamera.Instance.transform;
        _cameraFound = true;
    }

    void LateUpdate()
    {
        if (!_cameraFound) return;

        // Forces the gameobject to always face the camera
        transform.LookAt(transform.position + _cam.rotation * Vector3.forward, _cam.rotation * Vector3.up);
    }
}
