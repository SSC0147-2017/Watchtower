using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Utilities {

	public static GameManager GM;
	
	public List<GameObject> CharPrefabs = new List<GameObject>();
	
	int NumPlayers;
	List<int> PlayerCharacters = new List<int>();
	
	public List<GameObject> PlayerRefs = new List<GameObject>();

    public GameObject TargetGroup;

    public GameObject BlackScreen;

    public GameObject Canvas;

	void Awake(){
		if(GM == null){
			GM = this;
		}
		if(GM != this){
			Destroy(GM);
		}

        StartCoroutine(FadeOut(BlackScreen, 2f, 1f));

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
		}
		else{
			print("deu merda");
		}
		
		InstantiatePrefabs();
		SetCamera();
		SetControllers();
		SetUI();
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void InstantiatePrefabs(){
		
		for (int i = 0 ; i < NumPlayers; i++){
			PlayerRefs.Add(Instantiate(CharPrefabs[PlayerCharacters[i]], transform.GetChild(0).position, transform.GetChild(0).rotation));
			Destroy(transform.GetChild(0).gameObject);
		}
		
		for(int i = NumPlayers; i < 4; i++)
			Destroy(transform.GetChild(i).gameObject);
	}
	
	void SetCamera()
	{
		for(int i = 0; i < NumPlayers; i++)
        {
            TargetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[i].target = PlayerRefs[i].transform;
            TargetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[i].weight = 1f;
            TargetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[i].radius = 0.5f;
        }
	}
	
	void SetControllers()
	{
		
	}
	
	void SetUI()
	{
		for(int i = 0; i < NumPlayers; i++)
        {
            Canvas.transform.GetChild(i).gameObject.SetActive(true);
            //pegar referencias de vida e skills do player
        }
	}
}
