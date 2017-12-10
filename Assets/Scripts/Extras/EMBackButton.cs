using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EMBackButton : Utilities {

    AudioSource SwooshSound;
    public GameObject BlackScreen;

    void Awake()
    {
        SwooshSound = GetComponent<AudioSource>();
        StartCoroutine(FadeOut(BlackScreen, 2f, 1f));
    }

    public void backToMainMenu(){

        SwooshSound.Play();
        StartCoroutine(FadeIn(BlackScreen, 2f, 1f));

        if (GameObject.Find("Music") != null)
            GameObject.Find("Music").name = "RealMusic";

        StartCoroutine(BackToMenuDelay(2f));
	}

    IEnumerator BackToMenuDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenu");
    }
}
