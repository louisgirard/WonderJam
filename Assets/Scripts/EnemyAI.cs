using UnityEngine;
using Pathfinding;
using System.Runtime.CompilerServices;

public class EnemyAI : MonoBehaviour
{	
	[SerializeField] float speed = 20;
	[SerializeField] float nextWaypointDistance = 1f;

	Transform target;
	Animator animator;

	Path path;
	int currentWaypoint = 0;
	Seeker seeker;

	void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(transform.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
		if(!p.error)
        {
			path = p;
			currentWaypoint = 0;
        }
    }

    private void Update()
    {
		if (path == null) return;

		if(currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

		Vector2 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;
		transform.Translate(direction * speed * Time.deltaTime);
        animator.SetTrigger("walk");

		float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

		if(distance < nextWaypointDistance)
        {
			currentWaypoint++;
        }
    }
}
