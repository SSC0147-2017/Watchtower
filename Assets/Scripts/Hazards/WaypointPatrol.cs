using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class WaypointPatrol : MonoBehaviour {

	public NavMeshAgent agent;

	public WaypointController path;

	private Vector3[] waypoints;
	private int nextWaypoint = 0;


	void Start(){

		waypoints = path.waypoints;
		moveToNextWaypoint ();
	}

	void moveToNextWaypoint(){
		if (waypoints.Length > 0) {
			agent.destination = waypoints [nextWaypoint];
			nextWaypoint = (nextWaypoint + 1) % waypoints.Length;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (agent.remainingDistance < 0.5f)
			moveToNextWaypoint ();

	}
}
