using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BossCheckMagaz : MonoBehaviour
{
    private int shtrafZa1Bottle=100;
    private int shtrafZa1Luzha=250;
    private int shtrafZaSafe=500;
    private int shtrafZaKass = 350;

    public Transform[] _BossPath;// старт, бутылки, кассы, сейф, конец

    public BossController _BossController;

    private bool CanMove = true;
    private float _targetRotationY;

    private Transform _targetMoveTransform;

    private NavMeshAgent agent;

    Animator _animator;

    private int _countPoints = 1;

    private SpawnerBottlesInPolks _spawnerBottles;

    private zpBlockController _zpBlock;
    private LoadedInfo _loadedInfo;
    private StatsController _statBlock;

    private PauseScript _pausedScript;

    private AudioSource[] _bossSound;
    private AudioSource _loseSound;

    private string _language;

    private int pointToShtrafBottle = 2, pointToShtrafKass = 3;
    void Start()
    {
        if (Random.Range(0, 100f) > 50)
        {
            pointToShtrafKass = 2;
            pointToShtrafBottle = 3;

            Transform _temp = _BossPath[2];
            _BossPath[2] = _BossPath[1];
            _BossPath[1] = _temp;
        }
        _bossSound = GetComponentsInChildren<AudioSource>();
        _loseSound = GameObject.Find("LoseSound").GetComponent<AudioSource>();

        _pausedScript = GameObject.Find("PauseButton").GetComponent<PauseScript>();


        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();

        _language = _loadedInfo._Language;

        _zpBlock = _loadedInfo.GetComponentInChildren<zpBlockController>();
        _statBlock = GameObject.Find("Canvas").GetComponentInChildren<StatsController>();


        _spawnerBottles = GameObject.Find("Spawner").GetComponent<SpawnerBottlesInPolks>();


        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();

        _animator.SetBool("PlayPC", false);
        _animator.SetBool("Move", true);

        _targetMoveTransform = _BossPath[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            if (Vector3.Distance(transform.position, _targetMoveTransform.position) < 0.5f)
            {
                if (_countPoints < 4)
                {
                    StartCoroutine(WaitCanMove());

                    _targetRotationY = _targetMoveTransform.rotation.eulerAngles.y;

                    _countPoints++;
                    _targetMoveTransform = _BossPath[_countPoints];
                }
                else 
                {
                    _animator.SetBool("Move", false);
                    _animator.SetBool("PlayPC", true);
                    Destroy(this);
                }
            }
            else
            {
                agent.destination = _targetMoveTransform.position;
            }


        }
        else
        {
            agent.transform.eulerAngles = new Vector3(0, _targetRotationY, 0);
        }
    }
    private void TalkBad()
    {
        _animator.SetTrigger("TalkBad");
        _bossSound[Random.Range(0,3)].Play();
        _loseSound.Play();
    }

    private IEnumerator WaitCanMove()
    {
        CanMove = false;
        _animator.SetBool("Move", false);
        yield return new WaitForSeconds(1f);

        Debug.Log(_countPoints);
        if(_countPoints == pointToShtrafBottle)
        {
            int _countLuzha = GameObject.FindGameObjectsWithTag("LuzhaTrigger").Length;
            if (_countLuzha > 0)
            {
                int countMinusMoney = shtrafZa1Luzha * _countLuzha;

                TalkBad();
                _zpBlock.MinusMoney(countMinusMoney);

                string message = "Штраф за грязный пол";
                if (_language == "en")
                {
                    message = "Penalty for dirty floor";
                }
                _statBlock.MinusHP(countMinusMoney / 100, message);

                yield return new WaitForSeconds(5f);
            }
            else
            {
                yield return new WaitForSeconds(2f);
            }
            int _countMinusMoneyBottlesbottles = 0;
            foreach (Transform _transform in _spawnerBottles._PolkiMasTransform)
            {
                int _countbottles =  _transform.GetComponent<TriggerPolka>()._positionTakedBottles.Count;

                if (_countbottles > 3)
                {
                    _countMinusMoneyBottlesbottles+= shtrafZa1Bottle * (_countbottles - 3);
                }
            }
            if(_countMinusMoneyBottlesbottles > 0)
            {
                TalkBad();
                string message = "Штраф за пустое место на полках";
                if (_language == "en")
                {
                    message = "Penalty for empty space on the shelves";
                }

                _zpBlock.MinusMoney(_countMinusMoneyBottlesbottles);

                _statBlock.MinusHP(_countMinusMoneyBottlesbottles / 100, message);

                yield return new WaitForSeconds(5f);
            }
            else
            {
                yield return new WaitForSeconds(2f);
            }
        }
        else if (_countPoints == pointToShtrafKass)
        {
           int _countMoneyTriggers= GameObject.FindGameObjectsWithTag("MoneyTrigger").Length;
            if(_countMoneyTriggers > 0)
            {
                int countMinusMoney = _countMoneyTriggers * shtrafZaKass;

                TalkBad();
                _zpBlock.MinusMoney(countMinusMoney);
                string message = "Штраф за большое количество денег в кассах";
                if (_language == "en")
                {
                    message = "A fine for a large amount of money in the cash register";
                }

                _statBlock.MinusHP(countMinusMoney / 100, message);

                yield return new WaitForSeconds(5f);
            }
            else
            {
                yield return new WaitForSeconds(2f);
            }
        }
        else if( _countPoints == 4)
        {
            int _countMoney = 0;
            foreach (string _str in _loadedInfo.PlayerInfo.itemsNameInRecourses)
            {
                if (_str == "money")
                {
                    _countMoney++;
                }
            }
            if (_countMoney>0)
            {
                int countMinusMoney = shtrafZaSafe;

                TalkBad();
                _zpBlock.MinusMoney(countMinusMoney);
                string message = "Штраф за нехватку денег в сейфе";
                if (_language == "en")
                {
                    message = "Penalty for lack of money in the safe";
                }

                _statBlock.MinusHP(countMinusMoney / 100, message );

                yield return new WaitForSeconds(5f);
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }

        CanMove = true;
        _animator.SetBool("Move", true);
    }

    private void OnDestroy()
    {
        if (pointToShtrafKass == 2)
        {
            pointToShtrafBottle = 2; 
            pointToShtrafKass = 3;
            Transform _temp = _BossPath[2];
            _BossPath[2] = _BossPath[1];
            _BossPath[1] = _temp;
        }
        if (SceneManager.sceneCount < 2)
        {
            _BossController.SetDefPosBoss();


            if (_loadedInfo.shtrafsForFirst == 0)
            {
                _loadedInfo.typePause = 1;
                foreach (int value in _loadedInfo._shtrafsValue)
                {
                    _loadedInfo.shtrafsForFirst += value;
                }

            }
            else
            {
                _loadedInfo.typePause = 2;
                foreach (int value in _loadedInfo._shtrafsValue)
                {
                    _loadedInfo.shtrafsForSecond += value;
                }
            }
            _pausedScript.ClickPause();
        }

    }
}
