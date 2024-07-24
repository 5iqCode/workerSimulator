using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KassController : MonoBehaviour
{
    [SerializeField] private float MoneyKass;

    [SerializeField] private GameObject StatusBarsKass;

    [SerializeField] private int _radiusShow;

    [SerializeField] private GameObject MoneyTrigger;

    Transform _mainCameraTransform;

    private GameObject _instStatusBarsGO;
    private GameObject triggerMoney;

    private int _countCustomers=0;

    private Animator _animator;

    private AudioSource _audioSource;
    private void Start()
    {
        _mainCameraTransform = Camera.main.transform;

        _animator = GetComponent<Animator>();

        _audioSource= GetComponentInChildren<AudioSource>();
    }

    private void FixedUpdate()
    {
            float _distance = Vector3.Distance(transform.position, _mainCameraTransform.position);
        if (_distance<_radiusShow)
            { 
                if (_instStatusBarsGO == null)
                {
                    _instStatusBarsGO = Instantiate(StatusBarsKass,transform);
                ChangeMoney();
                } 
            }
            else
            {
                if (_instStatusBarsGO != null)
                {
                    Destroy( _instStatusBarsGO );
                }
            }
    }

    private void ChangeMoney()
    {
        if (_instStatusBarsGO != null)
        {
            _instStatusBarsGO.GetComponent<RotateMoneyInfo>().ChangeStats(MoneyKass);
        }
    }

    public void WorkKassir()
    {
        _countCustomers++;
        _animator.SetBool("UseKass", true);
        _audioSource.Play();
    }

    
    public void AddMoney(int value)
    {
        _countCustomers--;
        if (_countCustomers <= 0)
        {
            _animator.SetBool("UseKass", false);
        }
        if (value == 0)
        {
            MoneyKass += Random.Range(500, 5000);
        }
        else
        {
            MoneyKass += value;
        }

        if (MoneyKass > 15000)
        {
            if (triggerMoney == null)
            {
                triggerMoney = Instantiate(MoneyTrigger,transform);
            }
        }
        if(_instStatusBarsGO!= null)
        {
            ChangeMoney();
        }
    }


    public void TakeMoney()
    {
        MoneyKass = Random.Range(3500, 5000);
        triggerMoney = null;
        ChangeMoney();
    }
}
