using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : Utilities {

    public EventSystem EventSys;

    public List<GameObject> PanelList = new List<GameObject>();

    public List<AudioSource> AudioList = new List<AudioSource>();

    public GameObject BlackScreen;
    public AudioSource SwooshSound;

	float SceneTransitionTimer = 2f;

	// Use this for initialization
	void Start () {

        SwooshSound = GetComponent<AudioSource>();

        StartCoroutine(FadeOut(BlackScreen, 2f, 1f));

        for(int i = 1; i < PanelList.Count; i++)
        {
            PanelList[i].gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
    }

	public void StartSceneTransition(string SceneName)
    {
        SoundManager.SM.PlayButton();
        SwooshSound.Play();
        StartCoroutine(FadeIn(BlackScreen, 2f, 1f));
		StartCoroutine(ChangeScene(SceneName));
    }

    public void MuteSound()
    {
        SoundManager.SM.PlayButton();

        for (int i = 0; i < AudioList.Count; i++)
        {
            AudioSource source = AudioList[i];

            source.mute = !source.mute;
        }
    }

    public void SetQuality(int qualityIndex)
    {
        SoundManager.SM.PlayButton();

        QualitySettings.SetQualityLevel(5-qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        SoundManager.SM.PlayButton();

        Screen.fullScreen = isFullscreen;
    }

    public void OpenPanel(GameObject window)
    {
        SoundManager.SM.PlayButton();

        for (int i = 0; i < PanelList.Count; i++)
        {
            if(window.name == PanelList[i].name)
            {
                PanelList[i].GetComponent<PanelBehaviour>().ActivateButtons();
            }
            else
            {
                PanelList[i].GetComponent<PanelBehaviour>().DeactivateButtons();
            }
        }

        StartCoroutine(FadeIn(window, 0.5f, 0.4f));
    }

    public void ClosePanel(GameObject window)
    {
        SoundManager.SM.PlayButton();

        PanelList[0].GetComponent<PanelBehaviour>().ActivateButtons();
        for (int i = 1; i < PanelList.Count; i++)
        {
            PanelList[i].GetComponent<PanelBehaviour>().DeactivateButtons();
        }

        StartCoroutine(FadeOut(window, 0.5f, 0.4f));
    }

    public void ExitGame()
    {
        SoundManager.SM.PlayButton();

        Application.Quit();
    }

	public IEnumerator ChangeScene(string SceneName)
    {
        SoundManager.SM.PlayButton();

        yield return new WaitForSeconds(SceneTransitionTimer);
		SceneManager.LoadScene(SceneName);
    }
}
