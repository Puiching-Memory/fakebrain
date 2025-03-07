using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDCard : Item
{
    public override void ClickEvent()
    {
        InventoryManager.Instance.AddItem(this);
        BlackLevelScene.Instance.point++;
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
