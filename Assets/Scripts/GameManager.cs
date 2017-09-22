using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Space(20)]
    [Header("Which players are ready to play")]
    public List<bool> ReadyPlayers = new List<bool>();

    [Space(20)]
    [Header("Which characters have been selected")]
    public List<Sprite> SelectedCharacters = new List<Sprite>();

    [Space(20)]
    [Header("List of Characters")]
    public List<Sprite> Sprites = new List<Sprite>();

    [Space(20)]
    [Header("Indicates Character is available to pick")]
    public List<bool> Available = new List<bool>();

    // Use this for initialization
    void Start () {
        //makes the object not destroyable between scenes, to pass info
        DontDestroyOnLoad(gameObject);

        //initialize lists
		for(int i = 0; i < 4; i++)
        {
            SelectedCharacters.Add(null);
            Available.Add(true);
        }
	}
}
