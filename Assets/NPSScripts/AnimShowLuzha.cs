using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimShowLuzha : MonoBehaviour
{
    private float _speed = 0.5f;
    Transform _objLuzha;
    void Start()
    {
        int _skinMatInt = Random.Range(0, 13);
        GetComponentInChildren<MeshRenderer>().material = GameObject.Find("Spawner").GetComponent<SpawnerNPS>()._skinMat[_skinMatInt];
        _objLuzha = GetComponentInChildren<LuzhaObjMark>().transform;
        _objLuzha.localScale = Vector3.zero;

        _objLuzha.transform.position += Vector3.up * _skinMatInt / 1000;

        _objLuzha.rotation = Quaternion.Euler(0,Random.Range(0,360),0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_objLuzha.localScale.x <= 1)
        {
            _objLuzha.localScale += Vector3.one * _speed * Time.fixedDeltaTime;
        }
        else
        {
            Destroy(this);
        }
    }
}
