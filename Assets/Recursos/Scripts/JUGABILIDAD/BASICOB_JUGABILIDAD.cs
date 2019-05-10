using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BASICOB_JUGABILIDAD : MonoBehaviour {
	    public Text txtSalir;
    public GameObject btnSalir;
    public Text txtFinal;
    private int intentos = 3;
    private int estado_juego = 1; // 1 personaje 1 o 2 personaje 2
    private String nombre_ubicacion;
    private int id_personaje_1, id_personaje_2, id_boton_superior, id_boton_inferior;
    private int ACIERTO = 0;
    private Transform pos_btn_A, pos_btn_B;
    public GameObject btnPrincipal;
    public Text txtcontinuar;
    private int valorSuperior = 0, valorInferior = 0, valorContinuar = 0;
    private bool tocar_boton_superior = true, tocar_boton_inferior = true;
    public Text tiempo_boton_Superior;
    public Text tiempo_boton_Inferior;

    public RectTransform panelA, panelB;
    private Image imgA, imgB;
    public AudioSource audioUbicacion;
    public AudioClip donde_esta, audio_personaje_1, audio_personaje_2;

    public AudioClip correcto, intenta_otra;

    public GameObject btnMsg;
    public GameObject btn_Inferior;
    private Texture2D texturaSuperior;
    public GameObject btn_Superior;
    private Texture2D texturaInferior;
    rgb colorSup = new rgb ();
    rgb colorInf = new rgb ();
    private int codigo_detalle_aprendizaje_1 = LOGIN_JUGABILIDAD.codigosBasicoB.ElementAt(0);
    private int codigo_detalle_aprendizaje_2 = LOGIN_JUGABILIDAD.codigosBasicoB.ElementAt(1);

    // Use this for initialization
    void Start () {
        texturaInferior = new Texture2D (256, 256);
        texturaSuperior = new Texture2D (256, 256);
        txtcontinuar.text = "";
        txtFinal.text = "";
        btnSalir.SetActive(false);
        btnMsg.SetActive (false);
        Debug.Log (codigo_detalle_aprendizaje_1);
        Debug.Log (codigo_detalle_aprendizaje_2);
        imgA = panelA.GetComponent<Image> ();
        imgB = panelB.GetComponent<Image> ();
        obtenerDatos1 (codigo_detalle_aprendizaje_1);
        obtenerDatos2 (codigo_detalle_aprendizaje_2);
    }

    // Update is called once per frame
    void Update () {

    }

    public void siguienteElemento () {

    }

    //colores guardados.
    private void obtenerDatos1 (int id) {
        Debug.Log ("funcion con id " + id);
        byte[] son = new byte[0];
        byte[] imagen_personaje = new byte[0];
        string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/dbdata.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection (conn);
        dbconn.Open ();
        IDbCommand dbcmd = dbconn.CreateCommand ();
        string sqlQuery;

        sqlQuery = "select t2.r_color, t2.g_color, t2.b_color, t3.id_personaje, t4.nombre_ubicacion,  t3.audio_personaje, t3.imagen_personaje from detalle_aprendizaje as t1 inner join color as t2 on t2.id = t1.id_color inner join personaje as t3 on t1.id_personaje = t3.id_personaje inner join ubicacion as t4 on t1.id_ubicacion = t4.id where t1.id_detalle_apre = " + id;
        //"select color.r_color, color.g_color, color.b_color, ubicacion.nombre_ubicacion, tpersonaje.audio_personaje, tpersonaje.imagen_personaje from detalle_aprendizaje  as detalle_aprendizaje inner join color on color.id = id_color inner join ubicacion on ubicacion.id = id_ubicacion inner join personaje as tpersonaje on tpersonaje.id_personaje = detalle_aprendizaje.id_personaje where id_detalle_apre = " + id;
        Debug.Log (sqlQuery);
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader ();
        while (reader.Read ()) {
            nombre_ubicacion = reader.GetString (4);
            id_personaje_1 = reader.GetInt32 (3);
            imagen_personaje = (byte[]) reader["imagen_personaje"];
            son = (byte[]) reader["audio_personaje"];
            if (nombre_ubicacion == "arriba") {
                id_boton_superior = id_personaje_1;
                colorSup.r = reader.GetInt32 (0);
                colorSup.g = reader.GetInt32 (1);
                colorSup.b = reader.GetInt32 (2);
            } else {
                id_boton_inferior = id_personaje_1;
                colorInf.r = reader.GetInt32 (0);
                colorInf.g = reader.GetInt32 (1);
                colorInf.b = reader.GetInt32 (2);
            }
        }
        WAV sonido = new WAV (son);
        audio_personaje_1 = AudioClip.Create ("personaje_1", sonido.SampleCount, 1, sonido.Frequency, false, false);
        audio_personaje_1.SetData (sonido.LeftChannel, 0);
        if (nombre_ubicacion == "arriba") {
            texturaSuperior.LoadImage (imagen_personaje);
        } else {
            texturaInferior.LoadImage (imagen_personaje);
        }
        reader.Close ();
        reader = null;
        dbcmd.Dispose ();
        dbcmd = null;
        dbconn.Close ();
        nombre_ubicacion = "";
        StartCoroutine (playsound ());
    }
    private void obtenerDatos2 (int id) {
        byte[] son = new byte[0];
        byte[] imagen_personaje = new byte[0];
        string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/dbdata.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection (conn);
        dbconn.Open ();
        IDbCommand dbcmd = dbconn.CreateCommand ();
        string sqlQuery;
        sqlQuery = "select t2.r_color, t2.g_color, t2.b_color, t3.id_personaje, t4.nombre_ubicacion,  t3.audio_personaje, t3.imagen_personaje from detalle_aprendizaje as t1 inner join color as t2 on t2.id = t1.id_color inner join personaje as t3 on t1.id_personaje = t3.id_personaje inner join ubicacion as t4 on t1.id_ubicacion = t4.id where t1.id_detalle_apre = " + id;
        Debug.Log (sqlQuery);
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader ();
        while (reader.Read ()) {
            id_personaje_2 = reader.GetInt32 (3);
            nombre_ubicacion = reader.GetString (4);
            son = (byte[]) reader["audio_personaje"];
            imagen_personaje = (byte[]) reader["imagen_personaje"];
            Debug.Log (id_personaje_2);
            if (nombre_ubicacion == "arriba") {
                id_boton_superior = id_personaje_2;
                colorSup.r = reader.GetInt32 (0);
                colorSup.g = reader.GetInt32 (1);
                colorSup.b = reader.GetInt32 (2);
            } else {
                id_boton_inferior = id_personaje_2;
                colorInf.r = reader.GetInt32 (0);
                colorInf.g = reader.GetInt32 (1);
                colorInf.b = reader.GetInt32 (2);
            }
        }
        WAV sonido = new WAV (son);
        audio_personaje_2 = AudioClip.Create ("personaje_2", sonido.SampleCount, 1, sonido.Frequency, false, false);
        audio_personaje_2.SetData (sonido.LeftChannel, 0);
        if (nombre_ubicacion == "arriba") {
            texturaSuperior.LoadImage (imagen_personaje);
        } else {
            texturaInferior.LoadImage (imagen_personaje);
        }
        reader.Close ();
        reader = null;
        dbcmd.Dispose ();
        dbcmd = null;
        dbconn.Close ();

    }
    public void interaccionPanelA () {
        //Debug.Log( "Tiempo del Cronometro = " + CRONOMETRO.cuentaAtras );
        imgA.color = new Color (colorSup.r, colorSup.g, colorSup.b);
    }
    public void interaccionPanelASalir () {
        imgA.color = new Color (255, 255, 255);
    }

    public void interccionPanelB () {
        imgB.color = new Color (colorInf.r, colorInf.g, colorInf.b);
    }
    public void interccionPanelBSalir () {
        imgB.color = new Color (255, 255, 255);
    }

    public void botonSuperior () {
        valorContinuar = 0;
        valorInferior = 0;
        tocar_boton_inferior = true;
        if (tocar_boton_superior) {
            valorSuperior++;
        }
        if (valorSuperior >= 100) {
            tocar_boton_superior = false;
            //Indicar Imagen
            InstanciaSuperior();
            btn_Superior.SetActive (false);
            btn_Inferior.SetActive (false);
            reiniciar ();
            btnMsg.SetActive (true);
            txtcontinuar.text = "CONTINUAR ";

            //Debug.Log (" ID DERECHA  " + id_boton_derecha);
            if (estado_juego == 1 && intentos > 0 ) {
                    intentos--;
                if (id_personaje_1 == id_boton_superior) {
                    Debug.Log (" ** ACIERTO **  ");
                    audioUbicacion.clip = correcto;
                    audioUbicacion.Play ();
                    ACIERTO = 1;
                } else {
                    Debug.Log ("--  NO ACIERTO -- ");
                    audioUbicacion.clip = intenta_otra;
                    audioUbicacion.Play ();
                    ACIERTO = 0;
                }

                Debug.Log("INTENTOS =>> " + intentos);
            }

            if (estado_juego == 2 && intentos > 0 ) {
                    intentos--;
                if (id_personaje_2 == id_boton_superior) {
                    Debug.Log (" ** ACIERTO **  ");
                    audioUbicacion.clip = correcto;
                    audioUbicacion.Play ();
                    ACIERTO = 3;
                } else {
                    Debug.Log ("--  NO ACIERTO -- ");
                    audioUbicacion.clip = intenta_otra;
                    ACIERTO = 2;
                    audioUbicacion.Play ();
                }
                Debug.Log("INTENTOS =>> " + intentos);
            }

        }
        tiempo_boton_Superior.text = "" + valorSuperior;
        tiempo_boton_Inferior.text = "" + valorInferior;
    }

    public void botonInferior () {
        valorContinuar = 0;
        valorSuperior = 0;
        tocar_boton_superior = true;
        if (tocar_boton_inferior) {
            valorInferior++;
        }
        if (valorInferior >= 100) {
            InstanciaInferior ();
            tocar_boton_inferior = false;
            //Ocultar Botones
            btn_Inferior.SetActive (false);
            btn_Superior.SetActive (false);
            reiniciar ();
            btnMsg.SetActive (true);
            txtcontinuar.text = "CONTINUAR ";

            
            if (estado_juego == 1 && intentos > 0) {
                    intentos--;
                if (id_personaje_1 == id_boton_inferior) {
                    Debug.Log (" ** ACIERTO **  ");
                    audioUbicacion.clip = correcto;
                    audioUbicacion.Play ();
                    ACIERTO = 1;
                } else {
                    Debug.Log ("--  NO ACIERTO -- ");
                    audioUbicacion.clip = intenta_otra;
                    audioUbicacion.Play ();
                    ACIERTO = 0;
                }
                Debug.Log("INTENTOS =>> " + intentos);
            }else
            {
                Debug.Log("Se acabaron los Intentos");
            }

            if (estado_juego == 2 && intentos > 0) {
                    intentos--;
                if (id_personaje_2 == id_boton_inferior) {
                    Debug.Log (" ** ACIERTO **  ");
                    audioUbicacion.clip = correcto;
                    audioUbicacion.Play ();
                    ACIERTO = 3;
                } else {
                    Debug.Log ("--  NO ACIERTO -- ");
                    audioUbicacion.clip = intenta_otra;
                    audioUbicacion.Play ();
                    ACIERTO = 2;
                }
                Debug.Log("INTENTOS =>> " + intentos);
            }else
            {
                Debug.Log("Se acabaron los intentos");
            }

        }
        tiempo_boton_Superior.text = "" + valorSuperior;
        tiempo_boton_Inferior.text = "" + valorInferior;
    }

    public void continuar () {
        tiempo_boton_Superior.text = "";
        tiempo_boton_Inferior.text = "";
        valorContinuar++;
        txtcontinuar.text = "CONTINUAR " + valorContinuar;
        if (valorContinuar >= 100) {
            txtcontinuar.text = "";
            btnMsg.SetActive (false);
            btn_Inferior.SetActive (true);
            btn_Superior.SetActive (true);
            if (pos_btn_A != null) {
                pos_btn_A.gameObject.SetActive (false);
            }
            if (pos_btn_B != null) {
                pos_btn_B.gameObject.SetActive (false);
            }

            if (ACIERTO == 0 && intentos > 0) {
                StartCoroutine (playsound ());
            }

            if (ACIERTO == 1 && intentos > 0) {
                StartCoroutine (playsound2 ());
                estado_juego = 2;
            }

            if (ACIERTO == 2 && intentos > 0){
                StartCoroutine (playsound2 ());
                estado_juego = 2;
            }

            if(intentos <= 0 || ACIERTO == 3){
                Debug.Log("Se acabaron tus intentos");
                btn_Inferior.SetActive (false);
                btn_Superior.SetActive (false);
                txtFinal.text = "Fase terminada... presiona en salir";
				btnSalir.SetActive(true);
                txtSalir.text = "Salir ";
                valorContinuar = 0;
            }


        }
    }

    void reiniciar () {
        valorSuperior = 0;
        valorInferior = 0;
        //btn_izquierda.GetComponent<Renderer>().material.color = Color.gray;
        //btn_derecha.GetComponent<Renderer>().material.color = Color.gray;
        tocar_boton_inferior = true;
        tocar_boton_superior = true;
        tiempo_boton_Superior.text = "B";
        tiempo_boton_Inferior.text = "A";
    }

     public void botonSalir () {
        
        valorContinuar++;
        txtSalir.text = "SALIR " + valorContinuar;
        if(valorContinuar >= 100){
             valorContinuar = 0;
             SceneManager.LoadScene (15);
        }
    

    }
    public void InstanciaSuperior () {
        btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (texturaSuperior, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
        GameObject imgSuperior = Instantiate (btnPrincipal.gameObject, new Vector3 (0f, 0.05f, 0.1f), transform.rotation);
        imgSuperior.transform.SetParent (this.transform);
        imgSuperior.transform.localScale = new Vector3 (1f, 1f, 0f);
        pos_btn_A = imgSuperior.GetComponent<Transform> ();
    }
    public void InstanciaInferior () {
        btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (texturaInferior, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
        GameObject imgInferior = Instantiate (btnPrincipal.gameObject, new Vector3 (0f, -0.05f, 0.11f), transform.rotation);
        imgInferior.transform.SetParent (this.transform);
        imgInferior.transform.localScale = new Vector3 (1f, 1f, 0f);
        pos_btn_B = imgInferior.GetComponent<Transform> ();
    }

    IEnumerator playsound () {
        Debug.Log ("reproducuiendo......");
        audioUbicacion.clip = donde_esta;
        audioUbicacion.Play ();
        yield return new WaitForSeconds (audioUbicacion.clip.length);
        audioUbicacion.clip = audio_personaje_1;
        audioUbicacion.Play ();
        //INICIAR TIEMPO 1

        

    }

    IEnumerator playsound2 () {
        Debug.Log ("reproducuiendo......");
        audioUbicacion.clip = donde_esta;
        audioUbicacion.Play ();
        yield return new WaitForSeconds (audioUbicacion.clip.length);
        audioUbicacion.clip = audio_personaje_2;
        audioUbicacion.Play ();
    }

}
