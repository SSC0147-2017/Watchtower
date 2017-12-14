using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if(GameObject.Find("RealMusic") && gameObject.name == "Music")
        {
            Destroy(gameObject);
        }
       
        DontDestroyOnLoad(gameObject);
	}

}
