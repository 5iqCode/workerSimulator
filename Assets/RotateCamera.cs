using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private bool IsDesktop;

    [SerializeField] private MoveMainHero _moveMainHero;
    [SerializeField] private Transform _targetCamera;

    [SerializeField] private Camera _camera;

    public bool _Contacts=false;
    public Vector3 posContact;
    public float _targetZ = -1.5f;

    private LoadedInfo _loadedInfo;

    int layerMask;
    private void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        layerMask = 1 << 8;
        layerMask = ~layerMask;

        IsDesktop = _loadedInfo._isDesktop;
    }

    private void Update()
    {

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Linecast(_targetCamera.position, transform.position,out RaycastHit hit, layerMask))
        {
            _targetZ = Mathf.Lerp(_targetZ, -(hit.distance + 0.01f), Time.deltaTime * 10);
        }
        else
        {
            _targetZ = Mathf.Lerp(_targetZ, -1.5f, Time.deltaTime * 5);
        }
        if (IsDesktop)
        {
            float MouseY = Input.GetAxis("Mouse Y");
            _camera.transform.localPosition+=(new Vector3(0, -MouseY * _moveMainHero._speedRotation/50 * Time.deltaTime,0) );

            float yPos = _camera.transform.localPosition.y;

            if (yPos > 3)
            {
                yPos = 3;

            } 
            else if (yPos < 0.5)
            {
                yPos = 0.5f;
            }
            if (_targetZ > -0.46f)
            {
                _targetZ = -0.46f;
            }
            _camera.transform.localPosition = (new Vector3(0, yPos, _targetZ));
        }
    }
    private void LateUpdate()
    {
        transform.LookAt(_targetCamera);
    }

}
