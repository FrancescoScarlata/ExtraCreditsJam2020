using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class can be expanded with more stuff in children if need.
/// The basic function is that the event can be played and eventually stopped
/// </summary>
public class SimpleFMODAudioSource : MonoBehaviour
{

    // this creates a separate instance, it is not attached to the emitter
    private FMOD.Studio.EventInstance instance;

    // this is the event, the one that is in the fmod project
    [FMODUnity.EventRef]
    public string fmodEvent;



    // Start is called before the first frame update
    protected virtual void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
    }

    /// <summary>
    /// This should start the instance sound. Aka it should play the sound.
    /// This will be called when someone needs to play this sfx
    /// </summary>
    public virtual void Play()
    {
        // this will start to play the sound
        instance.start();
    }

    /// <summary>
    /// Method called when the sfx should be stopped.
    /// </summary>
    public virtual void StopPlay()
    {
        // this will stop the sound.
        //The allow fadeout should fade the sound istead of cut it immediately
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

}
