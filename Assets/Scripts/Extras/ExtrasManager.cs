using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


/***
 * ExtrasManager implementa singleton, acessível por extrasManager
 */
public class ExtrasManager : MonoBehaviour{

	//Numero de fragmentos de Lore
	public int numLore = 6;
	//Numero de fragmento de Journal
	public int numJournal = 9;

	public enum extrasType{lore,journal,bios};

	public static ExtrasManager extrasManager;
	public bool [] arrLore ;
	public bool [] arrJournal ;
	public bool [] arrBios ;

	// Use this for initialization
	void Start () {
		if (extrasManager == null) {
			extrasManager = this;

			this.arrLore = new bool[numLore];
			this.arrJournal = new bool[numJournal];
			this.arrBios = new bool[4];

			Load ();
		}
		else if (extrasManager != this) {
			Destroy (this.gameObject);
		}
	}

	/**
	 * Método para salvar as informações carregadas em arquivo
	 * @return true=Salvou os dados no arquivo	false=Deu alguma merda
	 */
	public bool Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/UnlockedExtras.dat");

		ExtrasData ed = new ExtrasData();
		ed.arrJournal = this.arrJournal;
		ed.arrLore = this.arrLore;
		ed.arrBios = this.arrBios;

		bf.Serialize (file, ed);

		file.Close ();

		return true;
	}

	/**
	 * Método para carregar quais extras já foram desbloqueados
	 * @return true=Carregou os dados no arquivo	false=Arquivo não existente
	 */
	public bool Load(){

		//Verifica se o arquivo existe
		if (File.Exists (Application.persistentDataPath + "/UnlockedExtras.dat")) {

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/UnlockedExtras.dat", FileMode.Open);

			ExtrasData ed = (ExtrasData)bf.Deserialize (file);
			file.Close ();

			this.arrJournal = ed.arrJournal;
			this.arrLore = ed.arrLore;
			this.arrBios = ed.arrBios;

			return true;
		} else
			return false;
	}


	public bool Delete(){
		if (File.Exists (Application.persistentDataPath + "/UnlockedExtras.dat")) {
			File.Delete (Application.persistentDataPath + "/UnlockedExtras.dat");
			print ("All deleted");
			return true;
		} else
			return false;
	}
		
	/*Método para Setar todos os extras para falso ou verdadeiro
	Isso desbloqueia ou bloqueia todos os extras obtidos
	*/
	public bool SetAllExtras( bool value){
		for (int i = 0; i < numLore; i++) {
			arrLore [i] = value;
		}

		for (int i = 0; i < numJournal; i++) {
			arrJournal [i] = value;
		}

		for (int i = 0; i < arrBios.Length; i++) {
			arrBios [i] = value;
		}

		return Save ();
	}
		

	/**
	 * Método que desbloqueia um novo extra
	 * @param type	O tipo de extra desbloqueado
	 * @param index	O índice do vetor de extras
	 */
	public bool unlockExtra(extrasType type, int index){

		try{
			switch (type){
			case ExtrasManager.extrasType.lore:{
					arrLore[index] = true;
					break;
				}
			case ExtrasManager.extrasType.journal:{
					arrJournal[index] = true;
					break;
				}
			case ExtrasManager.extrasType.bios:{
					arrBios[index] = true;
					break;
				}
			}

			//Salva após pegar
			return Save();
		}
		catch(IndexOutOfRangeException e){
			print ("index incompatível com o array selecionado");
			Debug.LogError (e, this);
			return false;
		}
	}

	void Update(){

		if (Input.GetKeyDown (KeyCode.KeypadMinus) && Input.GetKey(KeyCode.LeftControl))
			SetAllExtras (false);
		if (Input.GetKeyDown (KeyCode.KeypadPlus)  && Input.GetKey(KeyCode.LeftControl)) 
			SetAllExtras (true);		
		if (Input.GetKeyDown (KeyCode.KeypadEnter) && Input.GetKey(KeyCode.LeftControl))
			Delete ();
	}
}

/**
 * Classe container que armazena os arrays de desbloqueáveis
 */
[Serializable]
class ExtrasData{

	public bool [] arrJournal ;
	public bool [] arrBios ;
	public bool [] arrLore ;

}
