using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_SimpleExplosionWithFMOD : MonoBehaviour
{
    public GameObject fmodGO;
    public FMODLocalParamaterSetter localSetter;
    
    public void DisableAndEnableGO()
    {
        fmodGO.SetActive(false);
        // changes the parameter if it was changed
        localSetter.SetParameterValueByName();
        fmodGO.SetActive(true);
    }


}
