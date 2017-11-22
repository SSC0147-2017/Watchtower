using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMListPanelManager : MonoBehaviour {

	public enum extrasType{lore,journal,bios};

	//Vetor com referências aos botões de textSwitch
	public GameObject[] textSwitchButtons;
	//Texto para indicar o número de extras desbloqueados
	public Text txtNumUnlocked;
	//Tipo de extras armazenados nessa lista
	public extrasType type;

	//Número de extras desbloqueados
	private int unlockedCount;
	private bool[] arrUnlocked;


	void OnEnable(){
		this.Start ();
	}


	// Use this for initialization
	void Start () {

		arrUnlocked = null;

		switch (type){
			case extrasType.lore:{
				arrUnlocked = ExtrasManager.extrasManager.arrLore;
				break;
			}
			case extrasType.journal:{
				arrUnlocked = ExtrasManager.extrasManager.arrJournal;
				break;
			}
			case extrasType.bios:{
				arrUnlocked = ExtrasManager.extrasManager.arrBios;
				break;
			}
			default:{
				arrUnlocked = ExtrasManager.extrasManager.arrJournal;
				break;
			}
		}

		unlockedCount = arrUnlocked.Length;

		try{
			//Ativa os botões dos textos desbloqueados
			for (int i = 0; i < arrUnlocked.Length; i++) {
				if (arrUnlocked [i] == false){
					textSwitchButtons [i].SetActive(false);
					unlockedCount--;
				}
			}
			if(txtNumUnlocked != null)
				txtNumUnlocked.text = unlockedCount + " of " + arrUnlocked.Length + " Discovered";
		}
		catch(IndexOutOfRangeException e){
			print ("Arrays de tamanho incompatível");
			Debug.LogError (e, this);
		}

	}

}
