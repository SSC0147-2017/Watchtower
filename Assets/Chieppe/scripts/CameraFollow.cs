using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform Player1;
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp (transform.position, Player1.transform.position, Time.deltaTime * 0.7f);
	}
}
