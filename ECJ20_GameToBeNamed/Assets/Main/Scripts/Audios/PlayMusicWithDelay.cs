using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayMusicWithDelay : MonoBehaviour
{
    public SimpleFMODAudioSource myAudioSource;
    public float timeToWait = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartMusicWithDelay()); 
    }


    IEnumerator StartMusicWithDelay()
    {
        yield return new WaitForSeconds(timeToWait);
        myAudioSource.Play();
    }
}
