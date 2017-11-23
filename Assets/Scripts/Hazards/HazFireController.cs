using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazFireController : MonoBehaviour {

	public Collider damageZone;
	public ParticlesOnOff fireObject; //Objeto com efeitos de particulas/iluminação, etc.
	public float period; //Tempo em segundos entre ativações
	public float activeTime;
	public float damage; //Dano dado enquanto a entidade ficar no fogo

	private bool isHazardActive;	//Estado booleano de fogo ativo ou não
	private float currTimer;

	// Use this for initialization
	void Start () {
		currTimer = period;
		isHazardActive = false;
	}
	
	// Update is called once per frame
	void Update () {
		currTimer -= Time.deltaTime;

		if (currTimer <= 0) {
		
			if (!isHazardActive)
				ActivateHazard ();
			else
				DeactivateHazard ();
		}
	}

	void ActivateHazard(){
		currTimer = activeTime;
		isHazardActive = true;
		damageZone.enabled = true;
		fireObject.ParticleSystemsPlay();
	}

	void DeactivateHazard(){
		currTimer = period;
		isHazardActive = false;
		damageZone.enabled = false;
		fireObject.ParticleSystemsStop();
	}

	void OnTriggerStay (Collider col){
		HealthController tgtHealth = col.gameObject.GetComponent<HealthController> ();
		if (tgtHealth != null) {
			print ("dando dano");
			tgtHealth.takeDamage (damage);
		}
	}
}
