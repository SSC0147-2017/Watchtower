using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corvo : MonoBehaviour {

	public Movement CorvoM;
	public Movement HobbesM;
	public Movement ArwinM;
	public Movement JackieM;

	public Transform HobbesT;
	public Transform ArwinT;
	public Transform JackieT;
	
	// Update is called once per frame
	void Update () {
		if(!CorvoM.isDead){
			if(HobbesM!=null)
				if(Vector3.Distance(HobbesT.position,transform.position)<=7.0f){
					HobbesM.GainHP(1);
				}
			if(ArwinM!=null)
				if(Vector3.Distance(ArwinT.position,transform.position)<=7.0f){
					ArwinM.GainHP(1);
				}
			if(JackieM!=null)
				if(Vector3.Distance(JackieT.position,transform.position)<=7.0f){
					JackieM.GainHP(1);
				}
		}
	}
}
