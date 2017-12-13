using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMListPanelManager : MonoBehaviour {

	//Vetor com referências aos botões de textSwitch
	public GameObject[] textSwitchButtons;
	//Texto para indicar o número de extras desbloqueados
	public Text txtNumUnlocked;
	//Tipo de extras armazenados nessa lista
	public ExtrasManager.extrasType type;

	//Número de extras desbloqueados
	private int unlockedCount;
	private bool[] arrUnlocked = null;


	void OnEnable(){
		this.Start ();
	}

	void Update(){
		if (ExtrasManager.extrasManager != null && arrUnlocked == null)
			Start ();
	}

	// Use this for initialization
	void Start () {
		if (ExtrasManager.extrasManager != null) {
			arrUnlocked = null;
			txtNumUnlocked.text = "";
			switch (type) {
			case ExtrasManager.extrasType.lore:
				{
					arrUnlocked = ExtrasManager.extrasManager.arrLore;
					break;
				}
			case ExtrasManager.extrasType.journal:
				{
					arrUnlocked = ExtrasManager.extrasManager.arrJournal;
					break;
				}
			case ExtrasManager.extrasType.bios:
				{
					arrUnlocked = ExtrasManager.extrasManager.arrBios;
					break;
				}
			}

			unlockedCount = arrUnlocked.Length;

			try {
				//Ativa os botões dos textos desbloqueados
				for (int i = 0; i < arrUnlocked.Length; i++) {
					if (arrUnlocked [i] == false) {
						textSwitchButtons [i].SetActive (false);
						unlockedCount--;
					}
				}
				if (txtNumUnlocked != null)
					txtNumUnlocked.text = unlockedCount + " of " + arrUnlocked.Length + " Discovered";
			} catch (IndexOutOfRangeException e) {
				print ("Arrays de tamanho incompatível");
				Debug.LogError (e, this);
			}
		}

	}


	public GameObject getFirstActiveButton(){
		GameObject btn = null;
		for (int i = 0; i < arrUnlocked.Length; i++) {
			if (textSwitchButtons [i].activeSelf){
				btn = textSwitchButtons [i];
				break;
			}
		}
		return btn;
	}

}
