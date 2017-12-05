using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bomb : MonoBehaviour {

	private float timeLeft;
	public float timeToBlow;
	public float damage;


	void Start(){
		timeLeft = timeToBlow;
	}

	void Update(){
		if (timeLeft > 0) {
			timeLeft -= Time.deltaTime;
		} else {
			Explode ();
		}
	}


	void Explode(){
	/**
	 * Da play em partículas, aciona a killzone,
	 */

	}
}
