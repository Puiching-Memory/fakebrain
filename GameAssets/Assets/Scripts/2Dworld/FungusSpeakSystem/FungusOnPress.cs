using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using Unity.VisualScripting;

public class FungusOnPress : MonoBehaviour
{

    [Header("对应的对话Block名")]
    public string BlockName;

    private Flowchart flowchart;
    private bool canSay;
    public bool OnlyOnce = false;
    private bool OnlyOnceSwitch;

    private GameObject PressPromptButton;


    void Start()
    {
        flowchart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        PressPromptButton = GameObject.Find("Player").transform.Find("PressPromptButton").gameObject;
        PressPromptButton.SetActive(false);

        OnlyOnceSwitch = true;
    }
    private void Update()
    {
        //鼠标按下左键触发对话方法
        if (Input.GetKeyDown(KeyCode.E))
        {
            Say();
            PressPromptButton.SetActive(false);
        }
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
        if (other.tag.Equals("Player")&&OnlyOnceSwitch)
        {
            PressPromptButton.SetActive(true);
            canSay = true;
            if (OnlyOnce)
            {
                OnlyOnceSwitch = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //如果检测到玩家离开触发范围
        if (other.tag.Equals("Player"))
        {
            PressPromptButton.SetActive(false);
            canSay = false;
        } 
    }
}

