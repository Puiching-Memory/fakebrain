using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCup : Item
{
    public override void ClickEvent()
    {
        InventoryManager.Instance.AddItem(this);
        gameObject.SetActive(false);
    }

    public override void HasInteractive()
    {
        gameObject.SetActive(false);
    }

    public override void NotInteractive()
    {

    }
}
