using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public void OnClickBuy()
    {
        TMP_Text[] _texts = GetComponentsInChildren<TMP_Text>();
        int price =0;
        int hp =0;
        int food =0;
        foreach( TMP_Text text in _texts)
        {
            if(text.tag == "Price")
            {
               Skidka skidka = text.GetComponentInChildren<Skidka>();
                if (skidka != null)
                {
                    price = int.Parse(skidka.GetComponent<TMP_Text>().text);
                }
                else
                {
                    price = int.Parse(text.text);
                }
                
            } else if(text.name == "HPText")
            {
                hp=int.Parse(text.text);
            }
            else if (text.name == "FoodText")
            {
                food = int.Parse(text.text);
            }
        }
        GameObject.Find("HomeController").GetComponent<HomeController>().OnClickBuy(price,gameObject.name,hp,food);
    }
}
