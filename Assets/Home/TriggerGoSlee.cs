using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerGoSlee : MonoBehaviour
{
    [SerializeField] private GameObject _blackScreen;

    private bool canGo = false;
    private bool showMessage = false;

    [SerializeField] private GameObject _messagePC;

    [SerializeField] private GameObject _messageMobile;

    private GameObject _showedMessage;

    private LoadedInfo _loadedInfo;
    private bool _isDesktop;

    private Transform _messageCanvas;

    private Transform transformChallanges;
    private ChallangeController[] _challanges;

    private MoveMainHero _playerMove;

    private GameObject _imageMobileButton;

    private string _language;
    private void Start()
    {
        _messageCanvas = GameObject.Find("MessageCanvas").transform;
        transformChallanges = GameObject.Find("ChallangeListHome").transform;
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        _language = _loadedInfo._Language;
        _isDesktop = _loadedInfo._isDesktop;
        if (_isDesktop == false)
        {
            _imageMobileButton = Resources.Load<GameObject>("mobileImages/GoSleep");
        }
        _playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveMainHero>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canGo = true;

            if (showMessage == false)
            {
                ShowMessage();
                showMessage = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (canGo)
        {
            canGo = false;
            showMessage = false;
            DestroyMessage();
        }
    }

    private void GoSleep()
    {
        canGo = false;
        _playerMove.CanMove = false;
        _challanges = transformChallanges.GetComponentsInChildren<ChallangeController>();
        foreach (ChallangeController challange in _challanges)
        {
            if (challange.time == 1)
            {
                challange.SwitchAnimWin();
                break;
            }
        }
        _loadedInfo.typePause = 4;
        GameObject _blackScreenObj = Instantiate(_blackScreen);
    }

    private void Update()
    {
        if (_isDesktop)
        {
            if (canGo)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    GoSleep();
                }
            }
        }
        else
        {
            if (canGo)
            {
                if (_buttonScript.IsClicked == true)
                {
                    _buttonScript.IsClicked = false;
                    GoSleep();
                }
            }
        }
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
            string message = "У - Лечь спать";
            if (_language == "en")
            {
                message = "E - Go to bed";
            }
            _showedMessage.GetComponentInChildren<TMP_Text>().text = message;
        }
        else
        {
            if (_imageMobileButton != null)
            {
                _showedMessage = Instantiate(_messageMobile, _messageCanvas);
                Instantiate(_imageMobileButton, _showedMessage.transform);
                _buttonScript = _showedMessage.GetComponent<MobileButtonScript>();
            }

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
