using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthDecLoop : MonoBehaviour {

	public Image img;
	public float rate = 0.1f;
	// Use this for initialization
	void Start () {
		if (img == null)
			img = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (img.fillAmount == 0)
			img.fillAmount = 1;
		else {
			img.fillAmount -= (rate * Time.deltaTime);
		}
	}
}
