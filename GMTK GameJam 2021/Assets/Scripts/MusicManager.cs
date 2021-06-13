using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource chillAudio;
    public AudioSource hypeAudio;
    bool marked = false;

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1 && !marked)
        {
            DontDestroyOnLoad(gameObject);
            marked = true;
        }
        if (GameManager.instance.superPositionState == SuperPositionState.TOGETHER) {
            chillAudio.volume = 1;
            hypeAudio.volume = 0;
        } else {
            chillAudio.volume = 0;
            hypeAudio.volume = 1;
        }
    }
}
