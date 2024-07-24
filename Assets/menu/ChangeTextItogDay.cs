using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTextItogDay : MonoBehaviour
{
    [SerializeField] private TMP_Text text1;
    [SerializeField] private TMP_Text text2;
    [SerializeField] private TMP_Text text3;
    [SerializeField] private TMP_Text text4;
    [SerializeField] private TMP_Text text5;
    [SerializeField] private TMP_Text text6;
    [SerializeField] private TMP_Text text7;
    [SerializeField] private TMP_Text text8;
    [SerializeField] private TMP_Text text9;
    [SerializeField] private TMP_Text text10;
    [SerializeField] private TMP_Text text11;
    [SerializeField] private TMP_Text text12;
    [SerializeField] private TMP_Text text13;

    private LoadedInfo _loadedInfo;
    void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();

        if (_loadedInfo._Language == "en")
        {
            text1.text = "The result of the day";
            text2.text = "Starting money";
            text3.text = "Salary for the current day";
            text4.text = "Initial salary";
            text5.text = "Awards";
            text6.text = "Penalties for the first check";
            text7.text = "Penalties of the second check";
            text8.text = "Final salary";
            text9.text = "Daily expenses";
            text10.text = "Food expenses";
            text11.text = "Expenses in the online store";
            text12.text = "Current money";
            text13.text = "Continue";
        }

        Destroy(this);
    }
}
