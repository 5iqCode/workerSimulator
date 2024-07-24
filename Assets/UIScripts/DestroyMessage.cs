using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMessage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitDestroy());
    }

    IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }
}
