using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlipEnnemies : MonoBehaviour
{
    [SerializeField] AIPath path;
    [SerializeField] float scale;

    private void Update()
    {
        if (path.desiredVelocity.x > 0.01f)
            transform.localScale = new Vector3(-scale, scale, scale);
        else if (path.desiredVelocity.x < -0.01f)
            transform.localScale = new Vector3(scale, scale, scale);
    }
}
