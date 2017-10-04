using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/***
 * EMTextSwitchButton = Extras Menu Text Switch Button
 * Script usado pelos botões laterais para modificar 
 * o TextAsset carregado pelo contentPanel
 */

public class EMTextSwtichButton : MonoBehaviour {

	public TextAsset textAsset;
	public EMContentPanel contentPanelScript;

	private Button btn;

	public void SwitchText(){
		contentPanelScript.switchTextAsset (textAsset);
	}

}
