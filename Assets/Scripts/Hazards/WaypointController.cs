using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaypointController : MonoBehaviour {

	public Vector3[] waypoints;

	void Awake(){
		for (int i = 0; i < transform.childCount; i++) {
			waypoints [i] = transform.GetChild (i).transform.position;
		}
	}


	/**
	 * Desenha as linhas entre os gizmos
	 */
	void OnDrawGizmos(){
		waypoints = new Vector3[transform.childCount];

		for (int i = 0; i < transform.childCount; i++) {
			waypoints [i] = transform.GetChild (i).transform.position;
		}

		for (int i = 1; i < waypoints.Length; i++) {
			Gizmos.color = new Color (1, 1, 1, 0.5f);
			Gizmos.DrawLine (waypoints [i - 1], waypoints [i]);
		} 

		Gizmos.DrawLine (waypoints [0], waypoints [waypoints.Length - 1]);

	}
}
