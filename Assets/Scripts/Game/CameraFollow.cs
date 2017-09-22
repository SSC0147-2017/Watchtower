using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform Player1;
	public Transform Player2;
	//public Transform Player3;
	//public Transform Player4;

	public Transform MidP;

	void Start(){
		Debug.Log(Input.GetJoystickNames());
	}
	
	// Update is called once per frame
	void Update () {
		MidP.position = new Vector3 ((Player1.position.x + Player2.position.x)/2, (Player1.position.y + Player2.position.y)/2, (Player1.position.z + Player2.position.z)/2);
		transform.position = Vector3.Lerp (transform.position, MidP.transform.position, Time.deltaTime * 0.7f);
	}
}
