using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using static UnityEditor.Progress;

public class InventoryManager : Singlten<InventoryManager>
{
    public List<Item> Items=new List<Item>();
    public ItemDetails_SO itemDetails_SO;
    public SlotUI slotUI;
    public int currentItemIndex;
    public int currentItemID;

    private void Start()
    {
        currentItemIndex = 0;
        currentItemID= -1;
    }
    private void Update()
    {

    }

    public void AddItem(Item item)
    {
        Items.Add(item);
        slotUI.SetSlotUI(itemDetails_SO.GetItemDetailFromID(item.id));
        currentItemIndex = Items.Count - 1;
        currentItemID = item.id;
        ObjectManager.Instance.itemDic[item.id] =true;
    }

    public void RemoveItem(int itemID)
    {
        foreach(Item item in Items)
        {
            if (item.id == itemID)
            {
                Items.Remove(item);
                NextItem();
                return;
            }
        }
    }

    public void NextItem()
    {
        currentItemIndex++;
        if(Items.Count<=0)
        {
            slotUI.SetSlotNull();
            currentItemID = -1;
            return;
        }
        if(currentItemIndex>Items.Count-1)
        {
            currentItemIndex = 0;
        }
        slotUI.SetSlotUI(itemDetails_SO.GetItemDetailFromID(Items[currentItemIndex].id));
        currentItemID = Items[currentItemIndex].id;
    }
    public void FontItem()
    {
        currentItemIndex--;
        if (Items.Count <= 0)
        {
            slotUI.SetSlotNull();
            return;
        }
        if (currentItemIndex <0)
        {
            currentItemIndex = Items.Count-1;
        }
        slotUI.SetSlotUI(itemDetails_SO.GetItemDetailFromID(Items[currentItemIndex].id));
        currentItemID = Items[currentItemIndex].id;
    }
}
