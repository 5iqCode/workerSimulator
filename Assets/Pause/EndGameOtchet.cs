using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameOtchet : MonoBehaviour
{
    LoadedInfo loadedInfo;
    [SerializeField] private TMP_Text startMoney;

    [SerializeField] private TMP_Text startZPValue;
    [SerializeField] private TMP_Text premiiValue;

    [SerializeField] private TMP_Text shtraf1provValue;
    [SerializeField] private TMP_Text shtraf2provValue;

    [SerializeField] private TMP_Text endZP;
    [SerializeField] private TMP_Text rashNaEdy;
    [SerializeField] private TMP_Text rashNaLerua;

    [SerializeField] private TMP_Text endMoney;
    void Start()
    {
        loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();

        startMoney.text = loadedInfo.startMoney.ToString();

        startZPValue.text = loadedInfo.PlayerInfo.startZPDay.ToString();

        premiiValue.text = loadedInfo._premii.ToString();

        shtraf1provValue.text = loadedInfo.shtrafsForFirst.ToString();

        shtraf2provValue.text = loadedInfo.shtrafsForSecond.ToString();

        endZP.text = loadedInfo._ZPDay.ToString();

        rashNaEdy.text = loadedInfo._rashodsFood.ToString();
        rashNaLerua.text = loadedInfo._rashodsLerua.ToString();
        endMoney.text = loadedInfo.PlayerInfo.money.ToString();
    }


    public void OnClickNextDay()
    {
        loadedInfo.ClearDay();
        loadedInfo.AddHard();

        Time.timeScale = 1;

        SceneManager.LoadScene("Home");
    }
}
