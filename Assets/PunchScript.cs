using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchScript : MonoBehaviour
{
    private LoadedInfo _loadedInfo;

    private Animator _playerAnimator;

    private Transform _messageCanvas;


    [SerializeField] private GameObject _messageMobile;

    private GameObject _showedMessage;

    private bool _isDesktop;

    private MoveMainHero _moveMainHero;

    [SerializeField] private GameObject _punchTrigger;

    private GameObject _punchTriggerGO;
    private Transform _handTransform;

    void Start()
    {
        _messageCanvas = GameObject.Find("Canvas").transform;

        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();

        _isDesktop = _loadedInfo._isDesktop;

        _moveMainHero = GetComponent<MoveMainHero>();

        _handTransform = GetComponentInChildren<HandMarker>().transform;

        _playerAnimator = GetComponentInChildren<Animator>();

        ShowMessagePunchMobile(true);
    }
    public bool _canPanch = true;

    private MobileButtonScript _buttonScript;
    public void ShowMessagePunchMobile(bool canPanch)
    {
        _canPanch = canPanch;
        if (canPanch)
        {
            if (_isDesktop == false)
            {
                _showedMessage = Instantiate(_messageMobile, _messageCanvas);

                _buttonScript = _showedMessage.GetComponent<MobileButtonScript>();
            }
        }
        else
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("PunchButton");
            if (objs.Length > 0)
            {
                foreach (GameObject _obj in objs)
                {
                    Destroy(_obj);
                }
            }

        }


    }
    // Update is called once per frame
    void Update()
    {
        if (_isDesktop)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (isPunch == false)
                {
                    StartPunch();

                }
                
            }
        }
        else
        {
            if (_buttonScript.IsClicked == true)
            {
                _buttonScript.IsClicked = false;

                if (isPunch == false)
                {
                    StartPunch();

                }
            }
        }
    }
    private bool isPunch = false;
    public void StartPunch()    
    {
        if (_canPanch)
        {
            _playerAnimator.transform.Rotate(0, 25, 0);
            
            _playerAnimator = GetComponentInChildren<Animator>();

            _playerAnimator.SetBool("Punch", true);
            isPunch = true;
            StartCoroutine(waitTimePunch());
        }
    }

    private void StopPanch()
    {
        if (_punchTriggerGO != null)
        {
            Destroy(_punchTriggerGO);
        }
        
        isPunch = false;
        _playerAnimator.SetBool("Punch", false);
    }

    IEnumerator waitTimePunch()
    {
        _moveMainHero.CanMove = false;
        yield return new WaitForSeconds(0.25f);

        _punchTriggerGO = Instantiate(_punchTrigger, _handTransform);
        yield return new WaitForSeconds(1f);

        _moveMainHero.CanMove = true;
        StopPanch();
    }
}
