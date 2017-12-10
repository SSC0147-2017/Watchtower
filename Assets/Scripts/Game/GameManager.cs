using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Utilities {

	public static GameManager GM;
	
	public AudioSource SwooshSound;
	
	public List<GameObject> CharPrefabs = new List<GameObject>();
	
	int NumPlayers;
	List<int> PlayerCharacters = new List<int>();
	
    //[HideInInspector]
	public List<GameObject> PlayerRefs = new List<GameObject>();

    public GameObject TargetGroup;

    public GameObject Canvas;

    private bool isGameOver = false;
    private bool isGamePaused = false;

	void Awake(){
		if(GM == null){
			GM = this;
		}
		if(GM != this){
			Destroy(GM.gameObject);
		}

        StartCoroutine(FadeOut(Canvas.transform.Find("BlackScreen").gameObject, 2f, 1f));

    }
	
	// Use this for initialization
	void Start () {
		
		if(GameObject.Find("CharacterSelectManager") != null){
			CharacterSelectManager csm = GameObject.Find("CharacterSelectManager").GetComponent<CharacterSelectManager>();
			PlayerCharacters = csm.SelectedCharacters;
			for(int i = 0; i < PlayerCharacters.Count; i++){
				if(PlayerCharacters[i] != -1){
					NumPlayers++;
				}
			}
			
			InstantiatePrefabs(csm);

			GameObject.Destroy(csm.gameObject);
		}
		else{
			print("deu merda");
		}		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick2Button7) || Input.GetKeyDown(KeyCode.Joystick3Button7) || Input.GetKeyDown(KeyCode.Joystick4Button7) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                UnPauseGame();
            }
            else
            {
                PauseGame();
            }
        }
	}
	
	void InstantiatePrefabs(CharacterSelectManager csm){
		
		for (int i = 0 ; i < 4; i++){
			if(PlayerCharacters[i] != -1){
				GameObject obj = Instantiate(CharPrefabs[PlayerCharacters[i]], transform.GetChild(0).position, transform.GetChild(0).rotation);
				PlayerRefs.Add(obj);
				Destroy(transform.GetChild(i).gameObject);
				SetController(obj, i+1, csm);
				SetCamera(i, obj);
				SetUI(i, obj);
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
	
	void SetUI(int index, GameObject obj)
	{
	
		GameObject ui = Canvas.transform.GetChild(index).gameObject;
			
        ui.SetActive(true);
        print("entrou");
        ui.GetComponent<UIBehaviour>().player = obj;
        print(ui.GetComponent<UIBehaviour>().player);   
	}

    void GameOver()
    {
		//StartCoroutine(FadeIn(Canvas.transform.Find("BlackScreen").gameObject, 2f, 1f));
		StartCoroutine(PanelDelay(2f, "GameOverPanel"));
        isGameOver = true;
    }

	public void RestartGame(){
		SceneManager.LoadScene ("CharacterSelect");
	}


	/**
	 * Função chamada quando os jogadores chegarem no Floofy
	 */
	public void Victory(){
		StartCoroutine(FadeIn(Canvas.transform.Find("BlackScreen").gameObject, 2f, 1f));
		StartCoroutine(PanelDelay(2f, "VictoryPanel"));
		isGameOver = true;
	}

    void PauseGame()
    {
        Canvas.transform.Find("PausePanel").gameObject.SetActive(true);
		Canvas.transform.Find("PausePanel").GetComponent<PauseMenuBehaviour>().SelectFirstButton();
        isGamePaused = true;
		Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        Canvas.transform.Find("PausePanel").gameObject.SetActive(false);
        isGamePaused = false;
		Time.timeScale = 1;
    }
	
	public void BackToMainMenu(){
		Time.timeScale = 1;
		SwooshSound.Play();
        if(!Canvas.transform.Find("BlackScreen").gameObject.activeSelf)
            StartCoroutine(FadeIn(Canvas.transform.Find("BlackScreen").gameObject, 1f, 1f));
		
		if(GameObject.Find("Music") != null)
            GameObject.Find("Music").name = "RealMusic";
		StartCoroutine(BackToMenuDelay(1.3f));
	}
	
	IEnumerator BackToMenuDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenu");
    }
	
	IEnumerator PanelDelay(float delay, string name){
		yield return new WaitForSeconds(delay);
		Canvas.transform.Find(name).gameObject.SetActive(true);
	}
}
