using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour {

    public string str1;
    public string str2;
    public string str3;
    public string str4;

    public CharacterSelect CharacterSelectPanel;
	
	// Update is called once per frame
	void Update () {
        if (CharacterSelectPanel.GetCurrentIndex() == 0)
            GetComponent<Text>().text = str1;
        else if (CharacterSelectPanel.GetCurrentIndex() == 1)
            GetComponent<Text>().text = str2;
        else if (CharacterSelectPanel.GetCurrentIndex() == 2)
            GetComponent<Text>().text = str3;
        else if (CharacterSelectPanel.GetCurrentIndex() == 3)
            GetComponent<Text>().text = str4;
    }
}
