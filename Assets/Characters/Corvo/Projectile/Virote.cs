using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Virote : MonoBehaviour {

	public float Dano;
	private GameObject Coll;

	void onCollisionEnter(Collision Col){
		Coll=Col.gameObject;
		while (Coll.transform.parent.gameObject!=null){
			Coll=Coll.transform.parent.gameObject;
		}
		Movement M = Coll.gameObject.GetComponent<Movement>();
		if(M!=null)
			M.takeDamage(Dano);
		HealthController H = Coll.gameObject.GetComponent<HealthController>();
		if(H!=null)
			H.takeDamage(Dano);
		Destroy(this.gameObject);
	}
}
