using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Precisa de um collider para detectar impactos
[RequireComponent (typeof(Collider))]

public class MeleeAttack : MonoBehaviour {

	public Collider hitbox;
	public float damage;
	public float cooldownTime;
	private bool isAttacking;

	// Use this for initialization
	void Start () {
		if (hitbox == null)
			hitbox = GetComponent<Collider> ();
		
	}

	/**
	 * Método principal do script. Inicia o ataque
	 */
	public void Attack(){
		if (!isAttacking) {
			print ("Attacking!");
			StartCoroutine (attackCooldown ());
			//TODO - Animações e coisas chiques
		}
	}

	void OnTriggerStay (Collider col){
		if (isAttacking) {//Só da dano se ele estiver atacando.
			HealthController tgtHealth = col.gameObject.GetComponent<HealthController> ();
			if (tgtHealth != null) {
				tgtHealth.takeDamage (damage);
			}
		}
	}


	//FUNÇÃO DE TESTE DE ATTACK COOLDOWN
	private IEnumerator attackCooldown() {
		isAttacking = true;
		yield return new WaitForSeconds(cooldownTime);
		isAttacking = false;

	}
}
