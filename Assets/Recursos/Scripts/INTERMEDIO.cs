using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class INTERMEDIO : MonoBehaviour {

    public RectTransform panelA, panelB, panelC, panelD;
    private Image imgA, imgB, imgC, imgD;

    public Button btnPrincipal;
    private Transform pos_btn_A, pos_btn_B, pos_btn_C, pos_btn_D;
    private int contador = 0, contador_r;
    private int r = conf_ini.num_repeticiones;
    private float velocidad = conf_ini.velocidad_boton;

    private int xi, xf, yi, yf;
    public Text txtMsg;
    public AudioSource audioUbicacion;

    // Use this for initialization
    void Start () {
        datosPosicion (5);
        txtMsg.enabled = false;
        contador_r = 1;
        InstanciarIzqSuperior ();
        //InstanciarIzqInferior();
        //InstanciarDerInferior();
        //InstanciarDerSuperior();
        imgA = panelA.GetComponent<Image> ();
        imgB = panelB.GetComponent<Image> ();
        imgC = panelC.GetComponent<Image> ();
        imgD = panelD.GetComponent<Image> ();
        //colors("azul", imgA);
        colors (conf_ini.a, imgA);

    }

    public void InstanciarIzqSuperior () {

        //POSICIONES INICIALES
        btnPrincipal.GetComponentInChildren<Text> ().text = "Izquierda Arriba";
        //btnPrincipal.onClick.AddListener(contar);
        //GameObject btnIzqSup = Instantiate(btnPrincipal.gameObject,new Vector3(-100 ,(Screen.height/2) + 25  ,0),transform.rotation);
        GameObject btnIzqSup = Instantiate (btnPrincipal.gameObject, new Vector3 (xi, (Screen.height / 2) + yi, 0), transform.rotation);
        Debug.Log ("contador_r = " + contador_r + " y r = " + r);
        btnIzqSup.transform.SetParent (this.transform);
        pos_btn_A = btnIzqSup.GetComponent<Transform> ();
        if (contador_r == r) {
            pos_btn_A.GetComponent<Button> ().onClick.AddListener (contar);
        }
        audioUbicacion.Play ();
        //Debug.Log("Xa = "+ pos_btn_A.position.x + "Ya = " + pos_btn_A.position.y);

    }

    public void InstanciarIzqInferior () {

        //POSICIONES INICIALES
        btnPrincipal.GetComponentInChildren<Text> ().text = "Izquierda Abajo";
        //btnPrincipal.onClick.AddListener(contar);
        //GameObject btnIzqInf = Instantiate(btnPrincipal.gameObject,new Vector3( -100, (Screen.height/2) - 25 ,0),transform.rotation);
        GameObject btnIzqInf = Instantiate (btnPrincipal.gameObject, new Vector3 (xi, (Screen.height / 2) + yi, 0), transform.rotation);
        Debug.Log ("contador_r = " + contador_r + " y r = " + r);
        btnIzqInf.transform.SetParent (this.transform);
        pos_btn_B = btnIzqInf.GetComponent<Transform> ();
        if (contador_r == 1) {
            pos_btn_B.GetComponent<Button> ().onClick.AddListener (contar);
        }
        audioUbicacion.Play ();
        //Debug.Log("Xa = "+ pos_btn_A.position.x + "Ya = " + pos_btn_A.position.y);

    }

    public void InstanciarDerInferior () {

        btnPrincipal.GetComponentInChildren<Text> ().text = "Derecha Abajo";
        //GameObject btnDerInf = Instantiate(btnPrincipal.gameObject,new Vector3( Screen.width + 100 , (Screen.height/2) - 25  ,0),transform.rotation);
        GameObject btnDerInf = Instantiate (btnPrincipal.gameObject, new Vector3 (Screen.width + xi, (Screen.height / 2) + yi, 0), transform.rotation);
        btnDerInf.transform.SetParent (this.transform);
        pos_btn_C = btnDerInf.GetComponent<Transform> ();
        Debug.Log (" Se creo el boton = " + contador_r);
        if (contador_r == r) {
            pos_btn_C.GetComponent<Button> ().onClick.AddListener (contar);
        }
        audioUbicacion.Play ();
    }
    public void InstanciarDerSuperior () {

        btnPrincipal.GetComponentInChildren<Text> ().text = "Derecha Arriba";
        // GameObject btnDerSup = Instantiate(btnPrincipal.gameObject,new Vector3( Screen.width + 100 , (Screen.height/2) + 25  ,0),transform.rotation);
        GameObject btnDerSup = Instantiate (btnPrincipal.gameObject, new Vector3 (Screen.width + xi, (Screen.height / 2) + yi, 0), transform.rotation);
        btnDerSup.transform.SetParent (this.transform);
        pos_btn_D = btnDerSup.GetComponent<Transform> ();
        if (contador_r == 1) {
            pos_btn_D.GetComponent<Button> ().onClick.AddListener (contar);
        }
        audioUbicacion.Play ();
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

    void contar () {
        contador++;
        Debug.Log (contador);
        if (contador == 1) {
            datosPosicion (6);
            imgA.color = Color.white;
            colors (conf_ini.b, imgB);
            //colors("amarillo", imgB);
            Destroy (pos_btn_A.gameObject);
            InstanciarIzqInferior ();
        }
        if (contador == 2) {
            datosPosicion (7);
            imgB.color = Color.white;
            colors (conf_ini.c, imgC);
            //colors("verde", imgC);
            Destroy (pos_btn_B.gameObject);
            InstanciarDerInferior ();
        }
        if (contador == 3) {
            datosPosicion (8);
            imgC.color = Color.white;
            //colors("rojo", imgD);
            colors (conf_ini.d, imgD);
            Destroy (pos_btn_C.gameObject);
            InstanciarDerSuperior ();
        }
        if (contador == 4) {
            imgD.color = Color.white;
            Destroy (pos_btn_D.gameObject);
        }
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
                    Debug.Log (contador_r);
                }
            }

        }

        if (contador == 1) {
            if (pos_btn_B.position.x > ((Screen.width / 2) - xf)) {
                pos_btn_B.position += new Vector3 (-Time.deltaTime * velocidad, 0f, 0f);
            }
            if (pos_btn_B.position.x < ((Screen.width / 2) - xf) && pos_btn_B.position.y > yf) {
                pos_btn_B.position += new Vector3 (0f, -Time.deltaTime * velocidad, 0f);
                if (pos_btn_B.position.y < yf && contador_r > 1) {
                    contador_r--;
                    Destroy (pos_btn_B.gameObject);
                    InstanciarIzqInferior ();
                    Debug.Log (contador_r);
                }
            }
            // if(movB.position.x >= 531 && movB.position.y <= 60 ){
            // 	d--;
            // 	 movB.Rotate( Vector3.up * velocidad * Time.deltaTime);
            // 	Debug.Log(d);
            // }
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
                    Debug.Log (contador_r);
                }
            }
            // if(movB.position.x >= 531 && movB.position.y <= 60 ){
            // 	d--;
            // 	 movB.Rotate( Vector3.up * velocidad * Time.deltaTime);
            // 	Debug.Log(d);
            // }
        }

        if (contador == 3) {
            if (pos_btn_D.position.x < ((Screen.width / 2) - xf)) {
                pos_btn_D.position += new Vector3 (Time.deltaTime * velocidad, 0f, 0f);
            }
            if (pos_btn_D.position.x > ((Screen.width / 2) - xf) && pos_btn_D.position.y < (Screen.height + yf)) {
                pos_btn_D.position += new Vector3 (0f, Time.deltaTime * velocidad, 0f);
                if (pos_btn_D.position.y > (Screen.height + yf) && contador_r > 1) {
                    contador_r--;
                    Destroy (pos_btn_D.gameObject);
                    InstanciarDerSuperior ();
                    Debug.Log (contador_r);
                }
            }
        }

        if (contador >= 4) {
            txtMsg.enabled = true;
            if (Input.GetKeyDown (KeyCode.R)) {
                btnPrincipal.GetComponentInChildren<Text> ().text = "";
                SceneManager.LoadScene (1);
            }
        }

    }

    void datosPosicion (int id) {
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
            Debug.Log (yi);
        }
        WAV sonido = new WAV (son);
        AudioClip audioClip = AudioClip.Create ("Sonido", sonido.SampleCount, 1, sonido.Frequency, false, false);
        audioClip.SetData (sonido.LeftChannel, 0);
        audioUbicacion.clip = audioClip;
        //audioA.Play();
        reader.Close ();
        reader = null;
        dbcmd.Dispose ();
        dbcmd = null;
        dbconn.Close ();
    }
}