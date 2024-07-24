using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGoHome : MonoBehaviour
{

    [SerializeField] private GameObject _blackScreen;

    private Transform transformChallanges;
    private ChallangeController[] _challanges;

    private MoveMainHero _playerMove;

    [SerializeField] private GameObject _openDoorAudio;

    private void Start()
    {
        _playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveMainHero>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject _audioObg = Instantiate(_openDoorAudio);
            AudioSource[] _sources = _audioObg.GetComponentsInChildren<AudioSource>();

            foreach (AudioSource source in _sources)
            {
                if (source.name == "OpenDoor2")
                {
                    source.Play();
                    break;
                }
            }

            transformChallanges = GameObject.Find("ChallangeList").transform;
            _challanges = transformChallanges.GetComponentsInChildren<ChallangeController>();
            foreach (ChallangeController challange in _challanges)
            {
                if (challange.time == 0)
                {
                    challange.SwitchAnimWin();
                    break;
                }
            }
            _playerMove.CanMove = false;
            GameObject _blackScreenObj = Instantiate(_blackScreen);

            _blackScreenObj.GetComponent<BlackScreenController>().targetScene = "Home";
        }

    }
}
