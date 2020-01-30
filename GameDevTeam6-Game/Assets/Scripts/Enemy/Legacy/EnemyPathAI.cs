using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;



public class EnemyPathAI : MonoBehaviour
{
    public Transform target;

    private float speed = 2f;
    private float nextWayPointDistance = 0.5f;

    private Path path;
    private int currentWayPoint = 0;
    private bool reachedEndofPath = false;

    private Seeker seeker;
    private Rigidbody2D rb;
    private EnemyGFX gfx;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        gfx = GetComponentInChildren<EnemyGFX>();

        InvokeRepeating("UpdatePath", 0f, 1f);
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    // FixedUpdate is framerate independent (called a fixed number of times per second)
    void FixedUpdate()
    {
        if(path == null)
            return;

        if(currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndofPath = true;
        } else
        {
            reachedEndofPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        //Vector2 force = direction * speed * Time.deltaTime;

        rb.velocity = direction * speed;
        gfx.Direction = direction;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }
    }

}
