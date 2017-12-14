using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Virote : MonoBehaviour {

	public float Dano;

	void Start(){
		if(this.gameObject.name == "Bullet(Clone)"){
			SoundManager.SM.PlayPistol();
		}
		else if(this.gameObject.name == "Virote(Clone)"){
			SoundManager.SM.PlayCrossbow();
		}
	}

	void OnTriggerEnter(Collider Col){
		if(Col.gameObject.CompareTag("Enemy") && Col.GetType()!=typeof(SphereCollider)){
			HealthController H = Col.gameObject.GetComponent<HealthController>();
			if(H!=null)
				H.takeDamage(Dano);
		}
		else
			if(Col.gameObject.CompareTag("Player") && Col.GetType()!=typeof(SphereCollider)){
				Debug.Log("Melee");
				Movement M = Col.gameObject.GetComponent<GetParentCol>().Get();
				if(M!=null)
					M.takeDamage(Dano);
			}

        if(Col.tag != "Spawner" && !(Col.gameObject.CompareTag("Enemy") && Col.GetType() == typeof(SphereCollider)) && !(Col.gameObject.CompareTag("Player") && Col.GetType() == typeof(SphereCollider)))
		    Destroy(this.gameObject);
	}
}
