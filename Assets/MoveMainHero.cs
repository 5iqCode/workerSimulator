using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class MoveMainHero : MonoBehaviour
{
    public bool CanMove = true;

    public bool IsDesktop;


    public CharacterController _controller;
    public Transform _transformMainHero;

    public float _speed;

    public float _speedRotation = 4;

    [SerializeField] private float _gravity = -9.81f;

    private Vector3 _velocity;

    private Transform _transformModelMainHero;
    private Animator _animator;

    private AudioSource _audioSource;

    private LoadedInfo _loadedInfo;

    private VariableJoystick _joystick;
    private GameObject _mobileCanvas;
    private void Start()
    {

        _mobileCanvas = GameObject.Find("MobileCanvas");

        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        _speedRotation = _loadedInfo.PlayerInfo._sensivity;

        _animator = GetComponentInChildren<Animator>();

        _transformModelMainHero = _animator.transform;

        _audioSource = GetComponent<AudioSource>();

        Cursor.lockState = CursorLockMode.None;//elfkbnm
        Cursor.visible = true;//elfkbnm

        IsDesktop = _loadedInfo._isDesktop;

        if (!IsDesktop)
        {
            _joystick = _mobileCanvas.GetComponentInChildren<VariableJoystick>();
        }
        else
        {
            Destroy(_mobileCanvas);
            if(SceneManager.GetActiveScene().name != "Home")
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

        }
    }

    private void Update()
    {

        if (CanMove)
        {
            Movement();
        }
        else
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }
        }
        
        Rotation();
    }


    private void Movement()
    {
        float moveHorizontal;
        float moveVertical;
        bool isGrounded = _controller.isGrounded;
        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2;
        }
        if (IsDesktop)
        {
            moveHorizontal = Input.GetAxis("Horizontal");

            moveVertical = Input.GetAxis("Vertical");
        }
        else
        {
            moveVertical = _joystick.Direction.y;
            moveHorizontal = _joystick.Direction.x;
        }


        if (moveHorizontal != 0 || moveVertical != 0)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
            else
            {
                if (_speed == 4)
                {
                    _audioSource.pitch = 0.7f;
                }
                else if (_speed == 3)
                {
                    _audioSource.pitch = 0.5f;
                }
                else 
                {
                    _audioSource.pitch = 0.4f;
                }
            }
            _animator.SetBool("IsMove", true);
        }
        else
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }
            _animator.SetBool("IsMove", false);
        }

        Vector3 _movement = _transformMainHero.right * moveHorizontal + _transformMainHero.forward * moveVertical;

        if ((moveVertical != 0) || (moveHorizontal != 0))
        {
            Vector3 movementDirection = new Vector3(moveHorizontal, 0, moveVertical);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            _transformModelMainHero.localRotation = Quaternion.RotateTowards(_transformModelMainHero.localRotation, toRotation, 5000 * Time.deltaTime);
        }
        if(moveVertical!=0&& moveHorizontal != 0)
        {
            _movement = _movement.normalized;
        }
        _controller.Move(_movement * _speed * Time.deltaTime);


        _velocity.y += _gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void Rotation()
    {
        if (IsDesktop)
        {
            float MouseX = Input.GetAxis("Mouse X");
            _transformMainHero.Rotate(new Vector3(0, MouseX, 0) * _speedRotation * Time.deltaTime);

            _transformModelMainHero.Rotate(new Vector3(0, -MouseX, 0) * _speedRotation * Time.deltaTime);
        }

    }

}
