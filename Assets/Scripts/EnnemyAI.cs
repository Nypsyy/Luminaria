using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 200f;
    public float nextWptDist = 3f;
    public Transform ennemyGFX;
    public float aggroDist = 10f;

    Path path;
    int currWpt = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb2D;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb2D = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            if (Vector2.Distance(rb2D.position, target.position) <= aggroDist)
                seeker.StartPath(rb2D.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currWpt = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null)
            return;

        if (currWpt >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
            reachedEndOfPath = false;

        Vector2 direction = ((Vector2)path.vectorPath[currWpt] - rb2D.position).normalized;
        Vector2 force = direction * speed * Time.fixedDeltaTime;

        rb2D.AddForce(force);

        float dist = Vector2.Distance(rb2D.position, path.vectorPath[currWpt]);

        if (dist < nextWptDist)
            currWpt++;

        if (force.x >= 0.01f)
            ennemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        else if (force.x <= 0.01f)
            ennemyGFX.localScale = new Vector3(1f, 1f, 1f);
    }
}
