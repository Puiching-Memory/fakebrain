using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PassWord2 : MonoBehaviour
{
    public GameObject IDCard;
    public int maxNum;
    public int currentIndex;
    public int[] password;
    public Text[] textUIs;

    private void Start()
    {
        currentIndex = -1;
        foreach(Text text in textUIs)
        {
            text.text=string.Empty;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            gameObject.SetActive(false);
        }
    }

    public void InputPassword(int num)
    {
        currentIndex++;
        if (currentIndex > maxNum - 1)
        {
            currentIndex--;
        }
        password[currentIndex] = num;
        textUIs[currentIndex].text = password[currentIndex].ToString();
    }

    public void DelectPassword()
    {
        textUIs[currentIndex].text = string.Empty;
        password[currentIndex] = 0;
        if (currentIndex <= -1) return;
        currentIndex--;
    }

    public void ConfirmPassWord()
    {
        if(GetCurrentNum()=="5")
        {
            IDCard.SetActive(true);
        }
    }

    public string GetCurrentNum()
    {
        string snum="";
        foreach(Text text in textUIs)
        {
            snum += text.text;
        }
        Debug.Log(snum);
        return snum;
    }
}
