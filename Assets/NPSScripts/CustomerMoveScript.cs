using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.AI;

public class CustomerMoveScript : MonoBehaviour
{
    private LoadedInfo _loadedInfo;
    private NavMeshAgent agent;

    private SpawnerNPS _spawnerNPS;

    public int countMove = 5;

    private int faktCountMove = 0;

    private bool CanMove = true;

    public int _chanceGoToBottles = 30; //уровень сложности покупателя, сколько раз подойдёт к бутылкам

    public int _chanceBrokeBottle = 5; //уровень сложности покупателя, шанс разбить бутылку

    private bool _inShop = false;

    private int countBottlesPolki;
    private int countFakePolki;

    private Transform _handTransform;

    GameObject _objkorzinka;
    private AudioSource _audioSource;

    public bool haveOnKorzinka;

    public Coroutine _job;

    private void Awake()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();

        if (_loadedInfo.PlayerInfo._countDays > 5)
        {
            _chanceBrokeBottle = 5;
        }
        else
        {
            _chanceBrokeBottle = 2;
        }

        if (_loadedInfo.PlayerInfo.changeAnonimus > Random.Range(0, 101))
        {
            gameObject.AddComponent<AnonimusScript>();

            Destroy(this);
        }
    }
    void Start()
    {
        _chanceGoToBottles = _loadedInfo.PlayerInfo.changeGoToBottle;

        countMove = Random.Range(1, _loadedInfo.PlayerInfo.maxCountMoveCustomer);

        _audioSource = GetComponent<AudioSource>();

        _animator = GetComponent<Animator>();
        _spawnerNPS = GameObject.Find("Spawner").GetComponent<SpawnerNPS>();

        countFakePolki = _spawnerNPS._FakePolksPoints.Length;
        countBottlesPolki = _spawnerNPS._BottlePolksPoints.Length;

        agent = GetComponent<NavMeshAgent>();

        targetMoveTransform = _spawnerNPS.KorzinkaPoint;
        _targetMoveVector = targetMoveTransform.position;

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

                if (Random.Range(0, 100) < _chanceGoToBottles)
                    {
                    targetMoveTransform = _spawnerNPS._BottlePolksPoints[Random.Range(0, countBottlesPolki)];
                    }
                    else
                    {
                    targetMoveTransform = _spawnerNPS._FakePolksPoints[Random.Range(0, countFakePolki)];
                    }

                _targetMoveVector = targetMoveTransform.position;
                faktCountMove++;

                if (_inShop)
                {
                    string targetPointName = _targetPoint.name;
                    if (targetPointName == "FakePolka")
                    {
                        _job = StartCoroutine(WaitCanMove("-1"));
                    }
                    else
                    {
                        _job = StartCoroutine(WaitCanMove(targetPointName.Substring(targetPointName.Length - 1)));
                    }
                }
                else
                {
                    _inShop = true;
                    _job = StartCoroutine(WaitCanMoveTakeKorzinka());
                }
                
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
    Animator _animator;

    public GameObject _objKorzinkaInNPS;
    IEnumerator WaitCanMoveTakeKorzinka()
    {
        _handTransform = GetComponentInChildren<HandMarker>().transform;
        _objkorzinka = _spawnerNPS.ObjKorzinka;
        CanMove = false;
        _animator.SetBool("IsMoving", false);
        _audioSource.Stop();
        yield return new WaitForSeconds(1);
        _objKorzinkaInNPS = Instantiate(_objkorzinka, _handTransform);
        CanMove = true;
        _animator.SetBool("IsMoving", true);
        _audioSource.Play();
    }
    IEnumerator WaitCanMove(string _nameProduct)
    {
        CanMove = false;
        _animator.SetBool("IsMoving", false);
        _audioSource.Stop();

        yield return new WaitForSeconds(Random.Range(1f, 3f));
        if (_nameProduct == "-1")
        {
            for (int i = 0; i <= (Random.Range(0, 4)); i++)
            {
                CreateFakeProduct();

                yield return new WaitForSeconds(Random.Range(1f, 3f));
            }
        }
        else
        {
            GameObject _polkaObj= GameObject.Find("polka" + _nameProduct);

            MeshRenderer[] _objs = _polkaObj.GetComponentsInChildren<MeshRenderer>(); //ошибка
            if(_objs.Length > 6)
            {
                if (haveOnKorzinka == false)
                {
                    haveOnKorzinka = true;
                }
                for (int i = 0; i <= (Random.Range(0, 4)); i++)
                {
                    _objs = _polkaObj.GetComponentsInChildren<MeshRenderer>();

                    GameObject _tempObjBottle = _objs[Random.Range(1, _objs.Length)].gameObject; //обращается к уничтоженому объекту мб исправил

                    if (_tempObjBottle.tag == "Untagged")
                    {
                        if (Random.Range(0, 101) < _chanceBrokeBottle)
                        {
                            BrokeBottle(_tempObjBottle, _polkaObj);
                        }
                        else
                        {
                            TakeBottle(_tempObjBottle, _polkaObj);
                        }

                        yield return new WaitForSeconds(Random.Range(1f, 3f));
                    }
                    else
                    {
                        i--;
                        yield return new WaitForSeconds(0.1f);
                    }

                }
            }
            else
            {
                //большой штраф триггер

                yield return new WaitForSeconds(0.1f);
            }

        }
        CanMove = true;
        _animator.SetBool("IsMoving", true);
        _audioSource.Play();
        if (faktCountMove >= countMove)
        {
            CustomerGoToKassa _tempScript = gameObject.AddComponent<CustomerGoToKassa>();
            _tempScript._objKorzinka = _objKorzinkaInNPS;
            Destroy(this);
        }
    }

    private void TakeBottle(GameObject bottleType,GameObject _polka)
    {
        _polka.GetComponent<TriggerPolka>()._positionTakedBottles.Add(bottleType.transform.position);

        bottleType.tag = "TakedNPSBottle";
        bottleType.transform.parent = _objKorzinkaInNPS.transform;
        bottleType.AddComponent<MovePorduct>();
    }

    private void BrokeBottle(GameObject bottleType, GameObject _polka)
    {
        _polka.GetComponent<TriggerPolka>()._positionTakedBottles.Add(bottleType.transform.position);

        bottleType.transform.localPosition += new Vector3(0, 0, 0.4f);
        bottleType.transform.localRotation = Quaternion.Euler(Random.Range(0,180), Random.Range(0, 180), Random.Range(0, 180));
        bottleType.transform.parent = null;
        bottleType.AddComponent<CapsuleCollider>();
        bottleType.AddComponent<Rigidbody>();
        bottleType.AddComponent<BrokenBottleScript>();
    }

    private void CreateFakeProduct()
    {
        GameObject _tempObj = Instantiate(_spawnerNPS._FakeProductsMas[Random.Range(0, 10)], _targetPoint);

        _tempObj.transform.localPosition = new Vector3(0, Random.Range(0.09f, 1.351f), 0.781f);

        _tempObj.transform.parent = _objKorzinkaInNPS.transform;
        _tempObj.AddComponent<MovePorduct>();
    }
}
