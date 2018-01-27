using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SampleNavScript : MonoBehaviour {

    public GameObject target;
    GameObject t;
    NavMeshAgent agent;

    // Use this for initialization
    void Awake () { CreateTarget(); }
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update () {
        agent.SetDestination(t.transform.position);


    }

    private void OnCollisionEnter (Collision collision) {
        if (collision.gameObject == t) {
            t.transform.position = NewTargetPosition();
        }
    }

    Vector3 NewTargetPosition () {
        float x = Random.Range(-8.14f, 8.07f);
        float z = Random.Range(-4.4f, 3.69f);

        return (new Vector3(x, 0.5f, z));
    }

    void CreateTarget () {
        t = Instantiate(target);
        t.transform.position = NewTargetPosition();
        target = t;
    }

}
