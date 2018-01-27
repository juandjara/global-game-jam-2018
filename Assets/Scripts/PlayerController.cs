using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 3;
    public float angularSpeed = 100;

    Transform myTrans;

    bool handleControl = false;
    List<GameObject> infected = new List<GameObject>();

    public Material[] MATs;

    public GameSceneManager gameScene;
    // Use this for initialization
    void Start () {
        myTrans = this.transform;
        ChangeMAT();
    }

    // Update is called once per frame
    void Update () {
        if (!handleControl) {
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
    }

    void OnCollisionEnter (Collision col) {
        if (col.gameObject.layer == 8) {
            Dead();
        }
        if (col.gameObject.layer == 9) {
            SaveData();
            infected.Add(col.gameObject);
            col.gameObject.GetComponent<SampleNavScript>().Infect();


        }
    }

    void Dead () {
        if (infected.Count == 0) {
            // TODO GameOver scene
            PlayerPrefs.SetInt("Lista de infectados", 0);
        } else {
            ChangeBode();
        }
    }

    void ChangeMAT () {
        if (MATs[0] != null) {
            int m = Random.Range(0, MATs.Length);

            Renderer[] matr = GetComponentsInChildren<Renderer>();
            foreach (var item in matr) {
                item.material = MATs[m];
            }
        }
    }

    void ChangeBode () {
        handleControl = true;
        GameObject newPlayer = infected[Random.Range(0, infected.Count)];
        Destroy(newPlayer.GetComponent<SampleNavScript>());
        Destroy(newPlayer.GetComponent<UnityEngine.AI.NavMeshAgent>());
        newPlayer.AddComponent<PlayerController>();
        newPlayer.layer = 0;
        newPlayer.name = "Player(Clone)";
        newPlayer.transform.GetChild(0).GetComponent<Animator>().SetTrigger("alive");
        Instantiate(transform.GetChild(0).gameObject, newPlayer.transform); // TODO cambiar el orden de los hijos
        infected.Remove(newPlayer);
        Animator anim = transform.GetChild(1).GetComponent<Animator>();
        anim.SetTrigger("dead");
        SaveData();
        Invoke("DeleteThisPlayer", 3);
    }

    void DeleteThisPlayer () {
        Destroy(this.gameObject);
    }

    void SaveData () {
        PlayerPrefs.SetInt("Lista de infectados", infected.Count);
    }

    public void LoadData () {
        int i = PlayerPrefs.GetInt("Lista de infectados");
        int winPlataform = PlayerPrefs.GetInt("winPlataform");
        gameScene.InstantiateInfected(i, winPlataform);
    }

    public void AddInfected (GameObject newInfected) {
        infected.Add(newInfected);
    }
}
