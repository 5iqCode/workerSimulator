using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsContrHome : MonoBehaviour
{
    [SerializeField] private TMP_Text _changeStatHp;
    [SerializeField] private TMP_Text _changeStatFood;

    [SerializeField] private TMP_Text _statHP;
    [SerializeField] private TMP_Text _statFood;
    [SerializeField] private Slider _sliderHp;
    [SerializeField] private Slider _sliderFood;

    [SerializeField] private TMP_Text _moneyText;

    private LoadedInfo _loadedInfo;
    void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();

        UpdateEndDayInfo();
        UpdateMoney();
        UpdateStats();
    }

    public void UpdateMoney()
    {
        _moneyText.text = _loadedInfo.PlayerInfo.money.ToString();
    }

    public void UpdateStats()
    {
        Debug.Log("322");
        int hp = (int)_loadedInfo.PlayerInfo._statHP;
        int food = (int)_loadedInfo.PlayerInfo._statHangry;

        _sliderHp.value = hp;
        _statHP.text = hp.ToString();

        _sliderFood.value = food;
        _statFood.text = food.ToString();
    }

    public void UpdateEndDayInfo()
    {
        int hp = _loadedInfo.PlayerInfo.changeHPStatInEndDay;
        int food = _loadedInfo.PlayerInfo.changeFoodStatInEndDay;
        if(hp > 0)
        {
            _changeStatHp.text = "+"+hp.ToString();
            _changeStatHp.color = Color.green;
        }
        else
        {
            _changeStatHp.text = hp.ToString();
            _changeStatHp.color = Color.red;
        }
        if (food > 0)
        {
            _changeStatFood.text = "+"+food.ToString();
            _changeStatFood.color = Color.green;
        }
        else
        {
            _changeStatFood.text = food.ToString();
            _changeStatFood.color = Color.red;
        }
    }
}
