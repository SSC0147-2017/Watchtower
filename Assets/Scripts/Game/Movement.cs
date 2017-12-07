using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour {


	public int MaxHP;
	private bool canBeHurt;
	private int CurrentHP;
	private float defense;
	public bool isDead;
	public float invicibilityTime;
	public float RevivalTime;
	//HP

	private bool isMovable;
	public float turnSpeed;
	//Movement

	public Rigidbody Projectile;
	public Transform Pos;
	public float spread;
	public float ProjSpd;
	public int ColLayer;
	public float SpecialTime;
	private bool isSpe;
	//Special

	public float rollTime;
	public float rollDmgRed;
	private bool isDog;
	//Dodge

	public float AttackTime;
	private bool isAtk;
	//Attack

	public bool outsiderAtk=false;
	public bool outsiderSP=false;

	public string Controller;
	private Animator anim;

	void Start() {
		anim = GetComponent<Animator>();
		Physics.IgnoreLayerCollision(ColLayer,ColLayer,true);
		isMovable=true;
		canBeHurt=true;
		CurrentHP=MaxHP;
		isDead=false;
		isAtk=false;
		isDog=false;
		isSpe=false;
		defense=1.0f;
		anim.SetBool("CanAttack",true);
	}

	public void Initiate(string Con){
		Controller=Con;
	}


	void FixedUpdate () {
		if(!isDead){
			if(Input.GetButtonDown(Controller+"Fire1")) {
				if(!isAtk){
					StartCoroutine(waitAttackTime());
					anim.SetFloat("Speed",0.0f);
					anim.SetTrigger("Attack");
				}
			}
			if(Input.GetButtonDown(Controller+"Fire2")) {
				if(!isSpe){
					StartCoroutine(waitSpecialTime());
					anim.SetFloat("Speed",0.0f);
					anim.SetTrigger("Special");
					Special();
				}
			}
			if(Input.GetButtonDown(Controller+"Fire3")) {
				if(!isDog){
					anim.SetTrigger("Dodge");
					StartCoroutine(RollTime());
				}
			}
			ControlPlayer();
		}
	}

	private IEnumerator waitAttackTime() {
		isMovable=false;
		isAtk=true;
		isDog=true;
		isSpe=true;
		outsiderAtk=true;
		yield return new WaitForSeconds(AttackTime);
		isMovable=true;
		outsiderAtk=false;
		isAtk=false;
		isDog=false;
		isSpe=false;
	}

	private IEnumerator waitSpecialTime() {
		isMovable=false;
		isAtk=true;
		isDog=true;
		isSpe=true;
		outsiderSP=true;
		yield return new WaitForSeconds(SpecialTime);
		isMovable=true;
		outsiderSP=false;
		isAtk=false;
		isDog=false;
		isSpe=false;
	}

	public void takeDamage(float damage){
		if (canBeHurt) {
			float netDamage = damage * defense;
			if (netDamage > 0) {
				CurrentHP -= (int)netDamage;
				StartCoroutine(waitInvinciTime());
				anim.SetFloat("Speed",0.0f);
				anim.SetTrigger("Hit");
			}
			Debug.Log(ColLayer+" HP:"+CurrentHP);
		}
		else
			return;

		if (this.CurrentHP <= 0) {
			anim.SetTrigger("Dying");
			isDead = true;
			anim.SetBool("CanAttack",false);
			//this.die ();
		}

	}

	private IEnumerator waitInvinciTime() {
		canBeHurt = false;
		isMovable=false;
		isAtk=true;
		isDog=true;
		isSpe=true;
		anim.SetBool("CanAttack",false);
		yield return new WaitForSeconds(invicibilityTime);
		canBeHurt = true;
		isMovable=true;
		anim.SetBool("CanAttack",true);
		isAtk=false;
		isDog=false;
		isSpe=false;
	}

	private IEnumerator waitReviveTime() {
		canBeHurt = false;
		isAtk=true;
		isDog=true;
		isSpe=true;
		isMovable=false;
		anim.SetBool("CanAttack",false);
		yield return new WaitForSeconds(RevivalTime);
		canBeHurt = true;
		isAtk=false;
		isDog=false;
		isSpe=false;
		isMovable=true;
		anim.SetBool("CanAttack",true);
	}

	public void GainHP(int gain){
		CurrentHP+=gain;
		if (CurrentHP>MaxHP)
			CurrentHP=MaxHP;
	}

	public void Revive(){
		if(isDead){
			anim.SetTrigger("Revive");
			StartCoroutine(waitReviveTime());
			CurrentHP=MaxHP/2;
		}
	}

	private IEnumerator RollTime(){
		defense = rollDmgRed;
		//isMovable=false;
		isDog=true;
		anim.SetBool("CanAttack",false);
		yield return new WaitForSeconds (rollTime);
		defense = 1.0f;
		isDog=false;
		//isMovable=true;
		anim.SetBool("CanAttack",true);
	}

	void ControlPlayer() {
		float h = Input.GetAxisRaw (Controller+"Horizontal");
		float v = Input.GetAxisRaw (Controller+"Vertical");

		if(!isMovable){
			//anim.SetBool("IsMoving",false);
			anim.SetFloat("Speed",0.0f);
			return;
		}

		if(h*h+v*v<0.1){
			anim.SetFloat("Speed",0.0f);
			return;
		}
		//anim.SetBool("IsMoving",true);

		Vector3 movement = new Vector3(h, 0.0f, v);

		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), turnSpeed);
		//transform.Translate (movement * ActualSpeed * Time.deltaTime, Space.World);
		anim.SetFloat("Speed", AnimSpeed(h,v));

	}

	float AnimSpeed(float sideALength, float sideBLength) {
		if(Mathf.Sqrt(sideALength * sideALength + sideBLength * sideBLength)>1.0f)
			return 1.0f;
		return Mathf.Sqrt(sideALength * sideALength + sideBLength * sideBLength);
	}

	void Special() {
		if(Pos==null)
			return;
		Rigidbody Clone = (Rigidbody) Instantiate(Projectile, Pos.position, Pos.rotation);
		Clone.transform.Rotate(0.0f, Random.Range(-1.0f*spread,spread), 0.0f);
		Clone.velocity = Clone.transform.TransformDirection(Vector3.forward * ProjSpd);
	}
}