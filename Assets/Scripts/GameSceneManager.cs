using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour {

    public GameObject player;
    public GameObject enemy;
    public GameObject[] tiposVirus;

    int state;


    //public delegate void Pause ();
    public System.Action pause;

    // Use this for initialization
    void Start () {
        ToChoose();
        CreatePlayer();
        CreateEnemies();
        FindObjectOfType<PlayerController>().LoadData();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CallPause () {
        if (pause != null)
            pause();
    }

    void ToChoose () {
        state = Random.Range(0, tiposVirus.Length);
        PlayerPrefs.SetInt("shape", state);
    }

    void CreatePlayer () {
        GameObject p = Instantiate(player);
        p.transform.position = new Vector3(-7.11f, 0.5f, 2.13f);
        Instantiate(tiposVirus[state], p.transform);
    }

    void CreateEnemies () {
        for (int i = 0; i < tiposVirus.Length; i++) {
            GameObject e = Instantiate(enemy);
            e.transform.position = NewTargetPosition();
            Instantiate(tiposVirus[EnemyMesh()], e.transform);
        }
        GameObject o = Instantiate(enemy);
        o.transform.position = NewTargetPosition();
        Instantiate(tiposVirus[state], o.transform);
        o.layer = 9;
    }

    Vector3 NewTargetPosition () {
        float x = Random.Range(-16.44f, 16.08f);
        float z = Random.Range(-9.07f, 8.74f);

        return (new Vector3(x, 0.5f, z));
    }

    int EnemyMesh () {
        int i = state;
        while (i == state) {
            i = Random.Range(0, (tiposVirus.Length));
        }
        return i;
    }

    public void InstantiateInfected(int iInfected, int winPlataform) {
        if (iInfected != 0) {
            if (winPlataform == 1) { iInfected = +1; }
            ToChoose();
            CreatePlayer();
            CreateEnemies();

            for (int i = 0; i < iInfected; i++) {
                GameObject o = Instantiate(enemy);
                o.transform.position = NewTargetPosition();
                Instantiate(tiposVirus[state], o.transform);
                o.GetComponent<SampleNavScript>().Infect();
                o.layer = 10;
                FindObjectOfType<PlayerController>().AddInfected(o);
            }
        }


    }
}
