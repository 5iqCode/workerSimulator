using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossTeachCanvasController : MonoBehaviour
{
    [SerializeField] private TMP_Text _buttonText;
    private int _stageTeach;
    private string[] _citatsBoss;
    private List<string> _citatsBossInThisDialog = new List<string>();

    [SerializeField] private TMP_Text _citat;

    private int _currentCitat;

    private LoadedInfo _loadedInfo;

    private PunchScript _punshScript;

    private PauseScript _pauseScript;
    private void Awake()
    {
        _pauseScript = GameObject.Find("PauseButton").GetComponent<PauseScript>();

        _pauseScript._canPause = false;

        AudioListener.volume = 0;
        _punshScript = GameObject.Find("Player").GetComponent<PunchScript>();

        if (_punshScript != null)
        {
            _punshScript._canPanch = false;
        }
    }
    private void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();

        Time.timeScale = 0;
        if (_loadedInfo._isDesktop)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        if(_loadedInfo._Language == "en")
        {
            _buttonText.text = "Next";
            _citatsBoss = new string[15]
{
"Welcome to my store. You'll have to work seven days a week here. I hope you agree to this. Although I don't care...",
"Your first task will be to place the boxes in the warehouse. Starting tomorrow, you will take them apart every day. And have time to spread them out before 12 o'clock, otherwise I will fine you...",
"Are you still here? Run, march!",

            "I could have done it faster... Now go to the hall and wait for the buyer. So far, your area of responsibility in this store is shelves with bottles...",
            "Make sure that there are no empty places on the shelves, otherwise I will fine you! So quickly go to the hall and wait for the customers!",

            "There aren't many customers today, you're lucky, usually the store is always full of people... You can replenish your basket of bottles at the warehouse.",
"Oh, yes, if you want to eat, you can buy groceries in the employee area. Only there the prices are a little too high, because I regulate them! Heh heh heh",

            "Where have you been walking for so long? Are you at work or on a walk? So, in addition to the shelves with bottles, you also have to keep an eye on the money in the cash registers.",
            "If I see that there will be more than 10,000 in the cash register, then you will receive a fine, understand? The money from the cash register should be in the safe. And if there is not enough money in the safe, then you will not have enough salary...",
"Why are you still standing here? Quickly go and check how much money is in the cash register!",

            "It's also your responsibility to keep the store clean. Customers can sometimes drop and break a bottle, and you'll have to clean it up if you want to get paid...",
            "But besides the customers, hooligans come here who break bottles on purpose. Usually among them are former employees to whom I have not paid their salaries, and business competitors...",
"Your task is to protect the store from these people.Use a punch to drive them away, otherwise I'll drive you away!",

            "It's a pity that you can't stay working at night. I wish you were working every second... Okay, you did a good internship day.",
"See you at work tomorrow, don't be late, otherwise I'll fine you!"
};
        }
        else
        {
            _citatsBoss = new string[15]
{
            "����� ���������� � ��� �������. ����� ���� ������� �������� ��� ��������. ������� �� �������� �� ���. ���� ��� �� �����...",
            "������ ����� �������� ����� ���������� ������� �� ������. � ����������� ��� �� ������ ��������� �� ������ ����. � ����� �� ��������� �� 12 �����, ����� � ���� ���� ����������...",
            "�� ��� �����? ����� ����!",

            "��� �� � ��������� ����������... ������ ��� � ��� � ��� ����������. ����, ���� ���� ��������������� � ���� �������� - ����� � ���������...",
            "�����, ����� �� ������ �� ���� ������ ����, ����� � ���� ���� ����������! ������� ������ ��� � ��� � ��� ��������!",

            "��� �� ������� ���� ��������, ���� �������, ������ ������� ������ ����� �����... ��������� ���� �������� � ��������� �� ������ �� ������.",
            "�� ��, ���� �������� ����, ������ ������ �������� � ���� ��� �����������. ������ ��� ���� ������� ��������, ������ ��� �� ��������� �! ��-��-��",

            "�� ��� ��� ����� ������? �� �� ������ ��� �� ��������? ������ ���, ����� ����� � ��������� �� ��� ������ ������� �� �������� � ������",
            "���� � ����� ��� � ����� ����� ������ 10 000, �� �� �������� �����, �����? ������ � ����� ������ ���� � �����. � ���� � ����� �� ����� ������� �����, �� � ���� �� ����� ������� ��������...",
            "������ �� ��� ������ ���? ������ ��� � ������� ������� ����� � ������!",

            "��� � ���� ����������� ������ ������� �� �������� ��������. ������� ������ ����� ������� � ������� �������, � ���� ������� ��� �������, ���� ������� ������ �������� ��������...",
            "�� ����� �������� ���� �������� ��������, ������� ���������� ���� �������. ������ ����� ��� ������ ����������, ������� � �� �������� ��������, � ���������� �� �������...",
            "���� ������ - �������� ������� �� ���� �����.��������� ����, ����� �������� ��, ����� � ������� ����!",

            "����� ����, ��� �� �� ������ �������� �������� � �����. ��� �� � ����� ����� �� ������� ������ �������... �����, �� ������� ������ ������������� ����.",
            "�������� �� ������ ������, ������ �� ���������, ����� ��������!"
};
        }

        _stageTeach = _loadedInfo.GetComponentInChildren<TeachLVL>()._stageTeach;

        switch (_stageTeach)
        {
            case 3:
                {
                    _citatsBossInThisDialog.Add(_citatsBoss[0]);
                    _citatsBossInThisDialog.Add(_citatsBoss[1]);
                    _citatsBossInThisDialog.Add(_citatsBoss[2]);
                    break;
                }
            case 7:
                {
                    _citatsBossInThisDialog.Add(_citatsBoss[3]);
                    _citatsBossInThisDialog.Add(_citatsBoss[4]);
                    break;
                }
            case 8:
                {
                    _citatsBossInThisDialog.Add(_citatsBoss[5]);
                    _citatsBossInThisDialog.Add(_citatsBoss[6]);
                    break;
                }
            case 10:
                {
                    _citatsBossInThisDialog.Add(_citatsBoss[7]);
                    _citatsBossInThisDialog.Add(_citatsBoss[8]);
                    _citatsBossInThisDialog.Add(_citatsBoss[9]);
                    break;
                }
            case 11:
                {
                    _citatsBossInThisDialog.Add(_citatsBoss[10]);
                    _citatsBossInThisDialog.Add(_citatsBoss[11]);
                    _citatsBossInThisDialog.Add(_citatsBoss[12]);
                    break;
                }
            case 12:
                {
                    _citatsBossInThisDialog.Add(_citatsBoss[13]);
                    _citatsBossInThisDialog.Add(_citatsBoss[14]);
                    break;
                }
        }

        _citat.text = _citatsBossInThisDialog[0];
    }

    public void OnClickNext()
    {
        _currentCitat++;
        if (_currentCitat > _citatsBossInThisDialog.Count - 1)
        {
            if (_loadedInfo._isDesktop)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

            Time.timeScale = 1;
            Destroy(gameObject);
        }
        else
        {
            _citat.text = _citatsBossInThisDialog[_currentCitat];
        }
    }

    private void OnDestroy()
    {
        if (_punshScript != null)
        {
            _punshScript._canPanch = true;
        }

        if (_loadedInfo != null)
        {
            AudioListener.volume = _loadedInfo.PlayerInfo._volume;
        }
       if(_pauseScript!= null)
        {
            _pauseScript._canPause = true;
        }
    }
}
