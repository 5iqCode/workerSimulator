using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Transform[] _PathBoss;

    [SerializeField] private GameObject Boss;

    private Quaternion _defRotationBoss;
    private Vector3 _defPosBoss;

    private void Start()
    {
        _defPosBoss = Boss.transform.position;
        _defRotationBoss = Boss.transform.rotation;
    }
    public void SetDefPosBoss()
    {
        Boss.transform.rotation = _defRotationBoss;
        Boss.transform.position = _defPosBoss;
    }


    public void GoSheckMagazine()
    {
        BossCheckMagaz bossCheckMagaz = Boss.AddComponent<BossCheckMagaz>();

        bossCheckMagaz._BossPath = _PathBoss;
        bossCheckMagaz._BossController = this;
    }
}
