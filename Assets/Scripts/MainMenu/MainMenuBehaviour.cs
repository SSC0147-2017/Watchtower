using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : Utilities {

    public List<AudioSource> AudioList = new List<AudioSource>();
    public Button FirstButton;

    public GameObject BlackScreen;
    public AudioSource SwooshSound;

	// Use this for initialization
	void Start () {

        SwooshSound = GetComponent<AudioSource>();

        StartCoroutine(FadeOut(BlackScreen, 2f, 1f));

		if(FirstButton != null)
        {
            FirstButton.Select();
        }
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void NewGame()
    {
        SwooshSound.Play();
        StartCoroutine(FadeIn(BlackScreen, 2f, 1f));
        StartCoroutine(ChangeScene(2f));
    }

    public void MuteSound()
    {
        for (int i = 0; i < AudioList.Count; i++)
        {
            AudioSource source = AudioList[i];
            if (source.mute == false)
            {
                source.mute = true;
            }
            else
            {
                source.mute = false;
            }
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

    public IEnumerator ChangeScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("Character Select");
    }
}
