using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorScript : MonoBehaviour
{
    [SerializeField] private Transform _LeftDoor;
    [SerializeField] private Transform _RightDoor;

    private bool _openDoor = false;

    [SerializeField] private Quaternion RotationOpenLeft = Quaternion.Euler(0, -80, 0);
    [SerializeField] private Quaternion RotationOpenRight = Quaternion.Euler(0, 80, 0);

    [SerializeField] private Quaternion _defRotation = Quaternion.Euler(0, 0, 0);

    private Quaternion _zeroRotate = Quaternion.Euler(0, 0, 0);

    Quaternion _defLeft;
    Quaternion _defRight;

    private LoadedInfo _loadedInfo;
    private void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        _defLeft = RotationOpenLeft;
        _defRight = RotationOpenRight;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            if (_loadedInfo._inShop == false)
            {
                _loadedInfo._inShop = true;
            }
            if (other.gameObject.transform.position.z < 10)
            {
                RotationOpenRight = Quaternion.Euler(0, -80, 0);
                RotationOpenLeft = Quaternion.Euler(0, 80, 0);
            }
            else
            {
                RotationOpenRight = Quaternion.Euler(0, 80, 0);
                RotationOpenLeft = Quaternion.Euler(0, -80, 0);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "NPS"|| other.name == "Player")
        {
            _openDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPS" || other.name == "Player")
        {
            if(other.name == "Player")
            {
                RotationOpenRight = _defRight;
                RotationOpenLeft = _defLeft;
            }
            _openDoor = false;
        }
    }

    private void FixedUpdate()
    {
        if(_openDoor)
        {
            if(_LeftDoor.rotation!= RotationOpenLeft)
            {
                _LeftDoor.rotation = Quaternion.RotateTowards(_LeftDoor.rotation, RotationOpenLeft, Time.fixedDeltaTime * 150);
                _RightDoor.rotation = Quaternion.RotateTowards(_RightDoor.rotation, RotationOpenRight, Time.fixedDeltaTime * 150);
            }
        }
        else
        {
            if(_defRotation!= _LeftDoor.rotation)
            {
                _LeftDoor.rotation = Quaternion.RotateTowards(_LeftDoor.rotation, _zeroRotate, Time.fixedDeltaTime * 100);
                _RightDoor.rotation = Quaternion.RotateTowards(_RightDoor.rotation, _zeroRotate, Time.fixedDeltaTime * 100);
            }
        }
        
    }
}
