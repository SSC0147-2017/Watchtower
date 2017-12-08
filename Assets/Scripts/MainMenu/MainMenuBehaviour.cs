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
        SwooshSound.Play();
        StartCoroutine(FadeIn(BlackScreen, 2f, 1f));
		StartCoroutine(ChangeScene(SceneName));
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

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(5-qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void OpenPanel(GameObject window)
    {
        for(int i = 0; i < PanelList.Count; i++)
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
        PanelList[0].GetComponent<PanelBehaviour>().ActivateButtons();
        for (int i = 1; i < PanelList.Count; i++)
        {
            PanelList[i].GetComponent<PanelBehaviour>().DeactivateButtons();
        }

        StartCoroutine(FadeOut(window, 0.5f, 0.4f));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

	public IEnumerator ChangeScene(string SceneName)
    {
		yield return new WaitForSeconds(SceneTransitionTimer);
		SceneManager.LoadScene(SceneName);
    }
}
