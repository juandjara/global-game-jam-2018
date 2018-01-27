using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour {

    public GameObject player;
    public GameObject enemy;
    public GameObject[] tiposVirus;

    int state;

	// Use this for initialization
	void Start () {
        ToChoose();
        CreatePlayer();
        CreateEnemies();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ToChoose () {
        state = Random.Range(0, tiposVirus.Length);
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
        if(winPlataform == 1) { iInfected = +1; }
        ToChoose();
        CreatePlayer();
        CreateEnemies();

        for (int i = 0; i < iInfected; i++) {
            GameObject o = Instantiate(enemy);
            o.transform.position = NewTargetPosition();
            Instantiate(tiposVirus[state], o.transform);
            o.GetComponent<SampleNavScript>().Infect();
            o.layer = 10;
        }
        


    }
}
