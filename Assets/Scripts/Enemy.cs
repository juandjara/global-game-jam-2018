using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    Transform myTrans;
    public float speed;
    public float angularSpeed;


    // Use this for initialization
    void Start () {
        myTrans = this.transform;
    }

    // Update is called once per frame
    void Update () {



        Move();
	}

    void Move () {
        myTrans.Translate(Vector3.forward * speed * Time.deltaTime);
        myTrans.Rotate(Vector3.up * angularSpeed * Time.deltaTime);
    }
}
