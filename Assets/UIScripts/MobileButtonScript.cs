using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileButtonScript : MonoBehaviour
{
    public bool IsClicked = false;


    public void ClickButton()
    {
        IsClicked = true;
    }
}
