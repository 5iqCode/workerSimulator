using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class CarBoxScript : MonoBehaviour
{
    private bool canTakeBox = false;
    private bool showMessage = false;


    private Transform _player;
    [SerializeField] private float _distanceShow = 1;

    [SerializeField] private GameObject _messagePC;

    [SerializeField] private GameObject _messageMobile;

    private GameObject _showedMessage;

    private Transform _messageCanvas;

    private LoadedInfo _loadedInfo;
    private bool _isDesktop;

    private PlayerItemsController _playerItemsController;

    [SerializeField] private Vector3 _targetPosInHand;
    [SerializeField] private Vector3 _targetRotateInHand;

    private Rigidbody _rb;

    private GameObject _imageMobileButton;

    private string _language;

    public bool IsEndedBox = false;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerItemsController = GameObject.Find("PlayerItemsController").GetComponent<PlayerItemsController>();

        _player = GameObject.Find("Player").transform;


        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        _language = _loadedInfo._Language;
        _isDesktop = _loadedInfo._isDesktop;
        if (_isDesktop == false)
        {
            _imageMobileButton = Resources.Load<GameObject>("mobileImages/TakeBox");
        }
        _messageCanvas = GameObject.Find("Canvas").transform;
    }

    private void FixedUpdate()
    {
        if (IsEndedBox == false)
        {
            if (_playerItemsController._isTakedBoxOnCar == false)
            {

                if (Vector3.Distance(transform.position, _player.position) < _distanceShow)
                {
                    canTakeBox = true;

                    if (showMessage == false)
                    {
                        ShowMessage();
                        showMessage = true;
                    }

                }
                else
                {
                    if (canTakeBox)
                    {
                        canTakeBox = false;
                        showMessage = false;
                        DestroyMessage();
                    }
                }
            }

        }
    }

    private void Update()
    {
        if (IsEndedBox == false)
        {
            if (_isDesktop)
            {
                if (canTakeBox)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        TakeBox();
                    }
                }
            }
            else
            {
                if (canTakeBox)
                {
                    if (_buttonScript.IsClicked == true)
                    {
                        _buttonScript.IsClicked = false;
                        TakeBox();
                    }
                }
            }
        }
    }

    private void TakeBox()
    {

        if (_playerItemsController._isTakedBoxOnCar == false)
        {
            _playerItemsController._isTakedBoxOnCar = true;
            _playerItemsController._TakedBoxOnCarGO = gameObject;

            _playerItemsController.TakeCarBox();
            DestroyMessage();
            gameObject.layer = 8;
            _rb.isKinematic = true;
            transform.parent = _player.GetComponentInChildren<HandMarker>().transform;

            transform.localPosition = _targetPosInHand;
            transform.localRotation = Quaternion.Euler(_targetRotateInHand);
        }
        else
        {
            //messagError нет места
        }
    }

    public void DropBox()
    {
        _playerItemsController._isTakedBoxOnCar = false;
        gameObject.layer = 0;
        _rb.isKinematic = false;
        transform.parent = null;

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
            string message = "У - Взять коробку";
            if (_language == "en")
            {
                message = "E - Take the box";
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
           foreach(GameObject _obj in GameObject.FindGameObjectsWithTag("Message"))
            {
                Destroy(_obj);
            }
            
        }

    }
}
