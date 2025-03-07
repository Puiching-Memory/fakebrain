using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemImgUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public SlotUI slotUI;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.GetComponent<Image>().sprite == null) return;
        slotUI.describeUI.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        slotUI.describeUI.SetActive(false);
    }
}
