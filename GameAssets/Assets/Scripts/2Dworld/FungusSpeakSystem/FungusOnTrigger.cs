using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class FungusOnTrigger : MonoBehaviour
{

    [Header("对应的对话Block名")]
    public string BlockName;

    private Flowchart flowchart;
    private bool canSay;


    void Start()
    {
        flowchart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        canSay = true;
    }
    void Say()
    {
        if (canSay)
        {
            if (flowchart.HasBlock(BlockName))
            {
                flowchart.ExecuteBlock(BlockName);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //如果检测到玩家进入触发范围
        if (other.tag.Equals("Player")&&canSay)
        {
            Debug.Log("Enter！");
            Say();
            canSay = false;
        }
    }

}
