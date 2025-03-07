using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusicManager : MonoBehaviour
{
    public string MusicName;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.Play(MusicName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
