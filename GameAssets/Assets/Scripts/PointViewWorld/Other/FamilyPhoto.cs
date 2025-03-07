using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FamilyPhoto :MonoBehaviour,IClick
{
    public GameObject familyUI;
    public bool isShowPhoto;

    public void ClickEvent()
    {
        if (isShowPhoto) return;
        familyUI.GetComponent<Animator>().SetTrigger("Big");
        isShowPhoto= true;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1)&&isShowPhoto)
        {
            familyUI.GetComponent<Animator>().SetTrigger("Small");
            isShowPhoto= false;
        }
    }
}
