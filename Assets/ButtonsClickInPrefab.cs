using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsClickInPrefab : MonoBehaviour
{
    public string nameFood;
    public Transform idButton;
    public void CkickMoneyButton()
    {
        idButton = transform;
        PlayerItemsController playerItemsController = GameObject.Find("PlayerItemsController").GetComponent<PlayerItemsController>();
        playerItemsController.TakeMoney(idButton);
        GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>().PlayerInfo._selectedItem = int.Parse(gameObject.name)-1;
    }

    public void ClickFoodButton()
    {
        idButton = transform;
        PlayerItemsController playerItemsController = GameObject.Find("PlayerItemsController").GetComponent<PlayerItemsController>();
        playerItemsController.TakeFood(nameFood, idButton);
        GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>().PlayerInfo._selectedItem = int.Parse(gameObject.name) - 1;
    }
}
