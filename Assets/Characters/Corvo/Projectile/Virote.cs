using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Virote : MonoBehaviour {

	public float Dano;


	void OnTriggerEnter(Collider Col){
		if(Col.gameObject.CompareTag("Enemy")){
			HealthController H = Col.gameObject.GetComponent<HealthController>();
			if(H!=null)
				H.takeDamage(Dano);
		}
		else
			if(Col.gameObject.CompareTag("Player")){
				Debug.Log("Melee");
				Movement M = Col.gameObject.GetComponent<GetParentCol>().Get();
				if(M!=null)
					M.takeDamage(Dano);
			}
		Destroy(this.gameObject);
	}
}
