using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

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
    }

    Vector3 NewTargetPosition () {
        float x = Random.Range(-8.14f, 8.07f);
        float z = Random.Range(-4.4f, 3.69f);

        return (new Vector3(x, 0.5f, z));
    }

    int EnemyMesh () {
        int i = state;
        while (i == state) {
            i = Random.Range(0, (tiposVirus.Length-1));
        }
        return i;
    }
}
