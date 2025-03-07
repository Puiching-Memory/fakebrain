using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemDetails_SO",menuName ="ItemDetails")]
public class ItemDetails_SO : ScriptableObject
{
    public List<itemDetail> itemDetails;

    public itemDetail GetItemDetailFromID(int itemID)
    {
        foreach (itemDetail item in itemDetails)
        {
            if(item.id == itemID)
            {
                return item;
            }
        }
        Debug.Log("没找到物品的数据");
        return null;
    }
}

[Serializable]
public class itemDetail
{
    public int id;
    public string itemName;
    public Sprite sprite;
    [TextArea]
    public string describe;
}

