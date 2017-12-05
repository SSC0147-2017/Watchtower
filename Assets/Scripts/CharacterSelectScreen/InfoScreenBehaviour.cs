/*
 * This script controls the Information Screen, where the player has pre-selected a character and is reading information about their moveset;
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoScreenBehaviour : MonoBehaviour {

    [Space(20)]
    public int PlayerNumber;

    [Space(20)]
    [Header("Screens")]
    public GameObject SelectScreen;

    // Update is called once per frame
    void Update()
    {
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
        //if P1 presses B, goes back to the selection screen
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.LeftControl))
        {

            SelectScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    //inputs for player 2
    void GetInputsPlayer2()
    {

        //if P2 presses B, goes back to the selection screen
        if (Input.GetKeyDown(KeyCode.Joystick2Button1) || Input.GetKeyDown(KeyCode.Joystick2Button2))
        {
            SelectScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    //inputs for player 3
    void GetInputsPlayer3()
    {
        //if P3 presses B, goes back to the selection screen
        if (Input.GetKeyDown(KeyCode.Joystick3Button1) || Input.GetKeyDown(KeyCode.Joystick3Button2))
        {
            SelectScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    //inputs for player 4
    void GetInputsPlayer4()
    {
        //if P4 presses B, goes back to the selection screen
        if (Input.GetKeyDown(KeyCode.Joystick4Button1) || Input.GetKeyDown(KeyCode.Joystick4Button2))
        {
            SelectScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
