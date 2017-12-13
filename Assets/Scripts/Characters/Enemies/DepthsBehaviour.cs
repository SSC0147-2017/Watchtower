using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DepthsBehaviour : EnemyBehaviour {

	#region variables
	List<GameObject> Targets = new List<GameObject>();

	GameObject ClosestTarget;

	public float Multiplier;

	private HealthController HP;

	#endregion

	#region Monobehaviour Methods
	void Start()
	{
		base.Start ();
        GetComponent<AudioSource>().PlayOneShot(SoundManager.SM.GetDepths());
        HP =gameObject.GetComponent<HealthController>();
		for(int i = 0; i < GameManager.GM.PlayerRefs.Count; i++){
			Targets.Add(GameManager.GM.PlayerRefs[i]);
		}
	}
		
	void Update()
	{

		base.Update ();

		if(HP.isDead){
			Destroy(this);
		}
		//print("tgt " + target);
		CurrTarget = CalculateTarget();

		if(CurrTarget != null && TgtScript == null)
			TgtScript = CurrTarget.GetComponent<Movement>();

		//PERSEGUINDO/ATACANDO
		if (CurrTarget != null && TgtScript != null) {

			if (TgtScript.isDead) {
				CurrTarget = null;
				TgtScript = null;
			}
			else {
				LookAtTarget ();

				if (!isAttacking) {
					//Raio de ataque
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

	void OnTriggerStay(Collider col){

		if (col.gameObject.tag == "Player") {

			//Get possible target script
			Movement possTgtScript = col.GetComponent<GetParentCol>().Get();


			//No current target and possible target is alive
			if (ClosestTarget == null && !possTgtScript.isDead) {
				ClosestTarget = col.gameObject;	//Novo alvo
				TgtScript = possTgtScript;
			} 

			else if (TgtScript != null && CurrTarget != null) {
				//Choose the nearest live target
				if ( 
					(! TgtScript.isDead && !possTgtScript.isDead) 
					&&
					(Vector3.Distance (col.gameObject.transform.position, transform.position) <
						Vector3.Distance (ClosestTarget.transform.position, transform.position)) 
				) {
					ClosestTarget = col.gameObject;//Novo alvo
				}
			}
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject == ClosestTarget){
			ClosestTarget = null;
		}
	}

	#endregion

	#region EnemyBehaviour Methods
	GameObject CalculateTarget()
	{
		int i = 0;
		GameObject isolated = null;
		float dist, highest = 0;

		//List of targets who are not dead
		List <GameObject> validTargets = new List<GameObject> ();

		for (i = 0; i < Targets.Count; i++) {
			if (!Targets [i].GetComponent<Movement> ().isDead) {
				validTargets.Add (Targets [i]);
			}
		}

		i = 0;
		while(i < validTargets.Count)
		{
			dist = CalculateIsolation (i, validTargets);
				if (dist >= highest) {
					highest = dist;
					isolated = validTargets [i];
				}
			
			i++;
		}

		float closest;
		if (ClosestTarget != null) closest = Vector3.Distance(ClosestTarget.transform.position, transform.position);
		else closest = 1000;

		//print("m " + Multiplier + " c " + closest + " h " + highest);
		//if ((Multiplier / closest) > highest) return ClosestTarget;
		if (closest < Multiplier) return ClosestTarget;

		return isolated;
	}


	/**
	 * Calculates the degree of isolation of a given target 
	 * @param index 	the index of the target to check
	 * @param validTargets	the list with live targets
	 * @return degree of isolation
	 */
	float CalculateIsolation(int index, List<GameObject> validTargets)
	{

		int i = 0;

		float dist = 0;
		while(i < validTargets.Count)
		{
			if(i != index)
			{
				dist += Vector3.Distance(validTargets[index].transform.position, validTargets[i].transform.position);
			}
			i++;
		}
		if(validTargets.Count > 1) dist /= validTargets.Count - 1;
		return dist;
	}
	#endregion
}
