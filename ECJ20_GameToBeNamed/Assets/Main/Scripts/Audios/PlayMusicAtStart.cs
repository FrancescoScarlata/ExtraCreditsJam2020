using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicAtStart : MonoBehaviour
{
    public SimpleFMODAudioSource myAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource.Play();
    }



}
