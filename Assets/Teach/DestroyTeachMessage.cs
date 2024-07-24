using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTeachMessage : MonoBehaviour
{
    private PauseScript _pauseScript;
    private void Start()
    {
        _pauseScript = GameObject.Find("PauseButton").GetComponent<PauseScript>();

        _pauseScript._canPause = false;
    }
    public void OnClickCloseMessage()
    {
        if (GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>()._isDesktop)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        _pauseScript._canPause = true;

        Destroy(gameObject);
    }
}
