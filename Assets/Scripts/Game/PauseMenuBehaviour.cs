using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenuBehaviour : MonoBehaviour {

	public EventSystem EventSys;
	public Button[] btns;

	private string controller = null;

	public List<AudioSource> AudioList = new List<AudioSource>();

	bool isChangingBtn;
	int currentBtn = 0;

	GameObject FirstButton;

	void Start()
    {
		/*FirstButton = transform.Find("ResumeButton").gameObject;
        SelectFirstButton();*/
    }

	void OnEnable(){
		StartCoroutine( ButtonHighlightDelay ());
	}

	void Update(){

		if (controller != null) {

			btns[currentBtn].Select ();


			if (Input.GetAxis (controller + "Vertical") == 0) {
				isChangingBtn = false;
			}

			if(Input.GetAxis(controller+"Vertical") < 0 && !isChangingBtn){
				isChangingBtn = true;
				currentBtn = (currentBtn + 1) % btns.Length;
			}
			if(Input.GetAxis(controller+"Vertical") > 0 && !isChangingBtn){
				isChangingBtn = true;
				currentBtn = ((currentBtn - 1) + btns.Length )% btns.Length;
			}

			if(Input.GetButton(controller+"Fire0")){
				SoundManager.SM.PlayButton ();
				btns[currentBtn].onClick.Invoke();
			}

			if(Input.GetButton(controller+"Fire1")){
				Resume ();
			}
		}

	}
		
	public void setController(string str){
		controller = str;
	} 


    public void SelectFirstButton()
    {
        StartCoroutine(ButtonHighlightDelay());
    }
	
	public void MuteMusic(){

		for (int i = 0; i < AudioList.Count; i++)
		{
			AudioSource source = AudioList[i];

			source.mute = !source.mute;
		}
	}
	
	public void QuitGame(){
		GameManager.GM.BackToMainMenu();
	}
	
	public void Resume(){
		GameManager.GM.UnPauseGame();
	}
	
	IEnumerator ButtonHighlightDelay()
    {
        yield return new WaitForSeconds(0.2f);
		btns[currentBtn].Select ();
		//EventSys.SetSelectedGameObject(FirstButton.gameObject);
    }
}
