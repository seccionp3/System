using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AVANZADO_APRENDIZAJE : MonoBehaviour {

    public RectTransform panelA, panelB, panelC, panelD, panelE, panelF, panelG, panelH, panelI;
    private Image imgA, imgB, imgC, imgD, imgE, imgF, imgG, imgH, imgI;
    public Button btnPrincipal;

    public AudioSource audioUbicacion;

    private int contador = 0, contador_r;
    private int xi, xf, yi, yf;

    private Transform pos_btn_A, pos_btn_B, pos_btn_C, pos_btn_D, pos_btn_E, pos_btn_F, pos_btn_G, pos_btn_H, pos_btn_I;

    private AudioClip a, b;

    public GameObject btnMsg;

    private Texture2D tex;
    //VARIABLES DE CONFIGURACION INICIAL
    private int r = Menu_Aprendizaje_1.num_repeticiones;
    private int velocidad = Menu_Aprendizaje_1.velocidad_data;
    int contador_repeticiones = 2;
    private int codigo_ubicacion_2;
    int numero_inicial = 10;
    int numero_final = 13;
    // Use this for initialization
    void Start () {
        btnMsg.SetActive (false);
        datosPersonaje (Menu_Aprendizaje_1.cod_personaje_1);
        numero_inicial = UnityEngine.Random.Range (9, 18);
        imgA = panelA.GetComponent<Image> ();
        imgB = panelB.GetComponent<Image> ();
        imgC = panelC.GetComponent<Image> ();
        imgD = panelD.GetComponent<Image> ();
        imgE = panelE.GetComponent<Image> ();
        imgF = panelF.GetComponent<Image> ();
        imgG = panelG.GetComponent<Image> ();
        imgH = panelH.GetComponent<Image> ();
        imgI = panelI.GetComponent<Image> ();
        contador_r = 1;
        datosPosicion (numero_inicial);

    }

    // Update is called once per frame

    void Update () {
        if (contador == 0) {
            if (pos_btn_A.position.x > ((Screen.width / 3) - 350)) {
                //AudioSource.PlayClipAtPoint(Sonido, Posicion.position, Volumen);
                pos_btn_A.position += new Vector3 (-Time.deltaTime * velocidad, 0f, 0f);
            }
            if (pos_btn_A.position.x <= ((Screen.width / 3) - 350)) {
                if (contador_r < r) {
                    contador_r++;
                    Destroy (pos_btn_A.gameObject);
                    InstanciarPosA ();
                    //Debug.Log(contador_r);
                }
            }
        }
        if (contador == 1) {
            if (pos_btn_B.position.x > ((Screen.width / 3) - 350)) {
                //AudioSource.PlayClipAtPoint(Sonido, Posicion.position, Volumen);
                pos_btn_B.position += new Vector3 (-Time.deltaTime * velocidad, 0f, 0f);
            }
            if (pos_btn_B.position.x <= ((Screen.width / 3) - 350)) {
                if (contador_r < r) {
                    contador_r++;
                    Destroy (pos_btn_B.gameObject);
                    InstanciarPosB ();
                    //Debug.Log(contador_r);
                }
            }
        }

        if (contador == 2) {
            if (pos_btn_C.position.x < ((Screen.width / 1.5) + 350)) {
                //AudioSource.PlayClipAtPoint(Sonido, Posicion.position, Volumen);
                pos_btn_C.position += new Vector3 (Time.deltaTime * velocidad, 0f, 0f);
            }
            if (pos_btn_C.position.x >= ((Screen.width / 1.5) + 350)) {
                if (contador_r < r) {
                    contador_r++;
                    Destroy (pos_btn_C.gameObject);
                    InstanciarPosC ();
                    //Debug.Log(contador_r);
                }
            }
        }
        if (contador == 3) {
            if (pos_btn_D.position.x < ((Screen.width / 1.5) + 350)) {
                //AudioSource.PlayClipAtPoint(Sonido, Posicion.position, Volumen);
                pos_btn_D.position += new Vector3 (Time.deltaTime * velocidad, 0f, 0f);
            }
            if (pos_btn_D.position.x >= ((Screen.width / 1.5) + 350)) {
                if (contador_r < r) {
                    contador_r++;
                    Destroy (pos_btn_D.gameObject);
                    InstanciarPosD ();
                    //Debug.Log(contador_r);
                }
            }
        }

        if (contador == 4) {
            if (pos_btn_E.position.x > ((Screen.width / 3) - 350)) {
                //AudioSource.PlayClipAtPoint(Sonido, Posicion.position, Volumen);
                pos_btn_E.position += new Vector3 (-Time.deltaTime * velocidad, 0f, 0f);
            }
            if (pos_btn_E.position.x <= ((Screen.width / 3) - 350)) {
                if (contador_r < r) {
                    contador_r++;
                    Destroy (pos_btn_E.gameObject);
                    InstanciarPosE ();
                    //Debug.Log(contador_r);
                }
            }
        }

        if (contador == 5) {
            if (pos_btn_F.position.y > ((Screen.height / 3) - 150)) {
                //AudioSource.PlayClipAtPoint(Sonido, Posicion.position, Volumen);
                pos_btn_F.position += new Vector3 (0, -Time.deltaTime * velocidad, 0f);
            }
            if (pos_btn_F.position.y <= ((Screen.height / 3) - 150)) {
                if (contador_r < r) {
                    contador_r++;
                    Destroy (pos_btn_F.gameObject);
                    InstanciarPosF ();
                    //Debug.Log(contador_r);
                }
            }
        }

        if (contador == 6) {
            if (pos_btn_G.position.x < ((Screen.width / 1.5) + 350)) {
                //AudioSource.PlayClipAtPoint(Sonido, Posicion.position, Volumen);
                pos_btn_G.position += new Vector3 (Time.deltaTime * velocidad, 0, 0f);
            }
            if (pos_btn_G.position.x >= ((Screen.width / 1.5) + 350)) {
                if (contador_r < r) {
                    contador_r++;
                    Destroy (pos_btn_G.gameObject);
                    InstanciarPosG ();
                    //Debug.Log(contador_r);
                }
            }
        }
        if (contador == 7) {
            if (pos_btn_H.position.y < ((Screen.height / 1.5) + 150)) {
                //AudioSource.PlayClipAtPoint(Sonido, Posicion.position, Volumen);
                pos_btn_H.position += new Vector3 (0, Time.deltaTime * velocidad, 0f);
            }
            if (pos_btn_H.position.y >= ((Screen.height / 1.5) + 150)) {
                if (contador_r < r) {
                    contador_r++;
                    Destroy (pos_btn_H.gameObject);
                    InstanciarPosH ();
                    //Debug.Log(contador_r);
                }
            }
        }

        if (contador == 8) {

            if (contador_r < r) {
                contador_r++;
                Destroy (pos_btn_I.gameObject);
                Debug.Log (contador_r);
                InstanciarPosI ();
            }

        }
    }

    public void regresarEscena () {
        SceneManager.LoadScene (9);
    }
    void InstanciarPosA () {
        //POSICIONES INICIALES
        //btnPrincipal.GetComponentInChildren<Text> ().text = "Izquierda Arriba";
        //btnPrincipal.GetComponentInChildren<Text>().alignment = TextAnchor.LowerCenter;
        btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
        //btnPrincipal.onClick.AddListener(contar);
        GameObject btnIzqSup = Instantiate (btnPrincipal.gameObject, new Vector3 (400, (Screen.height / 6) * 5, 0), transform.rotation);
        Debug.Log ("contador_r = " + contador_r + " y r = " + r);
        btnIzqSup.transform.SetParent (this.transform);
        pos_btn_A = btnIzqSup.GetComponent<Transform> ();
        if (contador_r == r) {
            pos_btn_A.GetComponent<Button> ().onClick.AddListener (contar);
        }
        StartCoroutine (playsound ());

    }

    void InstanciarPosB () {
        //POSICIONES INICIALES
        //btnPrincipal.GetComponentInChildren<Text> ().text = "Izquierda Abajo";
        btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
        //btnPrincipal.onClick.AddListener(contar);
        GameObject btnIzqSup = Instantiate (btnPrincipal.gameObject, new Vector3 (400, Screen.height / 6, 0), transform.rotation);
        Debug.Log ("contador_r = " + contador_r + " y r = " + r);
        btnIzqSup.transform.SetParent (this.transform);
        pos_btn_B = btnIzqSup.GetComponent<Transform> ();
        if (contador_r == r) {
            pos_btn_B.GetComponent<Button> ().onClick.AddListener (contar);
        }
        StartCoroutine (playsound ());

    }
    void InstanciarPosC () {
        //POSICIONES INICIALES
        //btnPrincipal.GetComponentInChildren<Text> ().text = "Derecha Abajo";
        btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
        //btnPrincipal.onClick.AddListener(contar);
        GameObject btnIzqSup = Instantiate (btnPrincipal.gameObject, new Vector3 (Screen.width - 400, Screen.height / 6, 0), transform.rotation);
        Debug.Log ("contador_r = " + contador_r + " y r = " + r);
        btnIzqSup.transform.SetParent (this.transform);
        pos_btn_C = btnIzqSup.GetComponent<Transform> ();
        if (contador_r == r) {
            pos_btn_C.GetComponent<Button> ().onClick.AddListener (contar);
        }
        StartCoroutine (playsound ());

    }

    void InstanciarPosD () {
        //POSICIONES INICIALES
        //btnPrincipal.GetComponentInChildren<Text> ().text = "Derecha Arriba";
        btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
        //btnPrincipal.onClick.AddListener(contar);
        GameObject btnIzqSup = Instantiate (btnPrincipal.gameObject, new Vector3 (Screen.width - 400, (Screen.height / 6) * 5, 0), transform.rotation);
        btnIzqSup.transform.SetParent (this.transform);
        pos_btn_D = btnIzqSup.GetComponent<Transform> ();
        if (contador_r == r) {
            pos_btn_D.GetComponent<Button> ().onClick.AddListener (contar);
        }
        StartCoroutine (playsound ());

    }

    void InstanciarPosE () {
        //POSICIONES INICIALES
        //btnPrincipal.GetComponentInChildren<Text> ().text = "Izquierda Medio";
        btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
        //btnPrincipal.onClick.AddListener(contar);
        GameObject btnIzqSup = Instantiate (btnPrincipal.gameObject, new Vector3 (400, Screen.height / 2, 0), transform.rotation);
        btnIzqSup.transform.SetParent (this.transform);
        pos_btn_E = btnIzqSup.GetComponent<Transform> ();
        if (contador_r == r) {
            pos_btn_E.GetComponent<Button> ().onClick.AddListener (contar);
        }
        StartCoroutine (playsound ());

    }

    void InstanciarPosF () {
        //POSICIONES INICIALES
        //btnPrincipal.GetComponentInChildren<Text> ().text = "Abajo Medio";
        btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
        //btnPrincipal.onClick.AddListener(contar);
        GameObject btnIzqSup = Instantiate (btnPrincipal.gameObject, new Vector3 (Screen.width / 2, 200, 0), transform.rotation);
        btnIzqSup.transform.SetParent (this.transform);
        pos_btn_F = btnIzqSup.GetComponent<Transform> ();
        if (contador_r == r) {
            pos_btn_F.GetComponent<Button> ().onClick.AddListener (contar);
        }
        StartCoroutine (playsound ());

    }

    void InstanciarPosG () {
        //POSICIONES INICIALES
        //btnPrincipal.GetComponentInChildren<Text> ().text = "Derecha Medio";
        btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
        //btnPrincipal.onClick.AddListener(contar);
        GameObject btnIzqSup = Instantiate (btnPrincipal.gameObject, new Vector3 (Screen.width - 400, Screen.height / 2, 0), transform.rotation);
        btnIzqSup.transform.SetParent (this.transform);
        pos_btn_G = btnIzqSup.GetComponent<Transform> ();
        if (contador_r == r) {
            pos_btn_G.GetComponent<Button> ().onClick.AddListener (contar);
        }
        StartCoroutine (playsound ());

    }

    void InstanciarPosH () {
        //POSICIONES INICIALES
        //btnPrincipal.GetComponentInChildren<Text> ().text = "Arriba Medio";
        btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
        //btnPrincipal.onClick.AddListener(contar);
        GameObject btnIzqSup = Instantiate (btnPrincipal.gameObject, new Vector3 (Screen.width / 2, Screen.height - 200, 0), transform.rotation);
        btnIzqSup.transform.SetParent (this.transform);
        pos_btn_H = btnIzqSup.GetComponent<Transform> ();
        if (contador_r == r) {
            pos_btn_H.GetComponent<Button> ().onClick.AddListener (contar);
        }
        StartCoroutine (playsound ());

    }

    void InstanciarPosI () {
        //POSICIONES INICIALES
        btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
        //btnPrincipal.GetComponentInChildren<Text> ().text = "Centro";
        //btnPrincipal.onClick.AddListener(contar);
        GameObject btnIzqSup = Instantiate (btnPrincipal.gameObject, new Vector3 (Screen.width / 2, Screen.height / 2, 0), transform.rotation);
        btnIzqSup.transform.SetParent (this.transform);
        pos_btn_I = btnIzqSup.GetComponent<Transform> ();
        if (contador_r == r) {
            pos_btn_I.GetComponent<Button> ().onClick.AddListener (contar);
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
        numero_final = UnityEngine.Random.Range (9, 18);
        Debug.Log("Numero Aleatorio Final = " + numero_final );
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
            //datosPosicion (UnityEngine.Random.Range (9, 18));
            datosPosicion (numero_final);
        }
        if (contador == 2 && contador_repeticiones != 0) {
            Debug.Log (contador_repeticiones);
            Destroy (pos_btn_C.gameObject);
            imgC.color = Color.white;
            datosPersonaje (Menu_Aprendizaje_1.cod_personaje_2);
            //datosPosicion (UnityEngine.Random.Range (9, 18));
            datosPosicion (numero_final);
        }
        if (contador == 3 && contador_repeticiones != 0) {

            Debug.Log (contador_repeticiones);

            Destroy (pos_btn_D.gameObject);
            imgD.color = Color.white;
            datosPersonaje (Menu_Aprendizaje_1.cod_personaje_2);
            //datosPosicion (UnityEngine.Random.Range (9, 18));
            datosPosicion (numero_final);
        }
        if (contador == 4 && contador_repeticiones != 0) {
            Destroy (pos_btn_E.gameObject);
            imgE.color = Color.white;
            datosPersonaje (Menu_Aprendizaje_1.cod_personaje_2);
            //datosPosicion (UnityEngine.Random.Range (9, 18));
            datosPosicion (numero_final);
        }
        if (contador == 5 && contador_repeticiones != 0) {

            Debug.Log (contador_repeticiones);

            Destroy (pos_btn_F.gameObject);
            imgF.color = Color.white;
            datosPersonaje (Menu_Aprendizaje_1.cod_personaje_2);
            //datosPosicion (UnityEngine.Random.Range (9, 18));
            datosPosicion (numero_final);
        }
        if (contador == 6 && contador_repeticiones != 0) {

            Debug.Log (contador_repeticiones);

            Destroy (pos_btn_G.gameObject);
            imgG.color = Color.white;
            datosPersonaje (Menu_Aprendizaje_1.cod_personaje_2);
            //datosPosicion (UnityEngine.Random.Range (9, 18));
            datosPosicion (numero_final);
        }
        if (contador == 7 && contador_repeticiones != 0) {

            Debug.Log (contador_repeticiones);

            Destroy (pos_btn_H.gameObject);
            imgH.color = Color.white;
            datosPersonaje (Menu_Aprendizaje_1.cod_personaje_2);
            //datosPosicion (UnityEngine.Random.Range (9, 18));
            datosPosicion (numero_final);
        }
        if (contador == 8 && contador_repeticiones != 0) {

            Debug.Log (contador_repeticiones);

            Destroy (pos_btn_I.gameObject);
            imgI.color = Color.white;
            datosPersonaje (Menu_Aprendizaje_1.cod_personaje_2);
            //datosPosicion (UnityEngine.Random.Range (9, 18));
            datosPosicion (numero_final);
        }

        if (contador_repeticiones == 0) {
            saveDetalleAprendizaje (Menu_Aprendizaje_1.cod_aprendizaje, codigo_ubicacion_2, Menu_Aprendizaje_1.cod_color_2, Menu_Aprendizaje_1.cod_personaje_2);
            if (contador == 0) {
                contador = 10;
                Destroy (pos_btn_A.gameObject);
                imgA.color = Color.white;
            }
            if (contador == 1) {
                contador = 10;
                Destroy (pos_btn_B.gameObject);
                imgB.color = Color.white;
            }
            if (contador == 2) {
                contador = 10;
                Destroy (pos_btn_C.gameObject);
                imgC.color = Color.white;
            }
            if (contador == 3) {
                contador = 10;
                Destroy (pos_btn_D.gameObject);
                imgD.color = Color.white;
            }
            if (contador == 4) {
                contador = 10;
                Destroy (pos_btn_E.gameObject);
                imgE.color = Color.white;
            }
            if (contador == 5) {
                contador = 10;
                Destroy (pos_btn_F.gameObject);
                imgF.color = Color.white;
            }
            if (contador == 6) {
                contador = 10;
                Destroy (pos_btn_G.gameObject);
                imgG.color = Color.white;
            }
            if (contador == 7) {
                contador = 10;
                Destroy (pos_btn_H.gameObject);
                imgH.color = Color.white;
            }
            if (contador == 8) {
                contador = 10;
                Destroy (pos_btn_I.gameObject);
                imgI.color = Color.white;
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

        if (id == 9) {
            if (contador_repeticiones == 2) {
                Debug.Log ("Solo la primera vez a");
                saveDetalleAprendizaje (Menu_Aprendizaje_1.cod_aprendizaje, id, Menu_Aprendizaje_1.cod_color_1, Menu_Aprendizaje_1.cod_personaje_1);
                colors (Menu_Aprendizaje_1.colora, imgA);
            } else {
                colors (Menu_Aprendizaje_1.colorb, imgA);
            }
            contador = 0;
            InstanciarPosA ();
        }
        if (id == 10) {
            //solo la primera vez.
            if (contador_repeticiones == 2) {
                Debug.Log ("Solo la primera vez b");
                saveDetalleAprendizaje (Menu_Aprendizaje_1.cod_aprendizaje, id, Menu_Aprendizaje_1.cod_color_1, Menu_Aprendizaje_1.cod_personaje_1);
                colors (Menu_Aprendizaje_1.colora, imgB);
            } else {
                colors (Menu_Aprendizaje_1.colorb, imgB);
            }
            contador = 1;
            InstanciarPosB ();
        }
        if (id == 11) {
            if (contador_repeticiones == 2) {
                Debug.Log ("Solo la primera vez a");
                saveDetalleAprendizaje (Menu_Aprendizaje_1.cod_aprendizaje, id, Menu_Aprendizaje_1.cod_color_1, Menu_Aprendizaje_1.cod_personaje_1);
                colors (Menu_Aprendizaje_1.colora, imgC);
            } else {
                colors (Menu_Aprendizaje_1.colorb, imgC);
            }
            contador = 2;
            InstanciarPosC ();
        }
        if (id == 12) {
            if (contador_repeticiones == 2) {
                Debug.Log ("Solo la primera vez a");
                saveDetalleAprendizaje (Menu_Aprendizaje_1.cod_aprendizaje, id, Menu_Aprendizaje_1.cod_color_1, Menu_Aprendizaje_1.cod_personaje_1);
                colors (Menu_Aprendizaje_1.colora, imgD);
            } else {
                colors (Menu_Aprendizaje_1.colorb, imgD);
            }
            contador = 3;
            InstanciarPosD ();
        }
        if (id == 13) {
            if (contador_repeticiones == 2) {
                Debug.Log ("Solo la primera vez a");
                saveDetalleAprendizaje (Menu_Aprendizaje_1.cod_aprendizaje, id, Menu_Aprendizaje_1.cod_color_1, Menu_Aprendizaje_1.cod_personaje_1);
                colors (Menu_Aprendizaje_1.colora, imgE);
            } else {
                colors (Menu_Aprendizaje_1.colorb, imgE);
            }
            contador = 4;
            InstanciarPosE ();
        }
        if (id == 14) {
            if (contador_repeticiones == 2) {
                Debug.Log ("Solo la primera vez a");
                saveDetalleAprendizaje (Menu_Aprendizaje_1.cod_aprendizaje, id, Menu_Aprendizaje_1.cod_color_1, Menu_Aprendizaje_1.cod_personaje_1);
                colors (Menu_Aprendizaje_1.colora, imgF);
            } else {
                colors (Menu_Aprendizaje_1.colorb, imgF);
            }
            contador = 5;
            InstanciarPosF ();
        }
        if (id == 15) {
            if (contador_repeticiones == 2) {
                Debug.Log ("Solo la primera vez a");
                saveDetalleAprendizaje (Menu_Aprendizaje_1.cod_aprendizaje, id, Menu_Aprendizaje_1.cod_color_1, Menu_Aprendizaje_1.cod_personaje_1);
                colors (Menu_Aprendizaje_1.colora, imgG);
            } else {
                colors (Menu_Aprendizaje_1.colorb, imgG);
            }
            contador = 6;
            InstanciarPosG ();
        }
        if (id == 16) {
            if (contador_repeticiones == 2) {
                Debug.Log ("Solo la primera vez a");
                saveDetalleAprendizaje (Menu_Aprendizaje_1.cod_aprendizaje, id, Menu_Aprendizaje_1.cod_color_1, Menu_Aprendizaje_1.cod_personaje_1);
                colors (Menu_Aprendizaje_1.colora, imgH);
            } else {
                colors (Menu_Aprendizaje_1.colorb, imgH);
            }
            contador = 7;
            InstanciarPosH ();
        }
        if (id == 17) {
            if (contador_repeticiones == 2) {
                Debug.Log ("Solo la primera vez a");
                saveDetalleAprendizaje (Menu_Aprendizaje_1.cod_aprendizaje, id, Menu_Aprendizaje_1.cod_color_1, Menu_Aprendizaje_1.cod_personaje_1);
                colors (Menu_Aprendizaje_1.colora, imgI);
            } else {
                colors (Menu_Aprendizaje_1.colorb, imgI);
            }
            contador = 8;
            InstanciarPosI ();
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