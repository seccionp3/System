using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BASICOa : MonoBehaviour {
    public RectTransform panelA, panelB;
    private Image imgA, imgB;
    public Button btnPrincipal;

    private Transform pos_btn_A, pos_btn_B;

    private GameObject btnIzq;
    private int contador = 0, contador_r;
    //private int r = 3;
    private int r = conf_ini.num_repeticiones;
    private float velocidad = conf_ini.velocidad_boton;
    //private float velocidad = 100f;

    //Datos de la Base de Datos.
    public AudioSource audioUbicacion;
    private int xi;

    public Text txtMsg;

    // Use this for initialization
    void Start () {

        Debug.Log ("Velocidad = " + velocidad);
        Debug.Log ("Repeticiones = " + r);
        Debug.Log ("Color 1 = " + conf_ini.a);
        Debug.Log ("Color 2 = " + conf_ini.b);
        datosPosicion (1);
        txtMsg.enabled = false;
        contador_r = 1;
        Debug.Log (velocidad);
        InstanciarIzq ();
        //InstanciarDer();
        imgA = panelA.GetComponent<Image> ();
        imgB = panelB.GetComponent<Image> ();
        colors (conf_ini.a, imgA);
        //colors("azul", imgA);
    }

    public void InstanciarIzq () {

        //POSICIONES INICIALES
        btnPrincipal.GetComponentInChildren<Text> ().text = "IZQUIERDA";
        //btnPrincipal.onClick.AddListener(contar);
        //btnIzq = Instantiate(btnPrincipal.gameObject,new Vector3(-100,(Screen.height/2),0f),transform.rotation);
        btnIzq = Instantiate (btnPrincipal.gameObject, new Vector3 (xi, (Screen.height / 2), 0f), transform.rotation);
        Debug.Log ("contador_r = " + contador_r + " y r = " + r);
        btnIzq.transform.SetParent (this.transform);
        pos_btn_A = btnIzq.GetComponent<Transform> ();
        if (contador_r == r) {
            pos_btn_A.GetComponent<Button> ().onClick.AddListener (contar);
        }
        audioUbicacion.Play ();
        //Debug.Log("Xa = "+ pos_btn_A.position.x + "Ya = " + pos_btn_A.position.y);

    }
    public void InstanciarDer () {
        btnPrincipal.GetComponentInChildren<Text> ().text = "DERECHA";
        GameObject btnDer = Instantiate (btnPrincipal.gameObject, new Vector3 (Screen.width -600, (Screen.height / 2), 0f), transform.rotation);
        btnDer.transform.SetParent (this.transform);
        pos_btn_B = btnDer.GetComponent<Transform> ();
        if (contador_r == 2) {
            pos_btn_B.GetComponent<Button> ().onClick.AddListener (contar);
        }
        audioUbicacion.Play ();
    }

    // Update is called once per frame
    void Update () {
        if (contador == 0) {
            if (pos_btn_A.position.x > ((Screen.width / 2) - xi)) {
                //AudioSource.PlayClipAtPoint(Sonido, Posicion.position, Volumen);
                pos_btn_A.position += new Vector3 (-Time.deltaTime * velocidad, 0f, 0f);
            }
            if (pos_btn_A.position.x <= ((Screen.width / 2) - xi)) {
                if (contador_r < r) {
                    contador_r++;
                    Destroy (pos_btn_A.gameObject);
                    InstanciarIzq ();
                    //Debug.Log(contador_r);
                }
            }
        }
        if (contador == 1) {

            if (pos_btn_B.position.x < ((Screen.width / 2) - xi)) {
                pos_btn_B.position += new Vector3 (Time.deltaTime * velocidad, 0f, 0f);
            }
            if (pos_btn_B.position.x >= ((Screen.width / 2) - xi)) {
                if (contador_r > 1) {
                    Destroy (pos_btn_B.gameObject);
                    InstanciarDer ();
                    contador_r--;
                    Debug.Log (contador_r);
                }
            }
        }

        if (contador >= 2) {
            txtMsg.enabled = true;
            if (Input.GetKeyDown (KeyCode.R)) {
                btnPrincipal.GetComponentInChildren<Text> ().text = "";
                SceneManager.LoadScene (1);
            }
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

    void contar () {
        contador++;
        Debug.Log (contador);
        if (contador == 1) {
            datosPosicion (2);
            imgA.color = Color.white;
            colors (conf_ini.b, imgB);
            Destroy (pos_btn_A.gameObject);
            InstanciarDer ();
        }
        if (contador == 2) {

            imgB.color = Color.white;
            Destroy (pos_btn_B.gameObject);
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
            Debug.Log (xi);
        }
        WAV sonido = new WAV (son);
        AudioClip audioClip = AudioClip.Create ("testSound", sonido.SampleCount, 1, sonido.Frequency, false, false);
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