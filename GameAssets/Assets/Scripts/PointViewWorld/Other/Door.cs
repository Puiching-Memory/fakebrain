using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

public class Door : Item
{
    public GameObject openDoor;
    //public GameObject TransitionToPVL1;
    public GameObject Fungus;
    public override void ClickEvent()
    {
        if (InventoryManager.Instance.currentItemID != 0) return;
        InventoryManager.Instance.RemoveItem(0);
        openDoor.SetActive(true);
        //TransitionToPVL1.SetActive(true);
        Fungus.SetActive(true);
        gameObject.SetActive(false);
        ObjectManager.Instance.itemDic[id] = true;
    }

    public override void HasInteractive()
    {
        //TransitionToPVL1.SetActive(true);
        openDoor.SetActive(true);
        gameObject.SetActive(false);
    }

    public override void NotInteractive()
    {
        
    }
}
