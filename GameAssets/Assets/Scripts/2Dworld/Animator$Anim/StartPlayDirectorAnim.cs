using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartPlayDirectorAnim : MonoBehaviour
{
    public PlayableDirector director;
    protected bool m_AleadyTriggered;
    public GameObject PlayerCountrollerSwitch;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player")&&!m_AleadyTriggered)
        {
            director.Play();
            m_AleadyTriggered = true;
            PlayerCountrollerSwitch.SetActive(false);
            Invoke("FinishInoke",(float)director.duration);
        }
    }
    void FinishInoke()
    {
        PlayerCountrollerSwitch.SetActive(true);
    }

}
