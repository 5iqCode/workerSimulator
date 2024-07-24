using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoAwayCanvas : MonoBehaviour
{

    private LoadedInfo _loadedInfo;

    private PunchScript _punshScript;

    private PauseScript _pauseScript;
    
    Transform _challangeList;

    private StatsController _statBlock;

    private string _language;
    private void Awake()
    {
        StartCoroutine(WaitEndFrame());
    }

    IEnumerator WaitEndFrame()
    {
        yield return new WaitForEndOfFrame();
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        _language = _loadedInfo._Language;
        Time.timeScale = 0;

        if (_loadedInfo._isDesktop)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        _pauseScript = GameObject.Find("PauseButton").GetComponent<PauseScript>();

        _pauseScript._canPause = false;

        AudioListener.volume = 0;
        _punshScript = GameObject.Find("Player").GetComponent<PunchScript>();

        if (_punshScript != null)
        {
            _punshScript._canPanch = false;
        }
    }
    void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        _language = _loadedInfo._Language;

        _statBlock = GameObject.Find("Canvas").GetComponentInChildren<StatsController>();
        _challangeList = GameObject.Find("ChallangeList").transform;
        ChallangeController[] challanges = _challangeList.GetComponentsInChildren<ChallangeController>();

        foreach (ChallangeController challange in challanges)
        {
            if (challange.time != 10000)
            {
                challange.SwitchAnimLose();
            }
        }
        Button _button = GetComponentInChildren<Button>();
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnClickNext);

        string message = "Ты ХУДШИЙ работник! Уходи из моего магазина! Если завтра будешь работать также - я тебя уволю. Я ТЕБЯ НЕНАВИЖУ!";
        if (_language == "en")
        {
            message = "You're the WORST employee! Get out of my store! If you work the same way tomorrow, I'll fire you. I HATE YOU!";
            GameObject.Find("NextCitatText").GetComponent<TMP_Text>().text = "Next";
        }
        GameObject.Find("CitatBoss").GetComponent<TMP_Text>().text = message;
    }

    public void OnClickNext()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        TimerScript _timer = GameObject.Find("TimerController").GetComponent<TimerScript>();
        StopCoroutine(_timer._jobTimeCor);
        string message = "Вас выгнали за плохую работу, идите домой";
        if (_language == "en")
        {
            message = "You were kicked out for a bad job, go home";
        }
        _timer.InstantiateChallangeInList(message, 10000);
        TextMoneyMark _textMoney = GameObject.Find("MoneyBlock").GetComponentInChildren<TextMoneyMark>();
        _textMoney.UpdateMoneyText("-", "1000");
        _loadedInfo.PlayerInfo.money -= 1000;

        _textMoney.GetComponent<TMP_Text>().text = _loadedInfo.PlayerInfo.money.ToString();
        string message2 = "Вас выгнали с работы раньше времени";
        if (_language == "en")
        {
            message2 = "You were fired from your job ahead of time";
        }
        _statBlock.MinusHP(10, message2);
        Instantiate(Resources.Load<GameObject>("TriggerGoHome"));
        StopCoroutine(_statBlock._job);

        Destroy(GameObject.Find("TextGlobalTime"));

        if (_punshScript != null)
        {
            _punshScript._canPanch = true;
        }
        AudioListener.volume = _loadedInfo.PlayerInfo._volume;
        Time.timeScale = 1;
        _pauseScript._canPause = true;

        if (_loadedInfo._isDesktop)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
