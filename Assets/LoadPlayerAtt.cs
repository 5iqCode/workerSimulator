using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayerAtt : MonoBehaviour
{
    private LoadedInfo _loadedInfo;

    private Transform _head;

    [SerializeField]private Material[] _materials;

    private GameObject _modelPlayer;
    void Awake()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();

        //Destroy(GetComponentInChildren<Animator>().gameObject);
        _modelPlayer= Instantiate(Resources.Load<GameObject>(_loadedInfo.PlayerInfo.InfoPlayerString[0]), transform);


        _head = _modelPlayer.GetComponentInChildren<Head>().transform;
        int i = 0;
        foreach (string _loadSTR in _loadedInfo.PlayerInfo.InfoPlayerString)
        {
            if(i != 0)
            {
               
                Instantiate(Resources.Load<GameObject>(_loadSTR), _head);

            }
            else
            {
                i = 1;
            }
        }
        _modelPlayer.GetComponentInChildren<SkinnedMeshRenderer>().material = _materials[_loadedInfo.PlayerInfo.colorPlayer];

        GetComponent<MoveMainHero>().enabled = true;

        Destroy(this);

    }
}
