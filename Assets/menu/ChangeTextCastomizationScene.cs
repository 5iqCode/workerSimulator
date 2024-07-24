using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTextCastomizationScene : MonoBehaviour
{
    [SerializeField] private TMP_Text _pol;
    [SerializeField] private TMP_Text _cvet;
    [SerializeField] private TMP_Text _pricheska;
    [SerializeField] private TMP_Text _acsesuars;
    [SerializeField] private TMP_Text _boroda;
    [SerializeField] private TMP_Text _resumeButton;

    private LoadedInfo _loadedInfo;
    void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();

        if (_loadedInfo._Language == "en")
        {
            _pol.text = "Gender";
            _cvet.text = "Skin color";
            _pricheska.text = "Hairstyle";
            _acsesuars.text = "Accessories";
            _boroda.text = "Beard";
            _resumeButton.text = "Resume";
        }

        Destroy(this);
    }

}
