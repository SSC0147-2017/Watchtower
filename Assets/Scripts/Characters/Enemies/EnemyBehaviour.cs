using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour{

	#region variables
	protected NavMeshAgent navAgent;
	protected GameObject CurrTarget;
	Animator animator;

	//Game Object com os colisores para ataque
	[Header ("Left Arm")]
	public MeleeAttack claws;

	protected bool isAttacking;
	#endregion

	#region Monobehaviour methods
	protected void Start () {
		if (navAgent == null)
			navAgent = GetComponent<NavMeshAgent>();
		if (animator == null)
			animator = GetComponent<Animator> ();
		
		CurrTarget = null;
		isAttacking = false;

	}
	#endregion 


	#region EnemyBehaviour methods

	public bool getIsAttacking(){
		return isAttacking;
	}

	public void setIsAttacking(bool newAttacking){
		isAttacking = newAttacking;
	}

	public void Stop(){
		navAgent.isStopped = true;
		animator.SetFloat ("Speed", 0);
	}


	/*
	 * Função usada para a perseguição
	 */
	public void Chase(){
		animator.SetFloat ("Speed", 1);
		navAgent.isStopped = false;
		navAgent.SetDestination(CurrTarget.transform.position);
	}

	/**
	 * Função usada para sempre encarar o alvo.
	 */
	public void LookAtTarget(){
		Vector3 lookVector = CurrTarget.transform.position - transform.position;
		lookVector.y = 0;

		Quaternion lookRotation = Quaternion.LookRotation (lookVector);//Calcula a rotação para encarar o Alvo

		//Lerp faz a transição da original para a final.
		transform.rotation = Quaternion.Lerp (transform.rotation, lookRotation, Time.deltaTime * 3 );
	}

	#endregion
}
