using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBehaviour : Utilities {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenPanel(GameObject window)
    {
        StartCoroutine(FadeIn(window, 0.5f));
    }

    public void ClosePanel(GameObject window)
    {
        StartCoroutine(FadeOut(window, 0.5f));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
