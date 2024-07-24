
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenController : MonoBehaviour
{
    [SerializeField] private GameObject _objProverka;

    [SerializeField] private GameObject _litePause;

    [SerializeField] private GameObject _itogDay;

    [SerializeField] private GameObject _loseDay;

    [SerializeField] private Transform _canvas;
    LoadedInfo loadedInfo;
    private PunchScript _punshScript;
    private RotateAroundObj _rotateScript;
    private void Start()
    {
        loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        AudioListener.pause = true;
        AudioListener.volume = 0;
        Time.timeScale = 0;
        _punshScript = GameObject.Find("Player").GetComponent<PunchScript>();

        if (!loadedInfo._isDesktop)
        {
            _rotateScript = GameObject.Find("Player").GetComponentInChildren<RotateAroundObj>();
            _rotateScript._canUseScript = false;
        }

        if (_punshScript != null)
        {
            _punshScript._canPanch = false;
        }

        if(loadedInfo.typePause == 0)
        {
            Instantiate(_litePause, _canvas);
        }
        else if((loadedInfo.typePause==1)|| (loadedInfo.typePause == 2))
        {
            Instantiate(_objProverka, _canvas);
        }
        else if(loadedInfo.typePause == 4)
        {
            Instantiate(_itogDay, _canvas);
        } else if(loadedInfo.typePause == 5)
        {
            Instantiate(_loseDay, _canvas); 
        }
    }

    public void GoNextButton()
    {
        if (loadedInfo.typePause != 0)
        {
            loadedInfo.ShowFullScreenAdd();

            loadedInfo.typePause = 0;
        }
        else
        {
            SceneManager.UnloadSceneAsync("PauseScene");
        }

    }

    public void GoNextDay()
    {

        loadedInfo.ClearDay();
        loadedInfo.AddHard();



        SceneManager.UnloadSceneAsync("PauseScene");

    }

    private void lockCursore()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ResumeGameClick()
    {


        SceneManager.UnloadSceneAsync("PauseScene");

    }

    private void OnDestroy()
    {
        if (loadedInfo.typePause == 0)
        {
            AudioListener.pause = false;
            AudioListener.volume = loadedInfo.PlayerInfo._volume;
            Time.timeScale = 1;
        }
        
        if (loadedInfo._isDesktop)
        {
            lockCursore();
        }
        else
        {
            _rotateScript._canUseScript = true;
        }

        if (_punshScript != null)
        {
            _punshScript._canPanch = true;
        }

    }
}
