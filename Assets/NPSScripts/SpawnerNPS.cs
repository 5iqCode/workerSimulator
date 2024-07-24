using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerNPS : MonoBehaviour
{
    public int _targetCountMassovka;
    public int _chanceCustomer; // пример =5 каждый пятый прохожий зайдёт в магазин


    public GameObject ObjKorzinka;
    public GameObject ObjPacket;

    public GameObject[] _FakeProductsMas;




    public int _countSpawnedPeople = 0;

    private int countAllSpawns;

    [SerializeField] private Transform[] _spanwPoints;

    [SerializeField] private Transform[] _peshPerehod;

    public Transform[] _magazPoints;

    public Transform[] KassaKorzinkaPoints;
    public Transform[] _FakePolksPoints;
    public Transform[] _BottlePolksPoints;
    public Transform KorzinkaPoint;
    public Transform[] KassaPoint;

    [SerializeField] private GameObject[] _NPSModel;

    [SerializeField] private int[] maxAttMan; //причёска, аттрибуты, борода
    [SerializeField] private int[] maxAttWoman;//причёска, аттрибуты

    private int selectedHair;
    private int selectedAtt;
    private int selectedBeard;
    private int selectedPol;

    private GameObject _tempNPS;
    private Transform _headTempNPS;

    public Material[] _skinMat;

    private void Awake()
    {
        _chanceCustomer = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>().PlayerInfo.changeCustomer;
    }
    void Start()
    {
        StartCoroutine(SpawnNPSCor());
    }

    IEnumerator SpawnNPSCor()
    {
        while (true)
        {
            if (_countSpawnedPeople < _targetCountMassovka)
            {
                CreateNPSChar();
                yield return new WaitForSeconds(Random.Range(1, 4));
            }
            else
            {
                yield return new WaitForSeconds(5);
            }
        }

    }
   
    public void CreateNPSChar()
    {
      int modelId = Random.Range(0, _NPSModel.Length);
        int spawnPointId =  Random.Range(0, _spanwPoints.Length);
        _countSpawnedPeople++;
        countAllSpawns++;
        if (modelId > 2)
        {
            selectedPol = 0;
        }
        else
        {
            selectedPol = 1;
        }

        _tempNPS = Instantiate(_NPSModel[modelId]);
        _tempNPS.transform.position = _spanwPoints[spawnPointId].position;

        _tempNPS.GetComponentInChildren<SkinnedMeshRenderer>().material = _skinMat[Random.Range(0,13)];
            MoveMassovka _moveMassTrig = _tempNPS.AddComponent<MoveMassovka>();

        if (Random.Range(0,101)< _chanceCustomer)
        {
            _moveMassTrig.isCustomer = true;
        }

        _moveMassTrig._pathAgent = CreatePathNPSMassovka(spawnPointId, _moveMassTrig.isCustomer);

        _headTempNPS = _tempNPS.GetComponentInChildren<Head>().transform;
        InstantiateAttributes();

    }

    public Vector3[] CreatePathNPSMassovka(int spawnPointId,bool _isCustomer)
    {
        int endPointId = spawnPointId;

        if (_isCustomer)
        {
            if (((_spanwPoints[spawnPointId].transform.position.z > 15f) && (_magazPoints[0].transform.position.z < 15f)) || ((_spanwPoints[spawnPointId].transform.position.z < 15f) && (_magazPoints[0].transform.position.z > 15f)))
            {
                int start, end;
                if (_spanwPoints[spawnPointId].transform.position.z > 15f)
                {
                    start = 1; end = 0;
                }
                else
                {
                    start = 0; end = 1;
                }

                return new Vector3[3] { _peshPerehod[start].transform.position, _peshPerehod[end].transform.position, _magazPoints[0].transform.position };
            }
            else
            {
                return new Vector3[1] { _magazPoints[0].transform.position };
            }
        }
        else
        {
            while (endPointId == spawnPointId)
            {
                endPointId = Random.Range(0, _spanwPoints.Length);
            }

            if (((_spanwPoints[spawnPointId].transform.position.z > 15f) && (_spanwPoints[endPointId].transform.position.z < 15f)) || ((_spanwPoints[spawnPointId].transform.position.z < 15f) && (_spanwPoints[endPointId].transform.position.z > 15f)))
            {
                int start, end;
                if (_spanwPoints[spawnPointId].transform.position.z > 15f)
                {
                    start = 1; end = 0;
                }
                else
                {
                    start = 0; end = 1;
                }

                return new Vector3[3] { _peshPerehod[start].transform.position, _peshPerehod[end].transform.position, _spanwPoints[endPointId].transform.position };
            }
            else
            {
                return new Vector3[1] { _spanwPoints[endPointId].transform.position };
            }
        }

    }

    private void InstantiateAttributes()
    {
        if (selectedPol == 0)
        {
            selectedBeard = Random.Range(0, maxAttMan[2] + 1);
            selectedAtt = Random.Range(0, maxAttMan[1] + 1);
            selectedHair = Random.Range(0, maxAttMan[0] + 1);
        }
        else
        {
            selectedBeard = 0;
            selectedAtt = Random.Range(0, maxAttWoman[1] + 1);
            selectedHair = Random.Range(0, maxAttWoman[0] + 1);
        }

        if (selectedPol == 0)
        {
           GameObject _obj1 = Instantiate(Resources.Load<GameObject>("Person/Attributs/ManHair" + selectedHair), _headTempNPS);
            GameObject _obj2 = Instantiate(Resources.Load<GameObject>("Person/Attributs/ManAtt" + selectedAtt), _headTempNPS);
            GameObject _obj3 = Instantiate(Resources.Load<GameObject>("Person/Attributs/manBeard" + selectedBeard), _headTempNPS);

            _obj1.GetComponent<MeshRenderer>().material = _skinMat[Random.Range(0, 13)];
            _obj2.GetComponent<MeshRenderer>().material = _skinMat[Random.Range(0, 13)];
            _obj3.GetComponent<MeshRenderer>().material = _skinMat[Random.Range(0, 13)];
        }
        else
        {
            GameObject _obj1 = Instantiate(Resources.Load<GameObject>("Person/Attributs/womanHair" + selectedHair), _headTempNPS);
            GameObject _obj2 = Instantiate(Resources.Load<GameObject>("Person/Attributs/WomanAtt" + selectedAtt), _headTempNPS);
            _obj1.GetComponent<MeshRenderer>().material = _skinMat[Random.Range(0, 13)];
            _obj2.GetComponent<MeshRenderer>().material = _skinMat[Random.Range(0, 13)];
        }


    }
}
