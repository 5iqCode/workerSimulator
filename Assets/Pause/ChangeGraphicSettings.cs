using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeGraphicSettings : MonoBehaviour
{
    LoadedInfo _loadedInfo;

    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _text;
    private void Start()
    {
        _loadedInfo = GameObject.FindGameObjectWithTag("LoadedInfo").GetComponent<LoadedInfo>();

        _slider.value = _loadedInfo.PlayerInfo.graphicSettings;

        SetValueSettings();
    }
    public void SetValueSettings()
    {
        int _value = (int)_slider.value;
        QualitySettings.SetQualityLevel(_value, true);

        _text.text = _value.ToString();

        _loadedInfo.PlayerInfo.graphicSettings = _value;

        ChangeTargetFPS(_value);
    }

    private void ChangeTargetFPS(int idGraph)
    {
        switch (idGraph)
        {
            case 5:
                Application.targetFrameRate = 120;
                break;
            case 4:
                Application.targetFrameRate = 120;
                break;
            case 3:
                Application.targetFrameRate = 120;
                break;
            case 2:
                Application.targetFrameRate = 90;
                break;
            case 1:
                Application.targetFrameRate = 60;
                break;
            case 0:
                Application.targetFrameRate = 60;
                break;
        }
     }
}
