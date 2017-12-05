using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

	public enum enemiesType{bugfolk, depths, howler, waspinoid};

	[Space(20)]
	//reference to each enemy prefab
	public GameObject Enemy1;
	public GameObject Enemy2;
	//public GameObject Enemy3;
	//public GameObject Enemy4;
	
	//auxiliar count and bool to spawn only once.
	int count = 0;
	
	[Header ("1 - Bugfolk")]
	[Header ("2 - Spawn of the Depths")]
	//[Header ("3 - Howler of the Spawn")]
	//[Header ("4 - Waspinoid")]
	//list of all enemies that will spawn, and the delay before each spawn
	public List<enemiesType> enemies = new List<enemiesType>();
    
	List <GameObject> prefabs = new List<GameObject>();
	
	[Space(20)]
	public List<float> spawnDelays = new List<float>();
	
	[Space(20)]
    public Vector3 SpawnPosition;
    public float SpawnRange;

    //tracks if there are any enemies alive
    List<GameObject> refs = new List<GameObject>();
    [HideInInspector]
	public bool allDead = false;

    void Start(){
		for(int i = 0; i < enemies.Count; i++){
			if(enemies[i] == enemiesType.bugfolk){
				prefabs.Add(Enemy1);
			}
			else if(enemies[i] == enemiesType.depths){
				prefabs.Add(Enemy2);
			}
			/*else if(enemies[i] == enemiesType.howler){
				prefabs.Add(Enemy3);
			}
			else if(enemies[i] == enemiesType.waspinoid){
				prefabs.Add(Enemy4);
			}*/
		}
	}

    private void Update()
    {
        int refCount = 0;
        for(int i = 0; i < refs.Count; i++)
        {
            if(refs[i] == null)
            {
                refCount++;
            }
        }
        if(refCount > 0 && refCount == refs.Count)
        {
            allDead = true;
        }
    }
	
	public IEnumerator Spawn(){
		//delay before each spawn
		yield return new WaitForSeconds(spawnDelays[count]);

        //generates a random position for spawning, inside a range
        Vector3 pos = SpawnPosition + new Vector3(Random.Range(-SpawnRange, SpawnRange), transform.position.y, Random.Range(-SpawnRange, SpawnRange));

        //instantiates the next enemy
		GameObject obj = Instantiate(prefabs[count], pos, Quaternion.identity);

        refs.Add(obj);
		count++;

        //detects if it's the last enemy. if it's not, calls the coroutine again. if it is, destroys the spawner
        if (count < enemies.Count && count < spawnDelays.Count)
        {
            StartCoroutine(Spawn());
        }
	}
	
	//draws a cube for reference of where the spawn area is in the scene
	void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;	
        Gizmos.DrawCube(SpawnPosition, new Vector3(2*SpawnRange, 1, 2*SpawnRange));
    }
}
