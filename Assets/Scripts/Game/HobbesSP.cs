using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HobbesSP : MonoBehaviour {

	public float Dano;
	public Movement P;


	void OnTriggerEnter(Collider Col){
		if(P.outsiderSP){
			if(Col.gameObject.CompareTag("Enemy") && Col.GetType()!=typeof(SphereCollider)){
				HealthController H = Col.gameObject.GetComponent<HealthController>();
				if(H!=null)
					H.takeDamage(Dano);
			}
			else
				if(Col.gameObject.CompareTag("Player") && Col.GetType()!=typeof(SphereCollider)){
					Debug.Log("SP");
					Movement M = Col.gameObject.GetComponent<GetParentCol>().Get();
					if(M!=null)
						M.takeDamage(Dano);
				}
		}
	}
}
