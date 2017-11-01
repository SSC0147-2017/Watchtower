using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class teste : MonoBehaviour {

    public GameObject target1;
    public GameObject target2;
    int currTgt = 1;
    public NavMeshAgent navAgent;
    bool x = true;
	
	// Update is called once per frame
	void Update () {
        print(x + " " + currTgt);

        if (x)
        {
            if(currTgt == 1)
            navAgent.SetDestination(target1.transform.position);

            if(currTgt == 2)
            navAgent.SetDestination(target2.transform.position);

        }
        if (Input.GetKeyDown(KeyCode.Space) && x)
        {
            x = false;
            navAgent.isStopped = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !x)
        {
            x = true;
            navAgent.isStopped = false;
        }

        if (Vector3.Distance(target2.transform.position, transform.position) < Vector3.Distance(target1.transform.position, transform.position))
        {
            currTgt = 2;
        }
        else if (Vector3.Distance(target2.transform.position, transform.position) > Vector3.Distance(target1.transform.position, transform.position))
        {
            currTgt = 1;
        }
    }
}
