using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.AI;

public class AnonimusScript : MonoBehaviour
{
    private LoadedInfo _loadedInfo;
    private NavMeshAgent agent;
    private SpawnerNPS _spawnerNPS;
    private int countBottlesPolki;
    private Animator animator;

    public int countMove = 1;

    private int faktCountMove = 0;

    private GameObject _anonimusMask;
    private AudioSource _audioSource;
    private bool CanMove = false;

    public Coroutine _job;

    private AudioSource _badSmeh;

    private int _maxCountMove;
    void Start()
    {
        _anonimusMask = Resources.Load<GameObject>("AnonimusMask");
        
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        int _countDays = _loadedInfo.PlayerInfo._countDays;
        if (_countDays > 10)
        {
            _maxCountMove = 6;
        }else if( _countDays > 4)
        {
            _maxCountMove = 4;
        }
        else 
        {
            _maxCountMove = 2;
        }

        countMove = Random.Range(1, _maxCountMove);
        if (_countDays == 0)
        {
            countMove = 6;
        }
        _spawnerNPS = GameObject.Find("Spawner").GetComponent<SpawnerNPS>();
        countBottlesPolki = _spawnerNPS._BottlePolksPoints.Length;
        animator = GetComponentInChildren<Animator>();
        animator.runtimeAnimatorController = Resources.Load("NPS/AnonimusController") as RuntimeAnimatorController;

        animator.SetTrigger("Funny");

        _audioSource = GetComponent<AudioSource>();
        _audioSource.pitch = 0.8f;
        agent = GetComponent<NavMeshAgent>();

        GameObject _objMask = Instantiate(_anonimusMask, GetComponentInChildren<Head>().transform);
        _badSmeh = _objMask.GetComponent<AudioSource>();

        targetMoveTransform = _spawnerNPS._BottlePolksPoints[Random.Range(0, countBottlesPolki)];
        _targetMoveVector = targetMoveTransform.position;
        
        StartCoroutine(WaitMoveAnonimus());
    }

    IEnumerator WaitMoveAnonimus()
    {
        yield return new WaitForSeconds(3f);

        animator.SetBool("IsRun", true);

        CanMove = true;
    }

    private Vector3 _targetMoveVector;

    private float _targetRotationY;
    private Transform _targetPoint;

    Transform targetMoveTransform;
    void Update()
    {
        if (CanMove)
        {
            if (Vector3.Distance(transform.position, _targetMoveVector) < 0.5f)
            {
                _targetRotationY = targetMoveTransform.rotation.eulerAngles.y;

                _targetPoint = targetMoveTransform;

                targetMoveTransform = _spawnerNPS._BottlePolksPoints[Random.Range(0, countBottlesPolki)];

                _targetMoveVector = targetMoveTransform.position;

                string targetPointName = _targetPoint.name;

                faktCountMove++;

                _job = StartCoroutine(WaitCanMove(targetPointName.Substring(targetPointName.Length - 1)));

            }
            else
            {
                agent.destination = _targetMoveVector;
            }

        }
        else
        {
            agent.transform.eulerAngles = new Vector3(0, _targetRotationY, 0);
        }
    }


    IEnumerator WaitCanMove(string _nameProduct)
    {
        CanMove = false;
        animator.SetBool("IsRun", false);
        _audioSource.Stop();

        yield return new WaitForSeconds(2);

            GameObject _polkaObj = GameObject.Find("polka" + _nameProduct);

            MeshRenderer[] _objs = _polkaObj.GetComponentsInChildren<MeshRenderer>(); //ошибка
            if (_objs.Length > 6)
            {
                    _objs = _polkaObj.GetComponentsInChildren<MeshRenderer>();

                    GameObject _tempObjBottle = _objs[Random.Range(1, _objs.Length)].gameObject; //обращается к уничтоженому объекту мб исправил

                    if (_tempObjBottle.tag == "Untagged")
                    {
                     BrokeBottle(_tempObjBottle, _polkaObj);

                    yield return new WaitForSeconds(Random.Range(1f, 3f));
                    }
            animator.SetTrigger("Funny");

            _badSmeh.pitch = Random.Range(0.7f, 1.2f);
            _badSmeh.Play();
            yield return new WaitForSeconds(4f);
        }

        CanMove = true;
        animator.SetBool("IsRun", true);
        _audioSource.Play();
        if (faktCountMove >= countMove)
        {
            CheckPunch _checkPunch = GetComponent<CheckPunch>();
            _checkPunch._goAway = true;
            _spawnedPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

            agent.speed = 5;
            _checkPunch._targetPoint = _spawnedPoints[Random.Range(0, _spawnedPoints.Length)].transform.position;
            Destroy(this);
        }
    }
    private GameObject[] _spawnedPoints;

    private void BrokeBottle(GameObject bottleType, GameObject _polka)
    {
        _polka.GetComponent<TriggerPolka>()._positionTakedBottles.Add(bottleType.transform.position);

        bottleType.transform.localPosition += new Vector3(0, 0, 0.4f);
        bottleType.transform.localRotation = Quaternion.Euler(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180));
        bottleType.transform.parent = null;
        bottleType.AddComponent<CapsuleCollider>();
        bottleType.AddComponent<Rigidbody>();
        bottleType.AddComponent<BrokenBottleScript>();
    }
}

