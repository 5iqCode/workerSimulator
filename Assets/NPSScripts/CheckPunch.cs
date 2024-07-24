using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CheckPunch : MonoBehaviour
{
    private NavMeshAgent agent;

    private GameObject[] _spawnedPoints;
    public Vector3 _targetPoint;

    public bool _goAway = false;
    LoadedInfo _loadedInfo;

    AudioSource _audioSource;

    AudioSource _padenie;

    private string _language;
    private void Start()
    {

        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        _language = _loadedInfo._Language;
        agent = GetComponent<NavMeshAgent>();
    }
    public void Panched()
    {
        AudioSource[] _sources = GetComponentsInChildren<AudioSource>();
        foreach (AudioSource source in _sources)
        {
            if (source.name == "DropPeople")
            {
                _padenie = source;
                break;
            }
        }

        _spawnedPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        _targetPoint = _spawnedPoints[Random.Range(0, _spawnedPoints.Length)].transform.position;

        MoveMassovka _moveMassovka = GetComponent<MoveMassovka>();

        if( _moveMassovka != null)
        {
            Destroy(_moveMassovka);
        }
        AnonimusScript _anonimus = GetComponent<AnonimusScript>();

        if (_anonimus != null)
        {
            string message = "Вы ударили плохого покупателя!";
            if (_language == "en")
            {
                message = "You hit a bad customer!";
            }
            GameObject.Find("Canvas").GetComponentInChildren<StatsController>().PlusHP(1, message);
            try
            {
                AudioSource[] _sourcesAnonimus = agent.GetComponentsInChildren<AudioSource>();

                foreach(AudioSource sourceA in _sourcesAnonimus)
                {
                    if (sourceA.name == "AnonimusMask(Clone)")
                    {
                        sourceA.Stop();
                        break;
                    }
                }

                StopCoroutine(_anonimus._job);
            }
            catch
            {

            }
            Destroy(_anonimus);
        }

        CustomerMoveScript _customerMoveScript = GetComponent<CustomerMoveScript>();

        if( _customerMoveScript != null )
        {
            Debug.Log(322);
            if (_customerMoveScript._objKorzinkaInNPS!=null)
            {
                wasPanchCustomer();
                _customerMoveScript._objKorzinkaInNPS.transform.parent = null;

                _customerMoveScript._objKorzinkaInNPS.transform.position += Vector3.up;
                _customerMoveScript._objKorzinkaInNPS.AddComponent<Rigidbody>();
                _customerMoveScript._objKorzinkaInNPS.AddComponent<BoxCollider>();

                if (_customerMoveScript.haveOnKorzinka)
                {
                    _customerMoveScript._objKorzinkaInNPS.AddComponent<BrokenBottleScript>();
                }
                else
                {
                    _customerMoveScript._objKorzinkaInNPS.AddComponent<DestroyMessage3s>();
                }
            }
            try
            {
                StopCoroutine(_customerMoveScript._job);
            }
            catch
            {

            }

            Destroy(_customerMoveScript);
        }

        CustomerGoToKassa _customerGoToKassa = GetComponent<CustomerGoToKassa>();

        if(_customerGoToKassa != null)
        {
            StartCoroutine(Wait5sec(_customerGoToKassa));
            wasPanchCustomer();
            if (_customerGoToKassa.korzinkaInHands==true)
            {
                _customerGoToKassa._objKorzinka.transform.parent = null;

                _customerGoToKassa._objKorzinka.transform.position += Vector3.up;
                _customerGoToKassa._objKorzinka.AddComponent<Rigidbody>();
                _customerGoToKassa._objKorzinka.AddComponent<BoxCollider>();

                _customerGoToKassa._objKorzinka.AddComponent<BrokenBottleScript>();
            }
            else
            {
                Destroy(_customerGoToKassa._objKorzinka);
            }
            try
            {
                StopCoroutine(_customerGoToKassa._job);
            }
            catch
            {

            }
            Destroy(_customerGoToKassa);
        }

        agent.speed = 0;
        StartCoroutine(waitCor());
    }

    IEnumerator Wait5sec(CustomerGoToKassa _customerGoToKassa)
    {
        yield return new WaitForSeconds(5);
        if (_customerGoToKassa._objKorzinka != null)
        {
            Destroy(_customerGoToKassa._objKorzinka);
        }

    }
    IEnumerator waitCor()
    {
        yield return new WaitForSeconds(0.5f);
        _padenie.Play();
            yield return new WaitForSeconds(2f);

        yield return new WaitForSeconds(0.5f);
        _goAway = true;

            _audioSource = GetComponent<AudioSource>();

            _audioSource.pitch = 1;
            _audioSource.Play();


            GetComponent<NavMeshAgent>().speed = 5;

    }

    private void wasPanchCustomer()
    {
        _loadedInfo.GetComponentInChildren<zpBlockController>().MinusMoney(500);
        string message = "Вы ударили простого покупателя!";
        if (_language == "en")
        {
            message = "You hit a simple customer!";
        }
        GameObject.Find("Canvas").GetComponentInChildren<StatsController>().MinusHP(5, message);

    }
    private void Update()
    {
        if (_goAway)
        {
            if (Vector3.Distance(transform.position, _targetPoint) < 1)
            {
                GameObject.Find("Spawner").GetComponent<SpawnerNPS>()._countSpawnedPeople -= 1;
                Destroy(gameObject);
            }
            else
            {
                agent.destination = _targetPoint;
            }
        }

    }
}
