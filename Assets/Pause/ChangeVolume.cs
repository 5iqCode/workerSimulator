using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    LoadedInfo _loadedInfo;

    [SerializeField] private Slider _slider;

    private void Start()
    {
        _loadedInfo = GameObject.FindGameObjectWithTag("LoadedInfo").GetComponent<LoadedInfo>();

        _slider.value = (float)_loadedInfo.PlayerInfo._volume;

        SetValueVolume();
    }

    public void SetValueVolume()
    {
        float _value = _slider.value;

        _loadedInfo.PlayerInfo._volume = _value;
    }
}
