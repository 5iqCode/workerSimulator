using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CastomizerController : MonoBehaviour
{
    [SerializeField] private int[] maxAttMan; //причёска, аттрибуты, борода
    [SerializeField] private int[] maxAttWoman;//причёска, аттрибуты

    private int selectedPol=0;
    private int selectedHair;
    private int selectedAtt;
    private int selectedBeard;

    private string[] InfoPlayerString = new string[4];

    [SerializeField] private GameObject ButtonsBeard;

    private LoadedInfo _loadedInfo;

    [SerializeField] private GameObject _teachObj;

    public void StartPlay()
    {
        _loadedInfo.PlayerInfo.InfoPlayerString = InfoPlayerString;
        _loadedInfo.PlayerInfo.colorPlayer = _selectedColor;

        SceneManager.LoadScene("Home");
    }

    private void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();


        int _allMoney = _loadedInfo.PlayerInfo._AllMoneyCount;
        bool canUseLeaders = _loadedInfo.PlayerInfo._canUseLeaders;
        float _sensivity = _loadedInfo.PlayerInfo._sensivity;

        string _homeShopsBed = _loadedInfo.PlayerInfo._homeShopsBed;
        string _homeShopsKitchen = _loadedInfo.PlayerInfo._homeShopsKitchen;
        string _homeShopsMainRoom = _loadedInfo.PlayerInfo._homeShopsMainRoom;
        string _homeShopsPC = _loadedInfo.PlayerInfo._homeShopsPC;
        string _homeShopsToilet = _loadedInfo.PlayerInfo._homeShopsToilet;

        int changeHPStatInEndDay = _loadedInfo.PlayerInfo.changeHPStatInEndDay;
        int changeFoodStatInEndDay = _loadedInfo.PlayerInfo.changeFoodStatInEndDay;
    _loadedInfo.PlayerInfo = new PlayerInfo();

        _loadedInfo.PlayerInfo._AllMoneyCount = _allMoney;
        _loadedInfo.PlayerInfo._canUseLeaders = canUseLeaders;
        _loadedInfo.PlayerInfo._sensivity = _sensivity;

        _loadedInfo.PlayerInfo._homeShopsBed = _homeShopsBed;
        _loadedInfo.PlayerInfo._homeShopsKitchen = _homeShopsKitchen;
        _loadedInfo.PlayerInfo._homeShopsMainRoom = _homeShopsMainRoom;
        _loadedInfo.PlayerInfo._homeShopsPC = _homeShopsPC;
        _loadedInfo.PlayerInfo._homeShopsToilet = _homeShopsToilet;
        _loadedInfo.PlayerInfo.changeHPStatInEndDay = changeHPStatInEndDay;
        _loadedInfo.PlayerInfo.changeFoodStatInEndDay = changeFoodStatInEndDay;


        Instantiate(_teachObj, _loadedInfo.transform);
        InfoPlayerString[0] = "Person/Man";
        RandomPers();
    }
    public void OnClickNextPol()
    {
        Destroy(GameObject.FindGameObjectWithTag("ModelPlayer"));
        GameObject _tempObj;
        if (selectedPol == 0)
        {
            selectedPol = 1;
            _tempObj = Resources.Load<GameObject>("Person/Woman");
            InfoPlayerString = new string[3];
            InfoPlayerString[0] = "Person/Woman";

            ButtonsBeard.SetActive(false);
        }
        else
        {
            selectedPol = 0;
            _tempObj = Resources.Load<GameObject>("Person/Man");

            InfoPlayerString = new string[4];
            InfoPlayerString[0] = "Person/Man";

            ButtonsBeard.SetActive(true);
        }
        Instantiate(_tempObj);
        StartCoroutine(WaitCor());
    }
    IEnumerator WaitCor()
    {
        yield return new WaitForSeconds(0.1f);
        RandomPers();
    }

    public void ChangeHair(int value)//-1 back; 1 next
    {
        int maxHair;
        if (selectedPol == 0)
        {
            maxHair = maxAttMan[0];
        }
        else
        {
            maxHair = maxAttWoman[0];
        }

        if (value == -1)
        {
            if (selectedHair == 0)
            {
                
                selectedHair = maxHair;
            }
            else
            {
                selectedHair--;
            }
        }
        else
        {
            if (selectedHair == maxHair)
            {
                selectedHair = 0;
            }
            else
            {
                selectedHair++;
            }
        }

        InstantiateAttributes();
    }

    public void ChangeAtt(int value)//-1 back; 1 next
    {
        int maxAtt;
        if (selectedPol == 0)
        {
            maxAtt = maxAttMan[1];
        }
        else
        {
            maxAtt = maxAttWoman[1];
        }

        if (value == -1)
        {
            if (selectedAtt == 0)
            {

                selectedAtt = maxAtt;
            }
            else
            {
                selectedAtt--;
            }
        }
        else
        {
            if (selectedAtt == maxAtt)
            {
                selectedAtt = 0;
            }
            else
            {
                selectedAtt++;
            }
        }
        InstantiateAttributes();
    }


    public void ChangeBeard(int value)//-1 back; 1 next
    {
        int maxBeard = maxAttMan[2];

        if (value == -1)
        {
            if (selectedBeard == 0)
            {

                selectedBeard = maxBeard;
            }
            else
            {
                selectedBeard--;
            }
        }
        else
        {
            if (selectedBeard == maxBeard)
            {
                selectedBeard = 0;
            }
            else
            {
                selectedBeard++;
            }
        }
        InstantiateAttributes();
    }

    private void InstantiateAttributes()
    {
        GameObject[] _objs = GameObject.FindGameObjectsWithTag("AttributesOnHead");

        if (_objs.Length > 0)
        {
            foreach (GameObject obj in _objs)
            {
                Destroy(obj);
            }
        }


        Transform _head = GameObject.Find("Head").transform;

        if (selectedPol == 0)
        {
            Instantiate(Resources.Load<GameObject>("Person/Attributs/ManHair" + selectedHair), _head);
            Instantiate(Resources.Load<GameObject>("Person/Attributs/ManAtt" + selectedAtt), _head);

            InfoPlayerString[1] = "Person/Attributs/ManHair" + selectedHair;
            InfoPlayerString[2] = "Person/Attributs/ManAtt" + selectedAtt;
            Instantiate(Resources.Load<GameObject>("Person/Attributs/manBeard" + selectedBeard), _head);
            InfoPlayerString[3] = "Person/Attributs/manBeard" + selectedBeard;
        }
        else
        {
            Instantiate(Resources.Load<GameObject>("Person/Attributs/womanHair" + selectedHair), _head);
            Instantiate(Resources.Load<GameObject>("Person/Attributs/WomanAtt" + selectedAtt), _head);

            InfoPlayerString[1] = "Person/Attributs/womanHair" + selectedHair;
            InfoPlayerString[2] = "Person/Attributs/WomanAtt" + selectedAtt;
        }

        GameObject.FindGameObjectWithTag("ModelPlayer").GetComponentInChildren<SkinnedMeshRenderer>().material = materials[_selectedColor];
    }

    private void RandomPers()
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

        InstantiateAttributes();
    }

    [SerializeField] private Material[] materials;
    private int _selectedColor=0;

    public void ChangeHardLvl(int value)//-1 back; 1 next
    {
        int maxLvl = 2;

        if (value == -1)
        {
            if (_selectedColor == 0)
            {

                _selectedColor = maxLvl;
            }
            else
            {
                _selectedColor--;
            }
        }
        else
        {
            if (_selectedColor == maxLvl)
            {
                _selectedColor = 0;
            }
            else
            {
                _selectedColor++;
            }
        }

        InstantiateAttributes();
    }
}





