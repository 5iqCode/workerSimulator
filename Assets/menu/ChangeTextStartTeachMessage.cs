using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTextStartTeachMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text teach1;
    [SerializeField] private TMP_Text teach2;
    [SerializeField] private TMP_Text teach3;
    [SerializeField] private TMP_Text teach4;
    [SerializeField] private TMP_Text teach5;
    [SerializeField] private TMP_Text teach6;
    [SerializeField] private TMP_Text close;
    [SerializeField] private TMP_Text lkm;
    [SerializeField] private TMP_Text deistvie;

    private LoadedInfo _loadedInfo;
    void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        if (_loadedInfo._Language == "en")
        {
            teach1.text = "- Pause";
            teach2.text = "- Moving";
            teach3.text = "- Change of the active item in the inventory";
            teach4.text = "- Blow";
            teach5.text = " - Action";
            teach6.text = "* The controls on the mobile device are located on the screen.";
            close.text = "Close";
            lkm.text = "LKM";
            deistvie.text = "E";
        }
        Destroy(this);
    }
}
