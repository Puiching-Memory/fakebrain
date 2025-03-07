using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiChaInfiniteScoll : MonoBehaviour
{
    [Header("无限视图滚动")]
    public GameObject mainCamera;
    public float mapWidth;
    public int mapNums;
    private float totalwidth;
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mapWidth = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        //mapWidth *= 100*transform.localScale.x;
        totalwidth = mapWidth * mapNums;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tempPosition=transform.position;// 获取当前位置
        if(mainCamera.transform.position.x>transform.position.x + totalwidth / 2)
        {
            tempPosition.x += totalwidth;// 将地图向右平移一个完整的地图宽度
            transform.position=tempPosition;//更新位置
        }

        else if(mainCamera.transform.position.x< transform.position.x- totalwidth / 2)
        {
            tempPosition.x-= totalwidth;// 将地图向左平移一个完整的地图宽度
            transform.position=tempPosition;//更新位置
        }

    }
}