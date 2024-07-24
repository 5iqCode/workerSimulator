using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerCarWork : MonoBehaviour
{
    [SerializeField] private GameObject[] _objs;

    private int _countBoxes;
    private int _progressBoxes;

    private PosCardBoardScript _posCardBoardScript;

    private LoadedInfo _loadedInfo;

    private Slider progressSlider;
    void Start()
    {
        progressSlider=GetComponentInChildren<Slider>();

        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        _posCardBoardScript = GameObject.Find("PosSkladKorob").GetComponent<PosCardBoardScript>();
        _posCardBoardScript._spawnerCarWork = this;
        int minBoxes = 6;
        if (_loadedInfo.PlayerInfo._countDays > 5)
        {
            minBoxes = 10;
        }
        _countBoxes = Random.Range(minBoxes, _loadedInfo.PlayerInfo.maxSpawnBoxes);
        //максимум 18

        progressSlider.maxValue = _countBoxes;

        StartCoroutine(spawnCor());
    }

    IEnumerator spawnCor()
    {
        for (int i = 0; i < _countBoxes; i++)
        {
            Instantiate(_objs[Random.Range(0, 4)], transform);

            yield return new WaitForSeconds(0.3f);
        }
    }

    public void AddProgress()
    {
        _progressBoxes++;
        progressSlider.value = _progressBoxes;
        if (_progressBoxes >= _countBoxes)
        {
            _loadedInfo.GetComponentInChildren<zpBlockController>().PlusMoney(_progressBoxes*35);

            TeachLVL _teachTrigger = _loadedInfo.GetComponentInChildren<TeachLVL>();

            if (_teachTrigger == null)
            {
                _loadedInfo.GetComponentInChildren<TimerScript>().EndCarWorkChallange();
            }
            else
            {
                _teachTrigger.SwitchState();
            }
            Destroy(GetComponentInChildren<Canvas>().gameObject);
            Destroy(_posCardBoardScript);
            Destroy(this);
        }
    }
}
