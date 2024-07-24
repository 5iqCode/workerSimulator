using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LuzhaTrigger : MonoBehaviour
{

    [SerializeField] private float _distanceShow;


    private Transform _player;

    private bool canWish = false;
    private bool showMessage = false;

    [SerializeField] private GameObject _messagePC;

    [SerializeField] private GameObject _messageMobile;

    private GameObject _showedMessage;

    private LoadedInfo _loadedInfo;
    private bool _isDesktop;

    private Transform _messageCanvas;


    public float HP_Luzha = 100;

    private float HP_Luzha_defoult;
    private bool isWishing = false;
    private Animator _playerAnimator;
    private Vector3 _defoultPos;

    private PlayerItemsController _playerItemsController;

    private Transform _shwabra;
    private AudioSource _shwabraWishSound;
    private Vector3 _shwabraDefPosition;
    private Quaternion _shwabraDefRotation;
    private Vector3 _shwabraWishPosition = new Vector3(0.442f,0.57f,0.183f);
    private Quaternion _shwabraWishRotation = Quaternion.Euler(new Vector3(182.618f, -31.67103f, 35.631f));
    [SerializeField] private Transform _lusha;
    private Vector3 _lushaLocalScaleDef;
    private MoveMainHero _moveMainHero;

    [SerializeField] private Slider _progressSlider;

    private Vector3 _defoultPosShwabra;
    private GameObject _imageMobileButton;

    private string _language;
    private void Start()
    {
        HP_Luzha_defoult = HP_Luzha;
        _lushaLocalScaleDef = _lusha.localScale;

        _player = GameObject.FindGameObjectWithTag("Player").transform;


        _moveMainHero = _player.GetComponent<MoveMainHero>();
        _playerItemsController = GameObject.Find("PlayerItemsController").GetComponent<PlayerItemsController>();

        _messageCanvas = GameObject.Find("Canvas").transform;

        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        _language = _loadedInfo._Language;

        _isDesktop = _loadedInfo._isDesktop;
        if (_isDesktop == false)
        {
            _imageMobileButton = Resources.Load<GameObject>("mobileImages/ClearFloor");
        }
        _playerAnimator = GameObject.FindGameObjectWithTag("ModelPlayer").GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (_playerItemsController.selectedItem == 2)
        {
            if (isWishing == false)
            {
                if (Vector3.Distance(transform.position, _player.position) < _distanceShow)
                {
                    
                    canWish = true;

                    if (showMessage == false)
                    {
                        _shwabra = GameObject.FindGameObjectWithTag("ItemPlayer").transform;
                        _shwabraWishSound = _shwabra.GetComponent<AudioSource>();
                        ShowMessage();
                        showMessage = true;
                    }

                }
                else
                {
                    if (canWish)
                    {
                        canWish = false;
                        showMessage = false;
                        DestroyMessage();
                    }
                }
            }
            else
            {
                if ((_player.position.x != _defoultPos.x)||((_player.position.z != _defoultPos.z)))
                {
                    StopWish();
                }
            }
        }
        else
        {
            if ((isWishing)||(canWish))
            {
                if (canWish)
                {
                    canWish = false;
                    showMessage = false;
                    DestroyMessage();
                }
                if (isWishing)
                {
                    StopWish();
                }
            }
        }

    }

    private void StopWish()
    {
        
        if (_shwabra != null)
        {
            _shwabra.localPosition = _shwabraDefPosition;
            _shwabra.localRotation = _shwabraDefRotation;
            _shwabraWishSound.Stop();
        }


        isWishing = false;
        _playerAnimator.SetBool("IsWishingPol", false);
        StopCoroutine(_jobWishLizha);
    }
    private void Update()
    {
        if (_isDesktop)
        {
            if (canWish)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    WishLuzha();

                    AnimShowLuzha animShowLuzha = GetComponent<AnimShowLuzha>();
                    if (animShowLuzha != null)
                    {
                        Destroy(animShowLuzha);
                    }
                }
            }
        }
        else
        {
            if (canWish)
            {
                if (_buttonScript.IsClicked == true)
                {
                    _buttonScript.IsClicked = false;
                    WishLuzha();
                }
            }
        }
    }
    Coroutine _jobWishLizha;
    public void WishLuzha()
    {
        _shwabraWishSound.Play();
        _shwabraDefPosition = _shwabra.transform.localPosition;
        _shwabraDefRotation = _shwabra.transform.localRotation;

        _shwabra.localPosition = _shwabraWishPosition;
        _shwabra.localRotation = _shwabraWishRotation;
        _defoultPos = _player.position;

        _moveMainHero.CanMove=false;
       StartCoroutine(WainTime());

        isWishing = true;

        _playerAnimator.SetBool("IsWishingPol", true);

        DestroyMessage();

        _jobWishLizha = StartCoroutine(MinusHPLuzha());
    }
    private IEnumerator WainTime()
    {
       yield return new WaitForSeconds(0.5f);
        _moveMainHero.CanMove = true;   
    }

    private IEnumerator MinusHPLuzha()
    {
        while (HP_Luzha >= 0)
        {
            HP_Luzha -= 60 * Time.deltaTime;
            float _otnoshenie = HP_Luzha / HP_Luzha_defoult;
            _progressSlider.value = 1 - _otnoshenie;
            if (_otnoshenie > 0.1f)
            {
                _lusha.localScale = _lushaLocalScaleDef * _otnoshenie;
            }

            yield return new WaitForEndOfFrame();

            if (HP_Luzha <= 0)
            {
                StopWish();
                Destroy(gameObject);
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
            string message = "У - Вытереть лужу";
            if (_language == "en")
            {
                message = "E - Wipe up a puddle";
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

    private void OnDestroy()
    {
        if (_moveMainHero.CanMove == false)
        {
            _moveMainHero.CanMove = true;
        }
    }
}
