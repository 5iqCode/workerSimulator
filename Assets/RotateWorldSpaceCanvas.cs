using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWorldSpaceCanvas : MonoBehaviour
{
    private Camera _mainCamera;
    [SerializeField] private Canvas _canvasStatusBars;
    public GameObject _statusBar;
    private void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        _canvasStatusBars.worldCamera = _mainCamera;
    }
    private void FixedUpdate()
    {
        if (_statusBar.transform.rotation != _mainCamera.transform.rotation)
        {
            _statusBar.transform.rotation = _mainCamera.transform.rotation;
        }
    }
}
