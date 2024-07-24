using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    private LoadedInfo loadedInfo;
    private bool _isDesktop ;

    public bool _canPause = true;
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Start()
    {
        loadedInfo = GameObject.Find("LoadedInfo").gameObject.GetComponent<LoadedInfo>();

        _isDesktop = loadedInfo._isDesktop;

        if( _isDesktop)
        {
            GetComponent<Image>().enabled = false;
            GetComponent<Button>().enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ClickPause();
        }
    }
    public void ClickPause()
    {
        if (SceneManager.sceneCount < 2)
        {
            if (_canPause)
            {
                if (Time.timeScale == 1)
                {
                    Time.timeScale = 0;

                    SceneManager.LoadScene("PauseScene", LoadSceneMode.Additive);
                }
                if (_isDesktop)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }
    }
}
