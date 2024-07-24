
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmWindowScript : MonoBehaviour
{
    private LoadedInfo _loadedInfo;

    private void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
    }

    public void GoToMenu()
    {
        _loadedInfo.ClearDay();
        _loadedInfo.LoadInfoForLastDay();

        Time.timeScale = 1;
        AudioListener.volume = 1;

        TeachLVL teachLVL = _loadedInfo.GetComponentInChildren<TeachLVL>();
        if(teachLVL != null)
        {
            Destroy(teachLVL.gameObject);
        }
        GameObject _obj = GameObject.Find("TimerController");
        if(_obj != null)
        {
            Destroy(_obj);
        }
        SceneManager.LoadScene("MenuScene");


    }

    public void ClickCancel()
    {
        Destroy(gameObject);
    }
}
