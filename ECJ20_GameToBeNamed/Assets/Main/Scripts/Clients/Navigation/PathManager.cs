using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible to give the client a path to follow
/// </summary>
public class PathManager : MonoBehaviour
{
    [SerializeField] public PathToFollow[] pathsToFollow = new PathToFollow [12];


    /// <summary>
    /// Gets the path that a client has to follow to reach the section and go out,
    /// based on the item that is wanted
    /// </summary>
    /// <param name="itemToBuy"></param>
    /// <returns></returns>
    public PathToFollow GetThePath(ItemType itemToBuy)
    {
        return pathsToFollow[(int)itemToBuy];
    }


}

[System.Serializable]
public class PathToFollow
{
    public NavNode[] nodes;
}