using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOverTalk : MonoBehaviour
{
    public string fromSceneName;
    public string toSceneName;

    public void NextLevel()
    {
        TransitionManager.Instance.Transition(fromSceneName,toSceneName);
    }
}

