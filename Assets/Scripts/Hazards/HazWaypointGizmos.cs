using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazWaypointGizmos : MonoBehaviour {

	public Vector3 size;
	public Color color;

	void OnDrawGizmos() {
		Gizmos.color = color;
		Gizmos.DrawCube (transform.position, size);
	}
}
