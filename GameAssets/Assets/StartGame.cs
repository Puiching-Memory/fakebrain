using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void Update()
    {

    }

    public IEnumerator StartNewGameIEnumerator()
    {
        // 异步加载第一个场景
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Persistent", LoadSceneMode.Additive);
        
        //等待第一个场景加载完成
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        //异步加载第二个场景
        asyncLoad = SceneManager.LoadSceneAsync("2DLevel0", LoadSceneMode.Additive);
        
        //等待第二个场景加载完成
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.UnloadSceneAsync("StartMeum");
    }

    public void StartGameButton()
    {
        StartCoroutine(StartNewGameIEnumerator());
    }
}
