using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsController : MonoBehaviour
{
    [SerializeField] private GameObject _errorMessage;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Slider _foodSlider;
    [SerializeField] private TMP_Text _textHpSlider;
    [SerializeField] private TMP_Text _textfoodSlider;

    private LoadedInfo _loadedInfo;

    [SerializeField] private GameObject _minusStatsGO;
    [SerializeField] private GameObject _shtrafGO;

    [SerializeField] private GameObject _hangryImage;

    public Coroutine _job;
    private PauseScript _pausedScript;
    private GameObject _player;

    private string _language;

    private AudioSource _wantEat = null;
    private void Start()
    {

        

        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        _language = _loadedInfo._Language;
        _player = GameObject.Find("Player");

        _hpSlider.value = _loadedInfo.PlayerInfo._statHP;

        _textHpSlider.text = ((int)_loadedInfo.PlayerInfo._statHP).ToString();

        _foodSlider.value = _loadedInfo.PlayerInfo._statHangry;

        _textfoodSlider.text = ((int)_loadedInfo.PlayerInfo._statHangry).ToString();

        _job =StartCoroutine(WaitTime());

        if (_loadedInfo.PlayerInfo._statHangry <=20)
        {
            foreach (AudioSource _as in _player.GetComponentsInChildren<AudioSource>())
            {
                if (_as.name == "WantEatSound")
                {
                    _wantEat = _as;
                    break;
                }
            }
            StartCoroutine(WantEatCor());
        }
    }

    IEnumerator WantEatCor()
    {

        while (_loadedInfo.PlayerInfo._statHangry < 21)
        {
            _wantEat.pitch = Random.Range(0.8f, 1.3f);
            _wantEat.Play();

            yield return new WaitForSeconds(20);
        }
    }
    IEnumerator WaitTime()
    {
        while (true)
        {          
            if (_loadedInfo.PlayerInfo._statHangry > 0.5)
            {
                MinusFood(0.5f);
            }
            else
            {
                MinusHP(0.5f, "");
            }
            if(_loadedInfo.PlayerInfo._statHangry > 20)
            {
                if (_hangryImage.activeSelf)
                {
                    _hangryImage.SetActive(false);
                }
            }
            else
            {
                if (!_hangryImage.activeSelf)
                {
                    
                    _hangryImage.SetActive(true);

                    GameObject _error = Instantiate(_errorMessage, GetComponentInParent<Canvas>().transform);

                    string message = "Вы голодны!";
                    if (_language == "en")
                    {
                        message = "You're hungry!";
                    }

                    _error.GetComponent<TMP_Text>().text = message;

                    if (_wantEat != null)
                    {
                        StartCoroutine(WantEatCor());
                    }
                    else
                    {
                        foreach (AudioSource _as in _player.GetComponentsInChildren<AudioSource>())
                        {
                            if (_as.name == "WantEatSound")
                            {
                                _wantEat = _as;
                                break;
                            }
                        }
                    }


                    StartCoroutine(WantEatCor());
                }
            }

            yield return new WaitForSeconds(1.5f);
        }
    }

    public void MinusHP(float value,string shtraf)
    {
        _loadedInfo.PlayerInfo._statHP -= value;
        if (_loadedInfo.PlayerInfo._statHP<0)
        {
            _pausedScript = GameObject.Find("PauseButton").GetComponent<PauseScript>();
            _loadedInfo.typePause = 5;

            _pausedScript.ClickPause();
        }
        if (shtraf != "")
        {
            GameObject _obj1 = Instantiate(_minusStatsGO, transform);
            _obj1.GetComponent<TMP_Text>().text = "-" + ((int)value).ToString();
            GameObject _obj2 = Instantiate(_shtrafGO, transform);
            _obj2.GetComponentInChildren<TMP_Text>().text = shtraf;

            _loadedInfo._shtrafs.Add(shtraf);
        }
        _textHpSlider.text = ((int)_loadedInfo.PlayerInfo._statHP).ToString();

        _hpSlider.value = _loadedInfo.PlayerInfo._statHP;

    }
    public void PlusHP(float value, string shtraf)
    {

        _loadedInfo.PlayerInfo._statHP += value;
        if (_loadedInfo.PlayerInfo._statHP > 100)
        {
            _loadedInfo.PlayerInfo._statHP = 100;
        }
        if (shtraf != "")
        {
            GameObject _obj1 = Instantiate(_minusStatsGO, transform);
            _obj1.GetComponent<TMP_Text>().text = "+" + ((int)value).ToString();
            _obj1.GetComponent<TMP_Text>().color = Color.green;
            GameObject _obj2 = Instantiate(_shtrafGO, transform);
            _obj2.GetComponentInChildren<TMP_Text>().text = shtraf;
            _obj2.GetComponentInChildren<TMP_Text>().color = Color.green;
        }
        _textHpSlider.text = ((int)_loadedInfo.PlayerInfo._statHP).ToString();

        _hpSlider.value = _loadedInfo.PlayerInfo._statHP;

    }

    public void MinusFood(float value)
    {
        _loadedInfo.PlayerInfo._statHangry -= value;

        _textfoodSlider.text = ((int)_loadedInfo.PlayerInfo._statHangry).ToString();

        _foodSlider.value = _loadedInfo.PlayerInfo._statHangry;
    }

    private void PlusFood(int value)
    {
        _loadedInfo.PlayerInfo._statHangry += value;

        if (_loadedInfo.PlayerInfo._statHangry > 100)
        {
            _loadedInfo.PlayerInfo._statHangry = 100;
        }

        _textfoodSlider.text = ((int)_loadedInfo.PlayerInfo._statHangry).ToString();

        _foodSlider.value = _loadedInfo.PlayerInfo._statHangry;
    }

    public void EatFood(string food)
    {
        string valueRazn = "-";
        Color _color = Color.red;
        int valueFood =0;
        int valueHP =0;
        string text ="";

        if (food == "burger")
        {
            valueRazn = "-";
            _color = Color.red;
            valueFood = 40;
            valueHP = -3;
            text = "Невкусная еда";

        }
        else if (food == "potatoFri")
        {
            valueRazn = "-";
            _color = Color.red;
            valueFood = 35;
            valueHP = -1;
            text = "Невкусная еда";
        }
        else if (food == "pizza")
        {
            valueRazn = "+";
            _color = Color.green;
            valueFood = 50;
            valueHP = 1;
            text = "Вкусная еда";
        }
        else if (food == "shawa")
        {
            valueRazn = "+";
            _color = Color.green;
            valueFood = 80;
            valueHP = 2;
            text = "Вкусная еда";
        }
        else if (food == "cake")
        {
            valueRazn = "+";
            _color = Color.green;
            valueFood = 25;
            valueHP = 10;
            text = "Очень вкусная еда";
        }
        else
        {
            valueRazn = "+";
            _color = Color.green;
            valueFood = 10;
            valueHP = 15;
            text = "Очень вкусная еда";
        }
        if (_language == "en")
        {
            if(text == "Невкусная еда")
            {
                text = "Tasteless food";
            } else if(text == "Вкусная еда")
            {
                text = "Tasty food";
            }
            else
            {
                text = "Very tasty food";
            }
        }
        GameObject _obj1 = Instantiate(_minusStatsGO, transform);
        _obj1.GetComponent<TMP_Text>().text = valueRazn + (Mathf.Abs(valueHP)).ToString();
        _obj1.GetComponent<TMP_Text>().color = _color;
        GameObject _obj2 = Instantiate(_shtrafGO, transform);
        _obj2.GetComponentInChildren<TMP_Text>().text = text;
        _obj2.GetComponentInChildren<TMP_Text>().color = _color;

        _loadedInfo.PlayerInfo._statHP += valueHP;
        if (_loadedInfo.PlayerInfo._statHP > 100)
        {
            _loadedInfo.PlayerInfo._statHP = 100;
        }
        _textHpSlider.text = ((int)_loadedInfo.PlayerInfo._statHP).ToString();

        _hpSlider.value = _loadedInfo.PlayerInfo._statHP;

        PlusFood(valueFood);
    }
}
