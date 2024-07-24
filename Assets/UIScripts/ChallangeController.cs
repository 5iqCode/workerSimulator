using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallangeController : MonoBehaviour
{
    public int time;
    private Animator _animator;

    private AudioSource _winSound;
    private AudioSource _loseSound;
    private AudioSource _alertSound;


    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Home")
        {
            _winSound = GameObject.Find("WinSoundHome").GetComponent<AudioSource>();
            _loseSound = GameObject.Find("LoseSoundHome").GetComponent<AudioSource>();
            _alertSound = GameObject.Find("AlertSoundHome").GetComponent<AudioSource>();
        }
        else
        {
            _winSound = GameObject.Find("WinSound").GetComponent<AudioSource>();
            _loseSound = GameObject.Find("LoseSound").GetComponent<AudioSource>();
            _alertSound = GameObject.Find("AlertSound").GetComponent<AudioSource>();
        }

        _animator = GetComponent<Animator>();
    }
    public void SwitchAnimLitleTime()
    {
        _animator.SetBool("LittleTime", true);
        _alertSound.Play();
    }
    public void SwitchAnimLose()
    {
        _animator.SetTrigger("LoseTrigger");
        _loseSound.Play();
    }

    public void SwitchAnimWin()
    {
        _animator.SetTrigger("WinTrigger");
        _winSound.Play();
    }

    public void DestoyChallange()
    {
        Destroy(gameObject);
    }
}
