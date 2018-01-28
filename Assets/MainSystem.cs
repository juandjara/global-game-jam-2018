using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSystem : MonoBehaviour {

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void Init () {
        SceneManager.LoadScene("1");
    }
    public void Exit () {
        Application.Quit();
    }


    // Enlaces

    public void Jorge () {
        Application.OpenURL("https://twitter.com/RubioDeLimon");

    }

    public void Juan () {
        Application.OpenURL("https://twitter.com/fukeneltuerto");
    }

    public void Carlos () {
        Application.OpenURL("https://www.instagram.com/carlosrwar/");
    }

    public void Dani () {
        Application.OpenURL("mailto:dani.caravaca93@gmail.com");
    }
}

