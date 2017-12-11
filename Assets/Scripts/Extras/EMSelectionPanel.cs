using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent (typeof(EMContentPanel))]

public class EMSelectionPanel : MonoBehaviour {

	//Current Active List Panel
	private string currentActiveLP;

	/**
	 * Possible states on the extras menu:
	 * Outside the Tab, navigating between tabs
	 * Inside the Tab, navigating between their contents
	 * Exiting the menu, selecting the Back button
	 */
	private enum EMstate {outsideTab, insideTab, readingText, exiting};
	private EMstate currState;

	public GameObject listPanelLore;
	public GameObject listPanelJournal;
	public GameObject listPanelBios;
	
	public GameObject buttonLore;
	public GameObject buttonJournal;
	public GameObject buttonBios;

	public GameObject buttonBack;

	public ScrollRect buttonsScrollView;
	public Scrollbar buttonsScrollbar;
	public Scrollbar textScrollbar;

	public EMContentPanel contentPanelScript;
	
	public EventSystem EventSys;

    private bool TriggerInUse = false;
	//Used to go back and forth from the text to the button list
	private GameObject lastButton = null;

	public void Start()
    {
		currState = EMstate.outsideTab;
		currentActiveLP = "lore";
		switchPanel (currentActiveLP);
		StartCoroutine(ButtonHighlightDelay(buttonLore));
    }
	
	public void Update(){

		//OUTSIDE=================================================
		if (currState == EMstate.outsideTab) {

			//NEXT TAB
			if ((Input.GetKeyDown (KeyCode.Joystick1Button5) || Input.GetAxisRaw ("Joystick1Triggers") < 0) && TriggerInUse == false) {
				TriggerInUse = true;
				nextTab ();
			}

			//PREVIOUS TAB
			if ((Input.GetKeyDown (KeyCode.Joystick1Button4) || Input.GetAxisRaw ("Joystick1Triggers") > 0) && TriggerInUse == false) {
				TriggerInUse = true;
				prevTab ();
			}
			if (Input.GetAxisRaw ("Joystick1Triggers") == 0) {
				TriggerInUse = false;
			}


			//GO TO EXIT BUTTON
			if (Input.GetButtonDown ("Joystick1Fire3")) {
				currState = EMstate.exiting;
				StartCoroutine (ButtonHighlightDelay (buttonBack));
			}

			//Enter the tab
			if (Input.GetButtonDown ("Joystick1Fire0")) {

				//Puts the scrollbar on the top
				buttonsScrollbar.value = 1;

				EMListPanelManager ListPanelScr = null;

				if (currentActiveLP == "lore") {
					ListPanelScr = listPanelLore.GetComponent<EMListPanelManager> ();
				} else if (currentActiveLP == "journal") {
					ListPanelScr = listPanelJournal.GetComponent<EMListPanelManager> ();
				} else if (currentActiveLP == "bios") {
					ListPanelScr = listPanelBios.GetComponent<EMListPanelManager> ();				
				}
				
				StartCoroutine (ButtonHighlightDelay (ListPanelScr.getFirstActiveButton ()));

				currState = EMstate.insideTab;
			}
		} 

		//INSIDE A TAB=================================================
		else if (currState == EMstate.insideTab) {


			//Read Text
			if (Input.GetButtonDown ("Joystick1Fire0")) {
				lastButton = EventSys.currentSelectedGameObject;
				currState = EMstate.readingText;
				textScrollbar.value = 1;
				EventSys.SetSelectedGameObject (textScrollbar.gameObject);
			}

			//Exit the tab
			if (Input.GetButtonDown ("Joystick1Fire1")) {
				currState = EMstate.outsideTab;
				highlightCurrentTab ();
			}

			//Controlls scrollbar
			if (Input.GetAxis ("Joystick1Vertical") < 0) {
				StartCoroutine (ScrollDelay (buttonsScrollbar, false));
			}
			if (Input.GetAxis ("Joystick1Vertical") > 0) {
				StartCoroutine (ScrollDelay (buttonsScrollbar, true));
			}
		} 

		//READING TEXT=====================================================
		else if (currState == EMstate.readingText) {
			//Controlls scrollbar
			if (Input.GetAxis ("Joystick1Vertical") < 0) {
				StartCoroutine (ScrollDelay (textScrollbar, false));
			}
			if (Input.GetAxis ("Joystick1Vertical") > 0) {
				StartCoroutine (ScrollDelay (textScrollbar, true));
			}

			if (Input.GetButtonDown ("Joystick1Fire3")) {
				currState = EMstate.insideTab;
				EventSys.SetSelectedGameObject (lastButton);
			}
		}

		//EXITING THE MENU=================================================
		else {
			if (Input.GetButtonDown ("Joystick1Fire3") || Input.GetAxis ("Joystick1Vertical") > 0) {
				currState = EMstate.outsideTab;
				highlightCurrentTab ();
			}
		}
	}

	/**
	 * Tab Navigation
	 */
	void nextTab(){
		if (currentActiveLP == "lore") {
			EventSys.SetSelectedGameObject (buttonJournal);
			switchPanel ("journal");
		} else if (currentActiveLP == "journal") {
			EventSys.SetSelectedGameObject (buttonBios);
			switchPanel ("bios");
		} else if (currentActiveLP == "bios") {
			EventSys.SetSelectedGameObject (buttonLore);
			switchPanel ("lore");
		}
	}

	void prevTab(){
		if (currentActiveLP == "lore") {
			EventSys.SetSelectedGameObject (buttonBios);
			switchPanel ("bios");
		} else if (currentActiveLP == "journal") {
			EventSys.SetSelectedGameObject (buttonLore);
			switchPanel ("lore");
		} else if (currentActiveLP == "bios") {
			EventSys.SetSelectedGameObject (buttonJournal);
			switchPanel ("journal");
		}
	}



	/***
	 * Método usado nos botões para trocar o painel ativo
	 * @param btn	nome do botão que foi ativado
	 * (Obs: btn tem de usar strings como parâmetro por frescuras da unity)
	 */
	public void switchPanel(string btn){
		switch (btn) {

		case "lore":
			if (currentActiveLP != "lore") {
				currentActiveLP = "lore";
				listPanelLore.SetActive (true);
				listPanelBios.SetActive (false);
				listPanelJournal.SetActive (false);

				buttonsScrollView.content = listPanelLore.GetComponent<RectTransform>();

				//Limpa o texto e o titulo 
				contentPanelScript.switchTextAsset (null);
			}
			break;
		case "journal":
			if (currentActiveLP != "journal") {
				currentActiveLP = "journal";
				listPanelLore.SetActive (false);
				listPanelBios.SetActive (false);
				listPanelJournal.SetActive (true);

				buttonsScrollView.content = listPanelJournal.GetComponent<RectTransform>();

				//Limpa o texto e o titulo 
				contentPanelScript.switchTextAsset (null);
			}
			break;
		case "bios":
			if (currentActiveLP != "bios") {
				currentActiveLP = "bios";
				listPanelLore.SetActive (false);
				listPanelBios.SetActive (true);
				listPanelJournal.SetActive (false);

				buttonsScrollView.content = listPanelBios.GetComponent<RectTransform>();

				//Limpa o texto e o titulo 
				contentPanelScript.switchTextAsset (null);
			}
			break;

		}

	}



	void highlightCurrentTab(){
		if (currentActiveLP == "lore") {
			EventSys.SetSelectedGameObject (buttonLore);
		} else if (currentActiveLP == "journal") {
			EventSys.SetSelectedGameObject (buttonJournal);
		} else if (currentActiveLP == "bios") {
			EventSys.SetSelectedGameObject (buttonBios);
		}
	}
		

	IEnumerator ButtonHighlightDelay(GameObject btn)
	{
		yield return new WaitForSeconds(0.3f);
		EventSys.SetSelectedGameObject(btn);
	}

	IEnumerator ScrollDelay (Scrollbar bar, bool up) {
		if(up){
			bar.value+=0.05f;
			yield return new WaitForSeconds(1f);
		}else{
			bar.value-=0.05f;
			yield return new WaitForSeconds(1f);
		}


	}
}
