using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazDamageOnTrigger : MonoBehaviour {

	public float damage;

	void OnTriggerStay (Collider col){
		if(col.CompareTag("Enemy") && col.GetType()!=typeof(SphereCollider)){
			HealthController tgtHealth = col.gameObject.GetComponent<HealthController> ();
			if (tgtHealth != null) {
				tgtHealth.takeDamage (damage);
			}
		}
		else if(col.CompareTag("Player")){
				Movement M = col.gameObject.GetComponent<GetParentCol>().Get();
				if(M!=null)
					M.takeDamage(damage);
			}
	}

}
