using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floofy : MonoBehaviour {

    bool hasEntered = false;

    void OnTriggerEnter (Collider col){
		if (col.CompareTag ("Player") && !hasEntered) {
            hasEntered = true;
			GameManager.GM.Victory ();
		}
	}
}
