using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Jobs;
using UnityEngine;

public class EatFoodScript : MonoBehaviour
{
    private LoadedInfo _loadedInfo;

    private Animator _playerAnimator;

    private Transform _messageCanvas;

    [SerializeField] private GameObject _eatSound;

    [SerializeField] private GameObject _messagePC;

    [SerializeField] private GameObject _messageMobile;

    private GameObject _showedMessage;

    private bool _isDesktop;

    private Transform _player;
    private MoveMainHero _moveMainHero;

    private PlayerItemsController _playerItemsController;
    private GameObject _imageMobileButton;

    private string _language;
    void Start()
    {
        _messageCanvas = GameObject.Find("Canvas").transform;

        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        _language = _loadedInfo._Language;
        _isDesktop = _loadedInfo._isDesktop;
        if (_isDesktop == false)
        {
            _imageMobileButton = Resources.Load<GameObject>("mobileImages/EatFood");
        }
        ShowMessage();

        _player = GameObject.FindGameObjectWithTag("Player").transform;


        _moveMainHero = _player.GetComponent<MoveMainHero>();
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
            string message = "У - Съесть еду";
            if (_language == "en")
            {
                message = "E - To eat food";
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
    // Update is called once per frame
    void Update()
    {
        if (_isDesktop)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartEat();
            }
        }
        else
        {
            if(_buttonScript.IsClicked == true)
            {
                _buttonScript.IsClicked = false;

                StartEat();
            }
        }
    }
    private int _nameButtonId;

    private Coroutine _job;
    public void StartEat()
    {
        Destroy(_showedMessage);
        _playerAnimator = GameObject.FindGameObjectWithTag("ModelPlayer").GetComponent<Animator>();
        _playerItemsController = GameObject.Find("PlayerItemsController").GetComponent<PlayerItemsController>();

        _nameButtonId = _loadedInfo.PlayerInfo._selectedItem+1;

        _playerAnimator.SetBool("IsEating", true);

        _job = StartCoroutine(waitTimeEat());
    }

    private void StopEat()
    {
        _messageCanvas.GetComponentInChildren<StatsController>().EatFood(_loadedInfo.PlayerInfo.itemsNameInRecourses[_loadedInfo.PlayerInfo._selectedItem]);


        _playerItemsController.DestroyButton(_nameButtonId);
        _playerAnimator.SetBool("IsEating", false);
    }

    IEnumerator waitTimeEat()
    {
        _moveMainHero.CanMove = false;
        yield return new WaitForSeconds(2f);
        Instantiate(_eatSound);
        yield return new WaitForSeconds(1);
        _moveMainHero.CanMove = true;
        StopEat();
    }

    private void OnDestroy()
    {
        if( _job != null)
        {
            StopCoroutine(_job);

            _playerAnimator.SetBool("IsEating", false);

            _moveMainHero.CanMove = true;
        }
        else
        {
            Destroy(_showedMessage);
        }
    }
}
