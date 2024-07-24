using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class changeTextBlockTovar : MonoBehaviour
{
    [SerializeField] private TMP_Text _emocSost;
    [SerializeField] private TMP_Text _sitost;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _buy;

    private LoadedInfo _loadedInfo;
    void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        if (_loadedInfo._Language == "en")
        {
            _emocSost.text = "The level of happiness";
            _sitost.text = "Satiety level";
            _price.text = "Price";
            _buy.text = "Buy";
        }
        Destroy(this);
    }
}
