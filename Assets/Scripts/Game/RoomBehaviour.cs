using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour {

    public WaveSpawner[] SpawnerList;
    int SpawnCount = 0;

    public GameObject FlagPrefab;
    bool FlagSpawned = false;

	// Use this for initialization
	void Start () {
        SpawnerList = gameObject.GetComponents<WaveSpawner>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && SpawnCount < SpawnerList.Length)
        {
            StartCoroutine(SpawnerList[SpawnCount].Spawn());
            SpawnCount++;
        }

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
        }
        
    }

    void SpawnFlag()
    {
        Instantiate(FlagPrefab, transform.position + new Vector3(0, 10, 0), Quaternion.identity);
    }
}
