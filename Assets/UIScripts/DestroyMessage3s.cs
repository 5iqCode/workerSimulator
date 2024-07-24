using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMessage3s : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitDestroy());
    }

    IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(3.6f);

        Destroy(gameObject);
    }
}
