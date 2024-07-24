using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBottlesInPolks : MonoBehaviour
{
    public GameObject _luzhaTrigger;
    public Transform[] _PolkiMasTransform;
    public GameObject[] _bottles;
    private const float mnozhitel = 1.43f;



    private float[] XposBottles = new float[10] { -0.166f, -0.408f, -0.651f, -0.886f, -1.121f, -1.362f, -1.602f, -1.838f, -2.078f, -2.318f}; 
    private float[] YposBottles = new float[3] { 1.564008f, 0.943f, 0.321f}; 
    private float ZposBottles = 0.541f;


    private void Awake()
    {
        for (int _idBottle=0;_idBottle<6;_idBottle++)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                 GameObject _temp = Instantiate(_bottles[_idBottle], _PolkiMasTransform[_idBottle]);

                    _temp.transform.localPosition = new Vector3(XposBottles[j], YposBottles[i], ZposBottles);
                    _temp.transform.Rotate(Vector3.up * Random.Range(0, 360));
                    _temp.transform.localScale *= mnozhitel ;
                }
            }
        }
    }
}
