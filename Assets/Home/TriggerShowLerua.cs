using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerShowLerua : MonoBehaviour
{
    private bool _isShow;

    private Transform _player;

    private bool canBuy = false;
    private bool showMessage = false;

    [SerializeField] private GameObject _messagePC;

    [SerializeField] private GameObject _messageMobile;

    private GameObject _showedMessage;

    private LoadedInfo _loadedInfo;
    private bool _isDesktop;

    [SerializeField] private Transform _messageCanvas;
    [SerializeField] private Transform _leruaCanvas;

    private StatsContrHome _statsContrHome;

    private Vector3 _defoultPos;
    private MoveMainHero _moveMainHero;

    private GameObject _imageMobileButton;

    private string _language;
    private void Awake()
    {
        _leruaCanvas.gameObject.SetActive(false);
    }

    private void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        _language = _loadedInfo._Language;
        _isDesktop = _loadedInfo._isDesktop;

        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _moveMainHero = _player.GetComponent<MoveMainHero>();

        _statsContrHome = _messageCanvas.GetComponent<StatsContrHome>();

        if (_isDesktop == false)
        {
            _imageMobileButton = Resources.Load<GameObject>("mobileImages/OpenShop");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_loadedInfo._dayIsStart)
        {
            if (other.tag == "Player")
            {
                canBuy = true;

                if (showMessage == false)
                {
                    ShowMessage();
                    showMessage = true;
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (canBuy)
        {
            canBuy = false;
            showMessage = false;
            DestroyMessage();
        }
    }
    private void FixedUpdate()
    {
        if (_isShow)
        {
            if ((_player.position.x != _defoultPos.x) || ((_player.position.z != _defoultPos.z)))
            {
                StopCheckMagazin();
            }
        }

    }

    private void Update()
    {
        if (_isDesktop)
        {
            if (canBuy)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ShowLerua();
                }
            }
        }
        else
        {
            if (canBuy)
            {
                if(_buttonScript.IsClicked == true)
                {
                    _buttonScript.IsClicked = false;
                    ShowLerua();
                }
            }
        }
    }
    [SerializeField] private Transform transformChallanges;
    private ChallangeController[] _challanges;

    public void ClickClose()
    {
        StopCheckMagazin();
    }
    public void ShowLerua()
    {

        if (_isDesktop)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }


        _challanges = transformChallanges.GetComponentsInChildren<ChallangeController>();
        foreach (ChallangeController challange in _challanges)
        {
            if (challange.time ==2)
            {
                challange.SwitchAnimWin();
                break;
            }
        }
        _isShow = true;

        _messageCanvas.gameObject.SetActive(false);

        _leruaCanvas.gameObject.SetActive(true);

        DestroyMessage();

        _defoultPos = _player.position;

        _moveMainHero.CanMove = false;

        StartCoroutine(WainTime());
    }
    public void StopCheckMagazin()
    {
        if (_isDesktop)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }

        _isShow = false;
        _messageCanvas.gameObject.SetActive(true);

        _statsContrHome.UpdateMoney();
        _statsContrHome.UpdateEndDayInfo();

        _leruaCanvas.gameObject.SetActive(false);

        if (canBuy)
        {
            canBuy = false;
            showMessage = false;
            DestroyMessage();
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
        if (_isDesktop)
        {
            _showedMessage = Instantiate(_messagePC, _messageCanvas);

            string message = "У - Посмотреть товары в интернет магазине";
            if (_language == "en")
            {
                message = "E - View products in the online store";
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
