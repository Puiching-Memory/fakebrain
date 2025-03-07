using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackLevelScene : Singlten<BlackLevelScene>
{
    public int point=0;

    private void Update()
    {
        CheckWin();
    }

    public void CheckWin()
    {
        if(point>=2)
        {
            TransitionManager.Instance.Transition("BlackPVLevel1", "Ending");
        }
    }
}
