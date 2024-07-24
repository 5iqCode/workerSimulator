using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class CustomerGoToKassa : MonoBehaviour
{
    private NavMeshAgent agent;

    public GameObject _objKorzinka;

    private SpawnerNPS _spawnerNPS;

    Transform targetMoveTransform;
    private Vector3 _targetMoveVector;

    private bool CanMove = true;

    Animator _animator;

    private float _targetRotationY;

    private int idKassa;

    private KassController _kassController;

    private AudioSource _audioSource;

    public bool korzinkaInHands = true;
    public Coroutine _job;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _spawnerNPS = GameObject.Find("Spawner").GetComponent<SpawnerNPS>();
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        

        if (Random.Range(0, 11) > 5)
        {
            idKassa = 2;
        }
        else
        {
            idKassa = 0;
        }
        _kassController = GameObject.Find("Kassir"+idKassa.ToString()).GetComponent<KassController>();

        targetMoveTransform = _spawnerNPS.KassaPoint[idKassa];
        _targetMoveVector = targetMoveTransform.position;
    }

    // Update is called once per frame
    private bool inShop = true;
    void Update()
    {
        if (CanMove)
        {
            if (Vector3.Distance(transform.position, _targetMoveVector) < 0.5f)
            {
                if (inShop)
                {

                    _job = StartCoroutine(WaitCanMove());
                    if ((idKassa == 0) || (idKassa == 2))
                    {
                        _targetRotationY = targetMoveTransform.rotation.eulerAngles.y;


                        targetMoveTransform = _spawnerNPS.KassaPoint[idKassa + 1];
                        _targetMoveVector = targetMoveTransform.position;
                    }
                    else
                    {
                        _kassController.WorkKassir();
                    }
                }
                else
                {
                    GoAway();
                    Destroy(this);
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

    private void GoAway()
    {
        MoveMassovka _moveMassTrig = gameObject.AddComponent<MoveMassovka>();

        _moveMassTrig._pathAgent = _spawnerNPS.CreatePathNPSMassovka(0, false);

        _kassController.AddMoney(0);
    }

     IEnumerator WaitCanMove()
    {
        korzinkaInHands = false;

        CanMove = false;
        _animator.SetBool("IsMoving", false);
        _audioSource.Stop();

        if ((idKassa == 0) || (idKassa == 2))
        {

            _objKorzinka.transform.parent = _spawnerNPS.KassaKorzinkaPoints[idKassa];
            MovePorduct movePorduct = _objKorzinka.AddComponent<MovePorduct>();
            movePorduct._speed = 1f;
            _objKorzinka.transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
            yield return new WaitForSeconds(1);
            _objKorzinka.transform.parent = _spawnerNPS.KassaKorzinkaPoints[idKassa + 1];
            movePorduct = _objKorzinka.AddComponent<MovePorduct>();
            movePorduct._speed = 0.5f;
            idKassa++;
        }
        else
        {
            yield return new WaitForSeconds(6);

            GameObject _objPaket = Instantiate(_spawnerNPS.ObjPacket, gameObject.GetComponentInChildren<HandMarker>().transform);
            _objPaket.transform.localRotation = Quaternion.Euler(-90, 0, 90);
            Destroy(_objKorzinka);

            yield return new WaitForSeconds(1);

            // добавить деньги в кассу
            targetMoveTransform = _spawnerNPS._magazPoints[1];
            _targetMoveVector = targetMoveTransform.position;
            inShop = false;
        }

        CanMove = true;
        _animator.SetBool("IsMoving", true);
        _audioSource.Play();
    }
}
