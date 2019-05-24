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

	//Tiempos
	private string tiempo_reaccion, tiempo_cuadrado, tiempo_boton, acierto;
	public Text timerText;
	private float startTime;
	public GameObject HandRight;
	public GameObject HandLeft;
	private bool finalisacion = false;
	private int contador = 0;
	private string id_boton="";
	private int contadorBoton;

	public Text txtSalir;
    public GameObject btnSalir;
    public Text txtFinal;
    private int intentos = 3;
	private int intentos_fallos = 0;
	public int intentos_boton_seleccionado  = 0 ;
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

	public AudioClip correcto, intenta_otra, seleciona_otra_vez;

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
		iniciarReloj ();
        texturaInferior = new Texture2D (256, 256);
        texturaSuperior = new Texture2D (256, 256);
        txtcontinuar.text = "Iniciar";
        txtFinal.text = "";
        btnSalir.SetActive(false);
		btnMsg.SetActive (true);
        Debug.Log (codigo_detalle_aprendizaje_1);
        Debug.Log (codigo_detalle_aprendizaje_2);
        imgA = panelA.GetComponent<Image> ();
        imgB = panelB.GetComponent<Image> ();
        obtenerDatos1 (codigo_detalle_aprendizaje_1);
        obtenerDatos2 (codigo_detalle_aprendizaje_2);
    }

    // Update is called once per frame
    void Update () {
		procesoReloj();

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
		if (valorSuperior == 1)
		{
			finalisarReloj();
			tiempo_cuadrado = timerText.text;
			Debug.Log(tiempo_cuadrado);
			reinicioReloj();
		}
        if (valorSuperior >= 50) {
			tiempo_boton = timerText.text;
			Debug.Log(tiempo_boton);
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
				// intentos--;

       
					if (id_personaje_1 == id_boton_superior) {
						Debug.Log (" ** ACIERTO **  ");
						acierto = "acierto";
						audioUbicacion.clip = correcto;
						audioUbicacion.Play ();
						ACIERTO = 1;
						intentos_boton_seleccionado++;
						Debug.Log (" **  intentos para boto **  " + intentos_boton_seleccionado);

						if (intentos_boton_seleccionado == 1) {
							id_boton = "Boton Normal";
							btn_Superior.transform.localScale += new Vector3 (0.05F, 0.05F, 0.0F);
							StartCoroutine (playsoundOtravez (audio_personaje_1));
						}
						
						if (intentos_boton_seleccionado == 2) {
							id_boton = "Boton Grande";
							btn_Superior.transform.localScale -= new Vector3 (0.05F, 0.05F, 0.0F);
							StartCoroutine (playsoundOtravez (audio_personaje_1));

						}
						if (intentos_boton_seleccionado == 3) {
							id_boton = "Boton Mediano";
							btn_Superior.transform.localScale -= new Vector3 (0.05F, 0.05F, 0.0F);
							StartCoroutine (playsoundOtravez (audio_personaje_1));
							intentos--;
					}else if(contadorBoton==1){
						id_boton = "Boton Pequeño";
						contadorBoton = 0;
					}
						
                } else {
                    Debug.Log ("--  NO ACIERTO -- ");
					acierto = "no acierto";
                    audioUbicacion.clip = intenta_otra;
                    audioUbicacion.Play ();
                    ACIERTO = 0;
					intentos_fallos++;
                }
				saveAcierto(codigo_detalle_aprendizaje_1, tiempo_reaccion, tiempo_cuadrado, tiempo_boton, acierto);
                Debug.Log("INTENTOS =>> " + intentos);
            }

            if (estado_juego == 2 && intentos > 0 ) {
                    //intentos--;
                if (id_personaje_2 == id_boton_superior) {
                    Debug.Log (" ** ACIERTO **  ");
					acierto = "acierto";
                    audioUbicacion.clip = correcto;
                    audioUbicacion.Play ();
                    ACIERTO = 3;
						intentos_boton_seleccionado++;
						Debug.Log (" **  intentos para boto **  " + intentos_boton_seleccionado);

						if (intentos_boton_seleccionado == 1) {
							id_boton = "Boton Normal";
							btn_Superior.transform.localScale += new Vector3 (0.05F, 0.05F, 0.0F);
							StartCoroutine (playsoundOtravez (audio_personaje_2));
						}

						if (intentos_boton_seleccionado == 2) {
							id_boton = "Boton Grande";
							btn_Superior.transform.localScale -= new Vector3 (0.05F, 0.05F, 0.0F);
							StartCoroutine (playsoundOtravez (audio_personaje_2));

						}
						if (intentos_boton_seleccionado == 3) {
							id_boton = "Boton Mediano";
							btn_Superior.transform.localScale -= new Vector3 (0.05F, 0.05F, 0.0F);
							StartCoroutine (playsoundOtravez (audio_personaje_2));
							intentos--;
					}else if(contadorBoton==2){
						id_boton = "Boton Pequeño";
					}

                } else {
                    Debug.Log ("--  NO ACIERTO -- ");
					acierto = "no acierto";
                    audioUbicacion.clip = intenta_otra;
                    ACIERTO = 2;
                    audioUbicacion.Play ();
					intentos_fallos++;
                }
				saveAcierto(codigo_detalle_aprendizaje_1, tiempo_reaccion, tiempo_cuadrado, tiempo_boton, acierto);
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
		if (valorInferior == 1)
		{
			finalisarReloj();
			tiempo_cuadrado = timerText.text;
			Debug.Log(tiempo_cuadrado);
			reinicioReloj();
		}
        if (valorInferior >= 50) {
			tiempo_boton = timerText.text;
			Debug.Log(tiempo_boton);
            InstanciaInferior ();
            tocar_boton_inferior = false;
            //Ocultar Botones
            btn_Inferior.SetActive (false);
            btn_Superior.SetActive (false);
            reiniciar ();
            btnMsg.SetActive (true);
            txtcontinuar.text = "CONTINUAR ";

            
            if (estado_juego == 1 && intentos > 0) {
                    //intentos--;
                if (id_personaje_1 == id_boton_inferior) {
                    Debug.Log (" ** ACIERTO **  ");
					acierto = "acierto";
                    audioUbicacion.clip = correcto;
                    audioUbicacion.Play ();
                    ACIERTO = 1;
						intentos_boton_seleccionado++;
						Debug.Log (" **  intentos para boto **  " + intentos_boton_seleccionado);

						if (intentos_boton_seleccionado == 1) {
							id_boton = "Boton Normal";
							btn_Inferior.transform.localScale += new Vector3 (0.05F, 0.05F, 0.0F);
							StartCoroutine (playsoundOtravez (audio_personaje_1));
						}

						if (intentos_boton_seleccionado == 2) {
							id_boton = "Boton Grande";
							btn_Inferior.transform.localScale -= new Vector3 (0.05F, 0.05F, 0.0F);
							StartCoroutine (playsoundOtravez (audio_personaje_1));

						}
						if (intentos_boton_seleccionado == 3) {
							id_boton = "Boton Mediano";
							btn_Inferior.transform.localScale -= new Vector3 (0.05F, 0.05F, 0.0F);
							StartCoroutine (playsoundOtravez (audio_personaje_1));
							intentos--;
					}else if(contadorBoton==1){
						id_boton = "Boton Pequeño";
					}
                } else {
                    Debug.Log ("--  NO ACIERTO -- ");
					acierto = "no acierto";
                    audioUbicacion.clip = intenta_otra;
                    audioUbicacion.Play ();
                    ACIERTO = 0;
					intentos_fallos++;
                }
				saveAcierto(codigo_detalle_aprendizaje_1, tiempo_reaccion, tiempo_cuadrado, tiempo_boton, acierto);
                Debug.Log("INTENTOS =>> " + intentos);
            }else
            {
                Debug.Log("Se acabaron los Intentos");
            }

            if (estado_juego == 2 && intentos > 0) {
                    //intentos--;
                if (id_personaje_2 == id_boton_inferior) {
                    Debug.Log (" ** ACIERTO **  ");
					acierto = "acierto";
                    audioUbicacion.clip = correcto;
                    audioUbicacion.Play ();
                    ACIERTO = 3;
						intentos_boton_seleccionado++;
						Debug.Log (" **  intentos para boto **  " + intentos_boton_seleccionado);

						if (intentos_boton_seleccionado == 1) {
							id_boton = "Boton Normal";
							btn_Inferior.transform.localScale += new Vector3 (0.05F, 0.05F, 0.0F);
							StartCoroutine (playsoundOtravez (audio_personaje_2));
						}
						
						if (intentos_boton_seleccionado == 2) {
							id_boton = "Boton Grande";
							btn_Inferior.transform.localScale -= new Vector3 (0.05F, 0.05F, 0.0F);
							StartCoroutine (playsoundOtravez (audio_personaje_2));

						}
						if (intentos_boton_seleccionado == 3) {
							id_boton = "Boton Mediano";
							btn_Inferior.transform.localScale -= new Vector3 (0.05F, 0.05F, 0.0F);
							StartCoroutine (playsoundOtravez (audio_personaje_2));
							intentos--;
					}else if(contadorBoton==1){
						id_boton = "Boton Pequeño";
					}
                } else {
                    Debug.Log ("--  NO ACIERTO -- ");
					acierto = "no acierto";
                    audioUbicacion.clip = intenta_otra;
                    audioUbicacion.Play ();
                    ACIERTO = 2;
					intentos_fallos++;
                }
				saveAcierto(codigo_detalle_aprendizaje_1, tiempo_reaccion, tiempo_cuadrado, tiempo_boton, acierto);
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
        if (valorContinuar >= 50) {
			reinicioReloj();
			iniciarReloj();
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

			if (ACIERTO == 0 && intentos > 0 && intentos_boton_seleccionado != 0 && intentos_fallos != 3) {
				StartCoroutine (playsound ());
			}

			if (ACIERTO == 1 && intentos > 0 && intentos_boton_seleccionado == 4 && intentos_fallos != 3) {
				StartCoroutine (playsound2 ());
				estado_juego = 2;
				intentos_boton_seleccionado = 0;
				//intentos++;
			}

			if (ACIERTO == 2 && intentos > 0 && intentos_fallos != 3 ) {
				StartCoroutine (playsound2 ());
				estado_juego = 2;
				intentos_boton_seleccionado = 0;
				//intentos++;
			}

			if (intentos_fallos >= 3) {
				Debug.Log ("Se acabaron tus intentos");
                BASICOA_REAPRENDIZAJE.codigoA = codigo_detalle_aprendizaje_1;
                BASICOA_REAPRENDIZAJE.codigoB = codigo_detalle_aprendizaje_2;
                btn_Superior.SetActive (false);
				btn_Inferior.SetActive (false);
				txtFinal.text = "Intententos Agotados, regrese a la seccion de aprendizaje";
				btnSalir.SetActive (true);
				txtSalir.text = "Salir ";
				valorContinuar = 0;
			}

			Debug.Log ("intenos = " + intentos + " acierto = " + ACIERTO + " INTENTO BOTON = " + intentos_boton_seleccionado);
			if (intentos == 1 && ACIERTO == 3 && intentos_boton_seleccionado == 4) {
				Debug.Log ("Se acabaron tus intentos");
				btn_Superior.SetActive (false);
				btn_Inferior.SetActive (false);
				txtFinal.text = "Fase terminada... presiona en continuar";
				btnSalir.SetActive (true);
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
        if(valorContinuar >= 50){
            if (intentos_fallos >= 3)
            {
                SceneManager.LoadScene(22);
            }
            else
            {
                SceneManager.LoadScene(15);
            }
             valorContinuar = 0;
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
		iniciarReloj();
		}		IEnumerator playsoundOtravez ( AudioClip audio_personaje) {
				Debug.Log ("reproducuiendo......");
				audioUbicacion.clip = seleciona_otra_vez;
				audioUbicacion.Play ();
				yield return new WaitForSeconds (audioUbicacion.clip.length);
				audioUbicacion.clip = audio_personaje;
				audioUbicacion.Play ();
			}

    

    IEnumerator playsound2 () {
        Debug.Log ("reproducuiendo......");
        audioUbicacion.clip = donde_esta;
        audioUbicacion.Play ();
        yield return new WaitForSeconds (audioUbicacion.clip.length);
        audioUbicacion.clip = audio_personaje_2;
        audioUbicacion.Play ();
		valorContinuar = 0;
		txtcontinuar.text = "Iniciar";

		btnMsg.SetActive (true);
		iniciarReloj ();
    }

	public void saveAcierto(int id_detalle_aprendizaje, string time1, string time2, string time3, string acierto)
	{
		string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/dbdata.db";
		IDbConnection dbconn;
		dbconn = (IDbConnection)new SqliteConnection(conn);
		dbconn.Open();
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = "INSERT INTO detalle_aprendizaje_acierto (tiempo_reaccion,tiempo_cuadro, tiempo_boton, acierto, id_detalle_aprendizaje) Values ('" + time1 + "','" + time2 + "','" + time3 + "' , '" + acierto + "' , '" + id_detalle_aprendizaje + "' )";
		dbcmd.CommandText = sqlQuery;
		dbcmd.ExecuteReader();
		Debug.Log("Datos Guardados Corectamente!..");
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;
	}

	public void iniciarReloj()
	{
		startTime = Time.time;
	}

	public string procesoReloj()
	{
		float t = Time.time - startTime;
		string minutes = ((int)t / 60).ToString();
		string seconds = (t % 60).ToString("f2");

		timerText.text = minutes + ":" + seconds;
		return timerText.text;
	}

	public void reinicioReloj()
	{
		timerText.text = "00" + ":" + "00";
	}

	public void finalisarReloj()
	{
		finalisacion = true;
		timerText.color = Color.yellow;
	}
	public void reaccionMano(){
		if (contador == 0)
		{
			if (HandRight.activeInHierarchy || HandLeft.activeInHierarchy)
			{
				finalisarReloj();
				tiempo_reaccion = timerText.text;
				Debug.Log(tiempo_reaccion);
				reinicioReloj();
				iniciarReloj();
				contador++;
			}
		}
	}
}
