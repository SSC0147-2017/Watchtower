using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Precisa de um collider para detectar impactos
[RequireComponent (typeof(Collider))]

public class MeleeAttack : MonoBehaviour {

	public GameObject EnemyPrefab;
	EnemyBehaviour behav;
	Animator animator;

	Collider hitbox;
	public float damage;
	public float cooldownTime;

	// Use this for initialization
	void Start () {
		if (hitbox == null)
			hitbox = GetComponent<BoxCollider> ();
		if (animator == null)
			animator = EnemyPrefab.GetComponent<Animator> ();
		if (behav == null)
			behav = EnemyPrefab.GetComponent<EnemyBehaviour> ();		
	}

	/**
	 * Método principal do script. Inicia o ataque
	 */
	public void Attack(){
		if (!behav.getIsAttacking()) {
			print ("Attacking!");
			hitbox.enabled = true;
			StartCoroutine (attackCooldown ());
			//TODO - Animações e coisas chiques
			animator.SetTrigger("Attack");
		}
	}

	void OnTriggerStay (Collider col){

		if (behav.getIsAttacking() && col.gameObject.tag == "Player") {//Só da dano se ele estiver atacando.
			HealthController tgtHealth = col.gameObject.GetComponent<HealthController> ();
			if (tgtHealth != null) {
				tgtHealth.takeDamage (damage);
			}
		}
	}


	//FUNÇÃO DE TESTE DE ATTACK COOLDOWN
	private IEnumerator attackCooldown() {
		behav.setIsAttacking(true);
		yield return new WaitForSeconds(cooldownTime);
		hitbox.enabled = false;
		behav.setIsAttacking(false);

	}
}
