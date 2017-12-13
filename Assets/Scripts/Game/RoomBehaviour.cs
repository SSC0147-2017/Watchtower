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
        if (StartedWaves == false && other.tag == "Player")
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
		GameObject obj = Instantiate(FlagPrefab,transform.position + new Vector3(0, 2, 0), Quaternion.identity, transform.parent);
        obj.transform.Rotate(0, 180, 0);
		obj.transform.GetChild(0).GetComponent<Light>().range = FlagRadius;
		SoundManager.SM.PlayFlag();
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
