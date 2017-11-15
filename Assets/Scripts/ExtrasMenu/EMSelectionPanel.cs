using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(EMContentPanel))]

public class EMSelectionPanel : MonoBehaviour {

	//Current Active List Panel
	private string currentActiveLP;

	public GameObject listPanelLore;
	public GameObject listPanelJournal;
	public GameObject listPanelBios;

	public EMContentPanel contentPanelScript;

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

				//Limpa o texto e o titulo 
				contentPanelScript.switchTextAsset (null);
			}
			break;

		}

	}
}
