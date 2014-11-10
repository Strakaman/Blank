using UnityEngine;
using System.Collections;
//Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
//This line should always be present at the top of scripts which use pathfinding
using Pathfinding;

public class EnemyAI : MonoBehaviour {
	//The point to move to
	private Vector3 targetPosition;
	private GameObject player;   
	private Seeker seeker;
	private bool playerInSight;                      // Whether or not the player is currently sighted.
	private bool followPath;
	
	//The calculated path
	public Path path;
	
	//The AI's speed per second
	public float speed = 100;
	
	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 3;
	
	//The waypoint we are currently moving towards
	private int currentWaypoint = 0;
	
	public void Start () {
		seeker = GetComponent<Seeker>();
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	public void OnPathComplete (Path p) {
		//Debug.Log ("Yay, we got a path back. Did it have an error? "+p.error);
		if (!p.error) {
			path = p;
			//Reset the waypoint counter
			currentWaypoint = 0;
		}
	}
	
	void Update() {
		//Direction to the next waypoint
		if (followPath && currentWaypoint < path.vectorPath.Count) {
						Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized;
						dir *= speed * Time.fixedDeltaTime;
						this.rigidbody2D.velocity = dir;
				}
	}
	
	void calculatePath() {
		if (seeker) {
			seeker.StartPath (transform.position, targetPosition, OnPathComplete);
		}
	}
	
	public void FixedUpdate () {
		targetPosition = player.transform.position;
		Invoke("calculatePath", 1);
		
		if (path == null) {
			//We have no path to move after yet
			followPath = false;
			return;
		}
		
		if (currentWaypoint >= path.vectorPath.Count) {
			rigidbody2D.velocity = new Vector3(0,0,0);
			followPath = false;
			//Debug.Log ("End Of Path Reached");
			return;
		}
		
		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
		if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			followPath = true;
			return;
		}
	}

	public void setPlayerInSightTrue() {
		playerInSight = true;
	}
	public void setPlayerInSightFalse() {
		playerInSight = false;
	}
} 