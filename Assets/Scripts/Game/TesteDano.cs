using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteDano : MonoBehaviour {

	public Transform[] T;
	public Movement[] M;

	// Use this for initialization
	void Start () {
		M[0]=T[0].gameObject.GetComponent<Movement>();
		M[1]=T[1].gameObject.GetComponent<Movement>();
		M[2]=T[2].gameObject.GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Fire1")){
			M[0].takeDamage(10);
			Debug.Log("Hobbes");
		}
		if (Input.GetButton("Fire2")){
			M[1].takeDamage(10);
			Debug.Log("Corvo");
		}
		if (Input.GetButton("Fire3")){
			M[2].takeDamage(10);
			Debug.Log("Arwin");
		}
		if (Input.GetButton("Fire0")){
			M[3].takeDamage(10);
			Debug.Log("Jackie");
		}
	}
}
