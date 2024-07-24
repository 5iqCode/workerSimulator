using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePorduct : MonoBehaviour
{
    Vector3 position;

    public Transform _parent;

    public float _speed =2;
    private void Start()
    {
        
        if (tag== "bottleInHandPlayer")
        {
            transform.localRotation = Quaternion.Euler(0f, Random.Range(0, 180), 0f);
            position = Vector3.zero;
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            position = new Vector3(Random.Range(-0.201f, 0.201f), 0, 0);
        }

    }
    private void FixedUpdate()
    {
        float step = _speed * Time.fixedDeltaTime;
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, position, step);
        if(Vector3.Distance(transform.localPosition, position) < 0.001f)
        {
            if ((tag == "bottleInHandPlayer")||(tag == "BoxFromCar"))
            {
                if(tag == "BoxFromCar")
                {
                    Destroy(GetComponent<CarBoxScript>());
                }
                GameObject point = transform.parent.gameObject;
                transform.position = point.transform.position;
                transform.parent = _parent;
                tag = "Untagged";
                Destroy(point);
            }

            transform.localPosition+=new Vector3(0,0, Random.Range(-0.03f, 0.03f));
            Destroy(this);
        }
            
    }
}
