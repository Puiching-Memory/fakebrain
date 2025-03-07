using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour, IClick
{
    public GameObject paperUI;
    public bool isShowPaper;

    public void ClickEvent()
    {
        if (isShowPaper) return;
        paperUI.GetComponent<Animator>().SetTrigger("Big");
        isShowPaper = true;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && isShowPaper)
        {
            paperUI.GetComponent<Animator>().SetTrigger("Small");
            isShowPaper = false;
        }
    }
}
