using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadInfoPause : MonoBehaviour
{
    LoadedInfo loadedInfo;

    [SerializeField] private TMP_Text startZPValue;
    [SerializeField] private TMP_Text premiiValue;

    [SerializeField] private TMP_Text shtraf1provValue;

    [SerializeField] private GameObject shtraf1provValueGO1;
    [SerializeField] private GameObject shtraf1provValueGO2;

    [SerializeField] private Transform transformTextShtraf;
    [SerializeField] private GameObject posTextShtraf;

    [SerializeField] private TMP_Text itogZPValue;

    public void ClickNext()
    {
        
        GameObject.Find("PauseScreenController").GetComponent<PauseScreenController>().GoNextButton();
    }
    void Start()
    {
        loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        startZPValue.text = loadedInfo.PlayerInfo.startZPDay.ToString();
        
        premiiValue.text = "+"+loadedInfo._premii.ToString();
        if (loadedInfo.typePause == 1)
        {
            Destroy(shtraf1provValueGO1);
            Destroy(shtraf1provValueGO2);
        }
        else
        {
            shtraf1provValue.text = "-"+loadedInfo.shtrafsForFirst.ToString();
        }

        for (int i=0;i< loadedInfo._shtrafs.Count; i++)
        {
            GameObject _obj = Instantiate(posTextShtraf, transformTextShtraf);
            _obj.GetComponent<TMP_Text>().text ="-"+ loadedInfo._shtrafsValue[i].ToString();
            foreach(TMP_Text _text in _obj.GetComponentsInChildren<TMP_Text>())
            {
                if(_text.name== "TextShtraf")
                {
                    _text.text = loadedInfo._shtrafs[i];
                    break;
                }
            }

            if (loadedInfo._shtrafs.Count == 1)
            {
                break;
            }
        }

        itogZPValue.text = loadedInfo._ZPDay.ToString();
    }
    [SerializeField] private GameObject _CanvasBoss;
    private void OnDestroy()
    {
        if (loadedInfo._ZPDay <= 0)
        {
            GameObject _temp = Instantiate(_CanvasBoss);
           Destroy(_temp.GetComponent<BossTeachCanvasController>());
            _temp.AddComponent<GoAwayCanvas>();
        }

        loadedInfo._shtrafs.Clear();
        loadedInfo._shtrafsValue.Clear();
    }
}
