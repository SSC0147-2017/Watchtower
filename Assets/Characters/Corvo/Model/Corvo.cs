using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corvo : MonoBehaviour {

	public Movement CorvoM;
	public Collider C;
	public float T;
	public int cura;

	private Movement HobbesM=null;
	private Movement ArwinM=null;
	private Movement JackieM=null;

	private Transform HobbesT=null;
	private Transform ArwinT=null;
	private Transform JackieT=null;

	void Start() {
		StartCoroutine(ColOff());
		StartCoroutine(Cura());
	}

	IEnumerator ColOff() {
		yield return new WaitForSeconds(1.0f);
		C.enabled=false;
	}

	// Update is called once per frame
	IEnumerator Cura () {
		if(!CorvoM.isDead){
			if(HobbesM!=null){
				if(Vector3.Distance(HobbesT.position,transform.position)<=7.0f){
					HobbesM.GainHP(cura);
				}
			}
			if(ArwinM!=null) {
				if(Vector3.Distance(ArwinT.position,transform.position)<=7.0f){
					ArwinM.GainHP(cura);
				}
			}
			if(JackieM!=null){
				if(Vector3.Distance(JackieT.position,transform.position)<=7.0f){
					JackieM.GainHP(cura);
				}
			}
		}
		yield return new WaitForSeconds(T);
		StartCoroutine(Cura());
	}

	void OnTriggerEnter(Collider Col){
		Movement G;
		G=Col.gameObject.GetComponent<GetParentCol>().Get();
		if(Col.gameObject.layer==9){
			if(HobbesM==null){
				HobbesM=G;
				HobbesT=G.transform;
			}
		}
		if(Col.gameObject.layer==10){
			if(ArwinM==null){
				ArwinM=G;
				ArwinT=G.transform;
			}
		}
		if(Col.gameObject.layer==11){
			if(JackieM==null){
				JackieM=G;
				JackieT=G.transform;
			}
		}
	}
}
