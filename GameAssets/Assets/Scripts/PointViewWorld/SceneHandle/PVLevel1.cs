using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVLevel1 : MonoBehaviour
{
    public GameObject tvControl;

    private void Start()
    {
        if(ObjectManager.Instance.canAddTVControl)
        {
            tvControl.SetActive(true);
            ObjectManager.Instance.canAddTVControl = false;
        }
    }
}
