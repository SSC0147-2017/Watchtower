using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public List<GameObject> EnemyList = new List<GameObject>();

    public float SpawnDelay;
    public float SpawnRadius;

    int i = 0;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StartSpawn());
        }
	}

    IEnumerator StartSpawn()
    {
        Vector3 pos = transform.position;
        pos += new Vector3(Random.Range(-SpawnRadius, SpawnRadius), 0, Random.Range(-SpawnRadius, SpawnRadius));

        Instantiate(EnemyList[i], pos, Quaternion.identity);

        yield return new WaitForSeconds(SpawnDelay);

        if (i < EnemyList.Count)
        {
            i++;
            StartCoroutine(StartSpawn());
        }
    }
}
