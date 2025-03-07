using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackLockUI : MonoBehaviour
{
    public string fromScene;
    public string toScene;
    public Button btn;

    private void Start()
    {
        btn.onClick.AddListener(BackToLock);
    }

    public void BackToLock()
    {
        TransitionManager.Instance.Transition(fromScene, toScene);
    }
}
