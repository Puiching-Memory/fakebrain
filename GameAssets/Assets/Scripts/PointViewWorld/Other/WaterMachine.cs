using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMachine : MonoBehaviour, IClick
{
    public GameObject fullWaterCup;

    public void ClickEvent()
    {
        if (InventoryManager.Instance.currentItemID != 5) return;
        InventoryManager.Instance.RemoveItem(5);
        fullWaterCup.SetActive(true);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
