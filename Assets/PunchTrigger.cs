using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchTrigger : MonoBehaviour
{
    [SerializeField] private GameObject effect;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            GameObject _obj = Instantiate(effect);
            _obj.transform.position = other.ClosestPoint(transform.position);

            CheckPunch checkPunch = other.GetComponent<CheckPunch>();
            if (checkPunch != null)
            {
                if (checkPunch._goAway == false)
                {
                    checkPunch.GetComponent<AudioSource>().Stop();
                    checkPunch.GetComponentInChildren<Animator>().SetTrigger("IsPunched");
                    checkPunch.Panched();

                }

            }

            Destroy(gameObject);
        }
    }
}
