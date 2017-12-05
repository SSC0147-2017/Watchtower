using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floofy : MonoBehaviour {


	void OnTriggerEnter (Collider col){
		if (col.CompareTag ("Player")) {
			GameManager.GM.Victory ();
		}
	}
}
