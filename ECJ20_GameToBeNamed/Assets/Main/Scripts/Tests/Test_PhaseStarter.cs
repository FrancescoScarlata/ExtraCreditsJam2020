using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PhaseStarter : MonoBehaviour
{
    public _PhaseManager phaseToTest;
    // Start is called before the first frame update
    void Start()
    {
        phaseToTest.StartPhase();
    }

}
