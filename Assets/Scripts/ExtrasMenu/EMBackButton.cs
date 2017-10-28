using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EMBackButton : MonoBehaviour {

	public void backToMainMenu(){
		SceneManager.LoadScene ("MainMenu");
	}
}
