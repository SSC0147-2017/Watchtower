using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour {
	
	public BattleMusicManager MusicRef;

    WaveSpawner[] SpawnerList;
    int SpawnCount = 0;
    public float WaveDelay;

	public float FlagRadius;
    public GameObject FlagPrefab;

	[Header("Item Spawn Variables")]
	public GameObject BreadPickupPrefab;
	public GameObject BombPickupPrefab;
	[Tooltip("Maximum Range for spawning item pickups")]
	public float itemSpawnRange = 5;

	[Tooltip("% of bombSpawn chance on flag spawn")]
	[Range(0,100)]
	public int bombSpawnChance = 25;

	bool FlagSpawned = false;
	bool StartedWaves = false;

	// Use this for initialization
	void Start () {
        SpawnerList = gameObject.GetComponents<WaveSpawner>();
	}
	
	// Update is called once per frame
	void Update () {

	    int aux = 0;
        for(int i = 0; i< SpawnerList.Length; i++)
        {
            if(SpawnerList[i].allDead == true)
            {
                aux++;
            }
        }

        if(aux == SpawnerList.Length && FlagSpawned == false)
        {
            SpawnFlag();
            FlagSpawned = true;
			GameObject.Destroy (GetComponent<BoxCollider>());
			for(int i = 0; i <SpawnerList.Length; i++){
				GameObject.Destroy(SpawnerList[i]);
			}
			MusicRef.StopBattleMusic();
			StartCoroutine(DestroyDelay(1f));
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (StartedWaves == false && other.tag == "Player" && other.GetType() != typeof(SphereCollider))
        {
            StartCoroutine(Wave());
            StartedWaves = true;
			MusicRef.PlayBattleMusic();
        }
    }

    IEnumerator Wave()
    {
        
		StartCoroutine(SpawnerList[SpawnCount].Spawn());
		SpawnCount++;

		yield return new WaitForSeconds(WaveDelay);
		
		if (SpawnCount < SpawnerList.Length)
        {
            StartCoroutine(Wave());
        }
    }

    void SpawnFlag()
    {
		GameObject obj = Instantiate(FlagPrefab,transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform.parent);
        obj.transform.Rotate(0, 180, 0);
		obj.transform.GetChild(0).GetComponent<Light>().range = FlagRadius;
		SoundManager.SM.PlayFlag();

		SpawnItems();
    }

	/**
	 * Gets number of players. Spawn 1 bread for each player
	 * Has a chance to spawn a bomb for each player
	 */
	void SpawnItems(){
		int numPlayers = GameManager.GM.getNumPlayers ();

		//Spawn Bread
		for (int i = 0; i < numPlayers; i++) {
			Vector3 pos = transform.position + new Vector3 (Random.Range (-itemSpawnRange/2, itemSpawnRange/2), transform.position.y, Random.Range (-itemSpawnRange/2, itemSpawnRange/2));
			GameObject obj = Instantiate (BreadPickupPrefab,  pos, Quaternion.identity, transform.parent);
			obj.transform.Rotate (new Vector3 (-90, 0, 0), Space.World);
		}

		//Spawn bombs... maybe
		for (int i = 0; i < numPlayers; i++) {
			int die = Random.Range (1, 100);
			//print (die + "/" + bombSpawnChance);
			if (die <= bombSpawnChance) {
				Vector3 pos = transform.position + new Vector3 (Random.Range (-itemSpawnRange/2, itemSpawnRange/2), transform.position.y, Random.Range (-itemSpawnRange/2, itemSpawnRange/2));
				GameObject obj = Instantiate (BombPickupPrefab,  pos, Quaternion.identity, transform.parent);
				obj.transform.Rotate (new Vector3 (-90, 0, 0), Space.World);
			}
		}
	}

	
	void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;	
        Gizmos.DrawCube(transform.position, new Vector3(1, 5, 1));
    }
	
	IEnumerator DestroyDelay(float delay){
		yield return new WaitForSeconds(delay);
		GameObject.Destroy(this);
	}
}
