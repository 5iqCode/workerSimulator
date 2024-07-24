using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSensivity : MonoBehaviour
{
    LoadedInfo _loadedInfo;

    [SerializeField] private Slider _slider;

    private MoveMainHero _player;
    private RotateAroundObj _rotateScript;
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<MoveMainHero>();
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();

        _slider.value = _loadedInfo.PlayerInfo._sensivity;

        SetValueSensivity();
        if (_loadedInfo._isDesktop == false)
        {
            _rotateScript = GameObject.Find("Player").GetComponentInChildren<RotateAroundObj>();

            Debug.Log(_rotateScript._sensitivity);
        }

    }

    public void SetValueSensivity()
    {
        if (_loadedInfo._isDesktop == false)
        {
            if (_rotateScript == null)
            {
                _rotateScript = GameObject.Find("Player").GetComponentInChildren<RotateAroundObj>();
            }
        }

        int _value = (int)_slider.value;

        _loadedInfo.PlayerInfo._sensivity = _value;

        _player._speedRotation = _value;

        if(_loadedInfo._isDesktop == false)
        {
            _rotateScript._sensitivity = (float)_value;
        }
    }
}