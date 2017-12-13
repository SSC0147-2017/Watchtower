using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmabienceManager : MonoBehaviour {

	public AudioClip [] clips;
	public AudioSource source;

	// Use this for initialization
	void Start () {
		PlayAmbienceClip ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!source.isPlaying) {
			PlayAmbienceClip ();
		}
	}

	void PlayAmbienceClip(){
		int index = Random.Range(0, clips.Length);
		source.PlayOneShot(clips[index], 0.5f);
	}
}
