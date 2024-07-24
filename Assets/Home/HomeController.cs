
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeController : MonoBehaviour
{
    [SerializeField] private GameObject _card;
    [SerializeField] private Transform _content;

    [SerializeField] private Transform _canvasLerua;

    [SerializeField] private GameObject[] _Kitchen;
    [SerializeField] private GameObject[] _Bed;
    [SerializeField] private GameObject[] _MainRoom;
    [SerializeField] private GameObject[] _Toilet;
    [SerializeField] private GameObject[] _PC;

    [SerializeField] private GameObject _messageError;

    private AudioSource _audioSource;

    private LoadedInfo _loadedInfo;
    private string _language;

    private GameObject _player;

    private string _kitchenString;
    private string _bedString;
    private string _mainRoomString;
    private string _toiletString;
    private string _PCString;

    private int[,] typeSkidka = new int[6,3];//тип, конкретно какое, размер скидки в процентах

    [SerializeField] private GameObject _triggerGoToWork;
    [SerializeField] private GameObject _triggerGoToSleep;

    [SerializeField] private GameObject _startDayPrefab;

    [SerializeField] private GameObject _openDoorAudio;

    [SerializeField] private TMP_Text _moneyValueInLerua;

    private string[,] masForTranslate;
    private bool CreateSkidki()
    {
        bool repeat=false;
        for(int i = 0; i < 6; i++)
        {
            int type = UnityEngine.Random.Range(0, 5);
            typeSkidka[i, 0] = type;
            switch (type)
            {
                case 0:
                    typeSkidka[i, 1]= UnityEngine.Random.Range(0, 4);
                    break;
                case 1:
                    typeSkidka[i, 1] = UnityEngine.Random.Range(0, 9);
                    break;
                case 2:
                    typeSkidka[i, 1] = UnityEngine.Random.Range(0, 8);
                    break;
                case 3:
                    typeSkidka[i, 1] = UnityEngine.Random.Range(0, 4);
                    break;
                case 4:
                    typeSkidka[i, 1] = UnityEngine.Random.Range(0, 4);
                    break;
            }
            if (i > 0)
            {
                for (int j = i-1; j >= 0; j--)
                {
                    if ((typeSkidka[i, 1] == typeSkidka[j, 1])&& (typeSkidka[i, 0] == typeSkidka[j, 0]))
                    {
                        repeat = true;
                    }
                }
            }

            typeSkidka[i, 2] = UnityEngine.Random.Range(1, 50);
        }
        return repeat;
    }
    private string NoMoneyText;
    private string WasBuyText;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();

        if (_loadedInfo._Language == "en")
        {
            NoMoneyText = "There is not enough money!";
            WasBuyText = "Purchased";
            _language = "en";

            masForTranslate = new string[26, 2]
            {
                { "Двуспальная кровать","Double bed" },
                { "Кондиционер","Conditioner" },
                { "Компьютерный стол","Computer desk" },
                { "Лимонное дерево","Lemon Tree" },
                { "Настольное растение","Table plant" },
                { "Беговая дорожка","Treadmill" },
                { "Картины","Paintings" },
                { "Ковёр","Carpet" },
                { "Светильник напольный","Outdoor lamp" },
                { "Комнатные растения","Indoor plants" },
                { "Книжный шкаф","Bookcase" },
                { "Телескоп","Telescope" },
                { "Прикроватная тумба","Bedside table" },
                { "Кухонный блок","Kitchen unit" },
                { "Холодильник","Fridge" },
                { "Обеденный стол","The dining table" },
                { "Коврик","Carpet" },
                { "Зеркало","Mirror" },
                { "Ванна","Bath" },
                { "Полка","Shelf" },
                { "Телевизор с комодом","TV with chest of drawers" },
                { "Диван","Sofa" },
                { "Ужасный компьютер","A terrible computer" },
                { "Плохой компьютер","A bad computer" },
                { "Нормальный компьютер","A normal computer" },
                { "Крутой компьютер","Cool computer" },
            };
        }
        else
        {
            _language = "ru";
            NoMoneyText = "Не хватает денег!";
            WasBuyText = "Куплено";
        }

         _moneyValueInLerua.text = _loadedInfo.PlayerInfo.money.ToString();

        _player = GameObject.Find("Player");

        if (_loadedInfo._dayIsStart == false)
        {
            StartCoroutine(WaitEndFrameForCursor());
          
            GameObject _audioObg = Instantiate(_openDoorAudio);
            AudioSource[] _sources = _audioObg.GetComponentsInChildren<AudioSource>();

            foreach (AudioSource source in _sources)
            {
                if (source.name == "OpenDoor1")
                {
                    source.Play();
                    break;
                }
            }

            while (CreateSkidki())
            {
                CreateSkidki();
            }
            CharacterController characterController = _player.GetComponent<CharacterController>();
            characterController.enabled = false;
            _player.transform.position = new Vector3(15.206f, -38.72f, 0.96f);
            _player.transform.rotation = Quaternion.Euler(0, -90,0);
            characterController.enabled = true;
            Instantiate(_triggerGoToSleep);

            Destroy(GameObject.Find("TimerController"));
        }
        else
        {
            
            if (_loadedInfo.PlayerInfo._countDays != 0)
            {
                _loadedInfo.ShowFullScreenAdd();

                Instantiate(_startDayPrefab);

                _loadedInfo.PlayerInfo._statHP += _loadedInfo.PlayerInfo.changeHPStatInEndDay;
                _loadedInfo.PlayerInfo._statHangry += _loadedInfo.PlayerInfo.changeFoodStatInEndDay;
                if (_loadedInfo.PlayerInfo._statHangry < 0)
                {
                    _loadedInfo.PlayerInfo._statHangry = 0;
                }
                if (_loadedInfo.PlayerInfo._statHP < 10)
                {
                    _loadedInfo.PlayerInfo._statHP = 10;
                }
                if (_loadedInfo.PlayerInfo._statHP >100)
                {
                    _loadedInfo.PlayerInfo._statHP = 100;
                }
                StartCoroutine(WaitEndFrame());

                StartCoroutine(WaitEndFrameForCursor());
            }
            if (_loadedInfo.PlayerInfo._countDays == 1)
            {
                _loadedInfo.PlayerInfo.startZPDay =3000;
                _loadedInfo.PlayerInfo.changeCustomer = 30;
                _loadedInfo.PlayerInfo.maxCountMoveCustomer = 5;
                _loadedInfo.PlayerInfo.changeAnonimus = 5;
                _loadedInfo.PlayerInfo.changeGoToBottle = 30;
                _loadedInfo.PlayerInfo.maxSpawnBoxes = 10;
            }
            _loadedInfo.SafeInfoForLoadLastDay();
            _loadedInfo.startMoney = _loadedInfo.PlayerInfo.money;
            _loadedInfo._ZPDay = _loadedInfo.PlayerInfo.startZPDay;
            Instantiate(_triggerGoToWork);
        }

        _kitchenString = _loadedInfo.PlayerInfo._homeShopsKitchen;

        InstantiateObjs(_kitchenString, _Kitchen);
        _bedString = _loadedInfo.PlayerInfo._homeShopsBed;

        InstantiateObjs(_bedString,_Bed);
        _mainRoomString = _loadedInfo.PlayerInfo._homeShopsMainRoom;

        InstantiateObjs(_mainRoomString,_MainRoom);
        _toiletString = _loadedInfo.PlayerInfo._homeShopsToilet;

        InstantiateObjs(_toiletString, _Toilet);

        _PCString = _loadedInfo.PlayerInfo._homeShopsPC;



        for (int i = 3; i >= 0; i--)
        {
            if (_PCString[i].ToString() == "1")
            {
                Destroy(GameObject.FindWithTag("PC"));
                Instantiate(_PC[i]);
                break;
            }
        }

        InstntiateSpisok("MainRoom", _mainRoomString,2);

    }
    IEnumerator WaitEndFrameForCursor()
    {
        yield return new WaitForEndOfFrame();

        if (_loadedInfo._isDesktop)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    IEnumerator WaitEndFrame()
    {
        yield return new WaitForEndOfFrame();
        GameObject.Find("HPSlider").GetComponent<Slider>().value = _loadedInfo.PlayerInfo._statHP;
        GameObject.Find("FoodSlider").GetComponent<Slider>().value = _loadedInfo.PlayerInfo._statHangry;
    }
    private void InstantiateObjs(string _tempString,GameObject[] type)
    {
        int id = 0;
        foreach (char a in _tempString)
        {
            if (a.ToString() == "1")
            {
                if (type[id].name == "CompTable")
                {
                     Destroy(GameObject.Find("Table"));
                } else if (type[id].name == "BigBed")
                {
                        Destroy(GameObject.Find("Bed"));
                }
                    Instantiate(type[id]);
            }
            id++;
        }
    }


    public void OnClickButton(string type)
    {
        GameObject[] _objs = GameObject.FindGameObjectsWithTag("BlockTovar");
        foreach (GameObject _obj in _objs)
        {
            Destroy(_obj);
        }
        if(type == "Bed")
        {
            InstntiateSpisok(type,_bedString,1);
        } else if(type == "Kitchen")
        {
            InstntiateSpisok(type, _kitchenString,0);
        }
        else if(type == "MainRoom")
        {
            InstntiateSpisok(type, _mainRoomString,2);
        }
        else if (type == "Toilet")
        {
            InstntiateSpisok(type, _toiletString,3);
        }
        else if (type =="PC")
        {
            InstntiateSpisok(type, _PCString,4);
        }
    }
    
    public void OnClickBuy(int price,string nameButton,int hp,int food)
    {
       
        if (price> _loadedInfo.PlayerInfo.money)
        {
           GameObject _obj = Instantiate(_messageError, _canvasLerua);
            _obj.GetComponent<TMP_Text>().text = NoMoneyText;
        }
        else
        {
            _audioSource.Play();

            _loadedInfo._rashodsLerua += price;

            _loadedInfo.PlayerInfo.changeHPStatInEndDay += hp;
            _loadedInfo.PlayerInfo.changeFoodStatInEndDay += food;
            GameObject[] _objs = GameObject.FindGameObjectsWithTag("BlockTovar");
            foreach (GameObject _obj in _objs)
            {
                Destroy(_obj);
            }

            _loadedInfo.PlayerInfo.money -= price;
            _moneyValueInLerua.text = _loadedInfo.PlayerInfo.money.ToString();
            int id = (int)Char.GetNumericValue(nameButton[nameButton.Length-1]);
           string type = nameButton.Substring(0, nameButton.Length - 1);

            Debug.Log(type);

            if (type == "Bed")
            {
                _bedString = _bedString.Remove(id, 1).Insert(id, "1");
                if (_Bed[id].name == "BigBed")
                {
                    Destroy(GameObject.Find("Bed"));
                }
                Instantiate(_Bed[id]);

                InstntiateSpisok(type, _bedString,1);
                _loadedInfo.PlayerInfo._homeShopsBed = _bedString;
            }
            else if (type == "Kitchen")
            {
                _kitchenString = _kitchenString.Remove(id, 1).Insert(id, "1");

                Instantiate(_Kitchen[id]);

                InstntiateSpisok(type, _kitchenString,0);
                _loadedInfo.PlayerInfo._homeShopsKitchen = _kitchenString;
            }
            else if (type == "MainRoom")
            {
                _mainRoomString = _mainRoomString.Remove(id, 1).Insert(id, "1");
                
                if (_MainRoom[id].name == "CompTable")
                {
                    Destroy(GameObject.Find("Table"));
                }
                Instantiate(_MainRoom[id]);

                InstntiateSpisok(type, _mainRoomString,2);
                _loadedInfo.PlayerInfo._homeShopsMainRoom = _mainRoomString;
            }
            else if (type == "Toilet")
            {
                _toiletString = _toiletString.Remove(id, 1).Insert(id, "1");

                InstntiateSpisok(type, _toiletString,3);

                Instantiate(_Toilet[id]);
                _loadedInfo.PlayerInfo._homeShopsToilet = _toiletString;
            }
            else if (type == "PC")
            {
                if (id == 3)
                {
                    _PCString = "1111";
                }else if (id == 2)
                {
                    _PCString = "1110";
                }
                else
                {
                    _PCString = "1100";
                }
                InstntiateSpisok(type, _PCString,4);

                Destroy(GameObject.FindWithTag("PC"));
                Instantiate(_PC[id]);
                _loadedInfo.PlayerInfo._homeShopsPC = _PCString;
            }
        }
    }

    private void InstntiateSpisok(string type,string targetString,int idType)
    {
        for (int i = 0; i < targetString.Length; i++)
        {
            ScriptableObj _scriptableObj = Resources.Load<ScriptableObj>("Home/Cards/" + type + i.ToString());

            GameObject _obj = Instantiate(_card, _content);
            TMP_Text[] _texts = _obj.GetComponentsInChildren<TMP_Text>();

            Image[] _images = _obj.GetComponentsInChildren<Image>();
            Button _button = _obj.GetComponentInChildren<Button>();
            if (targetString[i].ToString() == "1")
            {
                _button.interactable = false;
                _button.GetComponentInChildren<TMP_Text>().text = WasBuyText;
            }
            else
            {
                _button.name = type + i.ToString();
            }

            foreach (TMP_Text _text in _texts)
            {
                if (_text.name == "FoodText")
                {
                    _text.text = _scriptableObj.foodStats;
                }
                else if (_text.name == "MoneyText")
                {
                    _text.text = _scriptableObj.Coast;

                    for (int k = 0; k < typeSkidka.Length/3; k++)
                    {
                        if ((idType == typeSkidka[k, 0]))
                        {
                            if (i == typeSkidka[k, 1])
                            {
                                AddSkidka(_text.transform, typeSkidka[k, 2], int.Parse(_scriptableObj.Coast));
                            }
                                
                        }
                    }

                }
                else if (_text.name == "HPText")
                {
                    _text.text = _scriptableObj.HPStats;
                }
                else if (_text.name == "Name")
                {
                    if (_language == "en")
                    {
                        string _name = _scriptableObj.Name;

                        for(int j=0;j<masForTranslate.Length;j++)
                        {
                            if (masForTranslate[j,0]== _name)
                            {
                                _text.text = masForTranslate[j,1];
                                break;
                            }
                        }

                    }
                    else
                    {
                        _text.text = _scriptableObj.Name;
                    }
                    
                }
            }

            foreach (Image _image in _images)
            {
                if (_image.name == "SpriteCard")
                {
                    _image.sprite = _scriptableObj.UIImage;
                    break;
                }
            }
        }
    }
    [SerializeField] private GameObject _skidkaObj;
    private void AddSkidka(Transform _parent,int procent,int _startCoast)
    {
       GameObject _obj = Instantiate(_skidkaObj,_parent);

        _obj.GetComponentInChildren<TMP_Text>().text = Convert.ToInt32(_startCoast * ((100f-procent)/100f)).ToString();
    }

}
