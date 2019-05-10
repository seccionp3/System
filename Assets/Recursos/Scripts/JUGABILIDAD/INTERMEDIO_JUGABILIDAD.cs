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
public class INTERMEDIO_JUGABILIDAD : MonoBehaviour {

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
	public Text txtA, txtB, txtC, txtD;

	//Valores de botones;

	private int valorA = 0, valorB = 0, valorC = 0, valorD = 0;
	private int valorContinuar = 0;

	//Variables de juego, comparaciones de id.
	private string nombre_ubicacion;
	private int id_personaje_1, id_personaje_2;
	private int id_boton_1 = 0, id_boton_2 = 0, id_boton_3 = 0, id_boton_4 = 0;

	//Sonidos
	public AudioSource audioUbicacion;
	public AudioClip donde_esta, correcto, intenta_otra_vez;
	public AudioClip audio_personaje_1, audio_personaje_2;
	//Paneles A.B.C.D
	public RectTransform panelA, panelB, panelC, panelD;
	private Image imgA, imgB, imgC, imgD;
	//Codigos del la vista nivel.
	private int codigo_detalle_aprendizaje_1 = LOGIN_JUGABILIDAD.codigosIntermedio.ElementAt (0);
	private int codigo_detalle_aprendizaje_2 = LOGIN_JUGABILIDAD.codigosIntermedio.ElementAt (1);

	//Botones en GameObject 4.
	public GameObject boton_pos_A, boton_pos_B, boton_pos_C, boton_pos_D;
	//Imagen de Boton.
	public GameObject btnPrinciapl;
	private Texture2D textura_pos_A, textura_pos_B, textura_pos_C, textura_pos_D;
	private Transform imagen_boton_A, imagen_boton_B, imagen_boton_C, imagen_boton_D;
	//Colores de los cuadrantes.
	private rgb Color_cuadrante_1, Color_cuadrante_2;
	private rgb Color_cuadrante_3, Color_cuadrante_4;
	// Use this for initialization
	void Start () {
		btnSalir.SetActive (false);
		txtContinuar.text = "";
		btnContinuar.SetActive (false);
		Debug.Log (codigo_detalle_aprendizaje_1);
		Debug.Log (codigo_detalle_aprendizaje_2);
		imgA = panelA.GetComponent<Image> ();
		imgB = panelB.GetComponent<Image> ();
		imgC = panelC.GetComponent<Image> ();
		imgD = panelD.GetComponent<Image> ();
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

	public void contacoBotonA () {
		valorB = 0;
		valorC = 0;
		valorD = 0;
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
		textoBotones (valorA, valorB, valorC, valorD);

	}

	public void contacoBotonB () {
		valorA = 0;
		valorC = 0;
		valorD = 0;
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
		textoBotones (valorA, valorB, valorC, valorD);
	}
	public void contacoBotonC () {
		valorB = 0;
		valorA = 0;
		valorD = 0;
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
		textoBotones (valorA, valorB, valorC, valorD);
	}
	public void contacoBotonD () {
		valorB = 0;
		valorC = 0;
		valorA = 0;
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
		textoBotones (valorA, valorB, valorC, valorD);
	}

	public void ocultarCubosABCD () {
		txtA.text = "";
		txtB.text = "";
		txtC.text = "";
		txtD.text = "";
		txtContinuar.text = "CONTINUAR 0";
		boton_pos_A.SetActive (false);
		boton_pos_B.SetActive (false);
		boton_pos_C.SetActive (false);
		boton_pos_D.SetActive (false);
		btnContinuar.SetActive (true);
		valorContinuar = 0;
		valorB = 0;
		valorC = 0;
		valorA = 0;
		valorD = 0;

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
	}

	public void mostrarCuboTextoABCD () {
		txtA.text = "A";
		txtB.text = "B";
		txtC.text = "C";
		txtD.text = "D";
		txtContinuar.text = "";
		boton_pos_A.SetActive (true);
		boton_pos_B.SetActive (true);
		boton_pos_C.SetActive (true);
		boton_pos_D.SetActive (true);
		btnContinuar.SetActive (false);
		valorContinuar = 0;
	}

	public void textoBotones (int a, int b, int c, int d) {
		txtA.text = "" + a;
		txtB.text = "" + b;
		txtC.text = "" + c;
		txtD.text = "" + d;
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
				btnSalir.SetActive (true);
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
		GameObject img = Instantiate (btnPrinciapl.gameObject, new Vector3 (-0.3f, 0.1f, 0.3f), transform.rotation);
		img.transform.SetParent (this.transform);
		img.transform.localScale = new Vector3 (1f, 1f, 0f);
		imagen_boton_A = img.GetComponent<Transform> ();
	}

	public void InstanciaAbajoIzquierda () {
		btnPrinciapl.GetComponent<Image> ().sprite = Sprite.Create (textura_pos_B, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
		GameObject img = Instantiate (btnPrinciapl.gameObject, new Vector3 (-0.3f, -0.1f, 0.3f), transform.rotation);
		img.transform.SetParent (this.transform);
		img.transform.localScale = new Vector3 (1f, 1f, 0f);
		imagen_boton_B = img.GetComponent<Transform> ();
	}
	public void InstanciaAbajoDerecha () {
		btnPrinciapl.GetComponent<Image> ().sprite = Sprite.Create (textura_pos_C, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
		GameObject img = Instantiate (btnPrinciapl.gameObject, new Vector3 (0.3f, -0.1f, 0.3f), transform.rotation);
		img.transform.SetParent (this.transform);
		img.transform.localScale = new Vector3 (1f, 1f, 0f);
		imagen_boton_C = img.GetComponent<Transform> ();
	}
	public void InstanciaArribaDerecha () {
		btnPrinciapl.GetComponent<Image> ().sprite = Sprite.Create (textura_pos_D, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
		GameObject img = Instantiate (btnPrinciapl.gameObject, new Vector3 (0.3f, 0.1f, 0.3f), transform.rotation);
		img.transform.SetParent (this.transform);
		img.transform.localScale = new Vector3 (1f, 1f, 0f);
		imagen_boton_D = img.GetComponent<Transform> ();
	}

}