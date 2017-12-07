using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneArwin : MonoBehaviour {

	public float damage;
	public CloneController C;
	public GameObject Explosion;
	public GameObject Smoke;
	private bool explode=false;

	// Use this for initialization
	void Start () {
		if(!C.addClone()){
			Destroy(this.gameObject);
		}
		Instantiate(Smoke,this.transform.position,this.transform.rotation);
		StartCoroutine(Explode());
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
		yield return new WaitForSeconds(5.0f);
		Instantiate(Explosion,this.transform.position,this.transform.rotation);
		explode=true;
	}

	// Update is called once per frame
	void Update () {
		if(explode){
			C.killClone();
			Destroy(this.gameObject);
		}
	}
}
