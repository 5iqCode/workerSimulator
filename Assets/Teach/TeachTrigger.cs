using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachTrigger : MonoBehaviour
{
    public TeachLVL teachLVL;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
                teachLVL._strelkaScript.target = Vector3.zero;
            teachLVL.SwitchState();
            Destroy(gameObject);
        }
    }
}
