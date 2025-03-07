using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singlten<ObjectManager>
{
    public Dictionary<int,bool> itemDic= new Dictionary<int,bool>();
    public bool canAddTVControl;

    private void OnEnable()
    {
        EventHandler.BeforeUnloadScene += OnBeforeUnloadScene;
        EventHandler.AfterLoadScene += OnAfterLoadScene;
    }


    private void OnDisable()
    {
        EventHandler.BeforeUnloadScene -= OnBeforeUnloadScene;
        EventHandler.AfterLoadScene -= OnAfterLoadScene;
    }

    private void OnAfterLoadScene()
    {
        foreach(Item item in FindObjectsOfType<Item>())
        {
            if(!itemDic.ContainsKey(item.id))
            {
                itemDic.Add(item.id, false);
            }
            else
            {
                item.hasBeInteractive = itemDic[item.id];
                item.CheckInteractive();
            }
        }
    }

    private void OnBeforeUnloadScene()
    {

    }
}
