using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _errorMessage;

    [SerializeField] private GameObject _objButtonMoney;
    private Transform _parentButton;



    [SerializeField] private float _distanceShow;

    private Transform _player;

    private bool canTakeMoney = false;
    private bool showMessage = false;

    [SerializeField] private GameObject _messagePC;

    [SerializeField] private GameObject _messageMobile;

    private GameObject _showedMessage;

    private LoadedInfo _loadedInfo;
    private bool _isDesktop;

    private Transform _messageCanvas;

    private int _maxItemsInventory;

    private PlayerItemsController _playerItemsController;

    private GameObject _imageMobileButton;

    private string _language;
    private void Start()
    {
        _messageCanvas = GameObject.Find("Canvas").transform;
        _playerItemsController = GameObject.Find("PlayerItemsController").GetComponent<PlayerItemsController>();

        _parentButton = GameObject.Find("ParrentButtons").transform;
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();

        _language = _loadedInfo._Language;

        _maxItemsInventory = _loadedInfo.PlayerInfo._countMaxItems;

        _isDesktop = _loadedInfo._isDesktop;
        if (_isDesktop == false)
        {
            _imageMobileButton = Resources.Load<GameObject>("mobileImages/TakeMoney");
        }
        _player = GameObject.Find("Player").transform;
    }
    private void FixedUpdate()
    {
            if (Vector3.Distance(transform.position, _player.position) < _distanceShow)
            {
                canTakeMoney = true;

                if (showMessage == false)
                {
                    ShowMessage();
                    showMessage = true;
                }

            }
            else
            {
                if (canTakeMoney)
                {
                    canTakeMoney = false;
                    showMessage = false;
                    DestroyMessage();
                }
            }

    }
    private void Update()
    {
        if (_isDesktop)
        {
            if (canTakeMoney)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    TakeMoney();
                }
            }
        }
        else
        {
            if (canTakeMoney)
            {
                if (_buttonScript.IsClicked==true)
                {
                    _buttonScript.IsClicked = false;
                    TakeMoney();
                }
            }
        }
    }

    public void TakeMoney()
    {
        int _countBlockedItems = _loadedInfo.PlayerInfo._countBlockedItems;
        if (_countBlockedItems < _maxItemsInventory)
        {
            _countBlockedItems++;
            GameObject _objButton = Instantiate(_objButtonMoney, _parentButton);

            _objButton.name = _countBlockedItems.ToString();

            _playerItemsController.TakeMoney(_objButton.transform);

            _loadedInfo.PlayerInfo._countBlockedItems = _countBlockedItems;
            _loadedInfo.PlayerInfo.itemsNameInRecourses[_countBlockedItems - 1] = "money";

            GetComponentInParent<KassController>().TakeMoney();

            DestroyMessage();
            Destroy(gameObject);
        }
        else
        {
            GameObject _error = Instantiate(_errorMessage, _messageCanvas);
            string message = "Не хватает места!";
            if (_language == "en")
            {
                message = "There is not enough space!";
            }
            _error.GetComponent<TMP_Text>().text = message;
        }

    }
    private MobileButtonScript _buttonScript;

    private void ShowMessage()
    {
        if (_isDesktop)
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Message");
            if (objs.Length > 0)
            {
                foreach (GameObject _obj in objs)
                {
                    Destroy(_obj);
                }
            }

            _showedMessage = Instantiate(_messagePC, _messageCanvas);
            string message = "У - Взять деньги";
            if (_language == "en")
            {
                message = "E - Take the money";
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
}
