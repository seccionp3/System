using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class INTERMEDIO_APRENDIZAJE : MonoBehaviour {

    public RectTransform panelA, panelB, panelC, panelD;
    private Image imgA, imgB, imgC, imgD;
    public Button btnPrincipal;

    public AudioSource audioUbicacion;

    private int contador = 0, contador_r;
    private int xi, xf, yi, yf;

    private Transform pos_btn_A, pos_btn_B, pos_btn_C, pos_btn_D;

    private AudioClip a, b;

    public GameObject btnMsg;

    private Texture2D tex;
    //VARIABLES DE CONFIGURACION INICIAL
    private int r = Menu_Aprendizaje_1.num_repeticiones;
    private int velocidad = Menu_Aprendizaje_1.velocidad_data;
    int contador_repeticiones = 2;
    private int codigo_ubicacion_2;

    int numero_inicial = 6;
    int numero_final = 7;
    // Use this for initialization
    void Start () {
        Debug.Log("A = "+ numero_inicial + "B = " + numero_final);
        btnMsg.SetActive (false);
        datosPersonaje (Menu_Aprendizaje_1.cod_personaje_1);
        imgA = panelA.GetComponent<Image> ();
        imgB = panelB.GetComponent<Image> ();
        imgC = panelC.GetComponent<Image> ();
        imgD = panelD.GetComponent<Image> ();

        contador_r = 1;
        numero_inicial = UnityEngine.Random.Range (5, 9);
        datosPosicion (numero_inicial);
    }

    void numerosAleatorios(){
    }

    // Update is called once per frame
    void Update () {

        if (contador == 0) {
            if (pos_btn_A.position.x > ((Screen.width / 2) - xf)) {
                pos_btn_A.position += new Vector3 (-Time.deltaTime * velocidad, 0f, 0f);
            }
            if (pos_btn_A.position.x < ((Screen.width / 2) - xf) && pos_btn_A.position.y < (Screen.height + yf)) {
                pos_btn_A.position += new Vector3 (0f, Time.deltaTime * velocidad, 0f);
                if (pos_btn_A.position.y > (Screen.height + yf) && contador_r < r) {
                    contador_r++;
                    Destroy (pos_btn_A.gameObject);
                    InstanciarIzqSuperior ();

                }
            }
        }

        if (contador == 1) {
            if (pos_btn_B.position.x > ((Screen.width / 2) - xf)) {
                pos_btn_B.position += new Vector3 (-Time.deltaTime * velocidad, 0f, 0f);
            }
            if (pos_btn_B.position.x < ((Screen.width / 2) - xf) && pos_btn_B.position.y > yf) {
                pos_btn_B.position += new Vector3 (0f, -Time.deltaTime * velocidad, 0f);
                if (pos_btn_B.position.y < yf && contador_r < r) {
                    contador_r++;
                    Destroy (pos_btn_B.gameObject);
                    InstanciarIzqInferior ();

                }
            }
        }

        if (contador == 2) {
            if (pos_btn_C.position.x < ((Screen.width / 2) - xf)) {
                pos_btn_C.position += new Vector3 (Time.deltaTime * velocidad, 0f, 0f);
            }
            if (pos_btn_C.position.x > ((Screen.width / 2) - xf) && pos_btn_C.position.y > yf) {
                pos_btn_C.position += new Vector3 (0f, -Time.deltaTime * velocidad, 0f);
                if (pos_btn_C.position.y < yf && contador_r < r) {
                    contador_r++;
                    Destroy (pos_btn_C.gameObject);
                    InstanciarDerInferior ();
                }
            }
        }

        if (contador == 3) {
            if (pos_btn_D.position.x < ((Screen.width / 2) - xf)) {
                pos_btn_D.position += new Vector3 (Time.deltaTime * velocidad, 0f, 0f);
            }
            if (pos_btn_D.position.x > ((Screen.width / 2) - xf) && pos_btn_D.position.y < (Screen.height + yf)) {
                pos_btn_D.position += new Vector3 (0f, Time.deltaTime * velocidad, 0f);
                if (pos_btn_D.position.y > (Screen.height + yf) && contador_r < r) {
                    contador_r++;
                    Destroy (pos_btn_D.gameObject);
                    InstanciarDerSuperior ();
                    Debug.Log (contador_r);
                }
            }
        }

    }

    public void regresarEscena () {
        SceneManager.LoadScene (9);
    }
    public void InstanciarIzqSuperior () {

        //POSICIONES INICIALES
        btnPrincipal.GetComponentInChildren<Text>().text = "Izquierda Arriba";
        btnPrincipal.GetComponentInChildren<Text>().alignment = TextAnchor.UpperLeft;
        btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
        //GameObject btnIzqSup = Instantiate(btnPrincipal.gameObject,new Vector3(-100 ,(Screen.height/2) + 25  ,0),transform.rotation);
        GameObject btnIzqSup = Instantiate (btnPrincipal.gameObject, new Vector3 (xi, (Screen.height / 2) + yi, 0), transform.rotation);
        btnIzqSup.transform.SetParent (this.transform);
        pos_btn_A = btnIzqSup.GetComponent<Transform> ();
        if (contador_r == r) {
            pos_btn_A.GetComponent<Button> ().onClick.AddListener (contar);
        }
        StartCoroutine (playsound ());

    }

    public void InstanciarIzqInferior () {

        //POSICIONES INICIALES
        btnPrincipal.GetComponentInChildren<Text>().text = "Izquierda Abajo";
        btnPrincipal.GetComponentInChildren<Text>().alignment = TextAnchor.LowerLeft;
        btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
        //GameObject btnIzqInf = Instantiate(btnPrincipal.gameObject,new Vector3( -100, (Screen.height/2) - 25 ,0),transform.rotation);
        GameObject btnIzqInf = Instantiate (btnPrincipal.gameObject, new Vector3 (xi, (Screen.height / 2) + yi, 0), transform.rotation);
        Debug.Log ("contador_r = " + contador_r + " y r = " + r);
        btnIzqInf.transform.SetParent (this.transform);
        pos_btn_B = btnIzqInf.GetComponent<Transform> ();
        if (contador_r == r) {
            pos_btn_B.GetComponent<Button> ().onClick.AddListener (contar);
        }
        StartCoroutine (playsound ());
        //Debug.Log("Xa = "+ pos_btn_A.position.x + "Ya = " + pos_btn_A.position.y);

    }

    public void InstanciarDerInferior () {

        btnPrincipal.GetComponentInChildren<Text>().text = "Derecha Abajo";
        btnPrincipal.GetComponentInChildren<Text>().alignment = TextAnchor.UpperRight;
        btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
        //GameObject btnDerInf = Instantiate(btnPrincipal.gameObject,new Vector3( Screen.width + 100 , (Screen.height/2) - 25  ,0),transform.rotation);
        GameObject btnDerInf = Instantiate (btnPrincipal.gameObject, new Vector3 (Screen.width + xi, (Screen.height / 2) + yi, 0), transform.rotation);
        btnDerInf.transform.SetParent (this.transform);
        pos_btn_C = btnDerInf.GetComponent<Transform> ();
        Debug.Log (" Se creo el boton = " + contador_r);
        if (contador_r == r) {
            pos_btn_C.GetComponent<Button> ().onClick.AddListener (contar);
        }
        StartCoroutine (playsound ());
    }
    public void InstanciarDerSuperior () {

        btnPrincipal.GetComponentInChildren<Text>().text = "Derecha Arriba";
        btnPrincipal.GetComponentInChildren<Text>().alignment = TextAnchor.UpperRight;
        btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
        // GameObject btnDerSup = Instantiate(btnPrincipal.gameObject,new Vector3( Screen.width + 100 , (Screen.height/2) + 25  ,0),transform.rotation);
        GameObject btnDerSup = Instantiate (btnPrincipal.gameObject, new Vector3 (Screen.width + xi, (Screen.height / 2) + yi, 0), transform.rotation);
        btnDerSup.transform.SetParent (this.transform);
        pos_btn_D = btnDerSup.GetComponent<Transform> ();
        if (contador_r == r) {
            pos_btn_D.GetComponent<Button> ().onClick.AddListener (contar);
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
        numero_final = UnityEngine.Random.Range (5, 9);
        Debug.Log("Numero Aletorio final = " + numero_final);
    }

    void contar () {
        
        contador_r = 1;
        contador_repeticiones--;
        if (contador == 0 && contador_repeticiones != 0) {
            Debug.Log (contador_repeticiones);
            Destroy (pos_btn_A.gameObject);
            imgA.color = Color.white;
            datosPersonaje (Menu_Aprendizaje_1.cod_personaje_2);
            datosPosicion (numero_final);
           
        }
        if (contador == 1 && contador_repeticiones != 0) {

            Debug.Log (contador_repeticiones);

            Destroy (pos_btn_B.gameObject);
            imgB.color = Color.white;
            datosPersonaje (Menu_Aprendizaje_1.cod_personaje_2);
                datosPosicion (numero_final);
            
        }
        if (contador == 2 && contador_repeticiones != 0) {
            Debug.Log (contador_repeticiones);
            Destroy (pos_btn_C.gameObject);
            imgC.color = Color.white;
            datosPersonaje (Menu_Aprendizaje_1.cod_personaje_2);
           
                datosPosicion (numero_final);
           
        }
        if (contador == 3 && contador_repeticiones != 0) {

            Debug.Log (contador_repeticiones);

            Destroy (pos_btn_D.gameObject);
            imgD.color = Color.white;
            datosPersonaje (Menu_Aprendizaje_1.cod_personaje_2);
           
                datosPosicion (numero_final);
           
        }

        if (contador_repeticiones == 0) {
            saveDetalleAprendizaje (Menu_Aprendizaje_1.cod_aprendizaje, codigo_ubicacion_2, Menu_Aprendizaje_1.cod_color_2, Menu_Aprendizaje_1.cod_personaje_2);
            if (contador == 0) {
                contador = 4;
                Destroy (pos_btn_A.gameObject);
                imgA.color = Color.white;
            }
            if (contador == 1) {
                contador = 4;
                Destroy (pos_btn_B.gameObject);
                imgB.color = Color.white;
            }
            if (contador == 2) {
                contador = 4;
                Destroy (pos_btn_C.gameObject);
                imgC.color = Color.white;
            }
            if (contador == 3) {
                contador = 4;
                Destroy (pos_btn_D.gameObject);
                imgD.color = Color.white;
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
            xi = reader.GetInt32 (4);
            yi = reader.GetInt32 (6);
            xf = reader.GetInt32 (5);
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

        if (id == 5) {
            if (contador_repeticiones == 2) {
                Debug.Log ("Solo la primera vez a");
                saveDetalleAprendizaje (Menu_Aprendizaje_1.cod_aprendizaje, id, Menu_Aprendizaje_1.cod_color_1, Menu_Aprendizaje_1.cod_personaje_1);
                colors (Menu_Aprendizaje_1.colora, imgA);
            } else {
                colors (Menu_Aprendizaje_1.colorb, imgA);
            }
            contador = 0;
            InstanciarIzqSuperior ();
        }
        if (id == 6) {
            //solo la primera vez.
            if (contador_repeticiones == 2) {
                Debug.Log ("Solo la primera vez b");
                saveDetalleAprendizaje (Menu_Aprendizaje_1.cod_aprendizaje, id, Menu_Aprendizaje_1.cod_color_1, Menu_Aprendizaje_1.cod_personaje_1);
                colors (Menu_Aprendizaje_1.colora, imgB);
            } else {
                colors (Menu_Aprendizaje_1.colorb, imgB);
            }
            contador = 1;
            InstanciarIzqInferior ();
        }
        if (id == 7) {
            if (contador_repeticiones == 2) {
                Debug.Log ("Solo la primera vez a");
                saveDetalleAprendizaje (Menu_Aprendizaje_1.cod_aprendizaje, id, Menu_Aprendizaje_1.cod_color_1, Menu_Aprendizaje_1.cod_personaje_1);
                colors (Menu_Aprendizaje_1.colora, imgC);
            } else {
                colors (Menu_Aprendizaje_1.colorb, imgC);
            }
            contador = 2;
            InstanciarDerInferior ();
        }
        if (id == 8) {
            if (contador_repeticiones == 2) {
                Debug.Log ("Solo la primera vez a");
                saveDetalleAprendizaje (Menu_Aprendizaje_1.cod_aprendizaje, id, Menu_Aprendizaje_1.cod_color_1, Menu_Aprendizaje_1.cod_personaje_1);
                colors (Menu_Aprendizaje_1.colora, imgD);
            } else {
                colors (Menu_Aprendizaje_1.colorb, imgD);
            }
            contador = 3;
            InstanciarDerSuperior ();
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