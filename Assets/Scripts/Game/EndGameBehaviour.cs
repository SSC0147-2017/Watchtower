using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EndGameBehaviour : MonoBehaviour {

    public EventSystem EventSys;
    public Selectable FirstButton;

	public Button[] btns;

	private string controller = "Joystick1";
    
	bool isChangingBtn;
	int currentBtn = 0;

	// Use this for initialization
    void Start () {
        StartCoroutine(ButtonHighlightDelay());
    }

	void OnEnable(){
		Time.timeScale = 0;
	}
		
	void Update(){

		if (controller != null) {

			if (Input.GetAxis (controller + "Vertical") == 0) {
				isChangingBtn = false;
			}

			if(Input.GetAxis(controller+"Vertical") < 0 && !isChangingBtn){
				isChangingBtn = true;
				currentBtn = (currentBtn + 1) % btns.Length;
				btns[currentBtn].Select ();
			}
			if(Input.GetAxis(controller+"Vertical") > 0 && !isChangingBtn){
				isChangingBtn = true;
				currentBtn = ((currentBtn - 1) + btns.Length )% btns.Length;
				btns[currentBtn].Select ();
			}

			if(Input.GetButton(controller+"Fire0")){
				SoundManager.SM.PlayButton ();
				btns[currentBtn].onClick.Invoke();
			}
		}

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
