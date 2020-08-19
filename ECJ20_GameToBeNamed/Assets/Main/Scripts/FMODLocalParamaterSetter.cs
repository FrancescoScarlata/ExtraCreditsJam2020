using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for a simple float parameter setter. It can be updated with the different types of parameter usages.
/// </summary>
public class FMODLocalParamaterSetter : MonoBehaviour
{

    private FMOD.Studio.EventInstance instance;

    [FMODUnity.EventRef]
    public string fmodEvent;

    public string parameterName;

    [SerializeField]
    [Range(-12, 12f)]
    private float parameterValue;

    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
    }


    private void Update()
    {
        instance.setParameterByName(parameterName, parameterValue);
    }

    public void SetParameterValueByName()
    {
        instance.setParameterByName(parameterName, parameterValue);
    }

}
