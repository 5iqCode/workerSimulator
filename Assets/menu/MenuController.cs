using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private LoadedInfo _loadedInfo;

    public GameObject _leadersWindow;

    [SerializeField] private Button _resumeButton;

    [SerializeField] private GameObject ConfirmWindow;

    [SerializeField] private TMP_Text _moneyValue;

    private void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();

        _loadedInfo.ShowFullScreenAdd();

        _moneyValue.text = _loadedInfo.PlayerInfo._AllMoneyCount.ToString();

        AudioListener.volume = _loadedInfo.PlayerInfo._volume;
        Time.timeScale = 1;

        
        if (_loadedInfo.PlayerInfo._countDays == 0)
        {
            _resumeButton.interactable = false ;
        }
        

        if (_loadedInfo._isDesktop)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void ResumeGame()
    {

        _loadedInfo.PlayerInfo._statHangry -= _loadedInfo.PlayerInfo.changeFoodStatInEndDay;
        _loadedInfo.PlayerInfo._statHP -= _loadedInfo.PlayerInfo.changeHPStatInEndDay;
        SceneManager.LoadScene("Home");
    }

    public void NewGame()
    {
        if(_resumeButton.interactable == false)
        {
            SceneManager.LoadScene("CastomizationScene");
        }
        else
        {
            Instantiate(ConfirmWindow,GameObject.Find("Canvas").transform);
        }
    }

    public void OnClickLeaders()
    {
        _loadedInfo.ClickLiaders();
    }
}
