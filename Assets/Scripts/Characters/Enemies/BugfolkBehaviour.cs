using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BugfolkBehaviour : MonoBehaviour {

	NavMeshAgent navAgent;
	public GameObject target;
	public float damage;

	private HealthController tgtHealth;
	private bool isAttacking;

	void Start () {
        navAgent = GetComponent<NavMeshAgent>();
		target = null;
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
        //print("tgt " + target);

		//PERSEGUINDO/ATACANDO
        if (target != null)
        {
			Chase ();
			//Raio de ataque
			if ( (target.transform.position - transform.position).magnitude <= navAgent.stoppingDistance) {
				Attack ();
			}
        }
        //PATRULHANDO/PARADO
		else
            navAgent.isStopped = true;
		
	}
		
	/*
	 * Função usada para a perseguição
	 */
	void Chase(){
		navAgent.isStopped = false;
		navAgent.SetDestination(target.transform.position);
	}

	/*
	 * Função usada para atacar um inimigo
	 */
	void Attack(){
		//Para antes de atacar
		if (!isAttacking) {
			print ("Attacking!");
			StartCoroutine (attackCooldown ());
			//TODO - Animações e coisas chiques
		}
	}

	//FUNÇÃO DE TESTE DE ATTACK COOLDOWN
	private IEnumerator attackCooldown() {
		navAgent.isStopped = true;
		isAttacking = true;
		tgtHealth.takeDamage (damage);
		yield return new WaitForSeconds(2);
		isAttacking = false;
		navAgent.isStopped = false;

	}
}
