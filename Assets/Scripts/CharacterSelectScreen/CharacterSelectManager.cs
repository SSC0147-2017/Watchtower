using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectManager : Utilities {

    [Space(20)]
    [Header("Which players are ready to play")]
    public List<bool> ReadyPlayers = new List<bool>();

    [Space(20)]
    [Header("Which characters have been selected")]
    public List<GameObject> SelectedCharacters = new List<GameObject>();

    [Space(20)]
    [Header("List of Characters")]
    public List<GameObject> Models = new List<GameObject>();

    [Space(20)]
    [Header("Indicates Character is available to pick")]
    public List<bool> Available = new List<bool>();

    AudioSource SwooshSound;
    public GameObject BlackScreen;

    // Use this for initialization
    void Start () {
        //makes the object not destroyable between scenes, to pass info
        DontDestroyOnLoad(gameObject);

        SwooshSound = GetComponent<AudioSource>();
        StartCoroutine(FadeOut(BlackScreen, 2f, 1f));

        //initialize lists
        for (int i = 0; i < 4; i++)
        {
            SelectedCharacters.Add(null);
            Available.Add(true);
        }

        for (int i = 0; i < Input.GetJoystickNames().Length; i++) {
            print(Input.GetJoystickNames()[i]);
        }
	}
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            int ready = 0;
            for (int i = 0; i < ReadyPlayers.Count; i++)
            {
                if (ReadyPlayers[i] == true)
                {
                    ready++;
                }
            }
            if (ready == ReadyPlayers.Count && ready != 0)
            {
                StartCoroutine(FadeIn(BlackScreen, 2f, 1f));
                print("Start game");
            }
            else{
                print("Wait for everybody to be ready");
            }
        }

    }

    public void BackToMenu()
    {
        SwooshSound.Play();
        StartCoroutine(FadeIn(BlackScreen, 2f, 1f));

        if(GameObject.Find("Music") != null)
            GameObject.Find("Music").name = "RealMusic";
                
        StartCoroutine(BackToMenuDelay(2f));
    }

    IEnumerator BackToMenuDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject);
    }
}
