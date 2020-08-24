using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopMusicAtEndLevel : MonoBehaviour
{

    public SimpleFMODAudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneUnloaded += OnSceneUnLoaded;
    }


    public void OnSceneUnLoaded(Scene thisScene) 
    {
        audioSource.StopPlay();
    }

}
