using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// Classwith some infos about the client
/// </summary>
public class ClientController : MonoBehaviour
{
    public float movSpeed=5;

    public bool isInfected;

    protected bool itemPickedUp=false;

    /// <summary>
    /// Method called to reset the client pick up
    /// </summary>
    public void ResetClient()
    {
        itemPickedUp = false;
    }

    /// <summary>
    /// Method called by the AI director. Will stop the client, make it start from a differentPosition
    /// </summary>
    /// <param name="newPosition"></param>
    public void Reposition(Vector3 newPosition)
    {

    }


}

public enum ClientState
{
    LOOKINFORITEM,
    GOINTOPAY
}
