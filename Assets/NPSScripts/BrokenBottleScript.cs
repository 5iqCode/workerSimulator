using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBottleScript : MonoBehaviour
{
    private bool wasSpawnTrigger=false;
    private void OnCollisionEnter(Collision collision)
    {
        
        if (wasSpawnTrigger==false)
        {
            if (collision.gameObject.tag == "floor")
            {
                wasSpawnTrigger = true;

                GameObject _obj = Instantiate(GameObject.Find("Spawner").GetComponent<SpawnerBottlesInPolks>()._luzhaTrigger);
                _obj.transform.position = collision.contacts[0].point;
                _obj.transform.position = new Vector3(_obj.transform.position.x, -33.77808f, _obj.transform.position.z);
                StartCoroutine(destroyObj());
            }
        }

    }


    IEnumerator destroyObj()
    {
        yield return new WaitForSeconds(3);

        Destroy(gameObject);
    }
}
