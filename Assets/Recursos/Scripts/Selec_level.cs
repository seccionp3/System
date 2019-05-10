using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Selec_level : MonoBehaviour {

    // Use this for initialization

    public static int nivel;
    public static string nombre_nivel;

    void Start () {
        Menu_Aprendizaje_1.cod_user = 0;

    }

    // Update is called once per frame
    void Update () {

    }

    public void nivel_a () {
        nombre_nivel = "BASICO A";
        nivel = 1;
        SceneManager.LoadScene (2);
    }
    public void nivel_b () {
        nombre_nivel = "BASICO B";
        nivel = 2;
        SceneManager.LoadScene (2);
    }
    public void nivel_c () {
        nombre_nivel = "INTERMEDIO";
        nivel = 3;
        SceneManager.LoadScene (2);
    }
    public void nivel_d () {
        nombre_nivel = "AVANZADO";
        nivel = 4;
        SceneManager.LoadScene (2);
    }

    public void regresar () {
        SceneManager.LoadScene (0);
    }
}