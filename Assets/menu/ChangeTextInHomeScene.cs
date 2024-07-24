using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTextInHomeScene : MonoBehaviour
{
    [SerializeField] private TMP_Text _changePosleSna;
    [SerializeField] private TMP_Text _changePosleSnaemocSost;
    [SerializeField] private TMP_Text _changePosleSnasitost;    
    [SerializeField] private TMP_Text _emocSost;
    [SerializeField] private TMP_Text _sitost;


    [SerializeField] private TMP_Text _shopName;
    [SerializeField] private TMP_Text _gost;
    [SerializeField] private TMP_Text _kuhn;
    [SerializeField] private TMP_Text _spaln;
    [SerializeField] private TMP_Text _toilet;
    [SerializeField] private TMP_Text _comp;

    private LoadedInfo _loadedInfo;
    void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        if (_loadedInfo._Language == "en")
        {
            _changePosleSna.text = "Changes after sleep";
            _changePosleSnaemocSost.text = "Emotional state";
            _changePosleSnasitost.text = "Satiety";           
            _emocSost.text = "Emotional state";
            _sitost.text = "Satiety";

            _shopName.text = "Ler Merlen";
            _gost.text = "For the living room";
            _kuhn.text = "For the kitchen";
            _spaln.text = "For the bedroom";
            _toilet.text = "For the toilet";
            _comp.text = "Computers";

        }
        Destroy(this);
    }

}
