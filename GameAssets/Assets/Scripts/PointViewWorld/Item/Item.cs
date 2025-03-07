using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour,IClick
{
    public int id;
    public string itemName;
    public bool hasBeInteractive;

    public virtual void ClickEvent()
    {
        
    }

    public virtual void CheckInteractive()
    {
        if(hasBeInteractive)
        {
            HasInteractive();
        }
        else
        {
            NotInteractive();
        }
    }

    public virtual void HasInteractive()
    {
        
    }

    public virtual void NotInteractive()
    {

    }
}
