using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelBehaviour : MonoBehaviour
{

    public EventSystem EventSys;
    public Selectable FirstButton;

    public List<Text> ChildrenTexts = new List<Text>();
    public List<Image> ChildrenImages = new List<Image>();

    public List<Button> ChildrenButtons = new List<Button>();

    public Color Black;
    public Color White;

    public void Start()
    {
        SelectFirstButton();
    }

    public void OnEnable()
    {
        SelectFirstButton();
    }

    public void SelectFirstButton()
    {
        StartCoroutine(ButtonHighlightDelay());
    }

    // Update is called once per frame
    void Update()
    {
        SynchronizeFade();
    }

    void SynchronizeFade()
    {
        float a = GetComponent<Image>().color.a;
        Color c;

        for (int i = 0; i < ChildrenTexts.Count; i++)
        {
            c = ChildrenTexts[i].GetComponent<Text>().color;
            c.a = a / 0.4f;
            ChildrenTexts[i].GetComponent<Text>().color = c;
        }
        for (int i = 0; i < ChildrenImages.Count; i++)
        {
            c = ChildrenImages[i].GetComponent<Image>().color;
            c.a = a;
            ChildrenImages[i].GetComponent<Image>().color = c;
        }
    }

    public void ActivateButtons()
    {
        Navigation nav = new Navigation();
        nav.mode = Navigation.Mode.Vertical;
        for (int i = 0; i < ChildrenButtons.Count; i++)
        {
            ChildrenButtons[i].navigation = nav;
        }
        if (transform.name == "ButtonsPanel")
            SelectFirstButton();
    }

    public void DeactivateButtons()
    {
        Navigation nav = new Navigation();
        nav.mode = Navigation.Mode.None;
        for (int i = 0; i < ChildrenButtons.Count; i++)
        {
            ChildrenButtons[i].navigation = nav;
        }
    }

    IEnumerator ButtonHighlightDelay()
    {
        yield return new WaitForSeconds(0.3f);
        EventSys.SetSelectedGameObject(FirstButton.gameObject);
    }

}