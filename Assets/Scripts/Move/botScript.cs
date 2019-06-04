using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class botScript : MonoBehaviour {

	private canonScript canonScript;
	private NavMeshAgent navMeshAgent;
    private List<GameObject> tanks;
    private GameObject target;

	// Use this for initialization
	void Start () {
		navMeshAgent = GetComponent<NavMeshAgent>();
        canonScript = transform.Find("canon").GetComponent<canonScript>();
        tankScript [] tankScript = FindObjectsOfType<tankScript>();
        tanks = new List<GameObject>();
        foreach (tankScript tmp in tankScript) {
            tanks.Add(tmp.gameObject);
        }	
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<tankScript>().isAlive()) {
            float distance = -1;
            foreach (GameObject tank in tanks)
            {
                if (tank) {
                    Debug.Log("name = " + name + " ; cible = " + tank.name);
                    Debug.Log("distance = " + Vector3.Distance(transform.position, tank.transform.position));
                    if (tank.transform.position != transform.position) {
                        if (distance < 0 || Vector3.Distance(transform.position, tank.transform.position) < distance) {
                            distance = Vector3.Distance(transform.position, tank.transform.position);
                            target = tank;
                        }
                    }
                }
            }
            if (target) {
                if (Vector3.Distance(transform.position, target.transform.position) > 10f) {
                    if (navMeshAgent.isOnNavMesh) {
                        navMeshAgent.destination = target.transform.position;
                    }
                } else {
                    navMeshAgent.destination = transform.position;
                }
                Vector3 targetDir = target.transform.position - canonScript.GetComponent<Transform>().position;
                Vector3 newDir = Vector3.RotateTowards(canonScript.GetComponent<Transform>().forward, targetDir, 1 * Time.deltaTime, 0.0f);
                Quaternion newRotate = Quaternion.LookRotation(newDir);
                newRotate.x = 0;
                newRotate.z = 0;
                canonScript.GetComponent<Transform>().rotation = newRotate;
            }
        }
	}
}
