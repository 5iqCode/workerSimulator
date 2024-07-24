using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorTriggerPlayer : MonoBehaviour
{
    [SerializeField] private Transform _door;

    [SerializeField] private int _ZposToChange;
    private Quaternion TargetAnge;
    private Quaternion _defRotation = Quaternion.Euler(0, 0, 0);

    private bool _openDoor = false;
    private void OnTriggerEnter(Collider other)
    {
        if ((other.name == "Player") || (other.name == "Boss"))
        {

            _openDoor = true;
            if (other.transform.position.z < _ZposToChange)
            {
                TargetAnge = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                TargetAnge = Quaternion.Euler(0, -90, 0);
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if((other.name == "Player")|| (other.name == "Boss"))
        {
            _openDoor = false;
        }
        
    }

    private void FixedUpdate()
    {
        if (_openDoor)
        {
            if (_door.rotation != TargetAnge)
            {
                _door.rotation = Quaternion.RotateTowards(_door.rotation, TargetAnge, Time.fixedDeltaTime * 150);
            }
        }
        else
        {
            if (_defRotation != _door.rotation)
            {
                _door.rotation = Quaternion.RotateTowards(_door.rotation, _defRotation, Time.fixedDeltaTime * 100);
            }
        }
    }
}
