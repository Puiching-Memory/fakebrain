using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostCard2 : Item
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
