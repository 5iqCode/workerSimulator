using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextMoneyMark : MonoBehaviour
{
    LoadedInfo _loadedInfo;

    TMP_Text _text;

    [SerializeField] private GameObject _objPrefab;
    private void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        _text = GetComponent<TMP_Text>();
        UpdateMoneyText("","");
    }

    public void UpdateMoneyText(string type,string value)
    {
        
        if (type == "+")
        {
            _loadedInfo.PlayerInfo.money += _loadedInfo._ZPDay;
            GameObject _obj = Instantiate(_objPrefab,transform.parent);
            _obj.GetComponent<TMP_Text>().text = "+"+value;
            _obj.GetComponent<TMP_Text>().color = Color.green;

        }
        else if (type == "-")
        {
            GameObject _obj = Instantiate(_objPrefab, transform.parent);
            _obj.GetComponent<TMP_Text>().text = "-" + value;
            _obj.GetComponent<TMP_Text>().color = Color.red;
        }
        _text.text = _loadedInfo.PlayerInfo.money.ToString();

    }
}
