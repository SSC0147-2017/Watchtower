using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuBehaviour : MonoBehaviour {

	public EventSystem EventSys;
	GameObject FirstButton;

	void Start()
    {
		FirstButton = transform.Find("ResumeButton").gameObject;
        SelectFirstButton();
    }

    public void SelectFirstButton()
    {
        StartCoroutine(ButtonHighlightDelay());
    }
	
	public void MuteMusic(){
		//pega referencia pro audio source
		//audio source.mute
	}
	
	public void QuitGame(){
		GameManager.GM.BackToMainMenu();
	}
	
	public void Resume(){
		GameManager.GM.UnPauseGame();
	}
	
	IEnumerator ButtonHighlightDelay()
    {
        yield return new WaitForSeconds(0.1f);
        EventSys.SetSelectedGameObject(FirstButton.gameObject);
    }
}
