using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionRoom : MonoBehaviour,IClick
{
    public string fromSceneName;
    public string toSceneName;

    public void ClickEvent()
    {
        TransitionManager.Instance.Transition(fromSceneName,toSceneName);
    }
}
