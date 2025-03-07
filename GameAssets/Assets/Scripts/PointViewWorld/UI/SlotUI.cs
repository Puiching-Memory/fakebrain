using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public Text itemName;
    public Image itemImg;
    public GameObject describeUI;
    public Text describeText;
    public Sprite nullSprite;


    public void SetSlotUI(itemDetail itemDetail)
    {
        if(itemDetail==null)
        {
            Debug.Log("传入的物品数据为空，无法更新slot的UI");
            return;
        }
        itemName.text = itemDetail.itemName;
        itemImg.sprite = itemDetail.sprite;
        itemImg.SetNativeSize();
        describeText.text = itemDetail.describe;
    }

    public void SetSlotNull()
    {
        itemName.text = string.Empty;
        itemImg.sprite = nullSprite;
        describeText.text = string.Empty;
    }


}
