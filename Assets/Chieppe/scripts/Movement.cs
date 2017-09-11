using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour {

	public float movementSpeed;
	public float turnSpeed;
	private Animator anim;
	public Rigidbody FireBall;
	public Transform Pos;

	void Start() {
		anim = GetComponent<Animator>();
	}

	void FixedUpdate () {
		
		if(Input.GetButtonDown("Fire1")) {
			anim.SetTrigger("Atk");
		}
		if(Input.GetButtonDown("Fire2")) {
			anim.SetTrigger("Spell");
			SpellCast();
		}
		ControllPlayer();
	}


	void ControllPlayer() {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		if(h*h+v*v<0.01){
			anim.SetBool("IsMoving",false);
			return;
		}
		anim.SetBool("IsMoving",true);

		Vector3 movement = new Vector3(h, 0.0f, v);

		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), turnSpeed);


		transform.Translate (movement * movementSpeed * Time.deltaTime, Space.World);


	}

	void SpellCast() {
		Rigidbody Clone = (Rigidbody) Instantiate(FireBall, Pos.position, Pos.rotation);
		Physics.IgnoreLayerCollision(8,8,true); 
		Clone.velocity = Pos.TransformDirection(Vector3.forward * 10.0f);
	}
}
