
using TMPro;
using UnityEngine;


public class ChangeTextFood : MonoBehaviour
{


    private LoadedInfo _loadedInfo;
    void Start()
    {
        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        if (_loadedInfo._Language == "en")
        {
            TMP_Text[] _texts = GetComponentsInChildren<TMP_Text>();

            foreach(TMP_Text _text in _texts)
            {
                if(_text.tag == "foodTextForTranslate")
                {
                    if(_text.text== "�������")
                    {
                        _text.text = "satiety";
                    } else if (_text.text == "����������")
                    {
                        _text.text = "sentiment";
                    }
                    else
                    {
                        if (_text.text== "�����")
                        {
                            _text.text = "Pizza";
                        }
                        else if (_text.text== "������")
                        {
                            _text.text = "Burger";
                        }
                        else if (_text.text == "����")
                        {
                            _text.text = "Cake";
                        }
                        else if (_text.text == "��������")
                        {
                            _text.text = "Potato";
                        }
                        else if (_text.text == "������")
                        {
                            _text.text = "Pineapple";
                        }
                        else if (_text.text == "������")
                        {
                            _text.text = "Shawa";
                        }
                    }
                }
            }
        }
        Destroy(this);
    }

}
