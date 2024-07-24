using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TriggerGoWork : MonoBehaviour
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

    [SerializeField] private GameObject _openDoorAudio;

    private GameObject _imageMobileButton;

    private string _language;
    private void Start()
    {
        _messageCanvas = GameObject.Find("MessageCanvas").transform;
        transformChallanges = GameObject.Find("ChallangeListHome").transform;
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        _language = _loadedInfo._Language;
        _isDesktop = _loadedInfo._isDesktop;

        if(_isDesktop == false)
        {
            _imageMobileButton = Resources.Load<GameObject>("mobileImages/OpenDoor");
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

    private void Update()
    {
        if (_isDesktop)
        {
            if (canGo)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    GoWork();
                }
            }
        }
        else
        {
            if (canGo)
            {
                if (_buttonScript.IsClicked == true)
                {
                    GoWork();
                }
            }
        }
    }
    private void GoWork()
    {
        GameObject _audioObg = Instantiate(_openDoorAudio);
        AudioSource[] _sources = _audioObg.GetComponentsInChildren<AudioSource>();

        foreach (AudioSource source in _sources)
        {
            if (source.name == "OpenDoor2")
            {
                source.Play();
                break;
            }
        }


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

        _loadedInfo._dayIsStart = false;

        GameObject _blackScreenObj = Instantiate(_blackScreen);

        _blackScreenObj.GetComponent<BlackScreenController>().targetScene = "Demo_01_Lite";
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
            string message = "У - Идти на работу";
            if (_language == "en")
            {
                message = "E - Go to work";
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
