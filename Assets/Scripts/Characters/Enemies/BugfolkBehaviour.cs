using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BugfolkBehaviour : EnemyBehaviour{

	private HealthController HP;

	#region Monobehaviour methods
	void Start () {
		base.Start ();
		HP=gameObject.GetComponent<HealthController>();
	}

	void Update () {
		if(HP.isDead){
			Destroy(this);
		}
		//PERSEGUINDO/ATACANDO
		if (CurrTarget != null) {

			LookAtTarget ();

			if (!isAttacking){
				//Raio de ataque
				//print("dist " + (CurrTarget.transform.position - claws.transform.position).magnitude);
				if((CurrTarget.transform.position - transform.position).magnitude <= navAgent.stoppingDistance) {
					Stop ();
					claws.Attack ();
				}
				else
					Chase ();
			}
		}
		//PATRULHANDO/PARADO
		else
			Stop ();
	}


	void OnTriggerStay(Collider col){

		if (col.gameObject.tag == "Player") {
			if (CurrTarget == null) {
				CurrTarget = col.gameObject;	//Novo alvo
			} else {
				//Caso haja mais de um, escolher o alvo mais próximo
				if (Vector3.Distance (col.gameObject.transform.position, transform.position) <
					Vector3.Distance (CurrTarget.transform.position, transform.position)) {
					CurrTarget = col.gameObject;//Novo alvo
				}
			}
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject == CurrTarget) {
			CurrTarget = null;
		}
	}

	#endregion
}
