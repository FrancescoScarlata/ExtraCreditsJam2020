using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for the phase Manager. it w
/// </summary>
public abstract class _PhaseManager : MonoBehaviour
{
    [Header("The GO that contains all the elements of this phase")]
    public GameObject thisPhaseRoot;

    /// <summary>
    /// Method to be called at the start of this phase
    /// </summary>
    public abstract void StartPhase();

    /// <summary>
    /// Method called when the phase is ended
    /// </summary>
    public abstract void EndPhase();


}
