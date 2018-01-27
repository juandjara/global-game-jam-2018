using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 3;
    public float angularSpeed = 100;

    Transform myTrans;

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
    }

    private void Dead () {
        // TODO GameOver scene
    }

    void ChangeMAT () {
        int m = Random.Range(0, MATs.Length);

        Renderer[] matr = GetComponentsInChildren<Renderer>();
        foreach (var item in matr) {
            item.material = MATs[m];
        }
    }
}
