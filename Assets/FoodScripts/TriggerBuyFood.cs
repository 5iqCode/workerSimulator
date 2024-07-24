using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class TriggerBuyFood : MonoBehaviour
{
    [SerializeField] private GameObject _errorMessage; 
    private Transform _parentButton;

    private bool _isShowFood;

    private Transform _player;

    private bool canBuyFood = false;
    private bool showMessage = false;

    [SerializeField] private GameObject _messagePC;

    [SerializeField] private GameObject _messageMobile;

    private GameObject _showedMessage;

    private LoadedInfo _loadedInfo;
    private bool _isDesktop;

    private Transform _messageCanvas;
    private Transform _canvas;

    private int _maxItemsInventory;

    private MoveMainHero _moveMainHero;

    [SerializeField] private Camera _cameraFood;

    private Vector3 _defoultPos;

    private PlayerItemsController _playerItemsController;

    private TimerScript _timerScript;

    private AudioSource _audioSource;

    private PunchScript _punchScript;

    private GameObject _mobileCanvas;

    private GameObject _imageMobileButton;

    private string _language;
    private void Awake()
    {
        _cameraFood.gameObject.SetActive(false);
    }
    private void Start()
    {
        _messageCanvas = GameObject.Find("CanvasTime").transform;
        _canvas = GameObject.Find("Canvas").transform;
        _parentButton = GameObject.Find("ParrentButtons").transform;
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        _language = _loadedInfo._Language;
        _timerScript = _loadedInfo.GetComponentInChildren<TimerScript>();

        _maxItemsInventory = _loadedInfo.PlayerInfo._countMaxItems;

        _isDesktop = _loadedInfo._isDesktop;
        if (_isDesktop == false)
        {
            _imageMobileButton = Resources.Load<GameObject>("mobileImages/OpenShop");
        }
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _playerItemsController = GameObject.Find("PlayerItemsController").GetComponent<PlayerItemsController>();

        _moveMainHero = _player.GetComponent<MoveMainHero>();

        _punchScript = _player.GetComponent<PunchScript>();

        _audioSource = GetComponent<AudioSource>();

        if (_loadedInfo._isDesktop == false)
        {
            _mobileCanvas = GameObject.Find("MobileCanvas");
        }
    }
    private void FixedUpdate()
    {
        if (_isShowFood)
        {
            if ((_player.position.x != _defoultPos.x) || ((_player.position.z != _defoultPos.z)))
            {
                StopCheckMagazin();
            }
              if(_playerItemsController.selectedItem != 0)
                {
                    StopCheckMagazin();
                }
        }

    }
    public void OnClickStopScheck()
    {
        StopCheckMagazin();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            {
            canBuyFood = true;

                if (showMessage == false)
                {
                    ShowMessage();
                    showMessage = true;
                }
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (canBuyFood)
            {
                canBuyFood = false;
                showMessage = false;
                DestroyMessage();
            }
        }
    }

        private void Update()
    {
        if (_isDesktop)
        {
            if (canBuyFood)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ShowFood();

                    if (_isDesktop)
                    {
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                    }
                }
            }
        }
        else
        {
            if (canBuyFood)
            {
                if (_buttonScript.IsClicked == true)
                {
                    _mobileCanvas.SetActive(false);
                    _buttonScript.IsClicked = false;
                    ShowFood();
                }
            }
        }
    }

    public void ShowFood()
    {
        
        _playerItemsController.TakeNothing();

        _isShowFood = true;

        _messageCanvas.GetComponent<Canvas>().enabled =false;

        _cameraFood.gameObject.SetActive(true);
        _player.GetComponentInChildren<Camera>().enabled = false;

        DestroyMessage();

        _defoultPos = _player.position;

        _moveMainHero.CanMove = false;
        StartCoroutine(WainTime());
        _punchScript.ShowMessagePunchMobile(false);
        _punchScript._canPanch = false;
    }

    public void StopCheckMagazin()
    {
        if (_isDesktop)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            _mobileCanvas.SetActive(true);
        }

        _isShowFood = false;
        _messageCanvas.GetComponent<Canvas>().enabled=true;

        _cameraFood.gameObject.SetActive(false);
        _player.GetComponentInChildren<Camera>().enabled = true;

        if (canBuyFood)
        {
            canBuyFood = false;
            showMessage = false;
            DestroyMessage();
        }

        if (_playerItemsController.selectedItem == 0)
        {
            _punchScript._canPanch = true;
            _punchScript.ShowMessagePunchMobile(true);
        }
        
    }

    private IEnumerator WainTime()
    {
        yield return new WaitForSeconds(0.5f);

        _defoultPos = _player.position;

        _moveMainHero.CanMove = true;
    }
    private MobileButtonScript _buttonScript;
    private void ShowMessage()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Message");
        if (objs.Length > 0)
        {
            foreach (GameObject _obj in objs)
            {
                Destroy(_obj);
            }
        }

        if (_isDesktop)
        {
            _showedMessage = Instantiate(_messagePC, _messageCanvas);
            string message = "У - Купить еду";
            if (_language == "en")
            {
                message = "E - Buy food";
            }
            _showedMessage.GetComponentInChildren<TMP_Text>().text = message;
        }
        else
        {
            _showedMessage = Instantiate(_messageMobile, _messageCanvas);
            Instantiate(_imageMobileButton, _showedMessage.transform);
            _buttonScript = _showedMessage.GetComponent<MobileButtonScript>();
        }

    }

    private void DestroyMessage()
    {
        if (_showedMessage != null)
        {
            Destroy(_showedMessage);
        }

    }
    [SerializeField] private GameObject _objButtonFood;
    public void buyFoodButtonClick(string nameFood)
    {
        int _countBlockedItems = _loadedInfo.PlayerInfo._countBlockedItems;
        if (_countBlockedItems < _maxItemsInventory)
        {
            int price = 0;
            int _idImg = 0;
            if (nameFood == "burger")
            {
                price = 90;
                _idImg = 0;
            }
            else if (nameFood == "potatoFri")
            {
                price = 125;
                _idImg = 1;
            }
            else if (nameFood == "pizza")
            {
                price = 190;
                _idImg = 2;
            }
            else if (nameFood == "shawa")
            {
                price = 250;
                _idImg = 3;
            }
            else if (nameFood == "cake")
            {
                price = 400;
                _idImg = 4;
            }
            else
            {
                price = 580;
                _idImg = 5;
            }
            if(_loadedInfo.PlayerInfo.money>=price)
            {
                _audioSource.Play();

                _loadedInfo._rashodsFood += price;
                _loadedInfo.PlayerInfo.money-=price;

                _canvas.GetComponentInChildren<TextMoneyMark>().UpdateMoneyText("-", price.ToString());

                _countBlockedItems++;
                GameObject _objButton = Instantiate(_objButtonFood, _parentButton);

                _objButton.name = _countBlockedItems.ToString();

                _objButton.GetComponent<ButtonsClickInPrefab>().nameFood = nameFood;

                _objButton.GetComponent<Button>().onClick.Invoke();

                _loadedInfo.PlayerInfo._countBlockedItems = _countBlockedItems;
                _loadedInfo.PlayerInfo.itemsNameInRecourses[_countBlockedItems - 1] = nameFood;

                Instantiate(_playerItemsController._imagesFood[_idImg], _objButton.transform);

                StopCheckMagazin();
            }
            else
            {
                GameObject _error = Instantiate(_errorMessage, _canvas);

                string message = "Не хватает денег!";
                if (_language == "en")
                {
                    message = "There is not enough money!";
                }
                _error.GetComponent<TMP_Text>().text = message;
            }

        }
        else
        {
            GameObject _error = Instantiate(_errorMessage, _canvas);
            string message = "Не хватает места!";
            if (_language == "en")
            {
                message = "There is not enough space!";
            }
            _error.GetComponent<TMP_Text>().text = message;
        }
    }
}
