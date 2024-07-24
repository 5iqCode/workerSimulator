using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageController : MonoBehaviour
{
    [SerializeField] private Image _ruLang;
    [SerializeField] private Image _enLang;

    [SerializeField] private TMP_Text _resumeText;
    [SerializeField] private TMP_Text _newGameText;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _leadersText;
    [SerializeField] private TMP_Text _iText;

    LoadedInfo _loadedInfo;
    private void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();

        string _language = _loadedInfo._Language;
        if (_language == "en")
        {
            _ruLang.color = Color.white;
            _enLang.color = Color.yellow;

            _resumeText.text = "Resume";
            _newGameText.text = "New game";
            _moneyText.text = "Money earned all the time:";
            _leadersText.text = "Take a place on the leaderboard (Authorization required)";
            _iText.text = "Purchases for an apartment are not reset at the start of a new game";
        }
        else if (_language == "ru")
        {
            _ruLang.color = Color.yellow;
            _enLang.color = Color.white;

            _resumeText.text = "����������";
            _newGameText.text = "����� ����";
            _moneyText.text = "����� ���������� �� �� �����:";
            _leadersText.text = "������ ����� � ������� ������� (��������� �����������)";
            _iText.text = "������� ��� �������� �� ������������ ��� ������ ����� ����";
        }
    }
    public void OnClickLanguage(string language)
    {
        if (language == "en")
        {
            _ruLang.color = Color.white;
            _enLang.color = Color.yellow;

            _loadedInfo._Language = language;

            _resumeText.text = "Resume";
            _newGameText.text = "New game";
            _moneyText.text = "Money earned all the time:";
            _leadersText.text = "Take a place on the leaderboard (Authorization required)";
            _iText.text = "Purchases for an apartment are not reset at the start of a new game";
        }
        else if (language == "ru")
        {
            _ruLang.color = Color.yellow;
            _enLang.color = Color.white;

            _loadedInfo._Language = language;

            _resumeText.text = "����������";
            _newGameText.text = "����� ����";
            _moneyText.text = "����� ���������� �� �� �����:";
            _leadersText.text = "������ ����� � ������� ������� (��������� �����������)";
            _iText.text = "������� ��� �������� �� ������������ ��� ������ ����� ����";
        }
    }
}
