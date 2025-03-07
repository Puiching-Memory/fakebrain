using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVControl : Item
{
    public override void ClickEvent()
    {
        if (InventoryManager.Instance.currentItemID != 2) return;
        InventoryManager.Instance.AddItem(this);
        InventoryManager.Instance.RemoveItem(2);
        gameObject.SetActive(false);
    }

    public override void HasInteractive()
    {
        gameObject.SetActive(false);
    }

    public override void NotInteractive()
    {
        gameObject.SetActive(true);
    }
}
