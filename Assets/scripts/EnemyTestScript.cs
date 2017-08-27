using UnityEngine;
using System.Collections;
using Pathfinding;
using System;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Rigidbody2D))]
public class EnemyTestScript : MonoBehaviour {
	//what to chase? THIS IS SET BY A COLLISION TRIGGER ON A SEPARATE SCRIPT
	public Transform target = null;

	//How many times each second we will update our path
	public float updateRate = 10f;

	//caching
	private Seeker seeker;
	private Rigidbody2D rb;

	//The calculated path
	public Path path;

	//The AI's speed per second
	public float speed = 2f;
	public ForceMode2D fMode;

	[HideInInspector]
	public bool pathIsEnded = false;

	//The max distance from the AI to a waypoint for it to continue to the nexxt waypoint
	public float nextWaypointDistance = 3;

	//The waypoint we are currently moving towards
	private int currentWaypoint = 0;

	//updating path
	/*
	 * This is a useful flag because it allows the path update coroutine to update only once.
	  Path update corouting is recursive so multiple calls at base causes problems because
	  it is inherently iterative*/
	public bool updatingPath = false;
	public bool pathAlreadyUpdated = false;

	void Start () {
		seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();
	}

	public void OnPathComplete(Path p){
		//Debug.Log ("We got a path. Did it have an error?" + p.error);
		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		}
	}

	IEnumerator UpdatePath(){
		seeker.StartPath(transform.position, target.position, OnPathComplete);

		yield return new WaitForSeconds (1f/updateRate);
		StartCoroutine (UpdatePath ());
	}

	void FixedUpdate(){
		//TODO: if player detected go to it
		GoToTarget();
	}

	//moves along nodes on graph along the seeker's path
	void GoToTarget(){
		if (target == null) {
			return;
		}
		if (updatingPath) {
			StartCoroutine (UpdatePath ());
			updatingPath = false;
			pathAlreadyUpdated = true;
		}

		//seeker.StartPath(transform.position, target.position, OnPathComplete);

		//TODO: Always look at player?

		if (path == null)
			return;

		if (currentWaypoint >= path.vectorPath.Count){
			if (pathIsEnded)
				return;

			//Debug.Log ("End of path reached");
			pathIsEnded = true;
			return;
		}
		pathIsEnded = false;

		//Direction to next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

		dir *= speed * Time.fixedDeltaTime;

		//Move the AI
		rb.AddForce (dir, fMode);

		float dist = Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]);
		if (dist < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}
}
