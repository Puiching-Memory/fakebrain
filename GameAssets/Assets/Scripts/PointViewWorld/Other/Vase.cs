using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour, IClick
{
    public GameObject iron;

    public void ClickEvent()
    {
        if (InventoryManager.Instance.currentItemID != 6) return;
        iron.SetActive(true);
        InventoryManager.Instance.RemoveItem(6);
    }
}
