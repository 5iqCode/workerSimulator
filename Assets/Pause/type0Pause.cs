using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class type0Pause : MonoBehaviour
{
    public void ClickNext()
    {
        GameObject.Find("PauseScreenController").GetComponent<PauseScreenController>().ResumeGameClick();
    }

    [SerializeField] private GameObject _objConfirm;

    public void OnClickGoToMenu()
    {
        Instantiate(_objConfirm, gameObject.transform);
    }
}
