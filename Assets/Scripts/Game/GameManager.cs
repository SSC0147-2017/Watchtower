using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Utilities {

	public static GameManager GM;
	
	public BattleMusicManager MusicRef;
	
	public AudioSource SwooshSound;
	public List<GameObject> CharPrefabs = new List<GameObject>();
	
	int NumPlayers;
	//Stores numbers of players still alive.
	int NumLivePlayers;

	List<int> PlayerCharacters = new List<int>();
    //[HideInInspector]
	public List<GameObject> PlayerRefs = new List<GameObject>();

    public GameObject TargetGroup;
    public GameObject Canvas;

	public Sprite[] BackIcons;
	public Sprite[] FrontIcons;

    private bool isGameOver = false;
    private bool isGamePaused = false;


	void Awake(){
		if(GM == null){
			GM = this;
		}
		if(GM != this){
			Destroy(GM.gameObject);
		}

        StartCoroutine(StartTextDelay());
        //StartCoroutine(FadeOut(Canvas.transform.Find("BlackScreen").gameObject, 2f, 1f));

    }
	
	// Use this for initialization
	void Start () {

		if (GameObject.Find ("Music") != null)
			GameObject.Destroy (GameObject.Find ("Music"));

		if (GameObject.Find ("RealMusic") != null)
			GameObject.Destroy (GameObject.Find ("RealMusic"));


		NumPlayers = 0;
		if(GameObject.Find("CharacterSelectManager") != null){
			CharacterSelectManager csm = GameObject.Find("CharacterSelectManager").GetComponent<CharacterSelectManager>();
			PlayerCharacters = csm.SelectedCharacters;
			for(int i = 0; i < PlayerCharacters.Count; i++){
				if(PlayerCharacters[i] != -1){
					NumPlayers++;
				}
			}
			NumLivePlayers = NumPlayers;
//			print ("Live: " + NumLivePlayers);

			InstantiatePrefabs(csm);

			GameObject.Destroy(csm.gameObject);

		
		}
		else{
			print("Character Select manager is Null");
		}		
	}
	
	// Update is called once per frame
	void Update ()
	{

		bool joystick1 = Input.GetKeyDown (KeyCode.Joystick1Button7);
		bool joystick2 = Input.GetKeyDown (KeyCode.Joystick2Button7);
		bool joystick3 = Input.GetKeyDown (KeyCode.Joystick3Button7);
		bool joystick4 = Input.GetKeyDown (KeyCode.Joystick3Button7);
        bool keyboard = Input.GetKeyDown (KeyCode.Escape);

		//Pause Game
		if(joystick1 || joystick2 || joystick3 || joystick4 || Input.GetKeyDown(KeyCode.Escape))
        {

            if (isGamePaused)
            {
                UnPauseGame();
            }
            else
            {
                if (joystick1)
                {
                    PauseGame("Joystick1");
                }
                else if (joystick2)
                {
                    PauseGame("Joystick2");
                }
                else if (joystick3)
                {
                    PauseGame("Joystick3");
                }
                else if (joystick4)
                {
                    PauseGame("Joystick4");
                }
                else if (keyboard)
                {
                    PauseGame("");
                }
            }
        }
    }

	#region Initialization Methods

	void InstantiatePrefabs(CharacterSelectManager csm){
		
		for (int i = 0 ; i < 4; i++){
			if(PlayerCharacters[i] != -1){
				GameObject obj = Instantiate(CharPrefabs[PlayerCharacters[i]], transform.GetChild(i).position, transform.GetChild(0).rotation);
				PlayerRefs.Add(obj);
				Destroy(transform.GetChild(i).gameObject);
				SetController(obj, i+1, csm);
				SetCamera(i, obj);
				SetUI(i, obj, PlayerCharacters[i]);
			}
			else
				Destroy(transform.GetChild(i).gameObject);
		}
		
		/*for(int i = 0; i < 4; i++) {
			if(transform.GetChild(i) != null) Destroy(transform.GetChild(i).gameObject);
		}*/
	}
	
	void SetCamera(int index, GameObject obj)
	{
		TargetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[index].target = obj.transform;
        TargetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[index].weight = 1f;
        TargetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[index].radius = 0.5f;
	}
	
	void SetController(GameObject obj, int index, CharacterSelectManager csm)
	{
		string str = "Joystick"+(index);
		if(index == 4 && csm.isKeyboardActive)
			str = "";
		obj.GetComponent<Movement>().Initiate(str); 		
	}
	
	void SetUI(int index, GameObject obj, int character)
	{
	
		GameObject ui = Canvas.transform.GetChild(index).gameObject;
		Canvas.transform.GetChild (index).GetChild (0).GetComponent<Image> ().sprite = BackIcons [character];
		Canvas.transform.GetChild (index).GetChild (1).GetComponent<Image> ().sprite = FrontIcons [character];
			
        ui.SetActive(true);
        ui.GetComponent<UIBehaviour>().player = obj;
		   
	}

	#endregion

	#region Pause Methods
	void PauseGame(string controller)
    {
        Canvas.transform.Find("PausePanel").gameObject.SetActive(true);
		//Canvas.transform.Find("PausePanel").GetComponent<PauseMenuBehaviour>().SelectFirstButton();
		Canvas.transform.Find("PausePanel").GetComponent<PauseMenuBehaviour>().setController(controller);
        isGamePaused = true;
		Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        print("unpause");
        Time.timeScale = 1;
        Canvas.transform.Find("PausePanel").gameObject.SetActive(false);
        isGamePaused = false;
    }
	
	public void BackToMainMenu(){
		Time.timeScale = 1;
        SoundManager.SM.PlayButton();
        SwooshSound.Play();
        if(!Canvas.transform.Find("BlackScreen").gameObject.activeSelf)
            StartCoroutine(FadeIn(Canvas.transform.Find("BlackScreen").gameObject, 1f, 1f));
		
		if(GameObject.Find("Music") != null)
            GameObject.Find("Music").name = "RealMusic";
		StartCoroutine(BackToMenuDelay(1.3f));
	}

	#endregion

	#region player death/revive methods
	// Method called by Movement.cs when a player "dies"
	public void playerDown(){
		NumLivePlayers--;
		//print ("Live: " + NumLivePlayers);
		if (NumLivePlayers <= 0) {
			GameOver ();
		}
	}

	//Method called by Movement.cs when a player is revived
	public void playerUp(){
		if(NumLivePlayers+1 <= NumPlayers)
			NumLivePlayers++;
		//print ("Live: " + NumLivePlayers);
	}

	#endregion

	#region Game Over / Victory methods
	void GameOver()
	{
        Physics.IgnoreLayerCollision(8, 13, true);
        Physics.IgnoreLayerCollision(9, 13, true);
        Physics.IgnoreLayerCollision(10, 13, true);
        Physics.IgnoreLayerCollision(11, 13, true);

        MusicRef.StopBattleMusic();
		MusicRef.StartGameOverMusic();
		StartCoroutine(FadeIn(Canvas.transform.Find("BlackScreen").gameObject, 2f, 1f));
		StartCoroutine(PanelDelay(2f, "GameOverPanel"));
		isGameOver = true;
	}

	public void RestartGame(){
		Time.timeScale = 1;
		SoundManager.SM.PlayButton();
		SwooshSound.Play();
		if (!Canvas.transform.Find("BlackScreen").gameObject.activeSelf)
			StartCoroutine(FadeIn(Canvas.transform.Find("BlackScreen").gameObject, 1f, 1f));

		if (GameObject.Find("Music") != null)
			GameObject.Find("Music").name = "RealMusic";
		StartCoroutine(RestartDelay(1.3f));
	}

	/**
	 * Função chamada quando os jogadores chegarem no Floofy
	 */
	public void Victory(){

        Physics.IgnoreLayerCollision(8, 13, true);
        Physics.IgnoreLayerCollision(9, 13, true);
        Physics.IgnoreLayerCollision(10, 13, true);
        Physics.IgnoreLayerCollision(11, 13, true);

        StartCoroutine(FadeIn(Canvas.transform.Find("BlackScreen").gameObject, 2f, 1f));
		StartCoroutine(PanelDelay(2f, "VictoryPanel"));
		isGameOver = true;

		//Unlocks all bios
		for (int i = 0; i < PlayerRefs.Count; i++) {
			
			if(PlayerRefs[i].layer== LayerMask.NameToLayer("Corvo"))
				ExtrasManager.extrasManager.unlockExtra (ExtrasManager.extrasType.bios, 1);
			else if (PlayerRefs[i].layer==LayerMask.NameToLayer("Hobbes"))
				ExtrasManager.extrasManager.unlockExtra (ExtrasManager.extrasType.bios, 2);
			else if (PlayerRefs[i].layer==LayerMask.NameToLayer("Jackie"))
				ExtrasManager.extrasManager.unlockExtra (ExtrasManager.extrasType.bios, 3);		
			else if (PlayerRefs[i].layer == LayerMask.NameToLayer("Arwin"))
                ExtrasManager.extrasManager.unlockExtra (ExtrasManager.extrasType.bios, 0);		
		}
	}

	#endregion

	#region delay methods
	IEnumerator RestartDelay(float delay)
	{
        print("entrou");
        yield return new WaitForSeconds(delay);
        print("saiu");
		SceneManager.LoadScene("CharacterSelect");
	}


	IEnumerator BackToMenuDelay(float delay)
    {
        print("entrou");
        yield return new WaitForSeconds(delay);
        print("saiu");
        SceneManager.LoadScene("MainMenu");
    }
	
	IEnumerator PanelDelay(float delay, string name){
		yield return new WaitForSeconds(delay);
		Canvas.transform.Find(name).gameObject.SetActive(true);
	}

    IEnumerator StartTextDelay()
    {
        StartCoroutine(FadeInText(Canvas.transform.Find("StartText").gameObject, 2f, 1f));
        yield return new WaitForSeconds(4f);
        StartCoroutine(FadeOutText(Canvas.transform.Find("StartText").gameObject, 1f, 1f));
        StartCoroutine(FadeOut(Canvas.transform.Find("BlackScreen").gameObject, 1f, 1f));

    }
	#endregion


	public int getNumPlayers(){
		return NumPlayers;
	}
}
