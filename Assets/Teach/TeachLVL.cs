using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class TeachLVL : MonoBehaviour
{
    [SerializeField] private GameObject _strelkaPrefab;

    [SerializeField] private GameObject _strelkaPrefabInventory;
    [SerializeField] private GameObject _strelkaInventory;

    private GameObject _strelkaWorldObj;
    public RotateStrelka _strelkaScript;

    [SerializeField] private GameObject _BossTalk;
    [SerializeField] private GameObject _controlsCanvas;
    [SerializeField] private GameObject TriggerTeach;

    [SerializeField] private GameObject _subTeachStatsCanvas;
    private LoadedInfo _loadedInfo;

    private SpawnerNPS spawnerNPS;

     Transform _challangeList;
    [SerializeField] GameObject _challangeObjPrefab;

    public int _stageTeach=0;

     private GameObject _nps = null;

    private Vector3[] posTriggers;

    [SerializeField] private GameObject _triggerBottleImage;
    private GameObject _triggerBottleImageObj;

    private string _language;
    private void Start()
    {
        posTriggers = new Vector3[5]
        {
            new Vector3(18.795f,-32.91f,10.93f),
            new Vector3(21.185f,-32.91f,-12.214f),
            new Vector3(20.985f,-32.91f,-15.514f),
            new Vector3(11.338f,-32.91f,-14.51f),
            new Vector3(11.277f,-32.91f,-19.474f),
        };
        _loadedInfo = GetComponentInParent<LoadedInfo>();
        _language = _loadedInfo._Language;
        _loadedInfo.PlayerInfo.changeCustomer = 101;
    }
    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name == "Home")
        {  
            Instantiate(_controlsCanvas);
            if (_loadedInfo._isDesktop)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
        } else if (SceneManager.GetActiveScene().name == "Demo_01_Lite")
        {
            _strelkaWorldObj = Instantiate(_strelkaPrefab, GameObject.Find("Player").transform);
            _strelkaScript = _strelkaWorldObj.GetComponent<RotateStrelka>();

            _strelkaScript.SetTarget(new Vector3(20.009f, -33.79001f, 10f));

            Destroy(GameObject.Find("TimerController").GetComponent<TimerScript>());
            Destroy(GameObject.Find("TextGlobalTime"));
            spawnerNPS = GameObject.Find("Spawner").GetComponent<SpawnerNPS>();
            spawnerNPS._countSpawnedPeople = 100;

            _challangeList = GameObject.Find("ChallangeList").transform;
            string message = "Идите в магазин";
            if (_language == "en")
            {
                message = "Go to the store";
            }
            InstantiateChallangeInList(message, 0);

            InstantiateTeachTrigger(posTriggers[0]);

            playerItemsController = GameObject.Find("PlayerItemsController").GetComponent<PlayerItemsController>();
            _playersPolksTransform = GameObject.Find("PlayerPolks").transform;

            _playersPolks = _playersPolksTransform.GetComponentsInChildren<TriggerPolka>();
        }
    }
    Transform _playersPolksTransform;
    TriggerPolka[] _playersPolks;
    public void SwitchState()
    {
        _stageTeach++;

        switch (_stageTeach)
        {
            case 0:
                {

                    break;
                }
            case 1:
                {
                    winChallange(0);
                    string message = "Идите к боссу";
                    if (_language == "en")
                    {
                        message = "Go to the boss";
                    }
                    InstantiateChallangeInList(message, 1);
                    InstantiateTeachTrigger(posTriggers[1]);

                    _strelkaScript.SetTarget(posTriggers[1]);
                    break;
                }
            case 2:
                {
                    winChallange(1);
                    string message2 = "Поговорите с боссом";
                    if (_language == "en")
                    {
                        message2 = "Talk to the boss";
                    }
                    InstantiateChallangeInList(message2, 2);
                    InstantiateTeachTrigger(posTriggers[2]);
                    _strelkaScript.SetTarget(posTriggers[2]);
                    // Разговор с боссом
                    break;
                }
            case 3:
                {
                    Instantiate(_BossTalk);
                    winChallange(2);
                    string message2 = "Идите на склад";
                    if (_language == "en")
                    {
                        message2 = "Go to the warehouse";
                    }
                    InstantiateChallangeInList(message2, 3);
                    InstantiateTeachTrigger(posTriggers[3]);
                    _strelkaScript.SetTarget(posTriggers[3]);

                    break;
                }
            case 4:
                {
                    InstantiateTeachTrigger(posTriggers[4]);
                    _strelkaScript.SetTarget(posTriggers[4]);
                    break;
                }
            case 5:
                {
                    winChallange(3);
                    string message2 = "Разместите коробки на пустых полках";
                    if (_language == "en")
                    {
                        message2 = "Place the boxes on empty shelves";
                    }
                    InstantiateChallangeInList(message2, 4);
                    //метка куда складывать
                    break;
                }
            case 6:
                {
                    winChallange(4);
                    string message2 = "Поговорите с боссом";
                    if (_language == "en")
                    {
                        message2 = "Talk to the boss";
                    }
                    InstantiateChallangeInList(message2, 5);
                    InstantiateTeachTrigger(posTriggers[2]);
                    _strelkaScript.SetTarget(posTriggers[2]);
                    //разговор
                    break;
                }
            case 7:
                {
                    Instantiate(_BossTalk);
                    winChallange(5);
                    string message2 = "Дождитесь первого покупателя";
                    if (_language == "en")
                    {
                        message2 = "Wait for the first customer";
                    }
                    InstantiateChallangeInList(message2, 6);

                    spawnerNPS.CreateNPSChar();
                    StartCoroutine(wait2sec());
                    StartCoroutine(WaitSpawnCustomer());

                    break;
                }
            case 8:
                {
                    Instantiate(_BossTalk);
                    winChallange(9);
                    string message2 = "Идите на склад и пополните корзину";
                    if (_language == "en")
                    {
                        message2 = "Go to the warehouse and replenish the basket";
                    }
                    InstantiateChallangeInList(message2, 10);

                    if (GameObject.FindGameObjectsWithTag("TriggerBottleImage").Length == 0)
                    {
                        _triggerBottleImageObj = Instantiate(_triggerBottleImage);
                    }

                    InstantiateTeachTrigger(posTriggers[4]);
                    _strelkaScript.SetTarget(posTriggers[4]);
                    //gметка где пополнять
                    break;
                }
            case 9:
                {
                    StartCoroutine(CheckPlayerBoxCountBottles());
                    break;
                }
            case 10:
                {
                    Instantiate(_BossTalk);
                    winChallange(12);
                    string message2 = "Возьмите деньги из кассы";
                    if (_language == "en")
                    {
                        message2 = "Take the money from the cash register";
                    }
                    InstantiateChallangeInList(message2, 13);
                    GameObject.Find("Kassir0").GetComponent<KassController>().AddMoney(12000);

                    _strelkaScript.SetTarget(new Vector3(13.33f, -33.562f, 6.6618f));

                    StartCoroutine(TakeMoney());
                    break;
                }
            case 11:
                {
                    Instantiate(_BossTalk);
                    winChallange(15);
                    string message2 = "Дождитесь хулигана";
                    if (_language == "en")
                    {
                        message2 = "Wait for the bully";
                    }
                    InstantiateChallangeInList(message2, 16);
                    spawnerNPS.CreateNPSChar();
                    StartCoroutine(WaitSpawnAnonimus());
                    break;
                }
            case 12:
                {
                    Instantiate(_BossTalk);
                    winChallange(19);

                    GameObject.Find("MoneyBlock").GetComponentInChildren<TextMoneyMark>().UpdateMoneyText("+", _loadedInfo._ZPDay.ToString());


                    StartCoroutine(WaitSec());

                    Instantiate(_GoHomeTrigger);
                    string message2 = "Стажировочный день закончен, идите домой";
                    if (_language == "en")
                    {
                        message2 = "The internship day is over, go home";
                    }
                    InstantiateChallangeInList(message2, 20);
                    _strelkaScript.SetTarget(new Vector3(40.859f, -33.72606f, 8.05f));
                    break;
                }

        }

    }

    IEnumerator WaitSec()
    {
        yield return new WaitForSeconds(2);
        if (_loadedInfo.PlayerInfo._countDays == 0)
        {
            Destroy(GameObject.Find("ZpBlock").GetComponentInChildren<zpBlockController>().gameObject);
        }
        else 
        {
            Destroy(_loadedInfo.GetComponentInChildren<zpBlockController>().gameObject); 
        }

        Destroy(gameObject);
    }

    [SerializeField] private GameObject _GoHomeTrigger;

    private IEnumerator wait2sec()
    {
        yield return new WaitForSeconds(2);

        Instantiate(_subTeachStatsCanvas);
    }

    private IEnumerator WaitSpawnAnonimus()
    {
        yield return new WaitForSeconds(1);

        _loadedInfo.PlayerInfo.changeAnonimus = 101;

        foreach (GameObject _npsTemp in GameObject.FindGameObjectsWithTag("NPS"))
        {
            if (_npsTemp.GetComponent<MoveMassovka>())
            {
                _nps = _npsTemp;
            }
        }
        NavMeshAgent _agent = _nps.GetComponent<NavMeshAgent>();
        _agent.speed = 3;
        _nps.GetComponent<MoveMassovka>().isCustomer=true;
       Destroy(_nps.GetComponent<CheckPunch>());

        while (GameObject.FindGameObjectWithTag("LuzhaTrigger") == false)
        {
            yield return new WaitForSeconds(1);
        }
        winChallange(16);
        string message2 = "Прогоните хулигана! (ударом)";
        if (_language == "en")
        {
            message2 = "Get rid of the bully! (with a blow)";
        }
        InstantiateChallangeInList(message2, 17);
        CheckPunch checkPunch = _nps.AddComponent<CheckPunch>();
        _agent.speed = 1;
        while (checkPunch._goAway == false)
        {
            yield return new WaitForSeconds(1);
        }
        winChallange(17);
        string message32 = "Вытрите все лужи шваброй (Швабра в инвентаре)";
        if (_language == "en")
        {
            message32 = "Wipe all the puddles with a mop (The mop is in the inventory)";
        }
        InstantiateChallangeInList(message32, 18);
        while (GameObject.FindGameObjectWithTag("LuzhaTrigger"))
        {
            yield return new WaitForSeconds(1);
        }
        winChallange(18);
        string message3 = "Поговорите с боссом";
        if (_language == "en")
        {
            message3 = "Talk to the boss";
        }
        InstantiateChallangeInList(message3, 19);
        InstantiateTeachTrigger(posTriggers[2]);
        _strelkaScript.SetTarget(posTriggers[2]);

    }
    private IEnumerator TakeMoney()
    {
        bool haveMoney = false;
        while (haveMoney == false)
        {
            foreach(string str in _loadedInfo.PlayerInfo.itemsNameInRecourses)
            {
                if (str == "money")
                {
                    haveMoney = true;
                }
            }

            yield return new WaitForSeconds(1f);
        }

        winChallange(13);
        string message3 = "Отнесите деньги в сейф (Кабинет босса)";
        if (_language == "en")
        {
            message3 = "Take the money to the safe (Boss's office)";
        }
        InstantiateChallangeInList(message3, 14);
        _strelkaScript.SetTarget(new Vector3(22.04134f, -32.552f, -14.271f));
        StartCoroutine(GoToSafe());
    }

    private IEnumerator GoToSafe()
    {
        bool haveMoney = true;
        while (haveMoney == true)
        {
            haveMoney = false;
            foreach (string str in _loadedInfo.PlayerInfo.itemsNameInRecourses)
            {
                if (str == "money")
                {
                    haveMoney = true;
                }
            }

            yield return new WaitForSeconds(1f);
        }

        winChallange(14);
        string message3 = "Поговорите с боссом";
        if (_language == "en")
        {
            message3 = "Talk to the boss";
        }
        InstantiateChallangeInList(message3, 15);

        InstantiateTeachTrigger(posTriggers[2]);
        _strelkaScript.SetTarget(posTriggers[2]);
    }

    private IEnumerator CheckPlayerBoxCountBottles()
    {
        while (playerItemsController.playerBoxItemCountBottles<11)
        {
            yield return new WaitForSeconds(0.5f);
        }
        winChallange(10);
        Destroy(_triggerBottleImageObj);
        string message3 = "Вы проголодались, купите еду в зоне сотрудников и съешьте её";
        if (_language == "en")
        {
            message3 = "You are hungry, buy food in the employee area and eat it";
        }
        InstantiateChallangeInList(message3, 11);
        _strelkaScript.SetTarget(new Vector3(18.79001f, -33.6621f, -16.45001f));
        StartCoroutine(EatFood());
    }

    private IEnumerator EatFood()
    {
        while (_loadedInfo.PlayerInfo._countBlockedItems == 3)
        {
            yield return new WaitForSeconds(1);
        }
        while (_loadedInfo.PlayerInfo._countBlockedItems != 3)
        {
            _strelkaScript.SetTarget(Vector3.zero);
            yield return new WaitForSeconds(1);
        }
        winChallange(11);
        string message3 = "Поговорите с боссом";
        if (_language == "en")
        {
            message3 = "Talk to the boss";
        }
        InstantiateChallangeInList(message3, 12);
        InstantiateTeachTrigger(posTriggers[2]);
        _strelkaScript.SetTarget(posTriggers[2]);
    }

    private IEnumerator WaitSpawnCustomer()
    {
        yield return new WaitForSeconds(1);

        _loadedInfo.PlayerInfo.changeAnonimus = -1;

        foreach (GameObject _npsTemp in GameObject.FindGameObjectsWithTag("NPS"))
        {
            if (_npsTemp.GetComponent<MoveMassovka>())
            {
                _nps = _npsTemp;
            }
        }
        NavMeshAgent agent = _nps.GetComponent<NavMeshAgent>();
        agent.speed = 3;
        Destroy(_nps.GetComponent<MoveMassovka>());
        CustomerMoveScript customerMoveScript = _nps.AddComponent<CustomerMoveScript>();
        _loadedInfo.PlayerInfo.changeGoToBottle = 101;
        customerMoveScript._chanceBrokeBottle = -1;
       Destroy(customerMoveScript.GetComponent<CheckPunch>());

        StartCoroutine(CheckCustomerState());
    }
    
    private IEnumerator CheckCustomerState()
    {
        while(_nps.GetComponent<CustomerGoToKassa>() == false)
        {
            yield return new WaitForSeconds(0.5f);
        }
        winChallange(6);
        _strelkaInventory = Instantiate(_strelkaPrefabInventory, GameObject.Find("Canvas").transform);
        string message3 = "Возьмите в руки корзину с бутылками (В инвентаре)";
        if (_language == "en")
        {
            message3 = "Pick up a basket of bottles (In the inventory)";
        }
        InstantiateChallangeInList(message3, 7);

        StartCoroutine(CheckHandsPlayer());
    }

    private PlayerItemsController playerItemsController;
    private IEnumerator CheckHandsPlayer()
    {
        yield return new WaitForSeconds(2f);
        while (playerItemsController.selectedItem!=1)
        {
            yield return new WaitForSeconds(0.5f);
        }
        winChallange(7);
        Destroy(_strelkaInventory);
        string message3 = "Заполните пустое место на полках";
        if (_language == "en")
        {
            message3 = "Fill the empty space on the shelves";
        }
        InstantiateChallangeInList(message3, 8);

        StartCoroutine(CheckPolks());
    }

    private IEnumerator CheckPolks()
    {
        int _count = 3;
        while (_count != 0)
        {
            _count = 0;
            foreach (TriggerPolka triggerPolka in _playersPolks)
            {
                if (triggerPolka._positionTakedBottles.Count > 0)
                {
                    _count++;
                }
            }
            yield return new WaitForSeconds(1);
        }
        winChallange(8);
        string message3 = "Поговорите с боссом";
        if (_language == "en")
        {
            message3 = "Talk to the boss";
        }
        InstantiateChallangeInList(message3, 9);
        InstantiateTeachTrigger(posTriggers[2]);
        _strelkaScript.SetTarget(posTriggers[2]);
    }

    private void winChallange(int time)
    {
        foreach (ChallangeController _challange in _challangeList.GetComponentsInChildren<ChallangeController>())
        {
            if (_challange.time == time)
            {
                _challange.SwitchAnimWin();
            }
        }
    }

    private void InstantiateTeachTrigger(Vector3 _posTrigger)
    {
        GameObject _obj = Instantiate(TriggerTeach);
        _obj.transform.position = _posTrigger;
        _obj.GetComponent<TeachTrigger>().teachLVL=this;
    }
    private void InstantiateChallangeInList(string text, int targetTime)
    {
        GameObject _objChallange = Instantiate(_challangeObjPrefab, _challangeList);
        _objChallange.GetComponentInChildren<TMP_Text>().text = text;
        _objChallange.GetComponent<ChallangeController>().time = targetTime;
    }

}
