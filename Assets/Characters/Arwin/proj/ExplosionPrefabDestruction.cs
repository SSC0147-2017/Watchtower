using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPrefabDestruction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(Xablau());	
	}

	IEnumerator Xablau(){
		yield return new WaitForSeconds(5.0f);
		Destroy(this.gameObject);
	}
}
