using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassWord : MonoBehaviour
{
    public int passWord;
    public int num1, num2, num3, num4,num5,num6,num7;
    public Text Tnum1, Tnum2, Tnum3, Tnum4,Tnum5,Tnum6,Tnum7;

    private void Start()
    {
        num1 = 0;
        num2 = 0;
        num3 = 0;
        num4 = 0;
        num5 = 0;
        num6 = 0;
        num7 = 0;
        UpdateText();
    }

    private void Update()
    {
        if (GetCurrentNum() == passWord)
        {
            ObjectManager.Instance.canAddTVControl=true;
            TransitionManager.Instance.Transition("LockScene", "PVLevel1");
            passWord = 0;
        }
    }

    public void UpNum(int index)
    {
        switch (index)
        {
            case 1:
                {
                    num1++;
                    if (num1 > 9)
                    {
                        num1 = 0;
                    }
                    break;
                }
            case 2:
                {
                    num2++;
                    if (num2 > 9)
                    {
                        num2 = 0;
                    }
                    break;
                }
            case 3:
                {
                    num3++;
                    if (num3 > 9)
                    {
                        num3 = 0;
                    }
                    break;
                }
            case 4:
                {
                    num4++;
                    if (num4 > 9)
                    {
                        num4 = 0;
                    }
                    break;
                }
            case 5:
                {
                    num5++;
                    if (num5 > 9)
                    {
                        num5 = 0;
                    }
                    break;
                }
            case 6:
                {
                    num6++;
                    if(num6 > 9)
                    {
                        num6 = 0;
                    }
                    break;
                }
            case 7:
                {
                    num7++;
                    if (num7 > 9)
                    {
                        num7 = 0;
                    }
                    break;
                }
        }
        UpdateText();
    }

    public void DownNum(int index)
    {
        switch (index)
        {
            case 1:
                {
                    num1--;
                    if (num1 < 0)
                    {
                        num1 = 9;
                    }
                    break;
                }
            case 2:
                {
                    num2--;
                    if (num2 < 0)
                    {
                        num2 = 9;
                    }
                    break;
                }
            case 3:
                {
                    num3--;
                    if (num3 < 0)
                    {
                        num3 = 9;
                    }
                    break;
                }
            case 4:
                {
                    num4--;
                    if (num4 < 0)
                    {
                        num4 = 9;
                    }
                    break;
                }
            case 5:
                {
                    num5--;
                    if (num5 < 0)
                    {
                        num5 = 9;
                    }
                    break;
                }
            case 6:
                {
                    num6--;
                    if (num6 < 0)
                    {
                        num6 = 9;
                    }
                    break;
                }
            case 7:
                {
                    num7++;
                    if (num7 < 0)
                    {
                        num7 = 9;
                    }
                    break;
                }
        }
        UpdateText();
    }

    public void UpdateText()
    {
        Tnum1.text = num1.ToString();
        Tnum2.text = num2.ToString();
        Tnum3.text = num3.ToString();
        Tnum4.text = num4.ToString();
        Tnum5.text = num5.ToString();
        Tnum6.text = num6.ToString();
        Tnum7.text = num7.ToString();
    }

    public int GetCurrentNum()
    {
        return (num1*1000000+num2*100000+num3*10000+num4*1000+num5*100+num6*10+num7);
    }
}
