/*
 * This script controls the Selection Screen (the main screen in the CharacterSelection Scene). The player can cycle between the available characters (not chosen) and can select one or see their information;
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectScreenBehaviour : MonoBehaviour {

    [Header("Game Manager")]
    public GameManager GM;

    [Space(20)]
    public int PlayerNumber;

    [Space(20)]
    [Header("Screens")]
    public GameObject WaitScreen;
    public GameObject InfoScreen;
    public GameObject SelectedScreen;

    // Update is called once per frame
    void Update () {

        //depending on PlayerNumber, calls a different input recieving function
        switch (PlayerNumber)
        {
            case 1:
                GetInputsPlayer1();
                break;
            case 2:
                GetInputsPlayer2();
                break;
            case 3:
                GetInputsPlayer3();
                break;
            case 4:
                GetInputsPlayer4();
                break;
            default:
                print("Player number has not been set");
                break;
        }
    }

    /// <summary>
    /// Note: All buttons considered are for Xbox 360 controller
    /// </summary>

    //inputs for player 1
    void GetInputsPlayer1()
    {

        //if P1 presses A, updates the SelectedCharacters list in GameManager with the selected character. also updates the ReadyPlayers list
        //next, changes the screen to ready screen
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            GM.SelectedCharacters[PlayerNumber - 1] = gameObject.GetComponent<CharacterSelect>().SelectCharacter();
            GM.ReadyPlayers[PlayerNumber - 1] = true;
            SelectedScreen.SetActive(true);
            gameObject.SetActive(false);
        }
        //if P1 presses B, goes back to the wait screen (Press A to Play)
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Backspace))
        {

            WaitScreen.SetActive(true);
            gameObject.SetActive(false);
        }
        //if P1 presses X, goes to the character info screen
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.LeftAlt))
        {
            InfoScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    //inputs for player 2
    void GetInputsPlayer2()
    {
        //if P2 presses A, updates the SelectedCharacters list in GameManager with the selected character. also updates the ReadyPlayers list
        //next, changes the screen to ready screen
        if (Input.GetKeyDown(KeyCode.Joystick2Button0) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            GM.SelectedCharacters[PlayerNumber - 1] = gameObject.GetComponent<CharacterSelect>().SelectCharacter();
            GM.ReadyPlayers[PlayerNumber - 1] = true;
            SelectedScreen.SetActive(true);
            gameObject.SetActive(false);
        }
        //if P2 presses B, goes back to the wait screen (Press A to Play)
        if (Input.GetKeyDown(KeyCode.Joystick2Button1))
        {
            WaitScreen.SetActive(true);
            gameObject.SetActive(false);
        }
        //if P2 presses X, goes to the character info screen
        if (Input.GetKeyDown(KeyCode.Joystick2Button2))
        {
            InfoScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    //inputs for player 3
    void GetInputsPlayer3()
    {
        //if P3 presses A, updates the SelectedCharacters list in GameManager with the selected character. also updates the ReadyPlayers list
        //next, changes the screen to ready screen
        if (Input.GetKeyDown(KeyCode.Joystick3Button0) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            GM.SelectedCharacters[PlayerNumber - 1] = gameObject.GetComponent<CharacterSelect>().SelectCharacter();
            GM.ReadyPlayers[PlayerNumber - 1] = true;
            SelectedScreen.SetActive(true);
            gameObject.SetActive(false);
        }
        //if P3 presses B, goes back to the wait screen (Press A to Play)
        if (Input.GetKeyDown(KeyCode.Joystick3Button1))
        {
            WaitScreen.SetActive(true);
            gameObject.SetActive(false);
        }
        //if P3 presses X, goes to the character info screen
        if (Input.GetKeyDown(KeyCode.Joystick3Button2))
        {
            InfoScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    //inputs for player 4
    void GetInputsPlayer4()
    {
        //if P4 presses A, updates the SelectedCharacters list in GameManager with the selected character. also updates the ReadyPlayers list
        //next, changes the screen to ready screen
        if (Input.GetKeyDown(KeyCode.Joystick4Button0) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            GM.SelectedCharacters[PlayerNumber - 1] = gameObject.GetComponent<CharacterSelect>().SelectCharacter();
            GM.ReadyPlayers[PlayerNumber - 1] = true;
            SelectedScreen.SetActive(true);
            gameObject.SetActive(false);
        }
        //if P4 presses B, goes back to the wait screen (Press A to Play)
        if (Input.GetKeyDown(KeyCode.Joystick4Button1))
        {
            WaitScreen.SetActive(true);
            gameObject.SetActive(false);
        }
        //if P4 presses X, goes to the character info screen
        if (Input.GetKeyDown(KeyCode.Joystick4Button2))
        {
            InfoScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
