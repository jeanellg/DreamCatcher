using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]

public class EnemyAI_aStar : MonoBehaviour {
    // The target being chased
    public Transform target;

    // Times per second for updating the path
    public float updateRate = 2f;
    private Seeker seeker;
    private Rigidbody2D rb;

    // The calculated path of the algorithm
    public Path path;

    // Enemy AI speed per second
    public float speed = 300f;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathEnd = false;

    // The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;
    // Waypoint we are currently going towards
    private int currentWaypoint = 0;



    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            Debug.LogError("No player found");
            return;
        }
        // Start a new path to the target, return the result on to OnPathComplete
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath ());
    }
    IEnumerator UpdatePath() { 
        if (target == null)
        {
             yield return false;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());

    }

   
    public void OnPathComplete(Path p)
    {
        Debug.Log(" We got a path. Test for error?" + p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;

        }
    }
    void FixedUpdate ()
    {
        if (target == null)
        {
            return;
        }
        if (path == null)
        {
            return;
        }
        if(currentWaypoint >= path.vectorPath.Count)
        {
            if (pathEnd)
                return;

            Debug.Log("End of path reached.");
            pathEnd = true;
            return;
        }
        pathEnd = false;

        // Getting directions to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        // Move the AI 
        rb.AddForce(dir, fMode);

        float dist = (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]));
        if( dist < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }
}
