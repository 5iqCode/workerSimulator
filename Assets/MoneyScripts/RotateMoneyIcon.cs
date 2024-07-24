using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMoneyIcon : MonoBehaviour
{
    private Camera _mainCamera;

    private void Start()
    {

        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        GetComponent<Canvas>().worldCamera = _mainCamera;
    }
    private void FixedUpdate()
    {
        if (transform.rotation != _mainCamera.transform.rotation)
        {
            transform.rotation = _mainCamera.transform.rotation;
        }
    }

}
