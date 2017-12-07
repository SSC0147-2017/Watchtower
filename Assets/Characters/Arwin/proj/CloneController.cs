using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneController : MonoBehaviour {

	private bool CloneAlive=false;

	public bool addClone(){
		if (CloneAlive)
			return false;
		CloneAlive = true;
		return true;
	}

	public void killClone() {
		CloneAlive=false;
	}
}
