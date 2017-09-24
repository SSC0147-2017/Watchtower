﻿/*
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
        if (GM.Models.Count > 0)
        {
            PlayerCamera.transform.position = CameraPositions[PlayerNumber - 1];
        }
	}

    //changes the currently previewed character is chosen by another player, changes the previewed character to the next one available 
    void Update()
    {
        if (GM.Available[CurrentIndex] == false) {
            NextCharacter();
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
    public void NextCharacter()
    {
        if (CurrentIndex == GM.Models.Count - 1)
        {
            Speed *= 2;
            CurrentIndex = 0;
        }
        else {
            Speed = CameraMoveSpeed;
            CurrentIndex++;
        }
        while (GM.Available[CurrentIndex] == false)
        {
            if (CurrentIndex == GM.Models.Count - 1)
            {
                CurrentIndex = 0;
            }
            else
            {
                CurrentIndex++;
            }
        }
        Moving = true;
    }

    //selects previous character in cycle
    //if it reaches the start, goes to the beginning
    //also skips any character that is not available (i.e. already chosen)
    public void PreviousCharacter()
    {
        if (CurrentIndex == 0)
        {
            Speed *= 2;
            CurrentIndex = GM.Models.Count - 1;
        }
        else
        {
            Speed = CameraMoveSpeed;
            CurrentIndex--;
        }
        while (GM.Available[CurrentIndex] == false)
        {
            if (CurrentIndex == 0)
            {
                CurrentIndex = GM.Models.Count - 1;
            }
            else
            {
                CurrentIndex--;
            }
        }
        Moving = true;
    }

    //updates GameManager lists accordingly by setting this character to "not available"
    //also returns the selected Character
    //used primarily by SelectScreenBehaviour
    public GameObject SelectCharacter()
    {
        GM.Available[CurrentIndex] = false;
        //SelectedSprite.GetComponent<Image>().sprite = GM.Models[CurrentIndex];
        return GM.Models[CurrentIndex];
    }
}
