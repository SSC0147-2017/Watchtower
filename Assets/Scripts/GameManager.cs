using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager GM;
	
	public List<GameObject> CharPrefabs = new List<GameObject>();
	
	public int NumPlayers;
	public List<int> PlayerCharacters = new List<int>();
	
	public List<GameObject> PlayerRefs = new List<GameObject>();

	void Awake(){
		if(GM == null){
			GM = this;
		}
		if(GM != this){
			Destroy(GM);
		}
		
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
			Instantiate(CharPrefabs[PlayerCharacters[i]], transform.GetChild(0).position, transform.GetChild(0).rotation);
			Destroy(transform.GetChild(0).gameObject);
		}
		
		for(int i = NumPlayers; i < 4; i++)
			Destroy(transform.GetChild(i).gameObject);
	}
	
	void SetCamera()
	{
		
	}
	
	void SetControllers()
	{
		
	}
	
	void SetUI()
	{
		
	}
}
