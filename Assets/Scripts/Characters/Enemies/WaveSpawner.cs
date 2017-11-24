using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

	public enum enemiesType{bugfolk, depths, howler, waspinoid};

	[Space(20)]
	//reference to each enemy prefab
	public GameObject Enemy1;
	public GameObject Enemy2;
	public GameObject Enemy3;
	public GameObject Enemy4;
	
	//auxiliar count and bool to spawn only once.
	int count = 0;
	bool hasSpawned = false;
	
	[Header ("1 - Bugfolk")]
	[Header ("2 - Spawn of the Depths")]
	[Header ("3 - Howler of the Spawn")]
	[Header ("4 - Waspinoid")]
	//list of all enemies that will spawn, and the delay before each spawn
	public List<enemiesType> enemies = new List<enemiesType>();
	List <GameObject> prefabs = new List<GameObject>();
	
	[Space(20)]
	public List<float> spawnDelays = new List<float>();
	
	[Space(20)]
	public float SpawnRange;
	
	void Start(){
		for(int i = 0; i < enemies.Count; i++){
			if(enemies[i] == enemiesType.bugfolk){
				prefabs.Add(Enemy1);
			}
			else if(enemies[i] == enemiesType.depths){
				prefabs.Add(Enemy2);
			}
			else if(enemies[i] == enemiesType.howler){
				prefabs.Add(Enemy3);
			}
			else if(enemies[i] == enemiesType.waspinoid){
				prefabs.Add(Enemy4);
			}
		}
	}

	//detects if a player reaches the trigger area and starts the spawning
	void OnTriggerEnter(Collider collision){
		if(hasSpawned == false){
			if(collision.tag == "Player"){
			
				if(prefabs.Count > 0){
					StartCoroutine(Spawn());
					hasSpawned = true;
				}
			}
		}
	}
	
	IEnumerator Spawn(){
		//delay before each spawn
		yield return new WaitForSeconds(spawnDelays[count]);
		
		//generates a random position for spawning, inside a range
		Vector3 pos = new Vector3(Random.Range(-SpawnRange, SpawnRange), transform.position.y, Random.Range(-SpawnRange, SpawnRange));
		print(pos);
		//instantiates the next enemy
		Instantiate(prefabs[count], transform.position + pos, Quaternion.identity);
		count++;
		
		//detects if it's the last enemy. if it's not, calls the coroutine again. if it is, destroys the spawner
		if(count < enemies.Count && count < spawnDelays.Count){
			StartCoroutine(Spawn());
		}	
		else{
			GameObject.Destroy(this.gameObject);
		}
	}
	
	//draws a cube for reference of where the spawn area is in the scene
	void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;	
        Gizmos.DrawCube(transform.position, new Vector3(2*SpawnRange, 1, 2*SpawnRange));
    }
}
