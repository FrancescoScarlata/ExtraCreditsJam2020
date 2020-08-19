using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that will change the volume of th sfxs using the buses in fmod
/// </summary>
public class FMODVolumeSetter : MonoBehaviour
{

    // all the buses
    FMOD.Studio.Bus master;
    FMOD.Studio.Bus test;


    float testVolume;
    float masterVolume;

    // Start is called before the first frame update
    void Awake()
    {
        // this will assign the bus. after the test it will be a string parameter instead of hard coded
        test = FMODUnity.RuntimeManager.GetBus("bus:/Master/Test"); 
        master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
    }


    public void UpdateTestVolume(float value)
    {
        testVolume = value;
        test.setVolume(value);
    }
    public void UpdateMasterVolume(float value)
    {
        masterVolume = value;
        master.setVolume(value);
    }
}
