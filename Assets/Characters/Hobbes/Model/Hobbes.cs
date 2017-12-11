using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hobbes : MonoBehaviour {

	private Melee CorvoM;
	public Movement HobbesM;
	private Melee ArwinMD;
	private Melee ArwinME;
	private Melee JackieM;

	public Transform CorvoT;
	public Transform ArwinT;
	public Transform JackieT;

	private float DA;
	private float DC;
	private float DJ;

	void Start() {
		CorvoM = CorvoT.gameObject.GetComponent<GetChildMelee>().GetD();
		ArwinMD = ArwinT.gameObject.GetComponent<GetChildMelee>().GetD();
		ArwinME = ArwinT.gameObject.GetComponent<GetChildMelee>().GetE();
		JackieM = JackieT.gameObject.GetComponent<GetChildMelee>().GetD();

		DA=ArwinMD.Dano;
		DC=CorvoM.Dano;
		DJ=JackieM.Dano;
	}

	// Update is called once per frame
	void Update () {
		if(!HobbesM.isDead){
			if(CorvoM!=null)
			if(Vector3.Distance(CorvoT.position,transform.position)<=7.0f){
				CorvoM.Dano=DC+10f;
			}
			else
				CorvoM.Dano=DC;
			if(ArwinMD!=null)
			if(Vector3.Distance(ArwinT.position,transform.position)<=7.0f){
				ArwinMD.Dano=DA+10f;
			}
			else
				ArwinMD.Dano=DA;
			if(ArwinME!=null)
			if(Vector3.Distance(ArwinT.position,transform.position)<=7.0f){
				ArwinME.Dano=DA+10f;
			}
			else
				ArwinME.Dano=DA;
			if(JackieM!=null)
			if(Vector3.Distance(JackieT.position,transform.position)<=7.0f){
				JackieM.Dano=DJ+10f;
			}
			else
				JackieM.Dano=DJ;
		}
		Debug.Log("Corvo: "+CorvoM.Dano);
		Debug.Log("ArwinD: "+ArwinMD.Dano);
		Debug.Log("ArwinE: "+ArwinME.Dano);
		Debug.Log("Jackie: "+JackieM.Dano);
	}
}