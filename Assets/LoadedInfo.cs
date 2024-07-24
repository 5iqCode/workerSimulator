using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerInfo
{
    public bool _canUseLeaders = false;

    public int _AllMoneyCount = 0;
    public float _sensivity =1000;
    public float _volume=1;

    public int graphicSettings =3;


    public string[] InfoPlayerString;
    public int colorPlayer;

    public int _countDays;
    public int startZPDay = 3000;
    public int changeCustomer = 30;
    public int maxCountMoveCustomer = 5;
    public int changeAnonimus = 50;
    public int changeGoToBottle = 25;
    public int maxSpawnBoxes = 10;


    public int _countPlayerItemsBoxBottles = 11;

    public int _countBlockedItems = 3;
    public int _countMaxItems = 6;
    public string[] itemsNameInRecourses = new string[6] { "", "PlayerBlock", "shwabra", "", "", "" };

    public int _selectedItem;

    public int money = 2500;



    public float _statHangry = 100;
    public float _statHP = 100;

    public int changeHPStatInEndDay = -20;
    public int changeFoodStatInEndDay = -50;

    public string _homeShopsBed = "000000000";//9 большая кровать, дорожка беговая, картины, ковёр, лампа, дерево, шкаф, телескоп, тумба
    public string _homeShopsKitchen = "0000";//4 штуки кухонный блок, холодильник, стол, ковёр
    public string _homeShopsMainRoom = "00000000";//8 тв, диван, картины, кондёр, ковёр, маленькое дерево, большое дерево, комп стол
    public string _homeShopsPC = "1000";
    public string _homeShopsToilet = "0000";//4 ванна, полка, зеркало, коврик

}
public class LoadedInfo : MonoBehaviour
{
    public PlayerInfo PlayerInfo;

    public PlayerInfo savePlayerInfo;

    public bool _isDesktop = true;
    public string _Language = "ru";

    public bool _inShop=false;

    public int shtrafsForFirst=0;
    public int shtrafsForSecond = 0;

    public int typePause = 0;//0 - просто пауза, 1- 1 проверка, 2-2 проверка, 3 - итог дня

    public List<string> _shtrafs;

    public List<int> _shtrafsValue;

    public int _premii;

    public int _ZPDay;

    public int _rashodsFood;
    public int _rashodsLerua;

    public bool _dayIsStart = false;

    public int startMoney;

    private bool _wasChangedInfo = false;

    [DllImport("__Internal")]
    private static extern void SaveExtern(string date);
    [DllImport("__Internal")]
    private static extern void LoadExtern();

    [DllImport("__Internal")]
    private static extern void ShowAdv();

    [DllImport("__Internal")]
    private static extern void RewardedVideoExtern();

    [DllImport("__Internal")]
    private static extern void ReloadGameExtern();
    [DllImport("__Internal")]
    private static extern void InitPlayerExtern(int _currentLvl);



    [SerializeField] private GameObject _errorWindow;
    private void Awake()
    {
        PlayerInfo = new PlayerInfo();
        savePlayerInfo = new PlayerInfo();

        startMoney = PlayerInfo.money;

        GameObject[] objs = GameObject.FindGameObjectsWithTag("LoadedInfo");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        StartCoroutine(TryLoadInfo());
    }
    IEnumerator TryLoadInfo()
    {
        for (int i = 0; i < 30; i++)
        {
            if (!_wasChangedInfo)
            {
                LoadExtern();
            }
            else
            {
                break;
            }

            if (i == 29) //не загрузилось
            {
                ReloadGameExtern();
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void SetPlayerInfo(string value)
    {
        _wasChangedInfo = true;
        PlayerInfo = JsonUtility.FromJson<PlayerInfo>(value);

        if (PlayerInfo._AllMoneyCount == 0)
        {
            Save();
        }

        SceneManager.LoadScene("MenuScene");

    }
    public void Save()
    {
        string jsonString = JsonUtility.ToJson(PlayerInfo);
        SaveExtern(jsonString);
    }
    public void SetDefaultLanguage(string languageName)
    {
        string _tempLn = languageName.Substring(1, 2);

        if (_tempLn == "ru")
        {
            _Language = _tempLn;
        }
        else
        {
            _Language = "en";
        }
    }
    public void SetDevice(int isDesctop)
    {
        if (isDesctop == 1)
        {
            if (PlayerInfo._sensivity == 1000)
            {
                PlayerInfo._sensivity = 50;
            }
            
            _isDesktop = true;
        }
        else
        {
            if (PlayerInfo._sensivity == 1000)
            {
                PlayerInfo._sensivity = 350;
            }
            _isDesktop = false;
        }

    }

    public void SafeInfoForLoadLastDay()
    {
        int lenghtInfoPlayer = PlayerInfo.InfoPlayerString.Length;
        if (PlayerInfo._countDays == 0)
        {
            savePlayerInfo.InfoPlayerString = new string[lenghtInfoPlayer];
            savePlayerInfo.itemsNameInRecourses = new string[6];
        }
        
        for(int i = 0;i<6;i++)
        {
            savePlayerInfo.itemsNameInRecourses[i] = PlayerInfo.itemsNameInRecourses[i];
        }

        for (int i = 0; i < lenghtInfoPlayer; i++)
        {
            savePlayerInfo.InfoPlayerString[i] = PlayerInfo.InfoPlayerString[i];
        }

        savePlayerInfo._AllMoneyCount = PlayerInfo._AllMoneyCount;

        savePlayerInfo.colorPlayer = PlayerInfo.colorPlayer;
        savePlayerInfo._countDays = PlayerInfo._countDays;
        savePlayerInfo.startZPDay = PlayerInfo.startZPDay;
        savePlayerInfo.changeCustomer = PlayerInfo.changeCustomer;
        savePlayerInfo.maxCountMoveCustomer = PlayerInfo.maxCountMoveCustomer;
        savePlayerInfo.changeAnonimus = PlayerInfo.changeAnonimus;
        savePlayerInfo.changeGoToBottle = PlayerInfo.changeGoToBottle;
        savePlayerInfo.maxSpawnBoxes = PlayerInfo.maxSpawnBoxes;

        savePlayerInfo._countPlayerItemsBoxBottles = PlayerInfo._countPlayerItemsBoxBottles;

        savePlayerInfo._countBlockedItems = PlayerInfo._countBlockedItems;
        savePlayerInfo._countMaxItems = PlayerInfo._countMaxItems;

        savePlayerInfo._selectedItem = PlayerInfo._selectedItem;

        savePlayerInfo.money = PlayerInfo.money;

        savePlayerInfo._statHangry = PlayerInfo._statHangry;
        savePlayerInfo._statHP = PlayerInfo._statHP;
        savePlayerInfo.changeHPStatInEndDay = PlayerInfo.changeHPStatInEndDay;
        savePlayerInfo.changeFoodStatInEndDay = PlayerInfo.changeFoodStatInEndDay;

        savePlayerInfo._homeShopsBed = PlayerInfo._homeShopsBed;
        savePlayerInfo._homeShopsKitchen = PlayerInfo._homeShopsKitchen;
        savePlayerInfo._homeShopsPC = PlayerInfo._homeShopsPC;
        savePlayerInfo._homeShopsToilet = PlayerInfo._homeShopsToilet;
        savePlayerInfo._homeShopsMainRoom = PlayerInfo._homeShopsMainRoom;
    }

    public void LoadInfoForLastDay()
    {
        for (int i = 0; i < 6; i++)
        {
            PlayerInfo.itemsNameInRecourses[i] = savePlayerInfo.itemsNameInRecourses[i];
        }
        int lenghtInfoPlayer = savePlayerInfo.InfoPlayerString.Length;

        for (int i = 0; i < lenghtInfoPlayer; i++)
        {
            PlayerInfo.InfoPlayerString[i] = savePlayerInfo.InfoPlayerString[i];
        }

        PlayerInfo._AllMoneyCount = savePlayerInfo._AllMoneyCount;
        PlayerInfo.colorPlayer = savePlayerInfo.colorPlayer;
        PlayerInfo._countDays = savePlayerInfo._countDays;
        PlayerInfo.startZPDay = savePlayerInfo.startZPDay;
        PlayerInfo.changeCustomer = savePlayerInfo.changeCustomer;
        PlayerInfo.maxCountMoveCustomer = savePlayerInfo.maxCountMoveCustomer;
        PlayerInfo.changeAnonimus = savePlayerInfo.changeAnonimus;
        PlayerInfo.changeGoToBottle = savePlayerInfo.changeGoToBottle;
        PlayerInfo.maxSpawnBoxes = savePlayerInfo.maxSpawnBoxes;

        PlayerInfo._countPlayerItemsBoxBottles = savePlayerInfo._countPlayerItemsBoxBottles;

        PlayerInfo._countBlockedItems = savePlayerInfo._countBlockedItems;
        PlayerInfo._countMaxItems = savePlayerInfo._countMaxItems;

        PlayerInfo._selectedItem = savePlayerInfo._selectedItem;

        PlayerInfo.money = savePlayerInfo.money;

        PlayerInfo._statHangry = savePlayerInfo._statHangry;
        PlayerInfo._statHP = savePlayerInfo._statHP;
        PlayerInfo.changeHPStatInEndDay = savePlayerInfo.changeHPStatInEndDay;
        PlayerInfo.changeFoodStatInEndDay = savePlayerInfo.changeFoodStatInEndDay;

        PlayerInfo._homeShopsBed = savePlayerInfo._homeShopsBed;
        PlayerInfo._homeShopsKitchen = savePlayerInfo._homeShopsKitchen;
        PlayerInfo._homeShopsPC = savePlayerInfo._homeShopsPC;
        PlayerInfo._homeShopsToilet = savePlayerInfo._homeShopsToilet;
        PlayerInfo._homeShopsMainRoom = savePlayerInfo._homeShopsMainRoom;
    }

    public void ClearDay()
    {
        PlayerInfo._AllMoneyCount += _ZPDay;
        _shtrafs.Clear();
        _shtrafsValue.Clear();
        _inShop = false;
        shtrafsForFirst = 0;
        shtrafsForSecond = 0;
        _premii = 0;
        _ZPDay = 0;
        _rashodsFood = 0;
            _rashodsLerua = 0;
        _dayIsStart = true;
        startMoney = 0;
        typePause = 0;
    }
    public void AddHard()
    {

        PlayerInfo._countDays++;
        PlayerInfo.startZPDay += 700;


        if (PlayerInfo.changeCustomer < 60)
        {
            PlayerInfo.changeCustomer += 3;
        }

        if (PlayerInfo.maxCountMoveCustomer < 10)
        {
                PlayerInfo.maxCountMoveCustomer += 1;
        }
        
        if (PlayerInfo.changeAnonimus < 30)
        {
            PlayerInfo.changeAnonimus += 2;
        }
        if (PlayerInfo.changeGoToBottle < 40)
        {
            PlayerInfo.changeGoToBottle += 2;
        }

        if (PlayerInfo.maxSpawnBoxes <18)
        {
                PlayerInfo.maxSpawnBoxes += 1;
        }

        Save();
    }
    public void ShowFullScreenAdd()
    {
        ShowAdv();
    }


    public void ShowRewardedAd()
    {

        RewardedVideoExtern();
    }
    public void RewardedTrue(int nothing)
    {
        ClearDay();
        LoadInfoForLastDay();

        Time.timeScale = 1;
        AudioListener.volume = 1;

        TeachLVL teachLVL = GetComponentInChildren<TeachLVL>();
        if (teachLVL != null)
        {
            Destroy(teachLVL.gameObject);
        }
        GameObject _obj = GameObject.Find("TimerController");
        if (_obj != null)
        {
            Destroy(_obj);
        }

        SceneManager.LoadScene("Home");
    }
    public void RewardedError()
    {
        GameObject _obj = Instantiate(_errorWindow);
        string _strError = "При загрузке рекламы произошла ошибка. Попробуйте снова.";
        if (_Language != "ru")
        {
            _strError = "An error occurred while loading the ad. Try again.";
        }
        _obj.GetComponentInChildren<TMP_Text>().text = _strError;
    }

    public void ClickLiaders()
    {
        if (PlayerInfo._canUseLeaders == false)
        {
            Save();
        }
        
        InitPlayerExtern(PlayerInfo._AllMoneyCount);
    }
    public void ErrorLoad(int _nothing)
    {
        GameObject[] _objs = GameObject.FindGameObjectsWithTag("LoadingWindow");
        foreach (GameObject obj in _objs)
        {
            Destroy(obj);
        }

        GameObject _obj = Instantiate(_errorWindow);
        string _strError = "При загрузке списка лидеров произошла ошибка. Повторите попытку.";
        if (_Language != "ru")
        {
            _strError = "An error occurred while loading the leaderboard. Please try again.";
        }
        _obj.GetComponentInChildren<TMP_Text>().text = _strError;
    }
    public void LoadedLeaderbords(string data)
    {
        if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            PlayerInfo._canUseLeaders = true;

            GameObject _leaderWindow = Instantiate(GameObject.Find("MenuController").GetComponent<MenuController>()._leadersWindow);

            _leaderWindow.GetComponent<leaderBoardController>().LoadedLeaderbordsInWindow(data);
        }
        else if (SceneManager.GetActiveScene().name == "Demo_01_Lite")
        {
            GameObject.Find("LeaderBoard").GetComponent<LeaderBoardInGameScene>().LoadedLeaderbordsInWindow(data);
        }


    }

    public void UnloadScenePause(int nothing)
    {
        if(SceneManager.GetActiveScene().name == "MenuScene")
        {
            Time.timeScale = 1;
            AudioListener.volume = PlayerInfo._volume;
            AudioListener.pause = false;
        }
        else if(SceneManager.GetActiveScene().name == "Home")
        {
            if (_isDesktop)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

            Time.timeScale = 1;
            AudioListener.volume = PlayerInfo._volume;
            AudioListener.pause = false;
        }
        else
        {
            SceneManager.UnloadSceneAsync("PauseScene");
        }
       
    }

    public void SetFocus(int _value)
    {
        if (_value == 0)
        {
            Time.timeScale = 0;
            AudioListener.volume = 0;
            AudioListener.pause=true;
        }
        else
        {
            if (SceneManager.sceneCount < 2)
            {
                Time.timeScale = 1;
                AudioListener.volume = PlayerInfo._volume;
                AudioListener.pause = false;
            }
        }
    }
}
