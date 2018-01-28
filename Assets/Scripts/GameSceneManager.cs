using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour {

    public GameObject player;
    public GameObject enemy;
    public GameObject[] tiposVirus;

    int currentPlayerShapeIndex;


    //public delegate void Pause ();
    public System.Action pause;
    public GameObject UI;

    public Material playerMaterial;

    // Use this for initialization
    void Start () {
        UI = GameObject.Find("Canvas");
        UI.SetActive(false);

        ToChoose();
        CreatePlayer();
        LoadData();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LoadData () {
        InstantiateInfected(ListaInfectados(), winPlataform());
    }
    int winPlataform () {
        int i = PlayerPrefs.GetInt("winPlatform");
        return i;
    }

    int ListaInfectados () {
        int i = PlayerPrefs.GetInt("Lista de infectados");
        //if(PlayerPrefs.GetInt("Lista de infectados") == null) { return 0; }
        return i;
    }
    public void CallPause () {
        if (pause != null)
            pause();
    }

    void ToChoose () {
        currentPlayerShapeIndex = Random.Range(0, tiposVirus.Length);
        PlayerPrefs.SetInt("shape", currentPlayerShapeIndex);
    }

    void CreatePlayer () {
        GameObject p = Instantiate(player);
        p.transform.position = new Vector3(-7.11f, 0.5f, 2.13f);
        Instantiate(tiposVirus[currentPlayerShapeIndex], p.transform);

        Renderer[] matr = GetComponentsInChildren<Renderer>();
        foreach (var item in matr) {
            item.material = playerMaterial;
        }
    }

    void CreateEnemies () {
        for (int i = 0; i < tiposVirus.Length; i++) {
            GameObject e = Instantiate(enemy);
            e.transform.position = NewTargetPosition();
            Instantiate(tiposVirus[EnemyMesh()], e.transform);
        }
        
    }

    void CreateTargetEnemy () {
        GameObject o = Instantiate(enemy);
        o.name = "Target";
        o.transform.position = NewTargetPosition();
        Instantiate(tiposVirus[currentPlayerShapeIndex], o.transform);
        o.layer = 9;
    }

    Vector3 NewTargetPosition () {
        float x = Random.Range(-16.44f, 16.08f);
        float z = Random.Range(-9.07f, 8.74f);

        return (new Vector3(x, 0.5f, z));
    }

    int EnemyMesh () {
        int i = currentPlayerShapeIndex;
        while (i == currentPlayerShapeIndex) {
            i = Random.Range(0, (tiposVirus.Length));
        }
        return i;
    }

    public void InstantiateInfected (int iInfected, int winPlataform) {
        Debug.Log(iInfected + "" + winPlataform);
        
        if (winPlataform == 1) { iInfected += 1;}
        

        CreateEnemies();
        CreateTargetEnemy();

        Debug.Log(iInfected + "fdsaf" + winPlataform);
        for (int i = 0; i < iInfected; i++) {
            GameObject o = Instantiate(enemy);
            o.name = "Infected";
            Instantiate(tiposVirus[currentPlayerShapeIndex], o.transform);
            o.transform.position = NewTargetPosition();
            o.GetComponent<SampleNavScript>().Infect();
            FindObjectOfType<PlayerController>().AddInfected(o);
        }
        


    }
}
