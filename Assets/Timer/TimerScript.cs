using System.Collections;
using TMPro;
using UnityEngine;


public class TimerScript : MonoBehaviour
{
    private int _startDayTime = 540; // нужно успеть в 
    private int _endDayTime = 1080; //можно идти домой (1200 закрытие магазина

    public int _currentTime = 510;

    [SerializeField] private MeshRenderer _sky;
    private Material _skyMaterial;

    [SerializeField] private GameObject _canvas;

    [SerializeField] private TMP_Text _timer;

    public Coroutine _jobTimeCor;

    [SerializeField] private Material _noLightMateral;

    [SerializeField] private Light _worldLight;

    [SerializeField] Transform _challangeList;
    [SerializeField] GameObject _challangeObjPrefab;

    private zpBlockController _zpBlock;
    private LoadedInfo _loadedInfo;
    private StatsController _statBlock;

    private BossController _bossController;
    private SpawnerNPS _spawnerNPC;

    private string _language;

    void Start()
    {
        transform.parent = GameObject.Find("LoadedInfo").transform;

        if (GameObject.FindGameObjectsWithTag("TimerController").Length > 1)
        {
            Destroy(gameObject);
        }
        StartCoroutine(WaitStart());
    }

    IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(0.1f);

        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();

        _language = _loadedInfo._Language;

        _zpBlock = _loadedInfo.GetComponentInChildren<zpBlockController>();
        _statBlock = GameObject.Find("Canvas").GetComponentInChildren<StatsController>();

        _bossController = GameObject.Find("Spawner").GetComponent<BossController>();
        _spawnerNPC = _bossController.GetComponent<SpawnerNPS>();

        _skyMaterial = _sky.material;

        _jobTimeCor = StartCoroutine(WaitSeconds());
        string message = "Ѕыть на рабочем месте. (09:00)";
        if (_language == "en")
        {
            message = "Be in the workplace. (09:00)";
        }

        InstantiateChallangeInList(message, 540);
    }

public void ResumeTimer()
    {
        _jobTimeCor = StartCoroutine(WaitSeconds());
    }

    IEnumerator WaitSeconds()
    {
        while(_currentTime<= _endDayTime)
        {
            float xSkyMat =0.5f+ 1.5f*(_currentTime-540) / 720f;

            _skyMaterial.mainTextureScale = new Vector2(xSkyMat, 1);

            _currentTime++;

            if( _currentTime == _endDayTime)
            {
                _worldLight.intensity = 0;
                
                foreach (GameObject _obj in GameObject.FindGameObjectsWithTag("Light"))
                {
                    _obj.GetComponent<MeshRenderer>().material = _noLightMateral;
                }
            }

                string hourString, minuteString;
                int hour = (_currentTime / 60);
                int min = (_currentTime % 60);

                if (hour < 10)
                {
                    hourString = 0 + hour.ToString();
                }
                else
                {
                    hourString = hour.ToString();
                }
                if (min < 10)
                {
                    minuteString = 0 + min.ToString();
                }
                else
                {
                    minuteString = min.ToString();
                }
                _timer.text = hourString + ":" + minuteString;

                if( _currentTime >= _endDayTime )
            {
                _timer.text = "";
            }


            CheckTime();

            yield return new WaitForSeconds(0.75f);
        }
    }
    public void EndCarWorkChallange()
    {
        ChallangeController[] challanges = _challangeList.GetComponentsInChildren<ChallangeController>();

        foreach (ChallangeController challange in challanges)
        {
            if (challange.time == 720)
            {
                challange.SwitchAnimWin();
                break;
            }
        }
    }

    public void InstantiateChallangeInList(string text,int targetTime)
    {
        GameObject _objChallange = Instantiate(_challangeObjPrefab, _challangeList);
        _objChallange.GetComponentInChildren<TMP_Text>().text = text;
        _objChallange.GetComponent<ChallangeController>().time = targetTime;
    }

    private void CheckTime()
    {
        if (_currentTime == 540)//старт работы
        {
            _spawnerNPC._targetCountMassovka = 20;

            ChallangeController[] challanges = _challangeList.GetComponentsInChildren<ChallangeController>();

            foreach (ChallangeController challange in challanges)
            {
                if (challange.time == 540)
                {
                    if (_loadedInfo._inShop)
                    {
                        challange.SwitchAnimWin();
                    }
                    else
                    {
                        //опоздал штраф
                        string message5 = "¬ы опоздали на работу";
                        if (_language == "en")
                        {
                            message5 = "You're late for work";
                        }

                        _statBlock.MinusHP(5, message5);
                        _zpBlock.MinusMoney(500);
                        challange.SwitchAnimLose();
                    }
              break;
                }
            }
            string message = "”спей разместить коробки на складе (до 12:00)";
            if (_language == "en")
            {
                message = "Have time to place the boxes in the warehouse (before 12:00)";
            }
            InstantiateChallangeInList(message, 720);
            string message2 = "Ѕосс будет провер€ть магазин в 13:00";
            if (_language == "en")
            {
                message2 = "The boss will check the store at 13:00.";
            }
            InstantiateChallangeInList(message2, 780);
        }
        else if (_currentTime == 690)//моргание разгрузки
        {
            _spawnerNPC._targetCountMassovka = 50;
            ChallangeController[] challanges = _challangeList.GetComponentsInChildren<ChallangeController>();

            foreach(ChallangeController challange in challanges)
            {
                if (challange.time == 720)
                {
                    challange.SwitchAnimLitleTime();
                    break;
                }
            }

        }
        else if( _currentTime == 721) //крайнее врем€ разгрузки
        {

            ChallangeController[] challanges = _challangeList.GetComponentsInChildren<ChallangeController>();

            foreach (ChallangeController challange in challanges)
            {
                if (challange.time == 720)
                {
                    if(GameObject.FindGameObjectsWithTag("BoxFromCar").Length > 0)
                    {
                        string message = "¬ы не разобрали коробки";
                        if (_language == "en")
                        {
                            message = "You didn't take apart the boxes";
                        }
                        _statBlock.MinusHP(5, message);
                        _zpBlock.MinusMoney(500);
                        challange.SwitchAnimLose();
                        break;
                    }

                }
            }
        }
        else if( _currentTime == 765)//моргание 1 преверки
        {
            ChallangeController[] challanges = _challangeList.GetComponentsInChildren<ChallangeController>();

            foreach (ChallangeController challange in challanges)
            {
                if (challange.time == 780)
                {
                    challange.SwitchAnimLitleTime();
                    break;
                }
            }
        }
        else if( _currentTime == 781)//1 проверка
        {
            ChallangeController[] challanges = _challangeList.GetComponentsInChildren<ChallangeController>();

            foreach (ChallangeController challange in challanges)
            {
                if (challange.time == 780)
                {
                    //босс должен пойти
                    _bossController.GoSheckMagazine();
                    challange.SwitchAnimLose();
                    break;
                }
            }
        }else if (_currentTime == 840)
        {
            _spawnerNPC._targetCountMassovka = 30;
            string message = "Ѕосс будет провер€ть магазин в 16:30";
            if (_language == "en")
            {
                message = "The boss will check the store at 16:30";
            }
            InstantiateChallangeInList(message, 1140);
        }
        else if( _currentTime == 975)//моргание 2 проверки
        {
            _spawnerNPC._targetCountMassovka = 20;

            ChallangeController[] challanges = _challangeList.GetComponentsInChildren<ChallangeController>();

            foreach (ChallangeController challange in challanges)
            {
                if (challange.time == 1140)
                {
                    challange.SwitchAnimLitleTime();
                    break;
                }
            }
        }
        else if( _currentTime == 990)//2 проверка
        {

            ChallangeController[] challanges = _challangeList.GetComponentsInChildren<ChallangeController>();

            foreach (ChallangeController challange in challanges)
            {
                if (challange.time == 1140)
                {
                    _bossController.GoSheckMagazine();
                    challange.SwitchAnimLose();
                    break;
                }
            }
        }
        else if (_currentTime == 1050)
        {
            string message = "ћагазин закрываетс€ в 18:00";
            if (_language == "en")
            {
                message = "The store closes at 18:00";
            }
            InstantiateChallangeInList(message, 1800);
            _spawnerNPC._targetCountMassovka = 15;
            _spawnerNPC._chanceCustomer = -1;

            GameObject[] _nps = GameObject.FindGameObjectsWithTag("NPS");

            foreach (GameObject nps in _nps)
            {
                if (nps.name != "Boss")
                {
                    nps.GetComponent<Animator>().SetBool("IsMoving", true);


                    CustomerMoveScript moveScript = nps.GetComponent(typeof(CustomerMoveScript)) as CustomerMoveScript;

                    if (moveScript != null)
                    {
                        
                        if (moveScript._objKorzinkaInNPS != null)
                        {
                            CustomerGoToKassa _tempScript = moveScript.gameObject.AddComponent<CustomerGoToKassa>();
                            _tempScript._objKorzinka = moveScript._objKorzinkaInNPS;
                        }
                        else
                        {
                            MoveMassovka _moveMassTrig = nps.AddComponent<MoveMassovka>();

                            _moveMassTrig._pathAgent = _spawnerNPC.CreatePathNPSMassovka(0, false);
                        }
                        

                        Destroy(moveScript);

                    }

                    MoveMassovka moveMassovkaScript = nps.GetComponent(typeof(MoveMassovka)) as MoveMassovka;

                    if (moveMassovkaScript != null)
                    {
                        MoveMassovka _moveMassTrig = nps.AddComponent<MoveMassovka>();

                        _moveMassTrig._pathAgent = _spawnerNPC.CreatePathNPSMassovka(0, false);

                        Destroy(moveMassovkaScript);
                    }
                }

            }
        }
        else if (_currentTime == 1081)//иди домой магазин закрыт
        {
            ChallangeController[] challanges = _challangeList.GetComponentsInChildren<ChallangeController>();

            foreach (ChallangeController challange in challanges)
            {
                if (challange.time == 1800)
                {
                    challange.SwitchAnimWin();
                    break;
                }
            }
            string message2 = "–абочий день завершЄн, иди домой";
            if (_language == "en")
            {
                message2 = "The working day is over, go home";
            }
            InstantiateChallangeInList(message2, 10000);
            GameObject.Find("MoneyBlock").GetComponentInChildren<TextMoneyMark>().UpdateMoneyText("+", _loadedInfo._ZPDay.ToString());

            StartCoroutine(WaitSec());

            Instantiate(_GoHomeTrigger);
            StopCoroutine(_statBlock._job);
        }
        else if (_currentTime == 530)//моргание опоздание
        {
            ChallangeController[] challanges = _challangeList.GetComponentsInChildren<ChallangeController>();

            foreach (ChallangeController challange in challanges)
            {
                if (challange.time == 540)
                {
                    challange.SwitchAnimLitleTime();
                    break;
                }
            }
        }
    }

    IEnumerator WaitSec()
    {
        yield return new WaitForSeconds(2);
        Destroy(GetComponentInChildren<zpBlockController>().gameObject);
    }

    [SerializeField] private GameObject _GoHomeTrigger;
}
