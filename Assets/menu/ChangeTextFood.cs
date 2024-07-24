
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
                    if(_text.text== "сытость")
                    {
                        _text.text = "satiety";
                    } else if (_text.text == "настроение")
                    {
                        _text.text = "sentiment";
                    }
                    else
                    {
                        if (_text.text== "Пицца")
                        {
                            _text.text = "Pizza";
                        }
                        else if (_text.text== "Бургер")
                        {
                            _text.text = "Burger";
                        }
                        else if (_text.text == "Торт")
                        {
                            _text.text = "Cake";
                        }
                        else if (_text.text == "Картошка")
                        {
                            _text.text = "Potato";
                        }
                        else if (_text.text == "Ананас")
                        {
                            _text.text = "Pineapple";
                        }
                        else if (_text.text == "Шаурма")
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
