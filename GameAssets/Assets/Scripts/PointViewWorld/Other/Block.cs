using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Item
{
    public GameObject downBlock;
    public BoxCollider2D boxCollider2D;


    public override void ClickEvent()
    {
        downBlock.SetActive(true);
        boxCollider2D.enabled = true;
        ObjectManager.Instance.itemDic[id] = true;
        gameObject.SetActive(false);
    }

    public override void HasInteractive()
    {
        downBlock.SetActive(true);
        boxCollider2D.enabled = true;
        gameObject.SetActive(false);
    }

    public override void NotInteractive()
    {
        
    }
}
