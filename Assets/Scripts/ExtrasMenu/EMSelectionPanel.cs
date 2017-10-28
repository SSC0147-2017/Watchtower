using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMSelectionPanel : MonoBehaviour {

	public GameObject listPanelLore;
	public GameObject listPanelJournal;
	public GameObject listPanelBios;


	public void switchPanel(string btn){
		switch (btn) {

		case "lore":
			print ("lore");
			listPanelLore.SetActive(true);
			listPanelBios.SetActive(false);
			listPanelJournal.SetActive(false);
			break;
		case "journal":
			print ("Journal");
			listPanelLore.SetActive(false);
			listPanelBios.SetActive(false);
			listPanelJournal.SetActive(true);
			break;
		case "bios":
			print("Bios");
			listPanelLore.SetActive(false);
			listPanelBios.SetActive(true);
			listPanelJournal.SetActive(false);
			break;
		}
			
	}
}
