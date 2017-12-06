using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent (typeof(EMContentPanel))]

public class EMSelectionPanel : MonoBehaviour {

	//Current Active List Panel
	private string currentActiveLP;
	//True if inside a tab and navigating its contents. False if navigating between tabs.
	private bool insideTab = false;

	public GameObject listPanelLore;
	public GameObject listPanelJournal;
	public GameObject listPanelBios;
	
	public GameObject buttonLore;
	public GameObject buttonJournal;
	public GameObject buttonBios;

	public ScrollRect buttonsScrollView;
	public Scrollbar buttonsScrollbar;

	public EMContentPanel contentPanelScript;
	
	public EventSystem EventSys;
	
	public void Start()
    {

		currentActiveLP = "lore";
        StartCoroutine(PanelHighlightDelay(buttonLore));
    }
	
	public void Update(){
		//print(currentActiveLP);

		if (!insideTab) {
			if (Input.GetKeyDown (KeyCode.Joystick1Button5) || Input.GetAxis ("Joystick1Triggers") < 0) {
				nextTab ();
			}
			if (Input.GetKeyDown (KeyCode.Joystick1Button4) || Input.GetAxis ("Joystick1Triggers") > 0) {
				prevTab ();
			}


			//Enter the tab
			if (Input.GetButton ("Joystick1Fire0")) {

				EMListPanelManager ListPanelScr = null;

				if (currentActiveLP == "lore") {
					ListPanelScr = listPanelLore.GetComponent<EMListPanelManager> ();
				} else if (currentActiveLP == "journal") {
					ListPanelScr = listPanelJournal.GetComponent<EMListPanelManager> ();
				} else if (currentActiveLP == "bios") {
					ListPanelScr = listPanelBios.GetComponent<EMListPanelManager> ();				
				}
				
				StartCoroutine (ButtonHighlightDelay (ListPanelScr.getFirstActiveButton ()));

				insideTab = true;
			}
		} 

		//Inside a tab
		else {
			//Exit the tab
			if (Input.GetButton ("Joystick1Fire1")) {
				insideTab = false;
				highlightCurrentTab ();
			}

			//Lowers the Scrollbar
			if (Input.GetAxis ("Joystick1Vertical") < 0) {
				buttonsScrollbar.value -= 0000.1f;
			}

			if (Input.GetAxis ("Joystick1Vertical") > 0) {
				buttonsScrollbar.value += 0000.1f;
			}
		}

	}


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


	void highlightCurrentTab(){
		if (currentActiveLP == "lore") {
			EventSys.SetSelectedGameObject (buttonLore);
		} else if (currentActiveLP == "journal") {
			EventSys.SetSelectedGameObject (buttonJournal);
		} else if (currentActiveLP == "bios") {
			EventSys.SetSelectedGameObject (buttonBios);
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
	
	IEnumerator PanelHighlightDelay(GameObject obj)
    {
        yield return new WaitForSeconds(0.3f);
        EventSys.SetSelectedGameObject(obj);
    }


	IEnumerator ButtonHighlightDelay(GameObject btn)
	{
		yield return new WaitForSeconds(0.3f);
		EventSys.SetSelectedGameObject(btn);
	}

}
