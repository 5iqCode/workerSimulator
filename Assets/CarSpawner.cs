using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _pointsSpawn;
    [SerializeField] private GameObject[] _cars;

    [SerializeField] private Material[] materials;

    [SerializeField] private Transform[] _pointsMove;
   
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform _point in _pointsSpawn)
        {
            GameObject _temp = Instantiate(_cars[Random.Range(0, 4)], _point);
            _temp.GetComponent<MeshRenderer>().material = materials[Random.Range(0, 10)];
        }

        StartCoroutine(_spawnCarsCor(_pointsMove[0], _pointsMove[1].position,0));
        StartCoroutine(_spawnCarsCor(_pointsMove[2], _pointsMove[3].position,-180));
    }

    IEnumerator _spawnCarsCor(Transform _startPoint,Vector3 _endPoint,int rotate)
    {
        while (true)
        {
            GameObject _temp = Instantiate(_cars[Random.Range(0, 4)], _startPoint);

            _temp.GetComponent<MeshRenderer>().material = materials[Random.Range(0, 10)];

            _temp.transform.rotation = Quaternion.Euler(0, rotate,0);

            MoveCarScript _moveCar = _temp.AddComponent<MoveCarScript>();

            _moveCar._posToMove = _endPoint;

            yield return new WaitForSeconds(Random.Range(5,15));
        }
    }
}
