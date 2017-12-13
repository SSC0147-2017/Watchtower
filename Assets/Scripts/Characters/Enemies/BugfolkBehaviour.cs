using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BugfolkBehaviour : EnemyBehaviour{

	private HealthController HP;

	#region Monobehaviour methods
	void Start () {
		base.Start ();
        GetComponent<AudioSource>().PlayOneShot(SoundManager.SM.GetBugfolk());
		HP=gameObject.GetComponent<HealthController>();
	}

	void Update () {

		base.Update ();

		if(HP.isDead){
			Destroy(this);
		}
		//PERSEGUINDO/ATACANDO
		if (CurrTarget != null) {
			//If the current target is dead, dismiss the target
			if (TgtScript.isDead)
				CurrTarget = null;
		
			else {
				LookAtTarget ();

				if (!isAttacking) {
					//Raio de ataque
					//print("dist " + (CurrTarget.transform.position - claws.transform.position).magnitude);
					if ((CurrTarget.transform.position - transform.position).magnitude <= navAgent.stoppingDistance) {
						Stop ();
						claws.Attack ();
					} else
						Chase ();
				}
			}
		}
		//PATRULHANDO/PARADO
		else
			Stop ();
	}
#endregion


    #region Class methods
    void OnTriggerStay(Collider col){

		if (col.gameObject.tag == "Player") {

			//Get possible target script
			Movement possTgtScript = col.GetComponent<GetParentCol>().Get();

			//No current target and possible target is alive
			if (CurrTarget == null && possTgtScript != null && !possTgtScript.isDead) {
				CurrTarget = col.gameObject;	//Novo alvo
				TgtScript = possTgtScript;
			} 

			else if (TgtScript != null && possTgtScript != null && CurrTarget != null) {
				//Choose the nearest live target
				if ( 
					(! TgtScript.isDead && !possTgtScript.isDead) 
					&&
					(Vector3.Distance (col.gameObject.transform.position, transform.position) <
						Vector3.Distance (CurrTarget.transform.position, transform.position)) 
				) {
					CurrTarget = col.gameObject;//Novo alvo
				}
			}
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject == CurrTarget) {
			CurrTarget = null;
			TgtScript = null;
		}
	}

	#endregion
}
