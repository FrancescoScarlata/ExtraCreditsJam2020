using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  A simple class to attach to the scene's main camera that tells it to follow the position of the Player with a small offset, lerp
///  for a smooth feeling follow effect.
/// </summary>
public class SimpleCameraFollow : MonoBehaviour
{
    #region Declared Variables
    public GameObject target;
    Vector3 offset;

    [SerializeField]
    [Range(0f, 10f)]
    float speed = 5.0f;

    bool b;
    #endregion

    private void LateUpdate()
    {
        // Find the Player GameObject if not already assigned via Inspector
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
            return;
        }
        else
        {
            // Record the position difference between the camera pos and Player pos as the offset. (we probably don't need this, copy+pasted from a previous project)
            if (!b)
            {
                offset = transform.position - target.transform.position;
                b = true;
            }

            // Lerp the camera position to the player position w/ offset.
            transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, Time.deltaTime * speed);
            return;
        }
    }
}
