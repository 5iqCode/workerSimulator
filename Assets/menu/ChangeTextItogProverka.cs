using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTextItogProverka : MonoBehaviour
{
    [SerializeField] private TMP_Text text1;
    [SerializeField] private TMP_Text text2;
    [SerializeField] private TMP_Text text3;
    [SerializeField] private TMP_Text text4;
    [SerializeField] private TMP_Text text5;
    [SerializeField] private TMP_Text text6;
    [SerializeField] private TMP_Text text7;


    private LoadedInfo _loadedInfo;
    void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();

        if (_loadedInfo._Language == "en")
        {
            text1.text = "Interim statistics";
            text2.text = "Initial salary";
            text3.text = "Awards";
            text4.text = "Penalties for the first check";
            text5.text = "Penalties of the current inspection";
            text6.text = "RESULT";
            text7.text = "Continue";
        }

        Destroy(this);
    }
}
