using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : Singlten<TransitionManager>
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;
    private bool isFade;

    public GameObject MainCamera;
    public GameObject POVCanvas;

    private void Update()
    {
        Scene scene1 = SceneManager.GetSceneByName("2DLevel0");
        Scene scene2 = SceneManager.GetSceneByName("BaseLevel");
        if (scene1.isLoaded||scene2.isLoaded)
        {
            MainCamera.SetActive(false);
            POVCanvas.SetActive(false);

        }
        else
        {
            MainCamera.SetActive(true);
            POVCanvas.SetActive(true);
        }

    }

    public void Transition(string from,string to)
    {
        if(!isFade)
        {
            StartCoroutine(TransitionToRoom(from, to));
        }
    }
    public IEnumerator TransitionToRoom(string from,string to)
    {
        yield return Fade(1);
        EventHandler.CallBeforeUnloadScene();
        yield return SceneManager.UnloadSceneAsync(from);
        yield return SceneManager.LoadSceneAsync(to,LoadSceneMode.Additive);
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);
        EventHandler.CallAfterLoadScene();
        yield return Fade(0);
    }

    public IEnumerator Fade(float targetAlpha)
    {
        isFade = true;
        fadeCanvasGroup.blocksRaycasts = true;
        float speed=Mathf.Abs(fadeCanvasGroup.alpha-targetAlpha)/fadeDuration;
        while(!Mathf.Approximately(fadeCanvasGroup.alpha,targetAlpha))
        {
            fadeCanvasGroup.alpha=Mathf.MoveTowards(fadeCanvasGroup.alpha,targetAlpha,speed*Time.deltaTime);
            yield return null;
        }
        isFade = false;
        fadeCanvasGroup.blocksRaycasts = false;
    }
}
