using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddBottlesToPlayerTrigger : MonoBehaviour
{
    private bool _canAddBottles;

    private LoadedInfo _loadedInfo;

    private bool showMessage = false;

    [SerializeField] private GameObject _messagePC;

    [SerializeField] private GameObject _messageMobile;

    private GameObject _showedMessage;
    private bool _isDesktop;
    private Transform _messageCanvas;

    [SerializeField] private TMP_Text _textPlayerBox;

    private PlayerItemsController _playerItemsController;
    private GameObject _imageMobileButton;
    private string _language;
    private void Start()
    {
        _playerItemsController = GameObject.Find("PlayerItemsController").GetComponent<PlayerItemsController>();

        _messageCanvas = GameObject.Find("Canvas").transform;

        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        _language = _loadedInfo._Language;
        _isDesktop = _loadedInfo._isDesktop;
        if (_isDesktop == false)
        {
            _imageMobileButton = Resources.Load<GameObject>("mobileImages/TakeBottles");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
            if (other.name == "Player")
            {
                if (_loadedInfo.PlayerInfo._countPlayerItemsBoxBottles < 11)
                {
                    showMessage = true;
                    ShowMessage();
                }
            }

    }
    private void OnTriggerExit(Collider other)
    {
            if (other.name == "Player")
            {
                if (_showedMessage != null)
                {
                    showMessage = false;
                    DestroyMessage();
                }
            }
    }
    private void Update()
    {
        if(_isDesktop)
        {
            if (showMessage)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    AddBottles();
                }
            }
        }
        else
        {
            if (showMessage)
            {
                if (_buttonScript.IsClicked==true)
                {
                    _buttonScript.IsClicked = false;
                    AddBottles();
                }
            }
        }

    }

    private void AddBottles()
    {
        _playerItemsController.GetComponent<AudioSource>().Play();
        _loadedInfo.PlayerInfo._countPlayerItemsBoxBottles = 11;
        _playerItemsController.TakeNothing();
        _playerItemsController.TakePlayerBoxItem();

        _textPlayerBox.text = _playerItemsController.playerBoxItemCountBottles.ToString();

        showMessage = false;

        GameObject _obj = GameObject.FindGameObjectWithTag("TriggerBottleImage");
        if (_obj != null)
        {
            Destroy(_obj);
        }

        DestroyMessage();
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
            string message = "У - Заполнить корзину";
            if (_language == "en")
            {
                message = "E - Fill the shopping cart";
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
