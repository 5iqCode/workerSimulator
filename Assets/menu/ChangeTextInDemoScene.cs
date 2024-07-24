using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTextInDemoScene : MonoBehaviour
{
    [SerializeField] private TMP_Text _zpEndDay;
    [SerializeField] private TMP_Text _emocSost;
    [SerializeField] private TMP_Text _sitost;

    private LoadedInfo _loadedInfo;
    void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        if (_loadedInfo._Language == "en")
        {
            _zpEndDay.text = "Salary at the end of the day:";
            _emocSost.text = "Emotional state";
            _sitost.text = "Satiety";
        }
        Destroy(this);
    }
}
