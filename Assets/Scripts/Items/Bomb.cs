using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bomb : MonoBehaviour {

	public float timeToBlow = 3.0f;
	public float damage;
	private bool explode = false;
	public GameObject Explosion;


	void Start(){
		StartCoroutine(Explode());
	}
		
	void Update(){
		if(explode){
			Destroy(this.gameObject);
		}
	}

	void OnTriggerStay(Collider col){
		if(explode){
			if(col.CompareTag("Enemy") && col.GetType()!=typeof(SphereCollider)){
				Debug.Log("Morreu?");
				HealthController tgtHealth = col.gameObject.GetComponent<HealthController> ();
				if (tgtHealth != null) {
					tgtHealth.takeDamage (damage);
				}
			}
			else if(col.CompareTag("Player")){
				Debug.Log("Stay");
				Movement M = col.gameObject.GetComponent<GetParentCol>().Get();
				if(M!=null)
					M.takeDamage(damage);
			}
		}
	}



	private IEnumerator Explode(){
		yield return new WaitForSeconds(timeToBlow);
		Instantiate(Explosion,this.transform.position,this.transform.rotation);
		explode=true;
	}

}
