using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_aprendizaje_2 : MonoBehaviour {

    // Use this for initialization
    public Text lblUser;
    public Text lblFecha;

    void Start () {

        lblUser.text = "Bienvenido " + Menu_Aprendizaje_1.nombre_user + " a la fase de aprendizaje";
        lblFecha.text = "Fecha: " + Menu_Aprendizaje_1.fecha;

    }

    // Update is called once per frame
    void Update () {

    }

    public void nivel_a () {
        SceneManager.LoadScene (10);
    }
    public void nivel_b () {
        SceneManager.LoadScene (11);
    }
    public void nivel_c () {
        SceneManager.LoadScene (12);
    }
    public void nivel_d () {
        SceneManager.LoadScene (13);
    }

    public void regresar () {
        SceneManager.LoadScene (8);
    }

    public void regresarMenu () {
        SceneManager.LoadScene (0);
    }

}