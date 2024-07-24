using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateStrelka : MonoBehaviour
{
    public Vector3 target;

    [SerializeField] private GameObject _objStrelka;

    private Transform _challangesTransform;
    private LoadedInfo _loadedInfo;
    private void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        if (_loadedInfo.PlayerInfo._countDays > 0)
        {
            Destroy(gameObject);
        }
        if (SceneManager.GetActiveScene().name == "Home")
        {
            if (_loadedInfo._dayIsStart)
            {
                target = new Vector3(16.7f,-36.8f,0.79f);

            }
            else
            {
                target = new Vector3(15.641f, -38.048f, -1.368f);
                _challangesTransform = GameObject.Find("ChallangeListHome").transform;
                StartCoroutine(_countChallangeCor());
            }
        }
    }
    IEnumerator _countChallangeCor()
    {
        while (_challangesTransform.GetComponentsInChildren<ChallangeController>().Length > 1)
        {
            yield return new WaitForSeconds(1f);
        }

        target = new Vector3(7.69f, -38.71f, -4.360001f);
    }
    public void SetTarget(Vector3 _targetTransform)
    {
        target = _targetTransform;
        _objStrelka.SetActive(true);
    }
    void Update()
    {
        if((target != null)&&(target!= Vector3.zero))
        {
            Vector3 relativePos = target - transform.position;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
        }
        else
        {
            if (_objStrelka.activeSelf)
            {
                _objStrelka.SetActive(false);
            }
        }

    }
}
