using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTextLoseScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text text1;
    [SerializeField] private TMP_Text text2;
    [SerializeField] private TMP_Text text3;
    [SerializeField] private TMP_Text text4;

    private LoadedInfo _loadedInfo;
    void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();

        if (_loadedInfo._Language == "en")
        {
            text1.text = "THE GAME IS OVER";
            text2.text = "Your emotional state has dropped below 0";
            text3.text = "Continue from the last saved day";
            text4.text = "Go to the main menu";
        }

        Destroy(this);
    }
}
