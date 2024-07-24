using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraMenu : MonoBehaviour
{
    [SerializeField] private float _speedRotateCamera;
    [SerializeField] private float _speedRotateLight;


     private Transform _cameraTransform;

    private int _directionCamera = -1;
    void Start()
    {
        _cameraTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        float _deltaTime = Time.deltaTime;


        if (_directionCamera == -1)
        {
            _cameraTransform.Rotate(0,_directionCamera * _speedRotateCamera * _deltaTime, 0);

            if (_cameraTransform.eulerAngles.y < 200)
            {
                _directionCamera = 1;
            }
        }
        else
        {
            _cameraTransform.Rotate(0, _directionCamera * _speedRotateCamera * _deltaTime, 0);

            if (_cameraTransform.eulerAngles.y > 330)
            {
                _directionCamera = -1;
            }
        }

    }
}
