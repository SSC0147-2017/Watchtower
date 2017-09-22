/*
 * This script controls the Ready Screen, where the player has selected a character, but can cancel the selection and go back to choosing;
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyScreenBehaviour : MonoBehaviour {

    [Header("Game Manager")]
    public GameManager GM;

    [Space(20)]
    public int PlayerNumber;

    [Space(20)]
    [Header("Screens")]
    public GameObject SelectScreen;

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

    //inputs for P1
    void GetInputsPlayer1()
    {
        //if P1 presses B, marks the player as not ready again and goes back to character selection scree
        //also goes to SelectedCaracters list and deselects the character, making it available to choose again
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            GM.ReadyPlayers[PlayerNumber-1] = false;

            //searches the Sprites list for the position that has this player's selected character
            //that position is the same for Available list, so it sets its value back to true (available to pick)
            for (int i = 0; i < GM.Sprites.Count; i++)
            {
                if (GM.SelectedCharacters[PlayerNumber - 1] == GM.Sprites[i])
                {
                    GM.Available[i] = true;
                }
            }

            GM.SelectedCharacters[PlayerNumber - 1] = null;

            SelectScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    //inputs for P2
    void GetInputsPlayer2()
    {
        //if P2 presses B, marks the player as not ready again and goes back to character selection screen
        //also goes to SelectedCaracters list and deselects the character, making it available to choose again
        if (Input.GetKeyDown(KeyCode.Joystick2Button1) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            GM.ReadyPlayers[PlayerNumber-1] = false;

            //searches the Sprites list for the position that has this player's selected character
            //that position is the same for Available list, so it sets its value back to true (available to pick)
            for (int i = 0; i < GM.Sprites.Count; i++)
            {
                if(GM.SelectedCharacters[PlayerNumber-1] == GM.Sprites[i])
                {
                    GM.Available[i] = true;
                }
            }

            GM.SelectedCharacters[PlayerNumber - 1] = null;

            SelectScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    //inputs for P3
    void GetInputsPlayer3()
    {
        //if P3 presses B, marks the player as not ready again and goes back to character selection scree
        //also goes to SelectedCaracters list and deselects the character, making it available to choose again
        if (Input.GetKeyDown(KeyCode.Joystick3Button1) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            GM.ReadyPlayers[PlayerNumber - 1] = false;

            //searches the Sprites list for the position that has this player's selected character
            //that position is the same for Available list, so it sets its value back to true (available to pick)
            for (int i = 0; i < GM.Sprites.Count; i++)
            {
                if (GM.SelectedCharacters[PlayerNumber - 1] == GM.Sprites[i])
                {
                    GM.Available[i] = true;
                }
            }

            GM.SelectedCharacters[PlayerNumber - 1] = null;

            SelectScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    //inputs for P4
    void GetInputsPlayer4()
    {
        //if P4 presses B, marks the player as not ready again and goes back to character selection scree
        //also goes to SelectedCaracters list and deselects the character, making it available to choose again
        if (Input.GetKeyDown(KeyCode.Joystick4Button1) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            GM.ReadyPlayers[PlayerNumber - 1] = false;

            //searches the Sprites list for the position that has this player's selected character
            //that position is the same for Available list, so it sets its value back to true (available to pick)
            for (int i = 0; i < GM.Sprites.Count; i++)
            {
                if (GM.SelectedCharacters[PlayerNumber - 1] == GM.Sprites[i])
                {
                    GM.Available[i] = true;
                }
            }

            GM.SelectedCharacters[PlayerNumber - 1] = null;

            SelectScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
