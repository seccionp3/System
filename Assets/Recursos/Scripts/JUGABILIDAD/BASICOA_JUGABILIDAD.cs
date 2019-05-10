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

public class BASICOA_JUGABILIDAD : MonoBehaviour {

    //Tiempos
    public string tiempo_reaccion, tiempo_cuadrado, tiempo_boton;
    public int acierto_bd;
    public Text txtSalir;
    public GameObject btnSalir;
    public Text txtFinal;
    private int intentos = 3;
    private int estado_juego = 1; // 1 personaje 1 o 2 personaje 2
    private String nombre_ubicacion;
    private int id_personaje_1, id_personaje_2, id_boton_derecha, id_boton_izquierda;
    private int ACIERTO = 0;
    private Transform pos_btn_A, pos_btn_B;
    public GameObject btnPrincipal;
    public Text txtcontinuar;
    private int valorDerecha = 0, valorIzquierda = 0, valorContinuar = 0;
    private bool tocar_boton_derecho = true, tocar_boton_izquierdo = true;
    public Text tiempo_boton_Derecha;
    public Text tiempo_boton_Izquierda;

    public RectTransform panelA, panelB;
    private Image imgA, imgB;
    public AudioSource audioUbicacion;
    public AudioClip donde_esta, audio_personaje_1, audio_personaje_2;

    public AudioClip correcto, intenta_otra;

    public GameObject btnMsg;
    public GameObject btn_izquierda;
    private Texture2D texturaIzquierda;
    public GameObject btn_derecha;
    private Texture2D texturaDerecha;
    rgb colorIzq = new rgb ();
    rgb colorDer = new rgb ();
    private int codigo_detalle_aprendizaje_1 = LOGIN_JUGABILIDAD.codigosBasicoA.ElementAt (0);
    private int codigo_detalle_aprendizaje_2 = LOGIN_JUGABILIDAD.codigosBasicoA.ElementAt (1);

    public  string tiempoTranscurrido;
    public  float startTime;
    public  float stopTime;
    public  float timerTime;
    public  bool isRunning = false;

    // Use this for initialization
    void Start () {
        TimerReset ();
        texturaIzquierda = new Texture2D (256, 256);
        texturaDerecha = new Texture2D (256, 256);
        txtcontinuar.text = "";
        txtFinal.text = "";
        btnSalir.SetActive (false);
        txtSalir.text = "";
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
        		timerTime = stopTime + (Time.time - startTime);
		int minutesInt = (int) timerTime / 60;
		int secondsInt = (int) timerTime % 60;
		int seconds100Int = (int) (Mathf.Floor ((timerTime - (secondsInt + minutesInt * 60)) * 100));

		if (isRunning) {
			tiempoTranscurrido = string.Format ("{00}:{01}:{02}", minutesInt.ToString(), secondsInt.ToString(), seconds100Int.ToString());
		}

    }
    public void TimerStart () {
        if (!isRunning) {
            Debug.Log ("START");
            isRunning = true;
            startTime = Time.time;
        }
    }

    public void TimerStop () {
        if (isRunning) {
            Debug.Log ("STOP");
            isRunning = false;
            stopTime = timerTime;
        }
    }

    public void TimerReset () {
        Debug.Log ("RESET");
        stopTime = 0;
        isRunning = false;
        //timerMinutes.text = timerSeconds.text = timerSeconds100.text = "00";
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
            if (nombre_ubicacion == "derecha") {
                id_boton_derecha = id_personaje_1;
                colorDer.r = reader.GetInt32 (0);
                colorDer.g = reader.GetInt32 (1);
                colorDer.b = reader.GetInt32 (2);
            } else {
                id_boton_izquierda = id_personaje_1;
                colorIzq.r = reader.GetInt32 (0);
                colorIzq.g = reader.GetInt32 (1);
                colorIzq.b = reader.GetInt32 (2);
            }
        }
        Debug.Log (colorIzq.r);
        WAV sonido = new WAV (son);
        audio_personaje_1 = AudioClip.Create ("personaje_1", sonido.SampleCount, 1, sonido.Frequency, false, false);
        audio_personaje_1.SetData (sonido.LeftChannel, 0);
        if (nombre_ubicacion == "derecha") {
            texturaDerecha.LoadImage (imagen_personaje);
        } else {
            texturaIzquierda.LoadImage (imagen_personaje);
        }
        reader.Close ();
        reader = null;
        dbcmd.Dispose ();
        dbcmd = null;
        dbconn.Close ();
        Debug.Log ("Ya salio de la base de datos");
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
            if (nombre_ubicacion == "derecha") {
                id_boton_derecha = id_personaje_2;
                colorDer.r = reader.GetInt32 (0);
                colorDer.g = reader.GetInt32 (1);
                colorDer.b = reader.GetInt32 (2);
            } else {
                id_boton_izquierda = id_personaje_2;
                colorIzq.r = reader.GetInt32 (0);
                colorIzq.g = reader.GetInt32 (1);
                colorIzq.b = reader.GetInt32 (2);
            }
        }
        WAV sonido = new WAV (son);
        audio_personaje_2 = AudioClip.Create ("personaje_2", sonido.SampleCount, 1, sonido.Frequency, false, false);
        audio_personaje_2.SetData (sonido.LeftChannel, 0);
        if (nombre_ubicacion == "derecha") {
            texturaDerecha.LoadImage (imagen_personaje);
        } else {
            texturaIzquierda.LoadImage (imagen_personaje);
        }
        reader.Close ();
        reader = null;
        dbcmd.Dispose ();
        dbcmd = null;
        dbconn.Close ();

    }
    public void interaccionPanelA () {
        //Debug.Log( "Tiempo del Cronometro = " + CRONOMETRO.cuentaAtras );
        imgA.color = new Color (colorIzq.r, colorIzq.g, colorIzq.b);
        // Guardar tiempo uno;
        Debug.Log (CRONOMETRO.tiempoTranscurrido);
        if (tiempo_cuadrado == null) {
            tiempo_cuadrado = CRONOMETRO.tiempoTranscurrido;
            CRONOMETRO_PANEL.TimerStart ();
        }

    }
    public void interaccionPanelASalir () {
        imgA.color = new Color (255, 255, 255);
        // Guardar tiempo uno;

    }

    public void interccionPanelB () {
        imgB.color = new Color (colorDer.r, colorDer.g, colorDer.b);
        Debug.Log (CRONOMETRO.tiempoTranscurrido);
        if (tiempo_cuadrado == null) {
            tiempo_cuadrado = CRONOMETRO.tiempoTranscurrido;
            CRONOMETRO_PANEL.TimerStart ();
        }
    }
    public void interccionPanelBSalir () {
        imgB.color = new Color (255, 255, 255);
    }

    public void VerPersonaje1 () {
        //btn_izquierda.GetComponent<Image>().sprite = Sprite.Create(texturaIzquierda, new Rect(0,0,256,256), new Vector2(0.5f,0.5f));
    }

    public void VerPersonaje2 () {
        //btn_derecha.GetComponent<Image>().sprite = Sprite.Create(texturaDerecha, new Rect(0,0,256,256), new Vector2(0.5f,0.5f));
    }

    public void botonderecho () {

        valorContinuar = 0;
        valorIzquierda = 0;
        tocar_boton_izquierdo = true;
        if (tocar_boton_derecho) {
            valorDerecha++;
        }
        if (valorDerecha >= 100) {
            //btn_derecha.GetComponent<Renderer>().material.color = Color.red;
            tocar_boton_derecho = false;
            //Indicar Imagen
            InstanciarDer ();
            btn_izquierda.SetActive (false);
            btn_derecha.SetActive (false);
            reiniciar ();
            btnMsg.SetActive (true);
            txtcontinuar.text = "CONTINUAR ";

            Debug.Log (" ID DERECHA  " + id_boton_derecha);
            if (estado_juego == 1 && intentos > 0) {
                intentos--;
                if (id_personaje_1 == id_boton_derecha) {
                    Debug.Log (" ** ACIERTO **  ");
                    //Tiempo a boton
                    Debug.Log (CRONOMETRO_PANEL.tiempoTranscurrido);
                    tiempo_boton = CRONOMETRO.tiempoTranscurrido;
                    audioUbicacion.clip = correcto;
                    audioUbicacion.Play ();
                    ACIERTO = 1;
                } else {
                    Debug.Log ("--  NO ACIERTO -- ");
                    //Tiempo a boton
                    Debug.Log (CRONOMETRO_PANEL.tiempoTranscurrido);
                    tiempo_boton = CRONOMETRO_PANEL.tiempoTranscurrido;
                    audioUbicacion.clip = intenta_otra;
                    audioUbicacion.Play ();
                    ACIERTO = 0;
                }

                Debug.Log ("INTENTOS =>> " + intentos);
            }

            if (estado_juego == 2 && intentos > 0) {
                intentos--;
                if (id_personaje_2 == id_boton_derecha) {
                    Debug.Log (" ** ACIERTO **  ");
                    //Tiempo a boton
                    Debug.Log (CRONOMETRO_PANEL.tiempoTranscurrido);
                    tiempo_boton = CRONOMETRO_PANEL.tiempoTranscurrido;
                    audioUbicacion.clip = correcto;
                    audioUbicacion.Play ();
                    ACIERTO = 3;
                } else {
                    Debug.Log ("--  NO ACIERTO -- ");
                    //Tiempo a boton
                    Debug.Log (CRONOMETRO_PANEL.tiempoTranscurrido);
                    tiempo_boton = CRONOMETRO_PANEL.tiempoTranscurrido;
                    audioUbicacion.clip = intenta_otra;
                    ACIERTO = 2;
                    audioUbicacion.Play ();
                }
                Debug.Log ("INTENTOS =>> " + intentos);
            }

        }
        tiempo_boton_Derecha.text = "" + valorDerecha;
        tiempo_boton_Izquierda.text = "" + valorIzquierda;
    }

    public void botonIzquierdo () {
        valorContinuar = 0;
        valorDerecha = 0;
        tocar_boton_derecho = true;
        if (tocar_boton_izquierdo) {
            valorIzquierda++;
        }
        if (valorIzquierda >= 100) {
            InstanciarIzq ();
            //btn_izquierda.GetComponent<Renderer>().material.color = Color.red;
            tocar_boton_izquierdo = false;
            //Ocultar Botones
            btn_izquierda.SetActive (false);
            btn_derecha.SetActive (false);
            reiniciar ();
            btnMsg.SetActive (true);
            txtcontinuar.text = "CONTINUAR ";

            Debug.Log (" id izquierda " + id_boton_izquierda);
            if (estado_juego == 1 && intentos > 0) {
                intentos--;
                if (id_personaje_1 == id_boton_izquierda) {
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
                Debug.Log ("INTENTOS =>> " + intentos);
            } else {
                Debug.Log ("Se acabaron los Intentos");
            }

            if (estado_juego == 2 && intentos > 0) {
                intentos--;
                if (id_personaje_2 == id_boton_izquierda) {
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
                Debug.Log ("INTENTOS =>> " + intentos);
            } else {
                Debug.Log ("Se acabaron los intentos");
            }

        }
        tiempo_boton_Derecha.text = "" + valorDerecha;
        tiempo_boton_Izquierda.text = "" + valorIzquierda;
    }

    public void continuar () {
        tiempo_boton_Derecha.text = "";
        tiempo_boton_Izquierda.text = "";
        valorContinuar++;
        txtcontinuar.text = "CONTINUAR " + valorContinuar;
        if (valorContinuar >= 100) {
            txtcontinuar.text = "";
            btnMsg.SetActive (false);
            btn_izquierda.SetActive (true);
            btn_derecha.SetActive (true);
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

            if (ACIERTO == 2 && intentos > 0) {
                StartCoroutine (playsound2 ());
                estado_juego = 2;
            }

            if (intentos <= 0 || ACIERTO == 3) {
                Debug.Log ("Se acabaron tus intentos");
                btn_izquierda.SetActive (false);
                btn_derecha.SetActive (false);
                txtFinal.text = "Fase terminada... presiona en continuar";
                btnSalir.SetActive (true);
                txtSalir.text = "Salir ";
                valorContinuar = 0;
            }

        }
    }

    void reiniciar () {
        valorDerecha = 0;
        valorIzquierda = 0;
        //btn_izquierda.GetComponent<Renderer>().material.color = Color.gray;
        //btn_derecha.GetComponent<Renderer>().material.color = Color.gray;
        tocar_boton_izquierdo = true;
        tocar_boton_derecho = true;
        tiempo_boton_Derecha.text = "B";
        tiempo_boton_Izquierda.text = "A";
    }

    public void botonSalir () {

        valorContinuar++;
        txtSalir.text = "SALIR " + valorContinuar;
        if (valorContinuar >= 100) {
            valorContinuar = 0;
            SceneManager.LoadScene (15);
        }

    }

    public void InstanciarIzq () {
        btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (texturaIzquierda, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
        GameObject btnIzq = Instantiate (btnPrincipal.gameObject, new Vector3 (-0.1f, 0f, 0.1f), transform.rotation);
        btnIzq.transform.SetParent (this.transform);
        btnIzq.transform.localScale = new Vector3 (1f, 1f, 0f);
        pos_btn_A = btnIzq.GetComponent<Transform> ();
    }
    public void InstanciarDer () {
        btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (texturaDerecha, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
        GameObject btnDer = Instantiate (btnPrincipal.gameObject, new Vector3 (0.1f, 0f, 0.1f), transform.rotation);
        btnDer.transform.SetParent (this.transform);
        btnDer.transform.localScale = new Vector3 (1f, 1f, 0f);
        pos_btn_B = btnDer.GetComponent<Transform> ();
    }

    IEnumerator playsound () {
        Debug.Log ("reproducuiendo......");
        audioUbicacion.clip = donde_esta;
        audioUbicacion.Play ();
        yield return new WaitForSeconds (audioUbicacion.clip.length);
        audioUbicacion.clip = audio_personaje_1;
        audioUbicacion.Play ();
        //INICIAR TIEMPO 1
        TimerStart ();

    }

    IEnumerator playsound2 () {
        Debug.Log ("reproducuiendo......");
        audioUbicacion.clip = donde_esta;
        audioUbicacion.Play ();
        yield return new WaitForSeconds (audioUbicacion.clip.length);
        audioUbicacion.clip = audio_personaje_2;
        audioUbicacion.Play ();
    }

    public void saveAcierto (int id_detalle_aprendizaje, string time1, string time2, string time3, int acierto) {
        string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/dbdata.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection (conn);
        dbconn.Open ();
        IDbCommand dbcmd = dbconn.CreateCommand ();
        string sqlQuery = "INSERT INTO detalle_aprendizaje_acierto (tiempo_reaccion,tiempo_cuadro, tiempo_boton, acierto, id_detalle_aprendozaje) Values ('" + time1 + "','" + time2 + "','" + time3 + "' , '" + acierto + "' , '" + id_detalle_aprendizaje + "' )";
        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteReader ();
        Debug.Log ("Datos Guardados Corectamente!..");
        dbcmd.Dispose ();
        dbcmd = null;
        dbconn.Close ();
        dbconn = null;
    }

}