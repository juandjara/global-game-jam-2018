using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 3;
    public float angularSpeed = 100;

    Transform myTrans;


    List<GameObject> infected = new List<GameObject>();

    public Material[] MATs;
    // Use this for initialization
    void Start () {
        myTrans = this.transform;
        ChangeMAT();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.W)) {
            myTrans.Translate(Vector3.forward * speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.S)) {
            myTrans.Translate(Vector3.back * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D)) {
            myTrans.Rotate(Vector3.up * angularSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.A)) {
            myTrans.Rotate(Vector3.down * angularSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter (Collision col) {
        if (col.gameObject.layer == 8) {
            Dead();
        }
        if (col.gameObject.layer == 9) {
            infected.Add(col.gameObject);
            col.gameObject.GetComponent<SampleNavScript>().Infect();


        }
    }

    private void Dead () {
        if (infected.Count == 0) {
            // TODO GameOver scene
        } else {
            ChangeBode();
        }
    }

    void ChangeMAT () {
        int m = Random.Range(0, MATs.Length);

        Renderer[] matr = GetComponentsInChildren<Renderer>();
        foreach (var item in matr) {
            item.material = MATs[m];
        }
    }

    void ChangeBode () {
        GameObject newPlayer = infected[Random.Range(0, infected.Count)];
        Destroy(newPlayer.GetComponent<SampleNavScript>());
        newPlayer.AddComponent<PlayerController>();
        newPlayer.layer = 0;
        newPlayer.name = "Player(Clone)";
        newPlayer.transform.GetChild(0).GetComponent<Animator>().SetTrigger("alive");
        infected.Remove(newPlayer);
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("dead");
        Invoke("DeleteThisPlayer", 3.1f);
    }

    void DeleteThisPlayer () {
        Destroy(this);
    }
}
