using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class zpBlockController : MonoBehaviour
{
    [SerializeField] private TMP_Text _textZP;

    private LoadedInfo _loadedInfo;

    [SerializeField] private GameObject _shablonText;

    public int ZPDay;

    void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();

        ZPDay = _loadedInfo.PlayerInfo.startZPDay;
        _textZP.text = ZPDay.ToString();
    }

    public void MinusMoney(int _value)
    {
        if (ZPDay > _value)
        {
            ZPDay -= _value;
            _textZP.text = ZPDay.ToString();

            GameObject _tempObj = Instantiate(_shablonText, transform);
            _tempObj.GetComponent<TMP_Text>().text = "-" + _value.ToString();
        }
        else
        {
            ZPDay =0;
            _textZP.text = ZPDay.ToString();
            //иди домой
        }
        _loadedInfo._ZPDay = ZPDay;

        _loadedInfo._shtrafsValue.Add(_value);


    }

    public void PlusMoney(int _value)
    {
        if (ZPDay > 0)
        {
                ZPDay += _value;
                _textZP.text = ZPDay.ToString();

                GameObject _tempObj = Instantiate(_shablonText, transform);
                _tempObj.GetComponent<TMP_Text>().text = "+" + _value.ToString();
            _tempObj.GetComponent<TMP_Text>().color = Color.green;
        }
        _loadedInfo._ZPDay = ZPDay;

        _loadedInfo._premii += _value;
    }
}
