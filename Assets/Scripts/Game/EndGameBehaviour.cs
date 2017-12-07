using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EndGameBehaviour : MonoBehaviour {

    public EventSystem EventSys;
    public Selectable FirstButton;

    // Use this for initialization
    void Start () {
        StartCoroutine(ButtonHighlightDelay());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Restart()
    {
		GameManager.GM.RestartGame ();
    }

    public void Quit()
    {
        GameManager.GM.BackToMainMenu();
    }

    IEnumerator ButtonHighlightDelay()
    {
        yield return new WaitForSeconds(0.3f);
        EventSys.SetSelectedGameObject(FirstButton.gameObject);
    }
}
