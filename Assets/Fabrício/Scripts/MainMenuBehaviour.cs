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
        StartCoroutine(FadeIn(window, 1f));
    }

    public void ClosePanel(GameObject window)
    {
        print("banana");
        StartCoroutine(FadeOut(window, 1f));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
