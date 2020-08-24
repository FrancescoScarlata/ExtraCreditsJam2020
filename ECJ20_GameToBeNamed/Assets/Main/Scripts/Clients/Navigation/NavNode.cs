using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Method with the infos about the nodes used during the navigation
/// </summary>
public class NavNode : MonoBehaviour
{
    //public List<NavNode> nodesWalkableFromHere;

    public bool isThisASectionNode;

    public SectionManager mySection;

    public bool isCounterNode;

    public float timeToWaitInThisNode = 0;

    public bool isExitNode;

}
