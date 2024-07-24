using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BottleStats : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    [SerializeField] private Slider _slider;

    [SerializeField] private Image _color;


    public void SetStats(int value)
    {
        _slider.value = value;
        _text.text = value.ToString() + " / 30";

        if (value < 20)
        {
            _color.color = Color.red;
        } else if (value < 28)
        {
            _color.color = Color.yellow;
        }
        else
        {
            _color.color = Color.green;
        }
    }
}
