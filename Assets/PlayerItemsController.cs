
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemsController : MonoBehaviour
{
    [SerializeField] private GameObject _selectedObjUIpewiu;

    private GameObject _selectedObjUIpewiuRealObj;

    [SerializeField] private GameObject _objButtonMoney;
    [SerializeField] private GameObject _objButtonFood;

    [SerializeField] private GameObject shwabraItem;
    [SerializeField] private GameObject moneyItem;
    [SerializeField] private GameObject playerBoxItem;

    private LoadedInfo _loadedInfo;

    [SerializeField] private GameObject _parentButtons;

    public int maxPlayerBoxItemCountBottles = 11;
    public int playerBoxItemCountBottles;

    private Transform _transformTakeRHand;

    private GameObject _modelPlayer;
    private Animator _playerAnimator;

    private bool _targetAnimWithBox = false;

    public int selectedItem = 0; //0 рука; 1 - бокс с водкой; 2 - швабра; 3 - деньги; 4 - еда

    private Vector3[,] _BoxItemBottlePosRot = new Vector3[11,2];
    [SerializeField] private GameObject[] _BoxItemBottleModel;

    [SerializeField] private GameObject[] _foodModel;

    public bool _isTakedBoxOnCar = false;
    public GameObject _TakedBoxOnCarGO;

    private MoveMainHero _moveMainHero;

    private PunchScript _playerPunchScript;

    private AudioSource _switchItemSound;

    public GameObject[] _imagesFood;
    private void Awake()
    {
        SetDefPosRotBottleInBoxItem();
    }
    private void SetDefPosRotBottleInBoxItem()
    {
        _BoxItemBottlePosRot[0, 0] = new Vector3(-0.1780222f, 0.01999998f, -0.1129999f);
        _BoxItemBottlePosRot[0, 1] = Vector3.zero;
        _BoxItemBottlePosRot[1, 0] = new Vector3(-0.1780222f, 0.01999998f, 0.06699991f);
        _BoxItemBottlePosRot[1, 1] = Vector3.zero;
        _BoxItemBottlePosRot[2, 0] = new Vector3(0.001098822f, 0.01999998f, -0.09700012f);
        _BoxItemBottlePosRot[2, 1] = Vector3.zero;
        _BoxItemBottlePosRot[3, 0] = new Vector3(0.001098822f, 0.01999998f, 0.1069999f);
        _BoxItemBottlePosRot[3, 1] = Vector3.zero;
        _BoxItemBottlePosRot[4, 0] = new Vector3(0.1802198f, 0.01999998f, -0.1129999f);
        _BoxItemBottlePosRot[4, 1] = Vector3.zero;
        _BoxItemBottlePosRot[5, 0] = new Vector3(0.1802198f, 0.01999998f, 0.07800007f);
        _BoxItemBottlePosRot[5, 1] = Vector3.zero;
        _BoxItemBottlePosRot[6, 0] = new Vector3(-0.1527472f, 0.3334f, -0.06960011f);
        _BoxItemBottlePosRot[6, 1] = new Vector3(70.108f, 9.177f, -47.912f);
        _BoxItemBottlePosRot[7, 0] = new Vector3(0.2740661f, 0.3069f, -0.0008997917f);
        _BoxItemBottlePosRot[7, 1] = new Vector3(-9.624f, -12.433f, 45.529f);
        _BoxItemBottlePosRot[8, 0] = new Vector3(-0.2562638f, 0.3380001f, 0.1842003f);
        _BoxItemBottlePosRot[8, 1] = new Vector3(47.161f, 94.594f, -36.85f);
        _BoxItemBottlePosRot[9, 0] = new Vector3(-0.0179f, 0.3069f, -0.2407f);
        _BoxItemBottlePosRot[9, 1] = new Vector3(-11.709f, 102.514f, 36.714f);
        _BoxItemBottlePosRot[10, 0] = new Vector3(0.2648f, 0.3344f, 0.1488f);
        _BoxItemBottlePosRot[10, 1] = new Vector3(-1.658f, 10.05f, 76.782f);
    }

    private void AddObwodka(string _buttonId)
    {
        Transform _buttonTransform = null;
        foreach (Button _button in _parentButtons.GetComponentsInChildren<Button>())
        {
            if (_button.name == _buttonId)
            {
                _buttonTransform = _button.transform;
                break;
            }
        }
        if (_selectedObjUIpewiuRealObj != null)
        {
            Debug.Log(322);
            Destroy(_selectedObjUIpewiuRealObj);
        }

        _selectedObjUIpewiuRealObj = Instantiate(_selectedObjUIpewiu, _buttonTransform);
    }
    private void Start()
    {

        AddObwodka("1");
        _switchItemSound = GetComponent<AudioSource>();
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();

        _modelPlayer = GameObject.FindGameObjectWithTag("ModelPlayer");

        _transformTakeRHand = _modelPlayer.GetComponentInChildren<HandMarker>().transform;

        _playerAnimator = _modelPlayer.GetComponent<Animator>();

        _moveMainHero = _modelPlayer.GetComponentInParent<MoveMainHero>();

        _playerPunchScript = _moveMainHero.GetComponent<PunchScript>();

        if (_loadedInfo.PlayerInfo._countBlockedItems > 3)
        {
            for(int i = 3; i < 6; i++)
            {
                if (_loadedInfo.PlayerInfo.itemsNameInRecourses[i] == "money")
                {
                    GameObject _objButton = Instantiate(_objButtonMoney, _parentButtons.transform);
                    _objButton.name = (i + 1).ToString();
                }
                else if (_loadedInfo.PlayerInfo.itemsNameInRecourses[i] != "")
                {
                    GameObject _objButton = Instantiate(_objButtonFood, _parentButtons.transform);

                    _objButton.name = (i + 1).ToString();

                    string nameFood = _loadedInfo.PlayerInfo.itemsNameInRecourses[i];
                    int _idImg;
                    _objButton.GetComponent<ButtonsClickInPrefab>().nameFood = nameFood;

                    if (nameFood == "burger")
                    {
                        _idImg = 0;
                    }
                    else if (nameFood == "potatoFri")
                    {
                        _idImg = 1;
                    }
                    else if (nameFood == "pizza")
                    {
                        _idImg = 2;
                    }
                    else if (nameFood == "shawa")
                    {
                        _idImg = 3;
                    }
                    else if (nameFood == "cake")
                    {
                        _idImg = 4;
                    }
                    else
                    {
                        _idImg = 5;
                    }

                    Instantiate(_imagesFood[_idImg],_objButton.transform);
                }
                

                
            }
        }
    }

    private void Update()
    {
        if (_loadedInfo._isDesktop)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchItemsPC(1);
            } else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchItemsPC(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SwitchItemsPC(3);
            } else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SwitchItemsPC(4);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SwitchItemsPC(5);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SwitchItemsPC(6);
            }
        }
    }

    private void SwitchItemsPC(int _numberKey)
    {
        int _countBlockedItems = _loadedInfo.PlayerInfo._countBlockedItems;
        if (_numberKey> _countBlockedItems)
        {
            TakeNothing();
        }
        else
        {
          foreach(Transform _transform in _parentButtons.GetComponentsInChildren<Transform>())
            {
                if(_transform.name == _numberKey.ToString())
                {
                    _transform.GetComponent<Button>().onClick.Invoke();
                }
            }


        }
    }


    GameObject _boxObj;
    public void TakePlayerBoxItem()
    {
        AddObwodka("2");
        _playerPunchScript.ShowMessagePunchMobile(false);
        if (selectedItem != 1)
        {
            _moveMainHero._speed = 3;

            _loadedInfo.PlayerInfo._selectedItem = 1;
            _targetAnimWithBox = true;

            ClearFinger();
            CheckCarBox();
            _boxObj = Instantiate(playerBoxItem, _transformTakeRHand);

            selectedItem = 1;
            AddBottlesInBox();
        }
    }


    private void AddBottlesInBox()
    {
        playerBoxItemCountBottles = _loadedInfo.PlayerInfo._countPlayerItemsBoxBottles;
        for (int i =0;i< playerBoxItemCountBottles; i++)
        {
            GameObject _tempBottle = Instantiate(_BoxItemBottleModel[Random.Range(0, 6)], _boxObj.transform);
            _tempBottle.tag = "bottleInHandPlayer";
            _tempBottle.transform.localPosition = _BoxItemBottlePosRot[i, 0];
            _tempBottle.transform.localRotation = Quaternion.Euler(_BoxItemBottlePosRot[i, 1]);
            _tempBottle.name = i.ToString();

        }
    }
    public void TakeShwabraItem()
    {
        AddObwodka("3");
        _playerPunchScript.ShowMessagePunchMobile(false);
        _moveMainHero._speed = 4;
        _loadedInfo.PlayerInfo._selectedItem = 2;
        _targetAnimWithBox = false;

        ClearFinger();

        Instantiate(shwabraItem, _transformTakeRHand);

        selectedItem = 2;
        CheckCarBox();
    }

    private void CheckCarBox()
    {
        if (_isTakedBoxOnCar)
        {
            _TakedBoxOnCarGO.GetComponent<CarBoxScript>().DropBox();
        }
    }

    public void TakeCarBox()
    {
        _switchItemSound.Play();
        _playerPunchScript.ShowMessagePunchMobile(false);
        _moveMainHero._speed = 2;
        _loadedInfo.PlayerInfo._selectedItem = 0;

        _targetAnimWithBox = true;

        ClearFinger();
        selectedItem = 0;

    }

    public void TakeNothing()
    {
        AddObwodka("1");
        _playerPunchScript.ShowMessagePunchMobile(true);
        _moveMainHero._speed = 4;
        _loadedInfo.PlayerInfo._selectedItem = 0;

        _targetAnimWithBox = false;

        ClearFinger();
        selectedItem = 0;
        CheckCarBox();
    }
    public void TakeMoney(Transform _posButton)
    {
        Destroy(_selectedObjUIpewiuRealObj);
        _selectedObjUIpewiuRealObj = Instantiate(_selectedObjUIpewiu, _posButton);

        _switchItemSound.Play();
        _playerPunchScript.ShowMessagePunchMobile(false);
        _moveMainHero._speed = 4;
        _targetAnimWithBox = false;

        ClearFinger();

        Instantiate(moneyItem, _transformTakeRHand);

        selectedItem = 3;
        CheckCarBox();
    }

    public string _selectedFoodModelName;
    public void TakeFood(string nameFood,Transform _posButton)
    {
        Destroy(_selectedObjUIpewiuRealObj);
        _selectedObjUIpewiuRealObj = Instantiate(_selectedObjUIpewiu, _posButton);

        _playerPunchScript.ShowMessagePunchMobile(false);
        _moveMainHero._speed = 4;
        _targetAnimWithBox = false;

        ClearFinger();

        foreach (GameObject obj in _foodModel)
        {
            if(obj.name == nameFood)
            {
                _selectedFoodModelName = nameFood;
                Instantiate(obj, _transformTakeRHand);
            }
        }

        selectedItem = 4;
        CheckCarBox();
    }

    public void DestroyButton(int _idButton)
    {
        _targetAnimWithBox = false;
        _loadedInfo.PlayerInfo._countBlockedItems--;

        foreach (Button _transform in _parentButtons.GetComponentsInChildren<Button>())
        {
           int idTransf = int.Parse(_transform.name);
            if (idTransf >= _idButton)
            {
                if(idTransf == _idButton)
                {
                    Destroy(_transform.gameObject);
                    if (idTransf != 6)
                    {
                        _loadedInfo.PlayerInfo.itemsNameInRecourses[_idButton - 1] = _loadedInfo.PlayerInfo.itemsNameInRecourses[_idButton];
                    }    
                    else
                    {
                        _loadedInfo.PlayerInfo.itemsNameInRecourses[_idButton - 1] = "";
                    }
                }
                else
                {
                    if (idTransf != 6)
                    {
                        _loadedInfo.PlayerInfo.itemsNameInRecourses[idTransf - 1] = _loadedInfo.PlayerInfo.itemsNameInRecourses[idTransf];
                    }
                    else
                    {
                        _loadedInfo.PlayerInfo.itemsNameInRecourses[idTransf - 1] = "";
                    }
                    _transform.name = (idTransf-1).ToString();
                    
                }
            }
        }
        TakeNothing();
    }

    private void TryChangeAnimation()
    {
        bool currentAnimWithBox = _playerAnimator.GetBool("WithBox");
        if (currentAnimWithBox!= _targetAnimWithBox)
        {
            _playerAnimator.SetBool("WithBox",_targetAnimWithBox);
        }
    }

    public void ClearFinger()
    {
        GameObject[] _objs = GameObject.FindGameObjectsWithTag("ItemPlayer");

        if (_objs.Length > 0)
        {
            foreach (GameObject obj in _objs)
            {
                Destroy(obj);
            }
        }
        TryChangeAnimation();
    }
}

