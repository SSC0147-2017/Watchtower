using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DepthsBehaviour : EnemyBehaviour {

	#region variables
	List<GameObject> Targets = new List<GameObject>();

	GameObject ClosestTarget;

	public float Multiplier;
	#endregion

	#region Monobehaviour Methods
	void Start()
	{
		base.Start ();
		for(int i = 0; i < GameManager.GM.PlayerRefs.Count; i++){
			Targets.Add(GameManager.GM.PlayerRefs[i]);
		}
	}
		
	void Update()
	{
		//print("tgt " + target);
		CurrTarget = CalculateTarget();

		//PERSEGUINDO/ATACANDO
		if (CurrTarget != null) {

			LookAtTarget ();

			if (!isAttacking){
				//Raio de ataque
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
		if (col.gameObject.tag == "Player"){
			if (ClosestTarget == null){
				ClosestTarget = col.gameObject;    //Novo alvo
			}
			else{
				//Caso haja mais de um, escolher o alvo mais próximo
				if (Vector3.Distance(col.gameObject.transform.position, transform.position) <
					Vector3.Distance(ClosestTarget.transform.position, transform.position)){
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

		while(i < Targets.Count)
		{
			dist = CalculateIsolation(i);
			if(dist >= highest)
			{
				highest = dist;
				isolated = Targets[i];
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

	float CalculateIsolation(int index)
	{
		int i = 0;
		float dist = 0;
		while(i < Targets.Count)
		{
			if(i != index)
			{
				dist += Vector3.Distance(Targets[index].transform.position, Targets[i].transform.position);
			}
			i++;
		}
		if(Targets.Count > 1) dist /= Targets.Count - 1;
		return dist;
	}
	#endregion
}
