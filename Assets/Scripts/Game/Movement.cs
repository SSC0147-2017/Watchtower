using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Movement : MonoBehaviour {


	public int MaxHP;
	private bool canBeHurt;
	[HideInInspector] public int CurrentHP;
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

	private int RevivePool=0;
	private int RevivePoolMax=2000;
	public int RevivePts;
	private bool isHelp;
	public Collider Range;
	public Canvas HealthCanvas;
	//Revive

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
		Range.enabled=false;
	}

	public void Initiate(string Con){
		Controller=Con;
	}


	void FixedUpdate () {
		if(!isDead){
			if(Input.GetButtonDown(Controller+"Fire0")) {
				if(!isHelp){
					anim.SetTrigger("Help");
					anim.SetFloat("Speed",0.0f);
					Range.enabled=true;
					StartCoroutine(waitHelpTime());
				}
			}
			if(Input.GetButtonDown(Controller+"Fire2")) {
				if(!isAtk){
                    //SoundManager.SM.PlayAttack();
					anim.SetTrigger("Attack");
					anim.SetFloat("Speed",0.0f);
					StartCoroutine(waitAttackTime());
				}
			}
			if(Input.GetButtonDown(Controller+"Fire3")) {
				if(!isSpe){
					anim.SetTrigger("Special");
					anim.SetFloat("Speed",0.0f);
					StartCoroutine(waitSpecialTime());
					Special();
				}
			}
			if(Input.GetButtonDown(Controller+"Fire1")) {
				if(!isDog){
					anim.SetTrigger("Dodge");
					StartCoroutine(RollTime());
				}
			}
			ControlPlayer();
		}
		else {
			if(!HealthCanvas.gameObject.activeSelf){
				HealthCanvas.transform.Find("FrontReviveBar").GetComponent<Image>().fillAmount = 0;
				HealthCanvas.gameObject.SetActive(true);
			}
			else{
				HealthCanvas.transform.Find("FrontReviveBar").GetComponent<Image>().fillAmount = (float)RevivePool/(float)RevivePoolMax;
			}
			
			if(RevivePool>=RevivePoolMax){
				HealthCanvas.transform.Find("FrontReviveBar").GetComponent<Image>().fillAmount = 0;
				HealthCanvas.gameObject.SetActive(false);
				Revive();
				RevivePool=0;
			}
			RevivePool-=1;
			if(RevivePool<0)
				RevivePool=0;
		}
	}

	public void Help(int Pts){
		RevivePool+=Pts;
	}

	void OnTriggerStay (Collider Col){
		if(isHelp){
			if(Input.GetButton(Controller+"Fire0")){
				if(Col.gameObject.CompareTag("Player")){
					Movement M = Col.gameObject.GetComponent<GetParentCol>().Get();
					if(M!=null)
						M.Help(RevivePts);
				}
			}
		}
	}

	private IEnumerator waitHelpTime(){
		isMovable=false;
		isAtk=true;
		isDog=true;
		isSpe=true;
		isHelp=true;
		yield return new WaitForSeconds(2.0f);
		isHelp=false;
		isMovable=true;
		isAtk=false;
		isDog=false;
		isSpe=false;
		Range.enabled=false;
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
            
            if(gameObject.name == "Arwin(Clone)")
            {
                //SoundManager.SM.PlayArwinGrunt();
            }
            else if(gameObject.name == "Hobbes(Clone)")
            {
                //SoundManager.SM.PlayHobbesGrunt();
            }
            else if(gameObject.name == "Corvo(Clone)" || gameObject.name == "Jackie(Clone)")
            {
                //SoundManager.SM.PlayCorJackGrunt();
            }
            //SoundManager.SM.PlayHit();

            float netDamage = damage * defense;
			if (netDamage > 0) {
				CurrentHP -= (int)netDamage;
				anim.SetTrigger("Hit");
				anim.SetFloat("Speed",0.0f);
				StartCoroutine(waitInvinciTime());
			}
			Debug.Log(ColLayer+" HP:"+CurrentHP);
		}
		else
			return;

		if (this.CurrentHP <= 0 && !isDead) {
			anim.SetTrigger("Dying");
			isDead = true;
			anim.SetBool("CanAttack",false);

			GameManager.GM.playerDown ();
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
		Debug.Log(ColLayer+": Danke Corvo! "+CurrentHP);
		if(!isDead)
			CurrentHP+=gain;
		if (CurrentHP>MaxHP)
			CurrentHP=MaxHP;
	}

	public void Revive(){
		if(isDead){
			anim.SetTrigger("Revive");
			StartCoroutine(waitReviveTime());
			CurrentHP=MaxHP/2;
			isDead=false;

			GameManager.GM.playerUp ();
		}
	}

	private IEnumerator RollTime(){
		defense = rollDmgRed;
		//isMovable=false;
		isDog=true;
		anim.SetBool("CanAttack",false);
		if(ColLayer!=9)
			Physics.IgnoreLayerCollision(ColLayer,13,true);
		yield return new WaitForSeconds (rollTime);
		defense = 1.0f;
		isDog=false;
		Physics.IgnoreLayerCollision(ColLayer,13,false);
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