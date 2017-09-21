using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Utilities : MonoBehaviour {


    public IEnumerator FadeOut(GameObject screen, float fadeDelay)
    {
        screen.gameObject.SetActive(true);

        Color c = screen.GetComponent<Image>().color;

        float time = 0;

        while (time < fadeDelay)
        {
            c.a = Mathf.Lerp(0.4f, 0f, time / fadeDelay);

            screen.GetComponent<Image>().color = c;
            yield return null;
            time += Time.deltaTime;
        }
        screen.gameObject.SetActive(false);
    }

    public IEnumerator FadeIn(GameObject screen, float fadeDelay)
    {
        screen.gameObject.SetActive(true);

        Color c = screen.GetComponent<Image>().color;

        float time = 0;

        while (time < fadeDelay)
        {
            c.a = Mathf.Lerp(0f, 0.4f, time / fadeDelay);

            screen.GetComponent<Image>().color = c;
            yield return null;
            time += Time.deltaTime;
        }

    }
}
