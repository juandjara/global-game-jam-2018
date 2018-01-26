using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SampleNavScript : MonoBehaviour {

    public Transform target;
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(target.position);

        
	}

    private void OnCollisionEnter (Collision collision) {
        if( collision.gameObject == target.gameObject) {
            target.position = newTargetPosition();
        }
    }

    Vector3 newTargetPosition () {

        float x = Random.Range(-12.68f, 33.48f);
        float z = Random.Range(1.74f, 24.84f);

        return (new Vector3(x, 0, z));
    }

}
