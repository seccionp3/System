using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour {

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void ensenianza () {
        SceneManager.LoadScene (1);
    }
    public void aprendizaje () {
        SceneManager.LoadScene (8);
    }
    public void jugabilidad () {
        SceneManager.LoadScene (14);
    }

}