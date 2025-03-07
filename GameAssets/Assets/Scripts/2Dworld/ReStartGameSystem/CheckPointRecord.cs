using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckPointRecord : MonoBehaviour
{
    private GameObject player;
    private ReStartManager reStartManager;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        reStartManager = player.GetComponent<ReStartManager>();
    }

    private void OnTriggerEnter2D(Collider2D collsion)
    {
        if(collsion.transform.tag == "Player")
        {
            EventHandler.CheckPointReStartGame2DEvent -= RefreshPlayerCheckpoint;
            reStartManager.CurrentCheckPoint = transform.position;
            EventHandler.CheckPointReStartGame2DEvent += RefreshPlayerCheckpoint;
            Debug.Log("Record new checkPoint!");
            
        }
    }
    private void RefreshPlayerCheckpoint()
    {
        player.transform.position = reStartManager.CurrentCheckPoint;
    }


    private void OnDisable()
    {
        EventHandler.CheckPointReStartGame2DEvent -= RefreshPlayerCheckpoint;
    }
}
