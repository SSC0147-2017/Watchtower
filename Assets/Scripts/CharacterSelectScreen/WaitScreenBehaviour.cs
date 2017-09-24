/*
 * This script controls the Waiting Screen, where the CharacterSelection Scene has just started and players have to press a button to join the party and play;
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitScreenBehaviour : MonoBehaviour {

    [Header("Game Manager")]
    public GameManager GM;

    [Space(20)]
    public int PlayerNumber;

    [Space(20)]
    [Header("Screens")]
    public GameObject SelectScreen;

    // Update is called once per frame
    void Update() {

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
        //if P1 presses A, goes to character selection screen and signals the GameManager that this player is active
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            GM.ReadyPlayers.Add(false);
            SelectScreen.SetActive(true);
            gameObject.SetActive(false);
        }

        //if P1 presses B, goes back to menu
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

    //inputs for player 2
    void GetInputsPlayer2()
    {
        //if P2 presses A, goes to character selection screen and signals the GameManager that this player is active
        if (Input.GetKeyDown(KeyCode.Joystick2Button0) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            GM.ReadyPlayers.Add(false);
            SelectScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    //inputs for player 3
    void GetInputsPlayer3()
    {
        //if P3 presses A, goes to character selection screen and signals the GameManager that this player is active
        if (Input.GetKeyDown(KeyCode.Joystick3Button0) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            GM.ReadyPlayers.Add(false);
            SelectScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    //inputs for player 4
    void GetInputsPlayer4()
    {
        //if P4 presses A, goes to character selection screen and signals the GameManager that this player is active
        if (Input.GetKeyDown(KeyCode.Joystick4Button0) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            GM.ReadyPlayers.Add(false);
            SelectScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
