using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BugfolkBehaviour : MonoBehaviour {

	NavMeshAgent navAgent;
	public GameObject target;
	public float damage;

	//Game Object com os colisores para ataque
	public MeleeAttack claws;
	public Animator animator;

	private HealthController tgtHealth;
	public bool isAttacking;

	void Start () {
        navAgent = GetComponent<NavMeshAgent>();
		target = null;
		isAttacking = false;
		if (claws == null)
			claws = gameObject.transform.GetChild (0).gameObject.GetComponent<MeleeAttack> ();
		if (animator == null)
			animator = GetComponent<Animator> ();
	}



	void OnTriggerStay(Collider col){

		if (col.gameObject.tag == "Player") {
			if (target == null) {
				target = col.gameObject;	//Novo alvo
			} else {
				//Caso haja mais de um, escolher o alvo mais próximo
				if (Vector3.Distance (col.gameObject.transform.position, transform.position) <
				   Vector3.Distance (target.transform.position, transform.position)) {
					target = col.gameObject;//Novo alvo
				}
			}

			tgtHealth = target.GetComponent<HealthController> ();
		}
	}


	void OnTriggerExit(Collider col){
		if (col.gameObject == target) {
			target = null;
		}
	}

	void Update () {
		//PERSEGUINDO/ATACANDO
		if (target != null) {
			
			LookAtTarget ();

			if (!isAttacking){
				//Raio de ataque
				if((target.transform.position - claws.transform.position).magnitude <= navAgent.stoppingDistance) {
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

	void Stop(){
		navAgent.isStopped = true;
		animator.SetFloat ("Speed", 0);
	}


	/*
	 * Função usada para a perseguição
	 */
	void Chase(){
		animator.SetFloat ("Speed", 1);
		navAgent.isStopped = false;
		navAgent.SetDestination(target.transform.position);
	}

	/**
	 * Função usada para sempre encarar o alvo.
	 */
	void LookAtTarget(){
		Vector3 lookVector = target.transform.position - transform.position;
		lookVector.y = 0;

		Quaternion lookRotation = Quaternion.LookRotation (lookVector);//Calcula a rotação para encarar o Alvo

		//Lerp faz a transição da original para a final.
		transform.rotation = Quaternion.Lerp (transform.rotation, lookRotation, Time.deltaTime * 3 );
	}
}
