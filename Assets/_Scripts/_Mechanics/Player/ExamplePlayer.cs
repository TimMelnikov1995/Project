using UnityEngine;

public class ExamplePlayer : Player
{
    [SerializeField] CharacterController m_characterController;
    [SerializeField] Transform m_groundCheck;
    [SerializeField] float m_groundDist = 0.4f;
    [SerializeField] LayerMask m_groundMask;

    PlayerMovement _movement;

    protected override void OnEnable()
    {
        base.OnEnable();
        InitMovement();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _movement.Dispose();
    }

    void InitMovement()
    {
        float movementSpeed = ServiceLocator.Get<RemoteSettings>().PlayerMovementSpeed;
        float gravity = ServiceLocator.Get<RemoteSettings>().PlayerGravity;
        float jumpHeigh = ServiceLocator.Get<RemoteSettings>().PlayerJumpHeigh;

        PlayerMovement.Settings settings = new PlayerMovement.Settings(
            movementSpeed,
            gravity,
            jumpHeigh,
            m_groundCheck,
            m_groundDist,
            m_groundMask);
        _movement = new PlayerMovement(m_characterController, settings);
    }
}