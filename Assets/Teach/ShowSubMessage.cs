using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowSubMessage : MonoBehaviour
{
    private TMP_Text _text;

    private LoadedInfo _loadedInfo;
    private string _language;
    void Start()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        _language = _loadedInfo._Language;
        if (_language == "en")
        {
            _text.text = "Keep an eye on the emotional state and the state of satiety";
        }


        StartCoroutine(WaitCor());
    }

    private IEnumerator WaitCor()
    {
        yield return new WaitForSeconds(5);
        string message = "Если сытость упадёт до нуля, эмоциональное состояние начнёт убывать.";
        if (_language == "en")
        {
            message = "If satiety drops to zero, the emotional state will begin to decrease.";
        }
        _text.text = message;
        yield return new WaitForSeconds(5);
        string message2 = "Если эмоциональное состояние упадёт до 0 - игра окончена.";
        if (_language == "en")
        {
            message2 = "If the emotional state drops to 0, the game is over.";
        }
        _text.text = message2;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
