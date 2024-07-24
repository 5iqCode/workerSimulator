using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorWindowController : MonoBehaviour
{
    public void OnClickClose()
    {
        Destroy(gameObject);
    }
}
