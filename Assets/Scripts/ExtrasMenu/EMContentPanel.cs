using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/***
 * EMContentPanel = Extras Menu Content Panel
 * O Content Panel é o painel principal do menu de extras. 
 * Dentro dele, existe o elemento de UI do titulo do texto janela rolável juntamente do texto mostrado.
 * 
 */


public class EMContentPanel : MonoBehaviour {

	//Refência ao asset de texto a ser carregado
	public TextAsset textAsset;
	//Referencia ao gameobject com o titulo
	private GameObject title;
	//Referencia ao gameobject dentro do scrollview 
	private GameObject textArea;

	private Text txtComp;
	private Text titleTxtComp;
	/**
	 * Função pública para alterar o texto mostrado na tela.
	 * @param newTextAsset novo TextAsset a ser colocado.
	 */
	public void switchTextAsset(TextAsset newTextAsset){
		this.textAsset = newTextAsset;

		txtComp.text = textAsset.text;
		titleTxtComp.text = textAsset.name;
	}

	void Awake () {
		title = this.transform.Find ("txt_Title").gameObject;
		textArea = this.transform.Find ("Scroll View").Find ("Viewport").Find ("txt_Area").gameObject;
	}

	void Start(){
		txtComp = textArea.GetComponent<Text> ();
		titleTxtComp = title.GetComponent<Text> ();

		if (textAsset != null) {
			txtComp.text = textAsset.text;
			titleTxtComp.text = textAsset.name;
		} else {
			txtComp.text = "";
			titleTxtComp.text = "";
		}
	}
}
