using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeDayCanvasController : MonoBehaviour
{
    private LoadedInfo loadedInfo;

    [SerializeField] private TMP_Text _text1;
    [SerializeField] private TMP_Text _text2;

    [SerializeField] private TMP_Text _textChisloPokup;
    [SerializeField] private TMP_Text _textChisloHul;

    [SerializeField] private TMP_Text _daysCount;
    [SerializeField] private TMP_Text _procentPocupatel;
    [SerializeField] private TMP_Text _procentHuligan;
    private void Start()
    {
        loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();

        if (loadedInfo._Language == "en")
        {
            _daysCount.text = "Day " + loadedInfo.PlayerInfo._countDays.ToString();
            _text1.text = "Working in the store every day";
            _text2.text = "HARDER AND HARDER";
            _textChisloPokup.text = "Buyers' Chance";
            _textChisloHul.text = "The chance of bullies";
        }
        else
        {
            _daysCount.text = "Δενό " + loadedInfo.PlayerInfo._countDays.ToString();
        }
        

        _procentPocupatel.text = loadedInfo.PlayerInfo.changeCustomer.ToString() + "%";
        _procentHuligan.text = loadedInfo.PlayerInfo.changeAnonimus.ToString() + "%";

        StartCoroutine(WaitDestroy());
    }


    IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(3.2f);

        Destroy(gameObject);
    }
}
