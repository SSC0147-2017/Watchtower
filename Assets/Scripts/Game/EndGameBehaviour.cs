using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EndGameBehaviour : MonoBehaviour {

	public EventSystem EventSys;
	GameObject FirstButton;

	void Start()
    {
		FirstButton = transform.Find("RestartButton").gameObject;
        SelectFirstButton();
    }
	
    public void SelectFirstButton()
    {
        StartCoroutine(ButtonHighlightDelay());
    }
	
	public void QuitGame(){
		GameManager.GM.BackToMainMenu();
	}
	
	public void Restart(){
		//restart
	}
	
	IEnumerator ButtonHighlightDelay()
    {
        yield return new WaitForSeconds(0.1f);
        EventSys.SetSelectedGameObject(FirstButton.gameObject);
    }
}
