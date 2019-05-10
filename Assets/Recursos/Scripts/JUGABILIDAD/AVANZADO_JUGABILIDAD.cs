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

public class AVANZADO_JUGABILIDAD : MonoBehaviour {

	//Salir
	public Text txtSalir;
	public GameObject btnSalir;

	private int estado_juego = 1; // 1 personaje 1 o 2 personaje 2

	//ACIERTOS... 3

	private int ACIERTOS = 3;

	//Texto y boton continuar
	public GameObject btnContinuar;
	public Text txtContinuar;

	//Textos de botones A-B-C-D
	public Text txtA, txtB, txtC, txtD, txtE, txtF, txtG, txtH, txtI;

	//Valores de botones;

	private int valorA = 0, valorB = 0, valorC = 0, valorD = 0, valorE = 0;
	private int valorF = 0, valorG = 0, valorH = 0, valorI = 0;
	private int valorContinuar = 0;

	//Variables de juego, comparaciones de id.
	private string nombre_ubicacion;
	private int id_personaje_1, id_personaje_2;
	private int id_boton_1 = 0, id_boton_2 = 0, id_boton_3 = 0, id_boton_4 = 0;
	private int id_boton_5 = 0, id_boton_6 = 0, id_boton_7 = 0, id_boton_8 = 0, id_boton_9 = 0;

	//Sonidos
	public AudioSource audioUbicacion;
	public AudioClip donde_esta, correcto, intenta_otra_vez;
	public AudioClip audio_personaje_1, audio_personaje_2;
	//Paneles A.B.C.D
	public RectTransform panelA, panelB, panelC, panelD;
	public RectTransform panelE, panelF, panelG, panelH, panelI;
	private Image imgA, imgB, imgC, imgD;
	private Image imgE, imgF, imgG, imgH, imgI;
	//Codigos del la vista nivel.
	private int codigo_detalle_aprendizaje_1 = LOGIN_JUGABILIDAD.codigosAvanzado.ElementAt (0);
	private int codigo_detalle_aprendizaje_2 = LOGIN_JUGABILIDAD.codigosAvanzado.ElementAt (1);

	//Botones en GameObject 4.
	public GameObject boton_pos_A, boton_pos_B, boton_pos_C, boton_pos_D;
	public GameObject boton_pos_E, boton_pos_F, boton_pos_G, boton_pos_H, boton_pos_I;
	//Imagen de Boton.
	public GameObject btnPrinciapl;
	private Texture2D textura_pos_A, textura_pos_B, textura_pos_C, textura_pos_D;
	private Texture2D textura_pos_E, textura_pos_F, textura_pos_G, textura_pos_H, textura_pos_I;
	private Transform imagen_boton_A, imagen_boton_B, imagen_boton_C, imagen_boton_D;
	private Transform imagen_boton_E, imagen_boton_F, imagen_boton_G, imagen_boton_H, imagen_boton_I;
	//Colores de los cuadrantes.
	private rgb Color_cuadrante_1, Color_cuadrante_2, Color_cuadrante_3, Color_cuadrante_4;
	private rgb Color_cuadrante_5, Color_cuadrante_6, Color_cuadrante_7, Color_cuadrante_8, Color_cuadrante_9;

	//Cronometro
	
	// Use this for initialization
	void Start () {
		
		btnSalir.SetActive(false);
		txtContinuar.text = "";
		btnContinuar.SetActive (false);
		Debug.Log (codigo_detalle_aprendizaje_1);
		Debug.Log (codigo_detalle_aprendizaje_2);
		imgA = panelA.GetComponent<Image> ();
		imgB = panelB.GetComponent<Image> ();
		imgC = panelC.GetComponent<Image> ();
		imgD = panelD.GetComponent<Image> ();
		imgE = panelE.GetComponent<Image> ();
		imgF = panelF.GetComponent<Image> ();
		imgG = panelG.GetComponent<Image> ();
		imgH = panelH.GetComponent<Image> ();
		imgI = panelI.GetComponent<Image> ();
		obtenerDatos1 (codigo_detalle_aprendizaje_1);
		obtenerDatos2 (codigo_detalle_aprendizaje_2);

	}

	// Update is called once per frame
	void Update () {

	}

	private void obtenerDatos1 (int id) {
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
			nombre_ubicacion = reader.GetString (4);
			id_personaje_1 = reader.GetInt32 (3);
			imagen_personaje = (byte[]) reader["imagen_personaje"];
			son = (byte[]) reader["audio_personaje"];

			if (nombre_ubicacion == "izquierda arriba") {
				Color_cuadrante_1 = new rgb ();
				id_boton_1 = id_personaje_1;
				Color_cuadrante_1.r = reader.GetInt32 (0);
				Color_cuadrante_1.g = reader.GetInt32 (1);
				Color_cuadrante_1.b = reader.GetInt32 (2);
			}
			if (nombre_ubicacion == "izquierda abajo") {
				Color_cuadrante_2 = new rgb ();
				id_boton_2 = id_personaje_1;
				Color_cuadrante_2.r = reader.GetInt32 (0);
				Color_cuadrante_2.g = reader.GetInt32 (1);
				Color_cuadrante_2.b = reader.GetInt32 (2);
			}
			if (nombre_ubicacion == "derecha abajo") {
				Color_cuadrante_3 = new rgb ();
				id_boton_3 = id_personaje_1;
				Color_cuadrante_3.r = reader.GetInt32 (0);
				Color_cuadrante_3.g = reader.GetInt32 (1);
				Color_cuadrante_3.b = reader.GetInt32 (2);
			}
			if (nombre_ubicacion == "derecha arriba") {
				Color_cuadrante_4 = new rgb ();
				id_boton_4 = id_personaje_1;
				Color_cuadrante_4.r = reader.GetInt32 (0);
				Color_cuadrante_4.g = reader.GetInt32 (1);
				Color_cuadrante_4.b = reader.GetInt32 (2);
			}
			if (nombre_ubicacion == "izquierda medio") {
				Color_cuadrante_5 = new rgb ();
				id_boton_5 = id_personaje_1;
				Color_cuadrante_5.r = reader.GetInt32 (0);
				Color_cuadrante_5.g = reader.GetInt32 (1);
				Color_cuadrante_5.b = reader.GetInt32 (2);
			}
			if (nombre_ubicacion == "abajo medio") {
				Color_cuadrante_6 = new rgb ();
				id_boton_6 = id_personaje_1;
				Color_cuadrante_6.r = reader.GetInt32 (0);
				Color_cuadrante_6.g = reader.GetInt32 (1);
				Color_cuadrante_6.b = reader.GetInt32 (2);
			}
			if (nombre_ubicacion == "derecha medio") {
				Color_cuadrante_7 = new rgb ();
				id_boton_7 = id_personaje_1;
				Color_cuadrante_7.r = reader.GetInt32 (0);
				Color_cuadrante_7.g = reader.GetInt32 (1);
				Color_cuadrante_7.b = reader.GetInt32 (2);
			}
			if (nombre_ubicacion == "arriba medio") {
				Color_cuadrante_8 = new rgb ();
				id_boton_8 = id_personaje_1;
				Color_cuadrante_8.r = reader.GetInt32 (0);
				Color_cuadrante_8.g = reader.GetInt32 (1);
				Color_cuadrante_8.b = reader.GetInt32 (2);
			}
			if (nombre_ubicacion == "medio centro") {
				Color_cuadrante_9 = new rgb ();
				id_boton_9 = id_personaje_1;
				Color_cuadrante_9.r = reader.GetInt32 (0);
				Color_cuadrante_9.g = reader.GetInt32 (1);
				Color_cuadrante_9.b = reader.GetInt32 (2);
			}
		}
		WAV sonido = new WAV (son);
		audio_personaje_1 = AudioClip.Create ("personaje_1", sonido.SampleCount, 1, sonido.Frequency, false, false);
		audio_personaje_1.SetData (sonido.LeftChannel, 0);

		if (nombre_ubicacion == "izquierda arriba") {
			textura_pos_A = new Texture2D (256, 256);
			textura_pos_A.LoadImage (imagen_personaje);
		}
		if (nombre_ubicacion == "izquierda abajo") {
			textura_pos_B = new Texture2D (256, 256);
			textura_pos_B.LoadImage (imagen_personaje);
		}
		if (nombre_ubicacion == "derecha abajo") {
			textura_pos_C = new Texture2D (256, 256);
			textura_pos_C.LoadImage (imagen_personaje);
		}
		if (nombre_ubicacion == "derecha arriba") {
			textura_pos_D = new Texture2D (256, 256);
			textura_pos_D.LoadImage (imagen_personaje);
		}
		if (nombre_ubicacion == "izquierda medio") {
			textura_pos_E = new Texture2D (256, 256);
			textura_pos_E.LoadImage (imagen_personaje);
		}
		if (nombre_ubicacion == "abajo medio") {
			textura_pos_F = new Texture2D (256, 256);
			textura_pos_F.LoadImage (imagen_personaje);
		}
		if (nombre_ubicacion == "derecha medio") {
			textura_pos_G = new Texture2D (256, 256);
			textura_pos_G.LoadImage (imagen_personaje);
		}
		if (nombre_ubicacion == "arriba medio") {
			textura_pos_H = new Texture2D (256, 256);
			textura_pos_H.LoadImage (imagen_personaje);
		}
		if (nombre_ubicacion == "medio centro") {
			textura_pos_I = new Texture2D (256, 256);
			textura_pos_I.LoadImage (imagen_personaje);
		}
		reader.Close ();
		reader = null;
		dbcmd.Dispose ();
		dbcmd = null;
		dbconn.Close ();
		nombre_ubicacion = "";
		StartCoroutine (playsound (audio_personaje_1));
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
			nombre_ubicacion = reader.GetString (4);
			id_personaje_2 = reader.GetInt32 (3);
			imagen_personaje = (byte[]) reader["imagen_personaje"];
			son = (byte[]) reader["audio_personaje"];

			if (nombre_ubicacion == "izquierda arriba") {
				Color_cuadrante_1 = new rgb ();
				id_boton_1 = id_personaje_2;
				Color_cuadrante_1.r = reader.GetInt32 (0);
				Color_cuadrante_1.g = reader.GetInt32 (1);
				Color_cuadrante_1.b = reader.GetInt32 (2);
			}
			if (nombre_ubicacion == "izquierda abajo") {
				Color_cuadrante_2 = new rgb ();
				id_boton_2 = id_personaje_2;
				Color_cuadrante_2.r = reader.GetInt32 (0);
				Color_cuadrante_2.g = reader.GetInt32 (1);
				Color_cuadrante_2.b = reader.GetInt32 (2);
			}
			if (nombre_ubicacion == "derecha abajo") {
				Color_cuadrante_3 = new rgb ();
				id_boton_3 = id_personaje_2;
				Color_cuadrante_3.r = reader.GetInt32 (0);
				Color_cuadrante_3.g = reader.GetInt32 (1);
				Color_cuadrante_3.b = reader.GetInt32 (2);
			}
			if (nombre_ubicacion == "derecha arriba") {
				Color_cuadrante_4 = new rgb ();
				id_boton_4 = id_personaje_2;
				Color_cuadrante_4.r = reader.GetInt32 (0);
				Color_cuadrante_4.g = reader.GetInt32 (1);
				Color_cuadrante_4.b = reader.GetInt32 (2);
			}
			if (nombre_ubicacion == "izquierda medio") {
				Color_cuadrante_5 = new rgb ();
				id_boton_5 = id_personaje_2;
				Color_cuadrante_5.r = reader.GetInt32 (0);
				Color_cuadrante_5.g = reader.GetInt32 (1);
				Color_cuadrante_5.b = reader.GetInt32 (2);
			}
			if (nombre_ubicacion == "abajo medio") {
				Color_cuadrante_6 = new rgb ();
				id_boton_6 = id_personaje_2;
				Color_cuadrante_6.r = reader.GetInt32 (0);
				Color_cuadrante_6.g = reader.GetInt32 (1);
				Color_cuadrante_6.b = reader.GetInt32 (2);
			}
			if (nombre_ubicacion == "derecha medio") {
				Color_cuadrante_7 = new rgb ();
				id_boton_7 = id_personaje_2;
				Color_cuadrante_7.r = reader.GetInt32 (0);
				Color_cuadrante_7.g = reader.GetInt32 (1);
				Color_cuadrante_7.b = reader.GetInt32 (2);
			}
			if (nombre_ubicacion == "arriba medio") {
				Color_cuadrante_8 = new rgb ();
				id_boton_8 = id_personaje_2;
				Color_cuadrante_8.r = reader.GetInt32 (0);
				Color_cuadrante_8.g = reader.GetInt32 (1);
				Color_cuadrante_8.b = reader.GetInt32 (2);
			}
			if (nombre_ubicacion == "medio centro") {
				Color_cuadrante_9 = new rgb ();
				id_boton_9 = id_personaje_2;
				Color_cuadrante_9.r = reader.GetInt32 (0);
				Color_cuadrante_9.g = reader.GetInt32 (1);
				Color_cuadrante_9.b = reader.GetInt32 (2);
			}
		}
		WAV sonido = new WAV (son);
		audio_personaje_2 = AudioClip.Create ("personaje_2", sonido.SampleCount, 1, sonido.Frequency, false, false);
		audio_personaje_2.SetData (sonido.LeftChannel, 0);

		if (nombre_ubicacion == "izquierda arriba") {
			textura_pos_A = new Texture2D (256, 256);
			textura_pos_A.LoadImage (imagen_personaje);
		}
		if (nombre_ubicacion == "izquierda abajo") {
			textura_pos_B = new Texture2D (256, 256);
			textura_pos_B.LoadImage (imagen_personaje);
		}
		if (nombre_ubicacion == "derecha abajo") {
			textura_pos_C = new Texture2D (256, 256);
			textura_pos_C.LoadImage (imagen_personaje);
		}
		if (nombre_ubicacion == "derecha arriba") {
			textura_pos_D = new Texture2D (256, 256);
			textura_pos_D.LoadImage (imagen_personaje);
		}
		if (nombre_ubicacion == "izquierda medio") {
			textura_pos_E = new Texture2D (256, 256);
			textura_pos_E.LoadImage (imagen_personaje);
		}
		if (nombre_ubicacion == "abajo medio") {
			textura_pos_F = new Texture2D (256, 256);
			textura_pos_F.LoadImage (imagen_personaje);
		}
		if (nombre_ubicacion == "derecha medio") {
			textura_pos_G = new Texture2D (256, 256);
			textura_pos_G.LoadImage (imagen_personaje);
		}
		if (nombre_ubicacion == "arriba medio") {
			textura_pos_H = new Texture2D (256, 256);
			textura_pos_H.LoadImage (imagen_personaje);
		}
		if (nombre_ubicacion == "medio centro") {
			textura_pos_I = new Texture2D (256, 256);
			textura_pos_I.LoadImage (imagen_personaje);
		}
		reader.Close ();
		reader = null;
		dbcmd.Dispose ();
		dbcmd = null;
		dbconn.Close ();
		nombre_ubicacion = "";
	}


	IEnumerator playsound (AudioClip sonido) {
		
		Debug.Log ("reproducuiendo......");
		audioUbicacion.clip = donde_esta;
		audioUbicacion.Play ();
		yield return new WaitForSeconds (audioUbicacion.clip.length);
		audioUbicacion.clip = sonido;
		audioUbicacion.Play ();
		//INICIAR TIEMPO 1
		CRONOMETRO.TimerStart();
	}



	public void contactopanelA () {
		if (id_boton_1 == id_personaje_1) {
			imgA.color = new Color (Color_cuadrante_1.r, Color_cuadrante_1.g, Color_cuadrante_1.b);
		} else if (id_boton_1 == id_personaje_2) {
			imgA.color = new Color (Color_cuadrante_1.r, Color_cuadrante_1.g, Color_cuadrante_1.b);
		} else {
			imgA.color = new Color (255, 255, 255);
		}
		
		
	}
	public void fincontactopanelA () {
		imgA.color = new Color32 (255, 255, 255, 100);
	}
	public void contactopanelB () {
		if (id_boton_2 == id_personaje_1) {
			imgB.color = new Color (Color_cuadrante_2.r, Color_cuadrante_2.g, Color_cuadrante_2.b);
		} else if (id_boton_2 == id_personaje_2) {
			imgB.color = new Color (Color_cuadrante_2.r, Color_cuadrante_2.g, Color_cuadrante_2.b);
		} else {
			imgB.color = new Color (255, 255, 255);
		}

	}

	public void fincontactopanelB () {
		imgB.color = new Color32 (255, 255, 255, 100);
	}

	public void contactopanelC () {
		if (id_boton_3 == id_personaje_1) {
			imgC.color = new Color (Color_cuadrante_3.r, Color_cuadrante_3.g, Color_cuadrante_3.b);
		} else if (id_boton_3 == id_personaje_2) {
			imgC.color = new Color (Color_cuadrante_3.r, Color_cuadrante_3.g, Color_cuadrante_3.b);
		} else {
			imgC.color = new Color (255, 255, 255);
		}
	}

	public void fincontactopanelC () {
		imgC.color = new Color32 (255, 255, 255, 100);
	}

	public void contactopanelD () {
		if (id_boton_4 == id_personaje_1) {
			imgD.color = new Color (Color_cuadrante_4.r, Color_cuadrante_4.g, Color_cuadrante_4.b);
		} else if (id_boton_4 == id_personaje_2) {
			imgD.color = new Color (Color_cuadrante_4.r, Color_cuadrante_4.g, Color_cuadrante_4.b);
		} else {
			imgD.color = new Color (255, 255, 255);
		}
	}

	public void fincontactopanelD () {
		imgD.color = new Color32 (255, 255, 255, 100);
	}

	public void contactopanelE () {
		if (id_boton_5 == id_personaje_1) {
			imgE.color = new Color (Color_cuadrante_5.r, Color_cuadrante_5.g, Color_cuadrante_5.b);
		} else if (id_boton_5 == id_personaje_2) {
			imgE.color = new Color (Color_cuadrante_5.r, Color_cuadrante_5.g, Color_cuadrante_5.b);
		} else {
			imgE.color = new Color (255, 255, 255);
		}
	}

	public void fincontactopanelE () {
		imgE.color = new Color32 (255, 255, 255, 100);
	}

	public void contactopanelF () {
		if (id_boton_6 == id_personaje_1) {
			imgF.color = new Color (Color_cuadrante_6.r, Color_cuadrante_6.g, Color_cuadrante_6.b);
		} else if (id_boton_6 == id_personaje_2) {
			imgF.color = new Color (Color_cuadrante_6.r, Color_cuadrante_6.g, Color_cuadrante_6.b);
		} else {
			imgF.color = new Color (255, 255, 255);
		}
	}

	public void fincontactopanelF () {
		imgF.color = new Color32 (255, 255, 255, 100);
	}

	public void contactopanelG () {
		if (id_boton_7 == id_personaje_1) {
			imgG.color = new Color (Color_cuadrante_7.r, Color_cuadrante_7.g, Color_cuadrante_7.b);
		} else if (id_boton_7 == id_personaje_2) {
			imgG.color = new Color (Color_cuadrante_7.r, Color_cuadrante_7.g, Color_cuadrante_7.b);
		} else {
			imgG.color = new Color (255, 255, 255);
		}
	}

	public void fincontactopanelG () {
		imgG.color = new Color32 (255, 255, 255, 100);
	}

	public void contactopanelH () {
		if (id_boton_8 == id_personaje_1) {
			imgH.color = new Color (Color_cuadrante_8.r, Color_cuadrante_8.g, Color_cuadrante_8.b);
		} else if (id_boton_8 == id_personaje_2) {
			imgH.color = new Color (Color_cuadrante_8.r, Color_cuadrante_8.g, Color_cuadrante_8.b);
		} else {
			imgH.color = new Color (255, 255, 255);
		}
	}

	public void fincontactopanelH () {
		imgH.color = new Color32 (255, 255, 255, 100);
	}

		public void contactopanelI () {
		if (id_boton_9 == id_personaje_1) {
			imgI.color = new Color (Color_cuadrante_9.r, Color_cuadrante_9.g, Color_cuadrante_9.b);
		} else if (id_boton_9 == id_personaje_2) {
			imgI.color = new Color (Color_cuadrante_9.r, Color_cuadrante_9.g, Color_cuadrante_9.b);
		} else {
			imgI.color = new Color (255, 255, 255);
		}
	}

	public void fincontactopanelI () {
		imgI.color = new Color32 (255, 255, 255, 100);
	}

	public void contacoBotonA () {
		valorB = 0;
		valorC = 0;
		valorD = 0;
		valorE = 0;
		valorF = 0;
		valorG = 0;
		valorH = 0;
		valorI = 0;
		valorA++;
		if (valorA >= 100) {
			//desaparecen los 4 botones.
			ocultarCubosABCD ();
			//indicar imagen, en el caso de existir
			if (textura_pos_A == null) {
				Debug.Log ("No ahy imagen");
			} else {
				InstanciaArribaIzquierda ();
			}

			if (estado_juego == 1) {
				if (id_personaje_1 == id_boton_1) {
					Debug.Log (" ** ACIERTO **  ");
					audioUbicacion.clip = correcto;
					audioUbicacion.Play ();
					ACIERTOS = 1;
				} else {
					Debug.Log ("--  NO ACIERTO -- ");
					audioUbicacion.clip = intenta_otra_vez;
					audioUbicacion.Play ();
					ACIERTOS = 0;
				}
			}

			if (estado_juego == 2) {
				if (id_personaje_2 == id_boton_1) {
					Debug.Log (" ** ACIERTO **  ");
					audioUbicacion.clip = correcto;
					audioUbicacion.Play ();
					ACIERTOS = 3;

				} else {
					Debug.Log ("--  NO ACIERTO -- ");
					audioUbicacion.clip = intenta_otra_vez;
					audioUbicacion.Play ();
					ACIERTOS = 2;
				}

			}
		}
		textoBotones (valorA, valorB, valorC, valorD, valorE, valorF, valorG, valorH, valorI);

	}

	public void contacoBotonB () {
		valorA = 0;
		valorC = 0;
		valorD = 0;
		valorE = 0;
		valorF = 0;
		valorG = 0;
		valorH = 0;
		valorI = 0;
		valorB++;
		if (valorB >= 100) {
			ocultarCubosABCD ();
			//indicar imagen, en el caso de existir
			if (textura_pos_B == null) {
				Debug.Log ("No ahy imagen");
			} else {
				InstanciaAbajoIzquierda ();
			}
			if (estado_juego == 1) {
				if (id_personaje_1 == id_boton_2) {
					Debug.Log (" ** ACIERTO **  ");
					audioUbicacion.clip = correcto;
					audioUbicacion.Play ();
					ACIERTOS = 1;
				} else {
					Debug.Log ("--  NO ACIERTO -- ");
					audioUbicacion.clip = intenta_otra_vez;
					audioUbicacion.Play ();
					ACIERTOS = 0;
				}
			}

			if (estado_juego == 2) {
				if (id_personaje_2 == id_boton_2) {
					Debug.Log (" ** ACIERTO **  ");
					audioUbicacion.clip = correcto;
					audioUbicacion.Play ();
					ACIERTOS = 3;

				} else {
					Debug.Log ("--  NO ACIERTO -- ");
					audioUbicacion.clip = intenta_otra_vez;
					audioUbicacion.Play ();
					ACIERTOS = 2;
				}

			}
		}
		textoBotones (valorA, valorB, valorC, valorD, valorE, valorF, valorG, valorH, valorI);
	}
	public void contacoBotonC () {
		valorB = 0;
		valorA = 0;
		valorD = 0;
		valorE = 0;
		valorF = 0;
		valorG = 0;
		valorH = 0;
		valorI = 0;
		valorC++;
		if (valorC >= 100) {
			ocultarCubosABCD ();
			//indicar imagen, en el caso de existir
			if (textura_pos_C == null) {
				Debug.Log ("No ahy imagen");
			} else {
				InstanciaAbajoDerecha ();
			}
			if (estado_juego == 1) {
				if (id_personaje_1 == id_boton_3) {
					Debug.Log (" ** ACIERTO **  ");
					audioUbicacion.clip = correcto;
					audioUbicacion.Play ();
					ACIERTOS = 1;
				} else {
					Debug.Log ("--  NO ACIERTO -- ");
					audioUbicacion.clip = intenta_otra_vez;
					audioUbicacion.Play ();
					ACIERTOS = 0;
				}
			}

			if (estado_juego == 2) {
				if (id_personaje_2 == id_boton_3) {
					Debug.Log (" ** ACIERTO **  ");
					audioUbicacion.clip = correcto;
					audioUbicacion.Play ();
					ACIERTOS = 3;

				} else {
					Debug.Log ("--  NO ACIERTO -- ");
					audioUbicacion.clip = intenta_otra_vez;
					audioUbicacion.Play ();
					ACIERTOS = 2;
				}

			}
		}
		textoBotones (valorA, valorB, valorC, valorD, valorE, valorF, valorG, valorH, valorI);
	}
	public void contacoBotonD () {
		valorB = 0;
		valorC = 0;
		valorA = 0;
		valorE = 0;
		valorF = 0;
		valorG = 0;
		valorH = 0;
		valorI = 0;;
		valorD++;
		if (valorD >= 100) {
			ocultarCubosABCD ();
			//indicar imagen, en el caso de existir
			if (textura_pos_D == null) {
				Debug.Log ("No ahy imagen");
			} else {
				InstanciaArribaDerecha ();
			}
			if (estado_juego == 1) {
				if (id_personaje_1 == id_boton_4) {
					Debug.Log (" ** ACIERTO **  ");
					audioUbicacion.clip = correcto;
					audioUbicacion.Play ();
					ACIERTOS = 1;
				} else {
					Debug.Log ("--  NO ACIERTO -- ");
					audioUbicacion.clip = intenta_otra_vez;
					audioUbicacion.Play ();
					ACIERTOS = 0;
				}
			}

			if (estado_juego == 2) {
				if (id_personaje_2 == id_boton_4) {
					Debug.Log (" ** ACIERTO **  ");
					audioUbicacion.clip = correcto;
					audioUbicacion.Play ();
					ACIERTOS = 3;

				} else {
					Debug.Log ("--  NO ACIERTO -- ");
					audioUbicacion.clip = intenta_otra_vez;
					audioUbicacion.Play ();
					ACIERTOS = 2;
				}

			}
		}
		textoBotones (valorA, valorB, valorC, valorD, valorE, valorF, valorG, valorH, valorI);
	}

	public void contacoBotonE () {
		valorA = 0;
		valorB = 0;
		valorC = 0;
		valorD = 0;
		valorF = 0;
		valorG = 0;
		valorH = 0;
		valorI = 0;
		valorE++;
		if (valorE >= 100) {
			ocultarCubosABCD ();
			//indicar imagen, en el caso de existir
			if (textura_pos_E == null) {
				Debug.Log ("No ahy imagen");
			} else {
				InstanciaIzquierdaMedio ();
			}
			if (estado_juego == 1) {
				if (id_personaje_1 == id_boton_5) {
					Debug.Log (" ** ACIERTO **  ");
					audioUbicacion.clip = correcto;
					audioUbicacion.Play ();
					ACIERTOS = 1;
				} else {
					Debug.Log ("--  NO ACIERTO -- ");
					audioUbicacion.clip = intenta_otra_vez;
					audioUbicacion.Play ();
					ACIERTOS = 0;
				}
			}

			if (estado_juego == 2) {
				if (id_personaje_2 == id_boton_5) {
					Debug.Log (" ** ACIERTO **  ");
					audioUbicacion.clip = correcto;
					audioUbicacion.Play ();
					ACIERTOS = 3;

				} else {
					Debug.Log ("--  NO ACIERTO -- ");
					audioUbicacion.clip = intenta_otra_vez;
					audioUbicacion.Play ();
					ACIERTOS = 2;
				}

			}
		}
		textoBotones (valorA, valorB, valorC, valorD, valorE, valorF, valorG, valorH, valorI);
	}

	public void contacoBotonF () {
		valorA = 0;
		valorB = 0;
		valorC = 0;
		valorD = 0;
		valorE = 0;
		valorG = 0;
		valorH = 0;
		valorI = 0;
		valorF++;
		if (valorF >= 100) {
			ocultarCubosABCD ();
			//indicar imagen, en el caso de existir
			if (textura_pos_F == null) {
				Debug.Log ("No ahy imagen");
			} else {
				InstanciaArribaDerecha ();
			}
			if (estado_juego == 1) {
				if (id_personaje_1 == id_boton_6) {
					Debug.Log (" ** ACIERTO **  ");
					audioUbicacion.clip = correcto;
					audioUbicacion.Play ();
					ACIERTOS = 1;
				} else {
					Debug.Log ("--  NO ACIERTO -- ");
					audioUbicacion.clip = intenta_otra_vez;
					audioUbicacion.Play ();
					ACIERTOS = 0;
				}
			}

			if (estado_juego == 2) {
				if (id_personaje_2 == id_boton_6) {
					Debug.Log (" ** ACIERTO **  ");
					audioUbicacion.clip = correcto;
					audioUbicacion.Play ();
					ACIERTOS = 3;

				} else {
					Debug.Log ("--  NO ACIERTO -- ");
					audioUbicacion.clip = intenta_otra_vez;
					audioUbicacion.Play ();
					ACIERTOS = 2;
				}

			}
		}
		textoBotones (valorA, valorB, valorC, valorD, valorE, valorF, valorG, valorH, valorI);
	}

	public void contacoBotonG () {
		valorA = 0;
		valorB = 0;
		valorC = 0;
		valorD = 0;
		valorE = 0;
		valorF = 0;
		valorH = 0;
		valorI = 0;
		valorG++;
		if (valorG >= 100) {
			ocultarCubosABCD ();
			//indicar imagen, en el caso de existir
			if (textura_pos_G == null) {
				Debug.Log ("No ahy imagen");
			} else {
				InstanciaArribaDerecha ();
			}
			if (estado_juego == 1) {
				if (id_personaje_1 == id_boton_7) {
					Debug.Log (" ** ACIERTO **  ");
					audioUbicacion.clip = correcto;
					audioUbicacion.Play ();
					ACIERTOS = 1;
				} else {
					Debug.Log ("--  NO ACIERTO -- ");
					audioUbicacion.clip = intenta_otra_vez;
					audioUbicacion.Play ();
					ACIERTOS = 0;
				}
			}

			if (estado_juego == 2) {
				if (id_personaje_2 == id_boton_7) {
					Debug.Log (" ** ACIERTO **  ");
					audioUbicacion.clip = correcto;
					audioUbicacion.Play ();
					ACIERTOS = 3;

				} else {
					Debug.Log ("--  NO ACIERTO -- ");
					audioUbicacion.clip = intenta_otra_vez;
					audioUbicacion.Play ();
					ACIERTOS = 2;
				}

			}
		}
		textoBotones (valorA, valorB, valorC, valorD, valorE, valorF, valorG, valorH, valorI);
	}

	public void contacoBotonH () {
		valorA = 0;
		valorB = 0;
		valorC = 0;
		valorD = 0;
		valorE = 0;
		valorF = 0;
		valorG = 0;
		valorH++;
		valorI = 0;
		if (valorH >= 100) {
			ocultarCubosABCD ();
			//indicar imagen, en el caso de existir
			if (textura_pos_H == null) {
				Debug.Log ("No ahy imagen");
			} else {
				InstanciaArribaDerecha ();
			}
			if (estado_juego == 1) {
				if (id_personaje_1 == id_boton_8) {
					Debug.Log (" ** ACIERTO **  ");
					audioUbicacion.clip = correcto;
					audioUbicacion.Play ();
					ACIERTOS = 1;
				} else {
					Debug.Log ("--  NO ACIERTO -- ");
					audioUbicacion.clip = intenta_otra_vez;
					audioUbicacion.Play ();
					ACIERTOS = 0;
				}
			}

			if (estado_juego == 2) {
				if (id_personaje_2 == id_boton_8) {
					Debug.Log (" ** ACIERTO **  ");
					audioUbicacion.clip = correcto;
					audioUbicacion.Play ();
					ACIERTOS = 3;

				} else {
					Debug.Log ("--  NO ACIERTO -- ");
					audioUbicacion.clip = intenta_otra_vez;
					audioUbicacion.Play ();
					ACIERTOS = 2;
				}

			}
		}
		textoBotones (valorA, valorB, valorC, valorD, valorE, valorF, valorG, valorH, valorI);
	}

	public void contacoBotonI () {
		valorA = 0;
		valorB = 0;
		valorC = 0;
		valorD = 0;
		valorE = 0;
		valorG = 0;
		valorH = 0;
		valorF = 0;
		valorI++;
		if (valorI >= 100) {
			ocultarCubosABCD ();
			//indicar imagen, en el caso de existir
			if (textura_pos_I == null) {
				Debug.Log ("No ahy imagen");
			} else {
				InstanciaArribaDerecha ();
			}
			if (estado_juego == 1) {
				if (id_personaje_1 == id_boton_9) {
					Debug.Log (" ** ACIERTO **  ");
					audioUbicacion.clip = correcto;
					audioUbicacion.Play ();
					ACIERTOS = 1;
				} else {
					Debug.Log ("--  NO ACIERTO -- ");
					audioUbicacion.clip = intenta_otra_vez;
					audioUbicacion.Play ();
					ACIERTOS = 0;
				}
			}

			if (estado_juego == 2) {
				if (id_personaje_2 == id_boton_9) {
					Debug.Log (" ** ACIERTO **  ");
					audioUbicacion.clip = correcto;
					audioUbicacion.Play ();
					ACIERTOS = 3;

				} else {
					Debug.Log ("--  NO ACIERTO -- ");
					audioUbicacion.clip = intenta_otra_vez;
					audioUbicacion.Play ();
					ACIERTOS = 2;
				}

			}
		}
		textoBotones (valorA, valorB, valorC, valorD, valorE, valorF, valorG, valorH, valorI);
	}

	public void ocultarCubosABCD () {
		txtA.text = "";
		txtB.text = "";
		txtC.text = "";
		txtD.text = "";
		txtE.text = "";
		txtF.text = "";
		txtG.text = "";
		txtH.text = "";
		txtI.text = "";
		txtContinuar.text = "CONTINUAR 0";
		boton_pos_A.SetActive (false);
		boton_pos_B.SetActive (false);
		boton_pos_C.SetActive (false);
		boton_pos_D.SetActive (false);
		boton_pos_E.SetActive (false);
		boton_pos_F.SetActive (false);
		boton_pos_G.SetActive (false);
		boton_pos_H.SetActive (false);
		boton_pos_I.SetActive (false);
		btnContinuar.SetActive (true);
		valorContinuar = 0;
		valorB = 0;
		valorC = 0;
		valorA = 0;
		valorD = 0;
		valorE = 0;
		valorF = 0;
		valorG = 0;
		valorH = 0;
		valorI = 0;
		if (imagen_boton_A != null) {
			imagen_boton_A.gameObject.SetActive (false);
		}
		if (imagen_boton_B != null) {
			imagen_boton_B.gameObject.SetActive (false);
		}
		if (imagen_boton_C != null) {
			imagen_boton_C.gameObject.SetActive (false);
		}
		if (imagen_boton_D != null) {
			imagen_boton_D.gameObject.SetActive (false);
		}
		if (imagen_boton_E != null) {
			imagen_boton_E.gameObject.SetActive (false);
		}
		if (imagen_boton_F != null) {
			imagen_boton_F.gameObject.SetActive (false);
		}
		if (imagen_boton_G != null) {
			imagen_boton_G.gameObject.SetActive (false);
		}
		if (imagen_boton_H != null) {
			imagen_boton_H.gameObject.SetActive (false);
		}
		if (imagen_boton_I != null) {
			imagen_boton_I.gameObject.SetActive (false);
		}
	}

	public void mostrarCuboTextoABCD () {
		txtA.text = "A";
		txtB.text = "B";
		txtC.text = "C";
		txtD.text = "D";
		txtE.text = "E";
		txtF.text = "F";
		txtG.text = "G";
		txtH.text = "H";
		txtI.text = "I";
		txtContinuar.text = "";
		boton_pos_A.SetActive (true);
		boton_pos_B.SetActive (true);
		boton_pos_C.SetActive (true);
		boton_pos_D.SetActive (true);
		boton_pos_E.SetActive (true);
		boton_pos_F.SetActive (true);
		boton_pos_G.SetActive (true);
		boton_pos_H.SetActive (true);
		boton_pos_I.SetActive (true);
		btnContinuar.SetActive (false);
		valorContinuar = 0;
	}

	public void textoBotones (int a, int b, int c, int d, int e, int f, int g, int h, int i) {
		txtA.text = "" + a;
		txtB.text = "" + b;
		txtC.text = "" + c;
		txtD.text = "" + d;
		txtE.text = "" + e;
		txtF.text = "" + f;
		txtG.text = "" + g;
		txtH.text = "" + h;
		txtI.text = "" + i;

	}

	public void continuar () {
		valorContinuar++;
		txtContinuar.text = "CONTINUAR " + valorContinuar;
		if (valorContinuar >= 100) {
			if (ACIERTOS == 0) {
				mostrarCuboTextoABCD ();
				StartCoroutine (playsound (audio_personaje_1));
			}
			if (ACIERTOS == 1) {
				mostrarCuboTextoABCD ();
				StartCoroutine (playsound (audio_personaje_2));
				estado_juego = 2;
			}
			if (ACIERTOS == 2) {
				mostrarCuboTextoABCD ();
				StartCoroutine (playsound (audio_personaje_2));
				estado_juego = 2;
			}
			if (ACIERTOS == 3) {
				txtContinuar.text = "";
				txtSalir.text = "Salir ";
				btnContinuar.SetActive (false);
				Debug.Log ("Fin de fase");
				btnSalir.SetActive(true);
				valorContinuar = 0;
			}

		}
	}

	public void botonSalir () {

		valorContinuar++;
		txtSalir.text = "SALIR " + valorContinuar;
		if (valorContinuar >= 100) {
			valorContinuar = 0;
			SceneManager.LoadScene (15);
		}

	}

	

	public void InstanciaArribaIzquierda () {
		btnPrinciapl.GetComponent<Image> ().sprite = Sprite.Create (textura_pos_A, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
		GameObject img = Instantiate (btnPrinciapl.gameObject, new Vector3 (-0.4f, 0.3f, 0.5f), transform.rotation);
		img.transform.SetParent (this.transform);
		img.transform.localScale = new Vector3 (1f, 1f, 0f);
		imagen_boton_A = img.GetComponent<Transform> ();
	}

	public void InstanciaAbajoIzquierda () {
		btnPrinciapl.GetComponent<Image> ().sprite = Sprite.Create (textura_pos_B, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
		GameObject img = Instantiate (btnPrinciapl.gameObject, new Vector3 (-0.4f, -0.3f, 0.5f), transform.rotation);
		img.transform.SetParent (this.transform);
		img.transform.localScale = new Vector3 (1f, 1f, 0f);
		imagen_boton_B = img.GetComponent<Transform> ();
	}
	public void InstanciaAbajoDerecha () {
		btnPrinciapl.GetComponent<Image> ().sprite = Sprite.Create (textura_pos_C, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
		GameObject img = Instantiate (btnPrinciapl.gameObject, new Vector3 (0.4f, -0.3f, 0.5f), transform.rotation);
		img.transform.SetParent (this.transform);
		img.transform.localScale = new Vector3 (1f, 1f, 0f);
		imagen_boton_C = img.GetComponent<Transform> ();
	}
	public void InstanciaArribaDerecha () {
		btnPrinciapl.GetComponent<Image> ().sprite = Sprite.Create (textura_pos_D, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
		GameObject img = Instantiate (btnPrinciapl.gameObject, new Vector3 (0.4f, 0.3f, 0.3f), transform.rotation);
		img.transform.SetParent (this.transform);
		img.transform.localScale = new Vector3 (1f, 1f, 0f);
		imagen_boton_D = img.GetComponent<Transform> ();
	}

	public void InstanciaIzquierdaMedio () {
		btnPrinciapl.GetComponent<Image> ().sprite = Sprite.Create (textura_pos_E, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
		GameObject img = Instantiate (btnPrinciapl.gameObject, new Vector3 (0.4f, 0.1f, 0.3f), transform.rotation);
		img.transform.SetParent (this.transform);
		img.transform.localScale = new Vector3 (1f, 1f, 0f);
		imagen_boton_E = img.GetComponent<Transform> ();
	}

	public void InstanciaAbajoMedio () {
		btnPrinciapl.GetComponent<Image> ().sprite = Sprite.Create (textura_pos_F, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
		GameObject img = Instantiate (btnPrinciapl.gameObject, new Vector3 (0.4f, 0f, 0.3f), transform.rotation);
		img.transform.SetParent (this.transform);
		img.transform.localScale = new Vector3 (1f, 1f, 0f);
		imagen_boton_F = img.GetComponent<Transform> ();
	}

	public void InstanciaDerechaMedio () {
		btnPrinciapl.GetComponent<Image> ().sprite = Sprite.Create (textura_pos_G, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
		GameObject img = Instantiate (btnPrinciapl.gameObject, new Vector3 (0.4f, 0f, 0.5f), transform.rotation);
		img.transform.SetParent (this.transform);
		img.transform.localScale = new Vector3 (1f, 1f, 0f);
		imagen_boton_G = img.GetComponent<Transform> ();
	}

	public void InstanciaArribaMedio () {
		btnPrinciapl.GetComponent<Image> ().sprite = Sprite.Create (textura_pos_H, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
		GameObject img = Instantiate (btnPrinciapl.gameObject, new Vector3 (0f, 0.3f, 0.5f), transform.rotation);
		img.transform.SetParent (this.transform);
		img.transform.localScale = new Vector3 (1f, 1f, 0f);
		imagen_boton_H = img.GetComponent<Transform> ();
	}

	public void InstanciaMedioCentro () {
		btnPrinciapl.GetComponent<Image> ().sprite = Sprite.Create (textura_pos_I, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
		GameObject img = Instantiate (btnPrinciapl.gameObject, new Vector3 (0.1f, 0.1f, 0.5f), transform.rotation);
		img.transform.SetParent (this.transform);
		img.transform.localScale = new Vector3 (1f, 1f, 0f);
		imagen_boton_I = img.GetComponent<Transform> ();
	}

}