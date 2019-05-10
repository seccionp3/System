using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SELEC_LEVEL_JUGABILIDAD : MonoBehaviour {

    // Use this for initialization
    public Text fecha;
    public Text titulo;

    //Nivel Basico A, Basico B, Intermedio, Avanzado
    public GameObject btnA, btnB, btnC, btnD;
    void Start () {
        fecha.text = LOGIN_JUGABILIDAD.fecha;
        titulo.text = "Bienvenido " + LOGIN_JUGABILIDAD.nombre_usuario_log + " a la sección de jugabilidad";
        if (LOGIN_JUGABILIDAD.a == false) {
            btnA.SetActive (false);
        }
        if (LOGIN_JUGABILIDAD.b == false) {
            btnB.SetActive (false);
        }
        if (LOGIN_JUGABILIDAD.c == false) {
            btnC.SetActive (false);
        }
        if (LOGIN_JUGABILIDAD.d == false) {
            btnD.SetActive (false);
        }

    }

    // Update is called once per frame
    void Update () {

    }
    public void regresarMenu () {
        LOGIN_JUGABILIDAD.a = false;
        LOGIN_JUGABILIDAD.b = false;
        LOGIN_JUGABILIDAD.c = false;
        LOGIN_JUGABILIDAD.d = false;
        
        SceneManager.LoadScene (0);
    }

    public void cargarBasicoA(){
        SceneManager.LoadScene (16);
    }
    public void cargarBasicoB(){
        SceneManager.LoadScene (17);
    }
    public void cargarIntermedio(){
         SceneManager.LoadScene (18);
    }
    public void cargarAvanzado(){
         SceneManager.LoadScene (19);
    }
}