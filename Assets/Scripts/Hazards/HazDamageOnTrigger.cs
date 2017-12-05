using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazDamageOnTrigger : MonoBehaviour {

	public float damage;

	void OnTriggerStay (Collider col){
<<<<<<< HEAD
<<<<<<< HEAD
		if(col.tag=="Player"){
			
=======
		if(col.tag=="Player"){
			
		}
		else{
			HealthController tgtHealth = col.gameObject.GetComponent<HealthController> ();
			if (tgtHealth != null) {
				tgtHealth.takeDamage (damage);
			}			
>>>>>>> master
		}
		else{
			HealthController tgtHealth = col.gameObject.GetComponent<HealthController> ();
			if (tgtHealth != null) {
				tgtHealth.takeDamage (damage);
			}			
=======
		if(col.CompareTag("Enemy")){
			HealthController tgtHealth = col.gameObject.GetComponent<HealthController> ();
			if (tgtHealth != null) {
				tgtHealth.takeDamage (damage);
			}
>>>>>>> Chieppe
		}
		else
			if(col.CompareTag("Player")){
				Movement M = col.gameObject.GetComponent<GetParentCol>().Get();
				if(M!=null)
					M.takeDamage(damage);
			}
	}

}
