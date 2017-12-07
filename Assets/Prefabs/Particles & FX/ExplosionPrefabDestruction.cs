using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPrefabDestruction : MonoBehaviour {

	public float timeToBlow = 5.0f;

	// Use this for initialization
	void Start () {
		StartCoroutine(Xablau());	
	}

	IEnumerator Xablau(){
		yield return new WaitForSeconds(timeToBlow);
		Destroy(this.gameObject);
	}
}
