using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTextConfirmWindowGoAway : MonoBehaviour
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
            text1.text = "Do you really want to get out?";
            text2.text = "Saving takes place only during sleep, all unsaved data will be deleted.";
            text3.text = "To stay";
            text4.text = "Exit";
        }

        Destroy(this);
    }
}
