using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EndGameBehaviour : MonoBehaviour {

    public EventSystem EventSys;
    public Selectable FirstButton;

	public Button[] btns;

	private string controller = "Joystick1";
    
	bool isChangingBtn=false;
	int currentBtn = 0;

	// Use this for initialization
    void Start () {
        StartCoroutine(FadeInButtons(transform.GetChild(0).gameObject, 1f));
        for (int i = 0; i < btns.Length; i++)
            StartCoroutine(FadeInButtons(btns[i].gameObject, 1f));
        StartCoroutine(ButtonHighlightDelay());
    }

	void OnEnable(){
        StartCoroutine(FadeInButtons(transform.GetChild(0).gameObject, 1f));
        for (int i = 0; i < btns.Length; i++)
            StartCoroutine(FadeInButtons(btns[i].gameObject, 1f));
        StartCoroutine(ButtonHighlightDelay());
        StartCoroutine(TimeScaleDelay(1.5f));
	}
		
	void Update(){

		if (controller != null) {

			btns[currentBtn].Select ();

			if (Input.GetAxis (controller + "Vertical") == 0) {
				isChangingBtn = false;
			}

			if(Input.GetAxis(controller+"Vertical") < 0 && !isChangingBtn){
				isChangingBtn = true;
				currentBtn = (currentBtn + 1) % btns.Length;
			}
			if(Input.GetAxis(controller+"Vertical") > 0 && !isChangingBtn){
				isChangingBtn = true;
				currentBtn = ((currentBtn - 1) + btns.Length )% btns.Length;
			}

			if(Input.GetButton(controller+"Fire0")){
				SoundManager.SM.PlayButton ();
				btns[currentBtn].onClick.Invoke();
			}
		}

	}

    public void Restart()
    {
        StartCoroutine(FadeOutButtons(transform.GetChild(0).gameObject, 1f));
        for (int i = 0; i < btns.Length; i++)
            StartCoroutine(FadeOutButtons(btns[i].gameObject, 1f));
        GameManager.GM.RestartGame ();
    }

    public void Quit()
    {
        StartCoroutine(FadeOutButtons(transform.GetChild(0).gameObject, 1f));
        for (int i = 0; i < btns.Length; i++)
            StartCoroutine(FadeOutButtons(btns[i].gameObject, 1f));
        GameManager.GM.BackToMainMenu();
    }

    IEnumerator FadeOutButtons(GameObject obj, float delay)
    {
        Color c = obj.GetComponent<Text>().color;
        c.a = 1f;
        obj.GetComponent<Text>().color = c;

        obj.gameObject.SetActive(true);

        float time = 0;

        while (time < delay)
        {
            c.a = Mathf.Lerp(1f, 0f, time / delay);

            obj.GetComponent<Text>().color = c;
            yield return null;
            time += Time.deltaTime;
        }
        obj.gameObject.SetActive(false);
    }

    IEnumerator FadeInButtons(GameObject obj, float delay)
    {
        Color c = obj.GetComponent<Text>().color;
        c.a = 0f;
        obj.GetComponent<Text>().color = c;

        obj.gameObject.SetActive(true);

        float time = 0;

        while (time < delay)
        {
            c.a = Mathf.Lerp(0f, 1f, time / delay);

            obj.GetComponent<Text>().color = c;
            yield return null;
            time += Time.deltaTime;
        }
    }

    IEnumerator ButtonHighlightDelay()
    {
        yield return new WaitForSeconds(0.3f);
        EventSys.SetSelectedGameObject(FirstButton.gameObject);
    }

    IEnumerator TimeScaleDelay(float f)
    {
        yield return new WaitForSeconds(f);
        Time.timeScale = 0;
    }
}
