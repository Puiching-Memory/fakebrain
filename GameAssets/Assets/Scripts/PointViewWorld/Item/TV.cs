using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV :MonoBehaviour,IClick
{
    public void ClickEvent()
    {
        if (InventoryManager.Instance.currentItemID != 4) return;
        TransitionManager.Instance.Transition("PVlevel1", "BlackPVlevel1");
    }
}
