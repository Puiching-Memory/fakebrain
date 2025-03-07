using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTransition : MonoBehaviour
{
    public string fromSceneName;
    public string toSceneName;

    public void TransitionRoom()
    {
        TransitionManager.Instance.Transition(fromSceneName, toSceneName);
    }
}
