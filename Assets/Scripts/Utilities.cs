using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Utilities : MonoBehaviour {


    public IEnumerator FadeOut(GameObject screen, float fadeDelay, float alphaMax)
    {
        if (alphaMax > 1) alphaMax = 1;
        if (alphaMax < 0) alphaMax = 0;

        screen.gameObject.SetActive(true);

        Color c = screen.GetComponent<Image>().color;

        float time = 0;

        while (time < fadeDelay)
        {
            c.a = Mathf.Lerp(alphaMax, 0f, time / fadeDelay);

            screen.GetComponent<Image>().color = c;
            yield return null;
            time += Time.deltaTime;
        }
        screen.gameObject.SetActive(false);
    }

    public IEnumerator FadeIn(GameObject screen, float fadeDelay, float alphaMax)
    {
        if (alphaMax > 1) alphaMax = 1;
        if (alphaMax < 0) alphaMax = 0;

        screen.gameObject.SetActive(true);

        Color c = screen.GetComponent<Image>().color;

        float time = 0;

        while (time < fadeDelay)
        {
            c.a = Mathf.Lerp(0f, alphaMax, time / fadeDelay);

            screen.GetComponent<Image>().color = c;
            yield return null;
            time += Time.deltaTime;
        }

    }
}
