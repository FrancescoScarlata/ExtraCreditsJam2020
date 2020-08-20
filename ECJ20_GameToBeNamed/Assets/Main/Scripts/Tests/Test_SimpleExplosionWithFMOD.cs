using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_SimpleExplosionWithFMOD : MonoBehaviour
{
    public GameObject fmodGO;
    public FMODLocalParamaterSetter localSetter;
    
    /// <summary>
    /// This will be used to test both the sound with the emitter, that the one written separately
    /// </summary>
    public void DisableAndEnableGO()
    {
        fmodGO.SetActive(false);

        fmodGO.SetActive(true);
    }


    public void DisableAndEnableWithRandomPitch()
    {
        fmodGO.SetActive(false);
        //changes the pitch
        if (localSetter)
            localSetter.SetRandomPitch();
        fmodGO.SetActive(true);
    }


}
