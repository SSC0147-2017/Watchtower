using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazLightningController : MonoBehaviour {

	public Transform path;//Transform do gameobject com path
	public float damage;

	private Vector3[] waypoints;

	void Start(){

		waypoints = new Vector3[path.childCount];

		for (int i = 0; i < path.childCount; i++) {
			waypoints [i] = path.GetChild (i).transform.position;
		}

	}

	void OnTriggerStay (Collider col){
		HealthController tgtHealth = col.gameObject.GetComponent<HealthController> ();
		if (tgtHealth != null) {
			tgtHealth.takeDamage (damage);
		}
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
