using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BugfolkBehaviour : MonoBehaviour {

	public NavMeshAgent navAgent;

	public GameObject target;

	// Use this for initialization
	void Start () {
		target = null;
	}

	void OnTriggerStay(Collider col){
		print ("c - " + col.gameObject);

		if (col.gameObject.tag == "Player") {
			
			if (target == null) {
				target = col.gameObject;
			} else {

				if (Vector3.Distance (col.gameObject.transform.position, transform.position) <
				   Vector3.Distance (target.transform.position, transform.position)) {
					target = col.gameObject;//Novo alvo
				}
			}
		}
	}


	void OnTriggerExit(Collider col){
		if (col.gameObject == target) {
			print ("Exit");
			target = null;
		}
	}

	// Update is called once per frame
	void Update () {
		print ("tgt " + target);

		if (target != null)
			navAgent.SetDestination (target.transform.position);
		else
			navAgent.isStopped = true;
		
	}
}
