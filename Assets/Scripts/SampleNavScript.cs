using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SampleNavScript : MonoBehaviour {

    public GameObject target;
    GameObject t;
    NavMeshAgent agent;
    public Material[] MATs;
    Animator anim;

    bool infectedStatus = false;

    // Use this for initialization
    void Awake () { CreateTarget(); }
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        ChangeMAT();
        SetAnim();
    }
    
    // Update is called once per frame
    void Update () {
        if (!infectedStatus) {
            agent.SetDestination(t.transform.position);
        }

    }

    void OnCollisionStay (Collision collision) {
        if (collision.gameObject == t) {
            t.transform.position = NewTargetPosition();
        }
    }

    Vector3 NewTargetPosition () {
        float x = Random.Range(-16.44f, 16.08f);
        float z = Random.Range(-9.07f, 8.74f);

        return (new Vector3(x, 0.5f, z));
    }

    void CreateTarget () {
        t = Instantiate(target);
        t.transform.position = NewTargetPosition();
        target = t;
    }

    void ChangeMAT () {
        int m = Random.Range(0, MATs.Length);

        Renderer[] matr = GetComponentsInChildren<Renderer>();
        foreach (var item in matr) {
            item.material = MATs[m];
        }
    }

    public void Infect () {
        infectedStatus = true;
        anim.SetTrigger("dead");
        gameObject.layer = 10;
    }


    void SetAnim () {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

}
