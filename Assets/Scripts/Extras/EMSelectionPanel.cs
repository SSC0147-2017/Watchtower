using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent (typeof(EMContentPanel))]

public class EMSelectionPanel : MonoBehaviour {

	//Current Active List Panel
	private string currentActiveLP;

	public GameObject listPanelLore;
	public GameObject listPanelJournal;
	public GameObject listPanelBios;
	
	public GameObject buttonLore;
	public GameObject buttonJournal;
	public GameObject buttonBios;

	public ScrollRect scrollView;

	public EMContentPanel contentPanelScript;
	
	public EventSystem EventSys;
	
	public void Start()
    {
		currentActiveLP = "lore";
        StartCoroutine(PanelHighlightDelay(buttonLore));
    }
	
	public void Update(){
		print(currentActiveLP);
		if(Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetAxis("Joystick1Triggers") < 0){
			if(currentActiveLP == "lore"){
				EventSys.SetSelectedGameObject(buttonJournal);
				switchPanel("journal");
			}
			else if(currentActiveLP == "journal"){
				EventSys.SetSelectedGameObject(buttonBios);
				switchPanel("bios");
			}
			else if(currentActiveLP == "bios"){
				EventSys.SetSelectedGameObject(buttonLore);
				switchPanel("lore");
			}
		}
		if(Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetAxis("Joystick1Triggers") > 0){
			if(currentActiveLP == "lore"){
				EventSys.SetSelectedGameObject(buttonBios);
				switchPanel("bios");
			}
			else if(currentActiveLP == "journal"){
				EventSys.SetSelectedGameObject(buttonLore);
				switchPanel("lore");
			}
			else if(currentActiveLP == "bios"){
				EventSys.SetSelectedGameObject(buttonJournal);
				switchPanel("journal");
			}
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

				scrollView.content = listPanelLore.GetComponent<RectTransform>();

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

				scrollView.content = listPanelJournal.GetComponent<RectTransform>();

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

				scrollView.content = listPanelBios.GetComponent<RectTransform>();

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
}
