using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using Unity.VisualScripting;
using UnityEngine;

public class LevelCamera : MonoBehaviour
{
    /*关卡镜头逻辑：
    1.玩家进入关卡区域碰撞箱，镜头移动到指定的中心位置，并一并缩放镜头大小，直到适应关卡尺寸
    2.玩家离开关卡区域，回到原有的跟随镜头模式*/
    public Transform CameraCenterPotion;
    public float LevelCameraSize = 15;

    public GameObject Camera;
    private CameraFollow cameraFollow;

    private float OrgCameraSize;

    private bool CameraIsInsideTheLevel;

    public float changeSpeed = 5;

    private Transform orgparent;

    

    // Update is called once per frame
    void Update()
    {
        if(CameraIsInsideTheLevel)
        {
            cameraFollow = Camera.GetComponent<CameraFollow>();
            cameraFollow.LevelCarmeraMode = true;

            float distance = Vector2.Distance(CameraCenterPotion.position, Camera.transform.position);
            if (distance>=0.5)
            {
                // 更新物体A的位置，使其逐渐向物体B移动
                Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, new Vector3(CameraCenterPotion.position.x, CameraCenterPotion.position.y, Camera.transform.position.z), changeSpeed * Time.deltaTime);
            }
            if (Camera.GetComponent<Camera>().orthographicSize<LevelCameraSize)
            {
                Camera.GetComponent<Camera>().orthographicSize += changeSpeed * Time.deltaTime;
            }

            if(Input.GetMouseButton(1))
            {
                Camera.transform.SetParent(orgparent);
                CameraIsInsideTheLevel = false;
            }
            else
            {
                Camera.transform.SetParent(CameraCenterPotion);
            }
            



        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)//玩家进入关卡区域
    {
        if (collision.gameObject.CompareTag("Player"))//获取玩家的子相机，改变参数
        {
            Camera = collision.gameObject.transform.Find("Main Camera").gameObject;//获取挂载在玩家身上的相机组件
            //Camera = GameObject.FindWithTag("MainCamera").gameObject;
            cameraFollow = Camera.GetComponent<CameraFollow>();
            OrgCameraSize = Camera.GetComponent<Camera>().orthographicSize;

            orgparent = Camera.transform.parent;
            Camera.transform.SetParent(CameraCenterPotion);

            CameraIsInsideTheLevel = true;

        }
    }
    private void OnTriggerStay2D(Collider2D collision)//玩家进入关卡区域
    {
        if (collision.gameObject.CompareTag("Player")&&!Input.GetMouseButton(1))//获取玩家的子相机，改变参数
        {
            //Camera = collision.gameObject.transform.Find("Main Camera").gameObject;//获取挂载在玩家身上的相机组件
            //cameraFollow = Camera.GetComponent<CameraFollow>();
            //Camera.transform.SetParent(CameraCenterPotion);
            CameraIsInsideTheLevel = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//玩家退出关卡区域
    {
        if (collision.gameObject.CompareTag("Player")&&cameraFollow != null)
        {
            
            Camera.transform.SetParent(orgparent);
            if (gameObject.activeInHierarchy)StartCoroutine(ExitAreaCoroutine());
            CameraIsInsideTheLevel = false;
            cameraFollow.LevelCarmeraMode = false;

        }


    }

    IEnumerator ExitAreaCoroutine()
    {
        bool CameraReturnisDone = false;
        while (!CameraReturnisDone)
        {
            if (Camera.GetComponent<Camera>().orthographicSize>OrgCameraSize)
            {
                Camera.GetComponent<Camera>().orthographicSize -= changeSpeed * Time.deltaTime;
            }
            else
            {
                CameraReturnisDone = true;
            }
        yield return null;
        }
    }

}

