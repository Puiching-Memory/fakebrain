using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReStartManager : MonoBehaviour
{
    public Vector2 CurrentCheckPoint;
    // void Awake()
    // {
    //     if(Instance==null)
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(this);
    //     }
    //     else
    //     {
    //         Destroy(Instance);
    //     }
        
    // }

    void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        CurrentCheckPoint = player.transform.position;
        EventHandler.CheckPointReStartGame2DEvent += RefreshPlayerCheckpoint;
        
    }

    public void ResetGameFromCheckpoint()//从检查点开始
    {
        Debug.Log("RestartGame！");
        EventHandler.CallCheckPointReStartGame2D();
    }

        private void RefreshPlayerCheckpoint()
    {
        transform.position = CurrentCheckPoint;
    }

    private void OnDisable()
    {
        EventHandler.CheckPointReStartGame2DEvent -= RefreshPlayerCheckpoint;
    }


}
