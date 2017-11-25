using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazDamageOnTrigger : MonoBehaviour {

	public float damage;

	void OnTriggerStay (Collider col){
		HealthController tgtHealth = col.gameObject.GetComponent<HealthController> ();
		if (tgtHealth != null) {
			tgtHealth.takeDamage (damage);
		}
	}

}
