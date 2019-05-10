using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BASICOB_APRENDIZAJE : MonoBehaviour {
    public RectTransform panelA, panelB;
    private Image imgA, imgB;
    public Button btnPrincipal;

    public AudioSource audioUbicacion;

    private int contador = 0, contador_r;
    private int yi, yf;

    private Transform pos_btn_A, pos_btn_B;

    private AudioClip a, b;

    public GameObject btnMsg;

    private Texture2D tex;
    //VARIABLES DE CONFIGURACION INICIAL
    private int r = Menu_Aprendizaje_1.num_repeticiones;
    private int velocidad = Menu_Aprendizaje_1.velocidad_data;
    int contador_repeticiones = 2;

    private int codigo_ubicacion_2;

    private int numero_inicial;
    // Use this for initialization
    void Start () {
        btnMsg.SetActive (false);
        datosPersonaje (Menu_Aprendizaje_1.cod_personaje_1);
        imgA = panelA.GetComponent<Image> ();
        imgB = panelB.GetComponent<Image> ();

        contador_r = 1;
        numero_inicial = UnityEngine.Random.Range (3, 5) ;
        datosPosicion (numero_inicial);

    }

    // Update is called once per frame
    void Update () {

        if (contador == 0) {
            if (pos_btn_A.position.y < ((Screen.height / 2) + yf)) {
                pos_btn_A.position += new Vector3 (0, Time.deltaTime * velocidad, 0f);
            }
            if (pos_btn_A.position.y >= ((Screen.height / 2) + yf)) {
                if (contador_r < r) {
                    contador_r++;
                    Destroy (pos_btn_A.gameObject);
                    InstanciarIzq ();
                    Debug.Log (contador_r);
                }
            }
        }

        if (contador == 1) {
            if (pos_btn_B.position.y > ((Screen.height / 2)) + yf) {
                pos_btn_B.position += new Vector3 (0, -Time.deltaTime * velocidad, 0);
            }
            if (pos_btn_B.position.y <= ((Screen.height / 2) + yf)) {
                if (contador_r < r) {
                    contador_r++;
                    Destroy (pos_btn_B.gameObject, 1f);
                    InstanciarDer ();
                }
            }
        }

    }

    public void regresarEscena () {
        SceneManager.LoadScene (9);
    }
    public void InstanciarIzq () {
        //POSICIONES INICIALES
        btnPrincipal.GetComponentInChildren<Text>().text = "ARRIBA";
        btnPrincipal.GetComponentInChildren<Text>().alignment = TextAnchor.UpperCenter;
        btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
        //btnIzq = Instantiate(btnPrincipal.gameObject,new Vector3(-100,(Screen.height/2),0f),transform.rotation);
        GameObject btnIzq = Instantiate (btnPrincipal.gameObject, new Vector3 ((Screen.width / 2),yi, 0), transform.rotation);
        Debug.Log ("contador_r = " + contador_r + " y r = " + r);
        btnIzq.transform.SetParent (this.transform);
        pos_btn_A = btnIzq.GetComponent<Transform> ();
        if (contador_r == r) {
            pos_btn_A.GetComponent<Button> ().onClick.AddListener (contar);
        }
        StartCoroutine (playsound ());
        //audioUbicacion.Play();
        //Debug.Log("Xa = "+ pos_btn_A.position.x + "Ya = " + pos_btn_A.position.y);

    }

    public void InstanciarDer () {
        btnPrincipal.GetComponentInChildren<Text>().text = "ABAJO";
        btnPrincipal.GetComponentInChildren<Text>().alignment = TextAnchor.LowerCenter;
        btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
        GameObject btnDer = Instantiate (btnPrincipal.gameObject, new Vector3 ((Screen.width / 2), Screen.height + yi, 0), transform.rotation);
        btnDer.transform.SetParent (this.transform);
        pos_btn_B = btnDer.GetComponent<Transform> ();
        if (contador_r == r) {
            pos_btn_B.GetComponent<Button> ().onClick.AddListener (contar);
        }
        StartCoroutine (playsound ());
    }

    IEnumerator playsound () {
        Debug.Log ("reproducuiendo......");
        audioUbicacion.clip = a;
        audioUbicacion.Play ();
        yield return new WaitForSeconds (audioUbicacion.clip.length);
        audioUbicacion.clip = b;
        audioUbicacion.Play ();
    }

    void contar () {
        contador_r = 1;
        contador_repeticiones--;
        if (contador == 0 && contador_repeticiones != 0) {
            Destroy (pos_btn_A.gameObject);
            imgA.color = Color.white;
            datosPersonaje (Menu_Aprendizaje_1.cod_personaje_2);
            if(numero_inicial == 3){
                datosPosicion(4);
            }else
            {
                datosPosicion(3);
            }
            //datosPosicion (UnityEngine.Random.Range (3, 5));
        }
        if (contador == 1 && contador_repeticiones != 0) {

            Debug.Log (contador_repeticiones);

            Destroy (pos_btn_B.gameObject);
            imgB.color = Color.white;
            datosPersonaje (Menu_Aprendizaje_1.cod_personaje_2);
            if(numero_inicial == 3){
                datosPosicion(4);
            }else
            {
                datosPosicion(3);
            }
            //datosPosicion (UnityEngine.Random.Range (3, 5));
        }
        if (contador_repeticiones == 0) {
            saveDetalleAprendizaje (Menu_Aprendizaje_1.cod_aprendizaje, codigo_ubicacion_2, Menu_Aprendizaje_1.cod_color_2, Menu_Aprendizaje_1.cod_personaje_2);
            if (contador == 0) {
                contador = 2;
                Destroy (pos_btn_A.gameObject);
                imgA.color = Color.white;
            }
            if (contador == 1) {
                contador = 2;
                Destroy (pos_btn_B.gameObject);
                imgB.color = Color.white;
            }
            btnMsg.SetActive (true);
            //Destroy(audioUbicacion);	
        }
    }
    void colors (string nombre_color, Image panel) {
        rgb data = new rgb ();
        string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/dbdata.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection (conn);
        dbconn.Open ();
        IDbCommand dbcmd = dbconn.CreateCommand ();
        string sqlQuery = "Select r_color,g_color,b_color from color where nombre_color = '" + nombre_color + "'";
        Debug.Log (sqlQuery);
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader ();
        while (reader.Read ()) {
            //int id = reader.GetInt32(0);
            int r = reader.GetInt32 (0);
            int g = reader.GetInt32 (1);
            int b = reader.GetInt32 (2);
            data.r = r;
            data.g = g;
            data.b = b;
        }
        Debug.Log ("Color = (" + data.r + "," + data.g + "," + data.b + ")");
        panel.color = new Color (data.r, data.g, data.b);

        reader.Close ();
        reader = null;
        dbcmd.Dispose ();
        dbcmd = null;
        dbconn.Close ();
    }

    void datosPosicion (int id) {
        Debug.Log ("Numero aleatorio = " + id);
        byte[] son = new byte[0];
        string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/dbdata.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection (conn);
        dbconn.Open ();
        IDbCommand dbcmd = dbconn.CreateCommand ();
        string sqlQuery = "Select * from ubicacion where id = " + id;
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader ();
        while (reader.Read ()) {
            son = (byte[]) reader["audio_ubicacion"];
            Debug.Log (son.Length);
            yi = reader.GetInt32 (6);
            yf = reader.GetInt32 (7);
        }

        WAV sonido = new WAV (son);
        b = AudioClip.Create ("testSound", sonido.SampleCount, 1, sonido.Frequency, false, false);
        b.SetData (sonido.LeftChannel, 0);
        //audioUbicacion.clip = audioClip;
        //audioA.Play();
        reader.Close ();
        reader = null;
        dbcmd.Dispose ();
        dbcmd = null;
        dbconn.Close ();

        if (id == 3) {
            if (contador_repeticiones == 2) {
                Debug.Log ("Solo la primera vez a");
                saveDetalleAprendizaje (Menu_Aprendizaje_1.cod_aprendizaje, id, Menu_Aprendizaje_1.cod_color_1, Menu_Aprendizaje_1.cod_personaje_1);
                colors (Menu_Aprendizaje_1.colora, imgA);
            } else {
                colors (Menu_Aprendizaje_1.colorb, imgA);
            }
            contador = 0;
            InstanciarIzq ();
        } else {
            //solo la primera vez.
            if (contador_repeticiones == 2) {
                Debug.Log ("Solo la primera vez b");
                saveDetalleAprendizaje (Menu_Aprendizaje_1.cod_aprendizaje, id, Menu_Aprendizaje_1.cod_color_1, Menu_Aprendizaje_1.cod_personaje_1);
                colors (Menu_Aprendizaje_1.colora, imgB);
            } else {
                colors (Menu_Aprendizaje_1.colorb, imgB);
            }
            contador = 1;
            InstanciarDer ();
        }
        codigo_ubicacion_2 = id;

    }

    void datosPersonaje (int id) {
        tex = new Texture2D (256, 256);
        byte[] audio_personaje = new byte[0];
        byte[] imagen_personaje = new byte[0];
        string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/dbdata.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection (conn);
        dbconn.Open ();
        IDbCommand dbcmd = dbconn.CreateCommand ();
        string sqlQuery = "Select * from personaje where id_personaje = " + id;
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader ();
        while (reader.Read ()) {
            audio_personaje = (byte[]) reader["audio_personaje"];
            imagen_personaje = (byte[]) reader["imagen_personaje"];
        }
        WAV sonido = new WAV (audio_personaje);
        a = AudioClip.Create ("sonidopersonaje", sonido.SampleCount, 1, sonido.Frequency, false, false);
        a.SetData (sonido.LeftChannel, 0);
        //Debug.Log(imagen_personaje);
        tex.LoadImage (imagen_personaje);
        //audioUbicacion.clip = a;
        //audioA.Play();

        reader.Close ();
        reader = null;
        dbcmd.Dispose ();
        dbcmd = null;
        dbconn.Close ();
    }

    public void saveDetalleAprendizaje (int codigo_aprendizaje, int codigo_ubicacion, int codigo_color, int codigo_personaje) {
        string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/dbdata.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection (conn);
        dbconn.Open ();
        IDbCommand dbcmd = dbconn.CreateCommand ();
        string sqlQuery = "INSERT INTO detalle_aprendizaje" +
            " (num_repeticiones, velocidad_detalle_apre, id_aprendizaje,id_ubicacion,id_color,id_personaje) Values ('" +
            r + "','" + velocidad + "','" + codigo_aprendizaje + "','" + codigo_ubicacion + "','" + codigo_color + "','" + codigo_personaje + "')";
        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteReader ();
        Debug.Log ("Datos Guardados Corectamente!..");
        dbcmd.Dispose ();
        dbcmd = null;
        dbconn.Close ();
        dbconn = null;
    }
}