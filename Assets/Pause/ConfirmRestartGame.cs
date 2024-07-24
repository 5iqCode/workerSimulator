using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmRestartGame : MonoBehaviour
{

    public void RestartGame()
    {
        SceneManager.LoadScene("CastomizationScene");
    }

    public void DestroyConfirm()
    {
        Destroy(gameObject);
    }
}
