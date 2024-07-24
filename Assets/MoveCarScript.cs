using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveCarScript : MonoBehaviour
{
    public Vector3 _posToMove;

    Transform[] _wheels = new Transform[4];

    private int _speedRotate = 500;
    private int _speedMove = 7;

    private void Start()
    {
      Wheel[] wheelsMarks = GetComponentsInChildren<Wheel>();

        for (int i = 0; i < wheelsMarks.Length; i++)
        {
            _wheels[i]= wheelsMarks[i].transform;
        }
    }

    private void FixedUpdate()
    {
        if (_posToMove != null)
        {
            float _fixedTime = Time.fixedDeltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _posToMove, _speedMove * _fixedTime);

            if (Vector3.Distance(transform.position, _posToMove) < 1f)
            {
                Destroy(gameObject);
            }

            foreach (Transform transform in _wheels)
            {
                transform.Rotate(_speedRotate * _fixedTime, 0, 0);
            }
        }

    }
}
