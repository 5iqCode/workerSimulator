using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTextRestartWindow : MonoBehaviour
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
            text1.text = "Start over?";
            text2.text = "All progress completed (except purchases for the apartment) will be deleted. Are you sure?";
            text3.text = "Cancel";
            text4.text = "Continue";
        }
        Destroy(this);
    }
}
