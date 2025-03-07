using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler
{
    public static event Action BeforeUnloadScene;
    

    public static void CallBeforeUnloadScene()
    {
        BeforeUnloadScene?.Invoke();
    }

    public static event Action AfterLoadScene;

    public static void CallAfterLoadScene()
    {
        AfterLoadScene?.Invoke();
    }

    public static event Action CheckPointReStartGame2DEvent;//2D关卡用：从存档重启游戏
    
    public static void CallCheckPointReStartGame2D()
    {
        CheckPointReStartGame2DEvent?.Invoke();
    }

    
}
