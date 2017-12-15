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

    [Header("Character Select Manager")]
    public CharacterSelectManager CSM;

    public Camera PlayerCamera;
    public List<Vector3> CameraPositions = new List<Vector3>();

    private int PlayerNumber;
    private int CurrentIndex;

    public float CameraMoveSpeed;
    float Speed;
    
    [HideInInspector]
    public bool Moving = false;

    // Use this for initialization
    void Start () {
        Speed = CameraMoveSpeed;

        //defines PlayerNumber and CurrentIndex as this objects corresponding player
        PlayerNumber = GetComponent<SelectScreenBehaviour>().PlayerNumber;
        CurrentIndex = PlayerNumber - 1;

        //set initial sprite as the first in the list
        //also set all positions in available as true
        if (CSM.Models.Count > 0)
        {
            PlayerCamera.transform.position = CameraPositions[PlayerNumber - 1];
        }
	}

    //changes the currently previewed character is chosen by another player, changes the previewed character to the next one available 
    void Update()
    {
        if (CSM.Available[CurrentIndex] == false) {
            transform.GetChild(0).GetComponent<Text>().text = NextCharacter();
        }
        PlayerCamera.transform.position = Vector3.MoveTowards(PlayerCamera.transform.position, CameraPositions[CurrentIndex], Speed * Time.deltaTime);

        if(PlayerCamera.transform.position == CameraPositions[CurrentIndex])
        {
            Moving = false;
            Speed = CameraMoveSpeed;
        }
    }

    //selects next character in cycle
    //if it reaches the end, goes to the beginning
    //also skips any character that is not available (i.e. already chosen)
    public string NextCharacter()
    {
        SoundManager.SM.PlayCharSwoosh();

        if (CurrentIndex == CSM.Models.Count - 1)
        {
            Speed *= 2;
            CurrentIndex = 0;
        }
        else {
            Speed = CameraMoveSpeed;
            CurrentIndex++;
        }
        while (CSM.Available[CurrentIndex] == false)
        {
            if (CurrentIndex == CSM.Models.Count - 1)
            {
                CurrentIndex = 0;
            }
            else
            {
                CurrentIndex++;
            }
        }
        Moving = true;

        if (CurrentIndex == 0)
            return "Corvo";
        else if (CurrentIndex == 1)
            return "Hobbes";
        else if (CurrentIndex == 2)
            return "Arwen";
        else if (CurrentIndex == 3)
            return "Jackie";
        else
            return null;
    }

    //selects previous character in cycle
    //if it reaches the start, goes to the beginning
    //also skips any character that is not available (i.e. already chosen)
    public string PreviousCharacter()
    {
        SoundManager.SM.PlayCharSwoosh();

        if (CurrentIndex == 0)
        {
            Speed *= 2;
            CurrentIndex = CSM.Models.Count - 1;
        }
        else
        {
            Speed = CameraMoveSpeed;
            CurrentIndex--;
        }
        while (CSM.Available[CurrentIndex] == false)
        {
            if (CurrentIndex == 0)
            {
                CurrentIndex = CSM.Models.Count - 1;
            }
            else
            {
                CurrentIndex--;
            }
        }
        Moving = true;

        if (CurrentIndex == 0)
            return "Corvo";
        else if (CurrentIndex == 1)
            return "Hobbes";
        else if (CurrentIndex == 2)
            return "Arwen";
        else if (CurrentIndex == 3)
            return "Jackie";
        else
            return null;
    }

    //updates GameManager lists accordingly by setting this character to "not available"
    //also returns the selected Character
    //used primarily by SelectScreenBehaviour
    public int SelectCharacter()
    {
        CSM.Available[CurrentIndex] = false;
        return CurrentIndex;
    }

    public int GetCurrentIndex()
    {
        return CurrentIndex;
    }
}
