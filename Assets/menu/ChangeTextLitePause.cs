using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTextLitePause : MonoBehaviour
{
    [SerializeField] private TMP_Text text1;
    [SerializeField] private TMP_Text text2;
    [SerializeField] private TMP_Text text3;
    [SerializeField] private TMP_Text text4;
    [SerializeField] private TMP_Text text5;

    private LoadedInfo _loadedInfo;
    void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();

        if (_loadedInfo._Language == "en")
        {
            text1.text = "PAUSE";
            text2.text = "Graphics level";
            text3.text = "Sensitivity";
            text4.text = "Volume";
            text5.text = "Resume";
        }

        Destroy(this);
    }
}
