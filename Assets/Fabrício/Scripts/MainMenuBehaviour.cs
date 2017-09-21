using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBehaviour : Utilities {

    public GameObject Camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MuteSound()
    {
        AudioSource source = Camera.GetComponent<AudioSource>();
        if(source.mute == false)
        {
            source.mute = true;
        }
        else
        {
            source.mute = false;
        }
    }

    public void OpenPanel(GameObject window)
    {
        StartCoroutine(FadeIn(window, 0.5f, 0.4f));
    }

    public void ClosePanel(GameObject window)
    {
        StartCoroutine(FadeOut(window, 0.5f, 0.4f));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
