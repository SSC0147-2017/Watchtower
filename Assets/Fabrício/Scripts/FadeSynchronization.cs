using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeSynchronization : MonoBehaviour {

    public List<Text> childrenText = new List<Text>();
    public List<Image> childrenImage = new List<Image>();

	// Update is called once per frame
	void Update () {

        float a = GetComponent<Image>().color.a;
        Color c;

		for(int i = 0; i < childrenText.Count; i++)
        {
            c = childrenText[i].GetComponent<Text>().color;
            c.a = a/0.4f;
            childrenText[i].GetComponent<Text>().color = c;
        }
        for (int i = 0; i < childrenImage.Count; i++)
        {
            c = childrenImage[i].GetComponent<Image>().color;
            c.a = a;
            childrenImage[i].GetComponent<Image>().color = c;
        }
    }
}
