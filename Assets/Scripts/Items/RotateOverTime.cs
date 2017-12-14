using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour {

	public float angle;
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(new Vector3(0,angle,0)*Time.deltaTime, Space.World);
	}
}
