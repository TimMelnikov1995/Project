using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Transform m_playerBody;

    float _sensitivity;
    float xRotation = 0;

    void Awake()
    {
        _sensitivity = ServiceLocator.Get<RemoteSettings>().Sensitivity;
    }

    void OnEnable()
    {
        ServiceLocator.Get<InputService>().EOn_LookInput += OnLook;
    }

    void OnDisable()
    {
        ServiceLocator.Get<InputService>().EOn_LookInput -= OnLook;
    }



    void OnLook(Vector2 axis)
    {
        float mouseX = axis.x * _sensitivity * Time.deltaTime;
        float mouseY = axis.y * _sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        m_playerBody.Rotate(Vector3.up * mouseX);
    }



    public void SetSensitivity(float value)
    {
        _sensitivity = value;
    }
}
