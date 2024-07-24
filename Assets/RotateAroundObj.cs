using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RotateAroundObj : MonoBehaviour
{
    Camera _camRotate;
    Transform _targetTransform;

    public float _sensitivity;
    private float _remoteness = -1.5f;

    private Vector3 _previousPosition;

    private bool _canRotate = false;

    public bool _canUseScript = true;

    private float width;
    private float height;

    private LoadedInfo _loadedInfo;

    public Transform _transformMainHero;
    private Transform _transformModelMainHero;
    int layerMask;

    private void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        if (_loadedInfo._isDesktop)
        {
            Destroy(this);
        }

        _targetTransform = GameObject.FindGameObjectWithTag("TargetPosCamera").transform;
        _transformMainHero = GameObject.Find("Player").transform;

        _camRotate = _transformMainHero.GetComponentInChildren<Camera>();

        _transformModelMainHero = _transformMainHero.GetComponentInChildren<Animator>().transform;

        layerMask = 1 << 8;
        layerMask = ~layerMask;

        
        _sensitivity = _loadedInfo.PlayerInfo._sensivity;
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
    }

    int _idTouchForRotate;
    Vector2 _touchForMove;

    int _countTouch =0;
    void Update()
    {
        Debug.Log(_idTouchForRotate);
            if (Input.touchCount != _countTouch)
            {
                _canRotate = false;
                _countTouch = Input.touchCount;
                Debug.Log(_countTouch);
                if (_countTouch > 0)
                {
                    for (int i = 0; i < _countTouch; i++)
                    {
                        Touch touch = Input.GetTouch(i);
                        if (true)
                        {
                            Vector2 pos = touch.position;
                            pos.x = (pos.x - width) / width;
                            pos.y = (pos.y - height) / height;

                            if (pos.x > -0.6f || pos.y > 0f) //мертвая зона для джостика
                            {
                            if(touch.phase == TouchPhase.Began)
                            {
                                _idTouchForRotate = i;
                                _canRotate = true;
                                _previousPosition = _camRotate.ScreenToViewportPoint(touch.position);
                            }
                            }
                            else
                            {
                                _touchForMove = touch.position;
                            }
                        }

                    }
                }

            }
            if (_canRotate == true)
            {
                if (Input.GetTouch(_idTouchForRotate).phase == TouchPhase.Moved)
                {
                    RotateScript(Input.GetTouch(_idTouchForRotate).position);
                }
            }


    }

    public void RotateScript(Vector2 posTouch)
    {
        if (Physics.Linecast(_targetTransform.position, transform.position, out RaycastHit hit, layerMask))
        {
            _remoteness = Mathf.Lerp(_remoteness, -(hit.distance + 0.01f), Time.deltaTime * 10);
        }
        else
        {
            _remoteness = Mathf.Lerp(_remoteness, -1.5f, Time.deltaTime * 5);
        }

        Vector3 _direction = _previousPosition - _camRotate.ScreenToViewportPoint(posTouch);

        if ((_direction.y < -0.1) || (_direction.y > 0.1))
        {
            _direction = _direction / (Mathf.Abs(_direction.y) * 10);
        }

        _camRotate.transform.position = _targetTransform.position;
        float eulerX = _camRotate.transform.eulerAngles.x;
        if (((_direction.y < 0) && (((eulerX >= 0) && (eulerX < 90f)) || ((eulerX > 310)&& (eulerX < 360)))) || ((_direction.y > 0) && ((eulerX <= 75) || (eulerX >= 200)))) //ограничение поворота на 360
        {
            _camRotate.transform.Rotate(new Vector3(1, 0, 0), _direction.y * _sensitivity/2);
        }

        _transformMainHero.transform.Rotate(new Vector3(0, 1, 0), _direction.x * _sensitivity/2 * -1, Space.World);
        _transformModelMainHero.transform.Rotate(new Vector3(0, 1, 0), _direction.x * _sensitivity / 2, Space.World);

        _camRotate.transform.Translate(new Vector3(0, 0, _remoteness));

        _previousPosition = _camRotate.ScreenToViewportPoint(posTouch);

        Vector3 _eulers = _camRotate.transform.eulerAngles;
        if (_eulers.x > 100)
        {

            _camRotate.transform.eulerAngles = new Vector3(5, _eulers.y, _eulers.z);
        }
    }

    private void LateUpdate()
    {
        Vector3 _eulers = _camRotate.transform.localEulerAngles;

        if ((_eulers.y > 1) || (_eulers.y < -1))
        {
            _camRotate.transform.localEulerAngles = new Vector3(_eulers.x, 0, _eulers.z);
        }
    }
}
