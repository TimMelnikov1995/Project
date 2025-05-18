using System;
using UnityEngine;

public class PlayerMovement : IDisposable
{
    CharacterController _characterController;
    Settings _settings;
    Vector3 _velocity;
    bool _isGrounded;
    float _x;
    float _z;

    public PlayerMovement(CharacterController characterController, Settings settings)
    {
        _characterController = characterController;
        _settings = settings;

        ServiceLocator.Get<UpdateService>().OnEveryFrame += OnUpdate;
        ServiceLocator.Get<InputService>().EOn_MovementInput += Move;
        ServiceLocator.Get<InputService>().EOn_JumpingInput += Jump;
    }

    public void Dispose()
    {
        ServiceLocator.Get<UpdateService>().OnEveryFrame -= OnUpdate;
        ServiceLocator.Get<InputService>().EOn_MovementInput -= Move;
        ServiceLocator.Get<InputService>().EOn_JumpingInput -= Jump;
    }

    void OnUpdate()
    {
        _isGrounded = Physics.CheckSphere(_settings.GroundCheck.position, _settings.GroundDist, _settings.GroundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        Vector3 move = _characterController.transform.right * _x + _characterController.transform.forward * _z;
        _characterController.Move(move * _settings.MovementSpeed * Time.deltaTime);

        _velocity.y += _settings.Gravity * Time.deltaTime;

        _characterController.Move(_velocity * Time.deltaTime);
    }

    void Move(Vector2 axis)
    {
        _x = axis.x;
        _z = axis.y;
    }

    void Jump(bool jump)
    {
        if(jump == false) 
            return;

        if (_isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_settings.JumpHeigh * -2 * _settings.Gravity);
        }
    }



    public class Settings
    {
        public float MovementSpeed {  get; private set; }
        public float Gravity {  get; private set; }
        public float JumpHeigh {  get; private set; }
        public Transform GroundCheck {  get; private set; }
        public float GroundDist {  get; private set; }
        public LayerMask GroundMask {  get; private set; }

        public Settings(float movementSpeed, float gravity, float jumpHeigh, Transform groundCheck, float groundDist, LayerMask groundMask)
        {
            MovementSpeed = movementSpeed;
            Gravity = gravity;
            JumpHeigh = jumpHeigh;
            GroundCheck = groundCheck;
            GroundDist = groundDist;
            GroundMask = groundMask;
        }
    }
}