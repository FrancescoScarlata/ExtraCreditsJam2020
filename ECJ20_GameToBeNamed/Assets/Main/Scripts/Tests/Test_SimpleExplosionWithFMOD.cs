using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_SimpleExplosionWithFMOD : MonoBehaviour
{
    public GameObject fmodGO;
    
    /// <summary>
    /// This will be used to test both the sound with the emitter, that the one written separately
    /// </summary>
    public void DisableAndEnableGO()
    {
        fmodGO.SetActive(false);

        fmodGO.SetActive(true);
    }


}
