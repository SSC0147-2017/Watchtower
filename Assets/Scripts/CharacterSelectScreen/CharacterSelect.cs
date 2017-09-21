/*
 * This script controls the character cycling and selection per se. It checks which characters are available, and always cicles to the next/previous character that is not chosen. It also controls the selection of a character.
 * This script has a reference to GameManager and updates its lists (Available, Sprites, SelectedCharacter) accordingly.
 * It also changes the preview of the pre-selected/selected character.
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour {

    [Header("Game Manager")]
    public GameManager GM;

    [Header("Sprites")]
    public GameObject PreviewSprite;
    public GameObject SelectedSprite;

    private int PlayerNumber;
    private int CurrentIndex;

    // Use this for initialization
    void Start () {
        //defines PlayerNumber and CurrentIndex as this objects corresponding player
        PlayerNumber = GetComponent<SelectScreenBehaviour>().PlayerNumber;
        CurrentIndex = PlayerNumber - 1;

        //set initial sprite as the first in the list
        //also set all positions in available as true
        if (GM.Sprites.Count > 0)
        {
            PreviewSprite.GetComponent<Image>().sprite = GM.Sprites[PlayerNumber-1];
        }
	}

    //changes the currently previewed character is chosen by another player, changes the previewed character to the next one available 
    void Update()
    {
        if (GM.Available[CurrentIndex] == false) {
            NextCharacter();
        }
    }

    //selects next character in cycle
    //if it reaches the end, goes to the beginning
    //also skips any character that is not available (i.e. already chosen)
    public void NextCharacter()
    {
        if (CurrentIndex == GM.Sprites.Count - 1)
        {
            CurrentIndex = 0;
        }
        else {
            CurrentIndex++;
        }
        while (GM.Available[CurrentIndex] == false)
        {
            if (CurrentIndex == GM.Sprites.Count - 1)
            {
                CurrentIndex = 0;
            }
            else
            {
                CurrentIndex++;
            }
        }
        PreviewSprite.GetComponent<Image>().sprite = GM.Sprites[CurrentIndex];
    }

    //selects previous character in cycle
    //if it reaches the start, goes to the beginning
    //also skips any character that is not available (i.e. already chosen)
    public void PreviousCharacter()
    {
        if (CurrentIndex == 0)
        {
            CurrentIndex = GM.Sprites.Count - 1;
        }
        else
        {
            CurrentIndex--;
        }
        while (GM.Available[CurrentIndex] == false)
        {
            if (CurrentIndex == 0)
            {
                CurrentIndex = GM.Sprites.Count - 1;
            }
            else
            {
                CurrentIndex--;
            }
        }
        PreviewSprite.GetComponent<Image>().sprite = GM.Sprites[CurrentIndex];
    }

    //updates GameManager lists accordingly by setting this character to "not available"
    //also returns the selected Character
    //used primarily by SelectScreenBehaviour
    public Sprite SelectCharacter()
    {
        GM.Available[CurrentIndex] = false;
        SelectedSprite.GetComponent<Image>().sprite = GM.Sprites[CurrentIndex];
        return GM.Sprites[CurrentIndex];
    }
}
