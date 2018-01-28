using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {


    public float speed = 3;
    public float angularSpeed = 100;

    Transform myTrans;

    bool handleControl = false;
    List<GameObject> infected = new List<GameObject>();

    public Material[] MATs;

    GameSceneManager gameScene;
    

    public Texture texture;


    // Use this for initialization
    void Awake () {
        gameScene = FindObjectOfType<GameSceneManager>();
    }

    void Start () {

        myTrans = this.transform;
        gameScene.pause += PauseControl;

        

        Renderer[] matr = GetComponentsInChildren<Renderer>();
        foreach (var item in matr) {
            item.material.mainTexture = texture;
        }
       
    }
    void PauseControl () {
        handleControl = true;
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
            Debug.Log("colision");
            Dead();
        }
        if (col.gameObject.layer == 9) {
            SaveData();
            infected.Add(col.gameObject);

            //
            // centrar camara
            string nombreColor = col.gameObject.GetComponentInChildren<Renderer>().material.name;

            if(nombreColor == "Blue") {
                PlayerPrefs.SetInt("enemyColor", 1);
            }else if(nombreColor == "Green") {
                PlayerPrefs.SetInt("enemyColor", 2);
            } else if (nombreColor == "Orange") {
                PlayerPrefs.SetInt("enemyColor", 3);
            } else if (nombreColor == "Red") {
                PlayerPrefs.SetInt("enemyColor", 4);
            }

            FindObjectOfType<CameraController>().target = col.transform;
            Invoke("ChangeScene", 0.3f);
            /// Cambiar de lugar
            


        }
    }

    void ChangeScene () {
        
        
        SceneManager.LoadScene(Random.Range(2,4));
        //col.gameObject.GetComponent<SampleNavScript>().Infect();
    }

    void Dead () {
        if (infected.Count == 0) {
            GameOver();
            PlayerPrefs.SetInt("Lista de infectados", 0);
        } else {
            ChangeBode();
        }
    }

    

    void ChangeBode () {
        GetComponent<Collider>().enabled = false;
        handleControl = true;
        GameObject newPlayer = infected[Random.Range(0, infected.Count)];
        Destroy(newPlayer.GetComponent<SampleNavScript>());
        Destroy(newPlayer.GetComponent<UnityEngine.AI.NavMeshAgent>());
        newPlayer.AddComponent<PlayerController>();
        newPlayer.layer = 0;
        newPlayer.name = "Player(Clone)";
        newPlayer.transform.GetChild(0).GetComponent<Animator>().SetTrigger("alive");
        newPlayer.AddComponent<Rigidbody>();
        newPlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
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

    

    public void AddInfected (GameObject newInfected) {
        infected.Add(newInfected);
    }

    void GameOver () {
        gameScene.CallPause();
        gameScene.UI.SetActive(true);

        PlayerPrefs.SetInt("Lista de infectados", 0);
        PlayerPrefs.SetInt("winPlatform", 0);
    }


}
