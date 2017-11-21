using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Virote : MonoBehaviour {

	public float Dano;
	public float Veneno;
	public int tempo;
	
	void onCollisionEnter(Collision Col){
		Movement M = Col.gameObject.GetComponent<Movement>();
		if(M!=null){
			M.takeDamage(Dano);
			for(int i=0;i<tempo;i++){
				StartCoroutine(VenenoP(M));
			}
		}
		HealthController H = Col.gameObject.GetComponent<HealthController>();
		if(H!=null){
			H.takeDamage(Dano);
			for(int i=0;i<tempo;i++){
				StartCoroutine(VenenoE(H));
			}
		}
		Destroy(this.gameObject);
	}

	private IEnumerator VenenoP (Movement M){
		yield return new WaitForSeconds (1.0f);
		M.takeDamage(Veneno);
	}

	private IEnumerator VenenoE (HealthController H){
		yield return new WaitForSeconds (1.0f);
		H.takeDamage(Veneno);
	}
}
