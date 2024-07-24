using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlackScreenController : MonoBehaviour
{
    public string targetScene;

    [SerializeField] private Image _blackScreen;

    public int _speedBlack =5;

    private LoadedInfo _loadedInfo;

    private PauseScript _pauseScript;
    private void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        _pauseScript = GameObject.Find("PauseButton").GetComponent<PauseScript>();
        _pauseScript._canPause = false;
    }
    private void LoadSceneScript()
    {
        SceneManager.LoadScene(targetScene);
    }
    Color _color;
    private void FixedUpdate()
    {
        _color.a += _speedBlack * Time.fixedDeltaTime/10;
        _blackScreen.color = _color;

        if (_color.a >= 1)
        {
            _pauseScript._canPause = true;
            if (_loadedInfo.typePause == 4)
            {
                //save
                
                _pauseScript.ClickPause();
            }
            else
            {
                LoadSceneScript();
            }
        }
    }
}
