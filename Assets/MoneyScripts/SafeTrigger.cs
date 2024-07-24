using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SafeTrigger : MonoBehaviour
{
    [SerializeField] private Transform _door;

    private Quaternion TargetAnge = Quaternion.Euler(-90f, 0, -48.475f);
    private Quaternion _defRotation = Quaternion.Euler(-90, 0, 90);

    private bool _openDoor = false;

    private LoadedInfo _loadedInfo;

    private List<int> _idButtons = new List<int>();

    private string[] _stringLoadedInfo;

    private bool showMessage = false;

    [SerializeField] private GameObject _messagePC;

    [SerializeField] private GameObject _messageMobile;

    private GameObject _showedMessage;
    private bool _isDesktop;
    private Transform _messageCanvas;

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
            _imageMobileButton = Resources.Load<GameObject>("mobileImages/SetMoneyToSafe");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
                _stringLoadedInfo = _loadedInfo.PlayerInfo.itemsNameInRecourses;
                for (int i = 0; i < _loadedInfo.PlayerInfo._countBlockedItems; i++)
                {
                    if (_stringLoadedInfo[i] == "money")
                    {

                        _openDoor = true;
                        _idButtons.Add(i);
                    }
                }
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
                _openDoor = false;
            _idButtons.Clear();
        }
    }
    private void FixedUpdate()
    {
        if (_openDoor)
        {
            if (showMessage == false)
            {
                ShowMessage();
                showMessage = true;
            }
            if (_door.rotation != TargetAnge)
            {
                _door.rotation = Quaternion.RotateTowards(_door.rotation, TargetAnge, Time.fixedDeltaTime * 150);
            }
        }
        else
        {
            if(showMessage == true)
            {
                showMessage = false;
                DestroyMessage();
            }

            if (_defRotation != _door.rotation)
            {
                _door.rotation = Quaternion.RotateTowards(_door.rotation, _defRotation, Time.fixedDeltaTime * 100);
            }
        }
    }

    private void Update()
    {
        if (_isDesktop)
        {
            if (_openDoor)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ClearMoney();
                }
            }
        }
        else
        {
            if (_openDoor)
            {
                if (_buttonScript.IsClicked == true)
                {
                    _buttonScript.IsClicked = false;
                    ClearMoney();
                }
            }
        }
    }
    private void ClearMoney()
    {
        _playerItemsController.GetComponent<AudioSource>().Play();
        _openDoor = false;
        for (int i = _idButtons.Count - 1; i >= 0; i--)
        {
            _playerItemsController.DestroyButton(_idButtons[i] + 1);
        }

        _idButtons.Clear();
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
            string message = "У - Сложить деньги";
            if (_language == "en")
            {
                message = "E - Add up the money";
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
