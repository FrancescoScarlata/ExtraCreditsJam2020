using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicManager : SimpleFMODAudioSource
{



    // Start is called before the first frame update
    protected override void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        Play();
    }


    public void UpdatePhaseMusicValue(float value)
    {
        instance.setParameterByName("HomeOrShop", value);
    }


}
