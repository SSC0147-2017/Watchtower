using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hobbes : MonoBehaviour {

	private Melee CorvoM;
	public Movement HobbesM;
	private Melee ArwinMD;
	private Melee ArwinME;
	private Melee JackieM;

	private Transform CorvoT;
	private Transform ArwinT;
	private Transform JackieT;

	private float DA;
	private float DC;
	private float DJ;

	public Collider C;
	public float B;

	void Start() {
		StartCoroutine(ColOff());
	}

	IEnumerator ColOff() {
		yield return new WaitForSeconds(1.0f);
		C.enabled=false;
	}

	// Update is called once per frame
	void Update () {
		if(!HobbesM.isDead){
			if(CorvoM!=null)
			if(Vector3.Distance(CorvoT.position,transform.position)<=7.0f){
				CorvoM.Dano=DC+B;
			}
			else
				CorvoM.Dano=DC;
			if(ArwinMD!=null)
			if(Vector3.Distance(ArwinT.position,transform.position)<=7.0f){
				ArwinMD.Dano=DA+B;
			}
			else
				ArwinMD.Dano=DA;
			if(ArwinME!=null)
			if(Vector3.Distance(ArwinT.position,transform.position)<=7.0f){
				ArwinME.Dano=DA+B;
			}
			else
				ArwinME.Dano=DA;
			if(JackieM!=null)
			if(Vector3.Distance(JackieT.position,transform.position)<=7.0f){
				JackieM.Dano=DJ+B;
			}
			else
				JackieM.Dano=DJ;
		}
	}

	void OnTriggerEnter(Collider Col){
		Movement M;
		
		if(Col.gameObject.layer==9){
            M = Col.GetComponent<GetParentCol>().Get();
            if (CorvoM==null){
				CorvoM=M.gameObject.GetComponent<GetChildMelee>().GetD();
				CorvoT=M.transform;
				DC=CorvoM.Dano;
			}
		}
		if(Col.gameObject.layer==10){
            M = Col.GetComponent<GetParentCol>().Get();
            if (ArwinMD==null){
				ArwinMD=M.gameObject.GetComponent<GetChildMelee>().GetD();
				ArwinT=M.transform;
			}
			if(ArwinME==null){
				ArwinME=M.gameObject.GetComponent<GetChildMelee>().GetE();
				DA=ArwinMD.Dano;
			}
		}
		if(Col.gameObject.layer==11){
            M = Col.GetComponent<GetParentCol>().Get();
            if (JackieM==null){
				JackieM=M.gameObject.GetComponent<GetChildMelee>().GetD();
				JackieT=M.transform;
				DJ=JackieM.Dano;
			}
		}
	}
}