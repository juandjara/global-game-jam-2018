using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 3;
    public float angularSpeed = 100;

    Transform myTrans;
    // Use this for initialization
    void Start () {
        myTrans = this.transform;
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

    /*enum class EVirusStatus{

    }*/
}
