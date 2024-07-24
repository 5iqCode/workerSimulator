using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomeChalangesController : MonoBehaviour
{
    [SerializeField] private GameObject _challangeObjPrefab;

    [SerializeField] private Transform _challangeList;

    LoadedInfo _loadedInfo;

    private string _language;
    private void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        _language = _loadedInfo._Language;
        if (_loadedInfo._dayIsStart)
        {
            string message = "Идите на работу!";
            if (_language == "en")
            {
                message = "Go to work!";
            }
            InstantiateChallangeInList(message, 1);
        }
        else
        {
            string message = "Посмотрите скидки в интернет магазине.";
            if (_language == "en")
            {
                message = "Look at the discounts in the online store.";
            }
            InstantiateChallangeInList(message, 2);
            string message2 = "Ложитесь спать.";
            if (_language == "en")
            {
                message2 = "Go to bed.";
            }
            InstantiateChallangeInList(message2, 3);
        }
    }
    private void InstantiateChallangeInList(string text,int targetTime)
    {
        GameObject _objChallange = Instantiate(_challangeObjPrefab, _challangeList);
        _objChallange.GetComponentInChildren<TMP_Text>().text = text;
        _objChallange.GetComponent<ChallangeController>().time = targetTime;
    }
}
