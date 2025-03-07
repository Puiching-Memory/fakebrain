using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock2 : MonoBehaviour, IClick
{
    public GameObject LockUI;

    public void ClickEvent()
    {
        LockUI.SetActive(true);
    }
}
