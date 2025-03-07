using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControlManager : MonoBehaviour
{
    public bool enablePlayerController;
    // 假设PlayerMovement组件是挂在同一个GameObject上的
    private PlayerMovement playerMovement;
    GameObject switchObject;

    public static event UnityAction AnimateEvent;

    void Start()
    {
        // 获取PlayerMovement组件的引用
        playerMovement = GetComponent<PlayerMovement>();

        // 寻找名为Fungus-PlayerCountrollerSwitchr的子物体
        switchObject = transform.Find("Fungus-PlayerCountrollerSwitch").gameObject;
    }
    void Update()
    {
        // 检查子物体是否存在
        if (switchObject != null)
        {
            // 根据子物体的激活状态来启用或禁用PlayerMovement组件
            if (switchObject.activeSelf)
            {
                ActivatePlayerMovementComponent();
            }
            else
            {
                DeactivatePlayerMovementComponent();
            }
        }
        else
        {
            //Debug.LogError("Fungus-PlayerCountrollerSwitchr not found on the object.");
        }
    }

    private void ActivatePlayerMovementComponent()
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
        else
        {
            //Debug.LogError("PlayerMovement component not found on the object.");
        }
    }

    private void DeactivatePlayerMovementComponent()
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            //Debug.LogError("PlayerMovement component not found on the object.");
        }
    }
    

    public static void CallAnimateEvent()//
    {
        AnimateEvent?.Invoke();
    }
}