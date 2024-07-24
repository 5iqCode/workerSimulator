
using UnityEngine;

[CreateAssetMenu(fileName = "CartShop", menuName = "ScriptableObject/CartShop")]
public class ScriptableObj : ScriptableObject
{
    public string Name;

    public GameObject Obj3D;

    public Sprite UIImage;

    public string foodStats;

    public string HPStats;

    public string Coast;
}

