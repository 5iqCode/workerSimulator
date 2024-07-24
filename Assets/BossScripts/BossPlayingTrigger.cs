using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayingTrigger : MonoBehaviour
{
    private GameObject _boss;

    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _boss = GameObject.Find("Boss");
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Player")
        {
            if (!_boss.GetComponent<BossCheckMagaz>())
            {
                if (!_audioSource.isPlaying)
                {
                    _audioSource.Play();
                    _audioSource.volume = 0.1f;
                    Debug.Log(Vector3.Distance(other.transform.position, transform.position));

                }
                else
                {
                    _audioSource.volume = 1/(Vector3.Distance(other.transform.position, transform.position)*10);
                }
            }
            else
            {
                if (_audioSource.isPlaying)
                {
                    _audioSource.Stop();
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }
        }
    }
}
