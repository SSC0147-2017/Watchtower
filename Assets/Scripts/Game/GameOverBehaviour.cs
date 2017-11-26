using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameOverBehaviour : MonoBehaviour {

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
