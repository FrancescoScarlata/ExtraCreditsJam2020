using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for a simple float parameter setter. It might be updated with the different types of parameter usages.
/// The audio class will need to be written in a good way to have a good hierarchy probably
/// </summary>
public class FMODLocalParamaterSetter : MonoBehaviour
{
    // this creates a separate instance, it is not attached to the emitter
    private FMOD.Studio.EventInstance instance;

    // this is the event, the one that is in the fmod project
    [FMODUnity.EventRef]
    public string fmodEvent;

    //the string of the parameter inside the fmod project
    public string parameterName;

    public string globalParaName;

    [SerializeField]
    [Range(-12, 12f)]
    private float parameterValue;

    [SerializeField]
    [Range(0,1f)]
    private float globalParaValue;

    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
    }

    // just to be coherent with the example with the emitter
    private void OnEnable()
    {
        Play();
    }

    //as above
    private void OnDisable()
    {
        StopPlay();
    }

    /// <summary>
    /// This should start the instance sound. Aka it should play the sound
    /// </summary>
    protected void Play( )
    {
        // this will start to play the sound
        instance.start();
        SetParameterValueByName();
    }


    protected void StopPlay()
    {
        // this will stop the sound.
        //The allow fadeout should fade the sound istead of cut it immediately
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void SetParameterValueByName()
    {
        instance.setParameterByName(parameterName, parameterValue);
    }

    public void SetRandomPitch()
    {
        parameterValue = Random.Range(-12, 12f);
    }

    public void SetReverbValue(float value)
    {
        globalParaValue = value;
        ChangeGlobalParameterByname();
    }

    public void ChangeGlobalParameterByname()
    {
        //This will change the global parameter. it is for all instances, so you don't call of a single instance
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(globalParaName, globalParaValue);
    }


}
