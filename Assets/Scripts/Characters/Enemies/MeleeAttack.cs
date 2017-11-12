using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Precisa de um collider para detectar impactos
[RequireComponent (typeof(Collider))]

public class MeleeAttack : MonoBehaviour {

	public BugfolkBehaviour behav;
	public Collider hitbox;
	public Animator animator;
	public float damage;
	public float cooldownTime;

	// Use this for initialization
	void Start () {
		if (hitbox == null)
			hitbox = GetComponent<Collider> ();
		if (animator == null)
			animator = GetComponentInParent<Animator> ();
		if (behav == null)
			behav = GetComponentInParent<BugfolkBehaviour> ();		
	}

	/**
	 * Método principal do script. Inicia o ataque
	 */
	public void Attack(){
		if (!behav.isAttacking) {
			print ("Attacking!");
			StartCoroutine (attackCooldown ());
			//TODO - Animações e coisas chiques
			animator.SetTrigger("Attack");
		}
	}

	void OnTriggerStay (Collider col){

		if (behav.isAttacking) {//Só da dano se ele estiver atacando.
			HealthController tgtHealth = col.gameObject.GetComponent<HealthController> ();
			if (tgtHealth != null) {
				tgtHealth.takeDamage (damage);
			}
		}
	}


	//FUNÇÃO DE TESTE DE ATTACK COOLDOWN
	private IEnumerator attackCooldown() {
		behav.isAttacking = true;
		yield return new WaitForSeconds(cooldownTime);
		behav.isAttacking = false;

	}
}
