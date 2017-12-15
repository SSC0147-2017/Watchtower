using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSwitch : MonoBehaviour {

	public Image imgA;
	public Image imgB;

	public float timeToChange = 5.0f;
	public bool change = false;

	// Use this for initialization
	void Start () {
		if (imgA == null)
			imgA = GetComponent<Image> ();
		if (imgB == null)
			imgB = GetComponent<Image> ();	
		StartCoroutine (timer ());
	}
	
	// Update is called once per frame
	void Update () {
		if (change) {
			imgA.enabled = !imgA.enabled;
			imgB.enabled = !imgB.enabled;
			change = false;
			StartCoroutine (timer ());
		}
	}

	IEnumerator timer(){
		yield return new WaitForSeconds (timeToChange);
		change = true;
	}
}
