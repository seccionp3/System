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

public class AVANZADO_REAPRENDIZAJE : MonoBehaviour {

    public static int codigoA,codigoB;

	//VARIABLES PARA PODER VIZUALIZAR..

	private String nombre_ubicacion;

	private int contador = -1;

	private int num_repeticiones, velocidad = 0;

	private rgb color_personaje_1 = new rgb ();
	private rgb color_personaje_2 = new rgb ();
	public AudioSource audioGeneral;
	public AudioClip audio_personaje_1, audio_personaje_2, audioUbicacion;

	public RectTransform panelA, panelB, panelC, panelD, panelE, panelF, panelG, panelH, panelI;
	private Image imgA, imgB, imgC, imgD, imgE, imgF, imgG, imgH, imgI;
	public Button btnPrincipal;

	private Texture2D tex;
	private Transform pos_btn_A, pos_btn_B, pos_btn_C, pos_btn_D, pos_btn_E, pos_btn_F, pos_btn_G, pos_btn_H, pos_btn_I;

    private int estado_personaje = 0;
    public GameObject btnMsg;

    // Use this for initialization
    void Start () {
        btnMsg.SetActive(false);
        obtenerDatos1 (codigoA);
		//btnMsg.SetActive (false);
		imgA = panelA.GetComponent<Image> ();
		imgB = panelB.GetComponent<Image> ();
		imgC = panelC.GetComponent<Image> ();
		imgD = panelD.GetComponent<Image> ();
		imgE = panelE.GetComponent<Image> ();
		imgF = panelF.GetComponent<Image> ();
		imgG = panelG.GetComponent<Image> ();
		imgH = panelH.GetComponent<Image> ();
		imgI = panelI.GetComponent<Image> ();
		Debug.Log (imgB);

	}
    public void regresarEscena()
    {
        SceneManager.LoadScene(14);
    }

    // Update is called once per frame
    void Update () {

		if (contador == 0) {
            imgA.color = new Color(color_personaje_1.r, color_personaje_1.g, color_personaje_1.b);
            if (pos_btn_A.position.x < ((Screen.width / 3) - 100)) {
				//AudioSource.PlayClipAtPoint(Sonido, Posicion.position, Volumen);
				pos_btn_A.position += new Vector3 (Time.deltaTime * velocidad, 0f, 0f);
			}
			if (pos_btn_A.position.x >= ((Screen.width / 3) - 100)) {
				if ( num_repeticiones > 0) {
					Destroy (pos_btn_A.gameObject);
					InstanciarPosA ();
					num_repeticiones--;
				}
			}
		}
		if (contador == 1) {
            imgB.color = new Color(color_personaje_1.r, color_personaje_1.g, color_personaje_1.b);
            if (pos_btn_B.position.x < ((Screen.width / 3) - 100)) {
				//AudioSource.PlayClipAtPoint(Sonido, Posicion.position, Volumen);
				pos_btn_B.position += new Vector3 (Time.deltaTime * velocidad, 0f, 0f);
			}
			if (pos_btn_B.position.x >= ((Screen.width / 3) - 100)) {
				if (num_repeticiones > 0) {
					Destroy (pos_btn_B.gameObject);
					InstanciarPosB ();
					num_repeticiones--;
                    imgB.color = Color.white;
                }
			}
		}

		if (contador == 2) {
            imgC.color = new Color(color_personaje_1.r, color_personaje_1.g, color_personaje_1.b);
            if (pos_btn_C.position.x > ((Screen.width / 1.5) + 100)) {
				//AudioSource.PlayClipAtPoint(Sonido, Posicion.position, Volumen);
				pos_btn_C.position += new Vector3 (-Time.deltaTime * velocidad, 0f, 0f);
			}
			if (pos_btn_C.position.x <= ((Screen.width / 1.5) + 100)) {
				if (num_repeticiones > 0) {
					Destroy (pos_btn_C.gameObject);
					InstanciarPosC ();
					num_repeticiones--;
				}
			}
		}
		if (contador == 3) {
			Debug.Log("Repericiones " + num_repeticiones);
            imgD.color = new Color(color_personaje_1.r, color_personaje_1.g, color_personaje_1.b);
            if (pos_btn_D.position.x > ((Screen.width / 1.5) + 100)) {
				//AudioSource.PlayClipAtPoint(Sonido, Posicion.position, Volumen);
				pos_btn_D.position += new Vector3 (-Time.deltaTime * velocidad, 0f, 0f);
			}
			if (pos_btn_D.position.x <= ((Screen.width / 1.5) + 100)) {
				if (num_repeticiones > 0) {
					Destroy (pos_btn_D.gameObject);
					InstanciarPosD ();
					num_repeticiones--;
				}
			}
		}

		if (contador == 4) {
            imgE.color = new Color(color_personaje_1.r, color_personaje_1.g, color_personaje_1.b);
            if (pos_btn_E.position.x < ((Screen.width / 3) - 100)) {
				//AudioSource.PlayClipAtPoint(Sonido, Posicion.position, Volumen);
				pos_btn_E.position += new Vector3 (Time.deltaTime * velocidad, 0f, 0f);
			}
			if (pos_btn_E.position.x >= ((Screen.width / 3) - 100)) {
				if (num_repeticiones > 0) {
					Destroy (pos_btn_E.gameObject);
					InstanciarPosE ();
					num_repeticiones--;
				}
			}
		}

		if (contador == 5) {
            imgF.color = new Color(color_personaje_1.r, color_personaje_1.g, color_personaje_1.b);
            if (pos_btn_F.position.y < ((Screen.height / 3))) {
				//AudioSource.PlayClipAtPoint(Sonido, Posicion.position, Volumen);
				pos_btn_F.position += new Vector3 (0, Time.deltaTime * velocidad, 0f);
			}
			if (pos_btn_F.position.y >= ((Screen.height / 3))) {
				if (num_repeticiones > 0) {
					Destroy (pos_btn_F.gameObject);
					InstanciarPosF ();
					num_repeticiones--;
				}
			}
		}

		if (contador == 6) {
            imgG.color = new Color(color_personaje_1.r, color_personaje_1.g, color_personaje_1.b);
            if (pos_btn_G.position.x > ((Screen.width / 1.5) + 100)) {
				//AudioSource.PlayClipAtPoint(Sonido, Posicion.position, Volumen);
				pos_btn_G.position += new Vector3 (-Time.deltaTime * velocidad, 0, 0f);
			}
			if (pos_btn_G.position.x <= ((Screen.width / 1.5) + 100)) {
				if (num_repeticiones > 0) {
					Destroy (pos_btn_G.gameObject);
					InstanciarPosG ();
					num_repeticiones--;
				}
			}
		}
		if (contador == 7) {
            imgH.color = new Color(color_personaje_1.r, color_personaje_1.g, color_personaje_1.b);
            Debug.Log("Contador = " + contador);
			if (pos_btn_H.position.y > ((Screen.height / 1.5))) {
				Debug.Log("Moviendo");
				//AudioSource.PlayClipAtPoint(Sonido, Posicion.position, Volumen);
				pos_btn_H.position += new Vector3 (0, -Time.deltaTime * velocidad, 0f);
			}
			if (pos_btn_H.position.y <= ((Screen.height / 1.5))) {
				Debug.Log("Estatico");
					Debug.Log("Repeticiones " + num_repeticiones);
				if (num_repeticiones > 0) {
					Destroy (pos_btn_H.gameObject);
					InstanciarPosH ();
					num_repeticiones--;
				}
			}
		}

		if (contador == 8) {
            imgI.color = new Color(color_personaje_1.r, color_personaje_1.g, color_personaje_1.b);
            if (num_repeticiones <= 0) {
				Destroy (pos_btn_I.gameObject);
				InstanciarPosI ();
				num_repeticiones--;
			}

		}

	}

	private void obtenerDatos1 (int id) {
		byte[] sonido_personaje = new byte[0];
		byte[] audio_ubicacion = new byte[0];
		byte[] imagen_personaje = new byte[0];
		string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/dbdata.db";
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection (conn);
		dbconn.Open ();
		IDbCommand dbcmd = dbconn.CreateCommand ();
		string sqlQuery;
		sqlQuery = "select t1.num_repeticiones, t1.velocidad_detalle_apre, t2.r_color, t2.g_color, t2.b_color, " +
			" t3.audio_personaje, t3.imagen_personaje, " +
			" t4.nombre_ubicacion, t4.audio_ubicacion " +
			" from detalle_aprendizaje as t1 inner join color as t2 on t2.id = t1.id_color " +
			" inner join personaje as t3 on t1.id_personaje = t3.id_personaje " +
			" inner join ubicacion as t4 on t1.id_ubicacion = t4.id where t1.id_detalle_apre = " + id;
		Debug.Log (sqlQuery);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader ();
		while (reader.Read ()) {

			num_repeticiones = reader.GetInt32 (0);
			velocidad = reader.GetInt32 (1);
			color_personaje_1.r = reader.GetInt32 (2);
			color_personaje_1.g = reader.GetInt32 (3);
			color_personaje_1.b = reader.GetInt32 (4);

			nombre_ubicacion = reader.GetString (7);

			imagen_personaje = (byte[]) reader["imagen_personaje"];
			audio_ubicacion = (byte[]) reader["audio_ubicacion"];
			sonido_personaje = (byte[]) reader["audio_personaje"];

		}
		Debug.Log ("colores" + color_personaje_1.r + color_personaje_1.g +	color_personaje_1.b);
		tex = new Texture2D (256, 256);
		tex.LoadImage (imagen_personaje);

		WAV sonido1 = new WAV (sonido_personaje);
		audio_personaje_1 = AudioClip.Create ("personaje", sonido1.SampleCount, 1, sonido1.Frequency, false, false);
		audio_personaje_1.SetData (sonido1.LeftChannel, 0);

		WAV sonido2 = new WAV (audio_ubicacion);
		audioUbicacion = AudioClip.Create ("ubicacion", sonido2.SampleCount, 1, sonido2.Frequency, false, false);
		audioUbicacion.SetData (sonido2.LeftChannel, 0);

		reader.Close ();
		reader = null;
		dbcmd.Dispose ();
		dbcmd = null;
		dbconn.Close ();
	
		//imgB.color = new Color (color_personaje_1.r, color_personaje_1.g, color_personaje_1.b);
		inicio_animacion ();
		//nombre_ubicacion = "";
		//StartCoroutine (playsound (audio_personaje_1));
	}

	void inicio_animacion () {
		num_repeticiones--;
		if (nombre_ubicacion == "izquierda arriba") {
			contador = 0;
			InstanciarPosA ();
		}
		if (nombre_ubicacion == "izquierda abajo") {
			contador = 1;
			InstanciarPosB ();
		}
		if (nombre_ubicacion == "derecha abajo") {
			contador = 2;
			InstanciarPosC ();
		}
		if (nombre_ubicacion == "derecha arriba") {
			contador =3;
			InstanciarPosD ();
		}
		if (nombre_ubicacion == "izquierda medio") {
			contador =4;
			InstanciarPosE ();
		}
		if (nombre_ubicacion == "abajo medio") {
			contador =5;
			InstanciarPosF ();
		}
		if (nombre_ubicacion == "derecha medio") {
			contador = 6;
			InstanciarPosG ();
		}
		if (nombre_ubicacion == "arriba medio") {
			contador = 7;
			InstanciarPosH ();
		}
		if (nombre_ubicacion == "medio centro") {
			contador = 8;
			InstanciarPosI ();
		}
	}

	void InstanciarPosA () {
		
		//POSICIONES INICIALES
		//btnPrincipal.GetComponentInChildren<Text> ().text = "Izquierda Arriba";
		//btnPrincipal.GetComponentInChildren<Text>().alignment = TextAnchor.LowerCenter;
		btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
		//btnPrincipal.onClick.AddListener(contar);
		GameObject btnIzqSup = Instantiate (btnPrincipal.gameObject, new Vector3 (-100, (Screen.height / 6) * 5, 0), transform.rotation);

		btnIzqSup.transform.SetParent (this.transform);
		pos_btn_A = btnIzqSup.GetComponent<Transform> ();
        if (num_repeticiones == 1)
        {
            pos_btn_A.GetComponent<Button>().onClick.AddListener(contar);
        }
        StartCoroutine (playsound ());


	}

	void InstanciarPosB () {
		btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
		GameObject btnIzqSup = Instantiate (btnPrincipal.gameObject, new Vector3 (-100, Screen.height / 6, 0), transform.rotation);
		btnIzqSup.transform.SetParent (this.transform);
		pos_btn_B = btnIzqSup.GetComponent<Transform> ();
        Debug.Log("Repeticiones en la instacia " + num_repeticiones);
        if (num_repeticiones == 1)
        {
            pos_btn_B.GetComponent<Button>().onClick.AddListener(contar);
        }
        StartCoroutine(playsound ());

	}
	void InstanciarPosC () {
		btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
		GameObject btnIzqSup = Instantiate (btnPrincipal.gameObject, new Vector3 (Screen.width + 100, Screen.height / 6, 0), transform.rotation);
		btnIzqSup.transform.SetParent (this.transform);
		pos_btn_C = btnIzqSup.GetComponent<Transform> ();
        if (num_repeticiones == 1)
        {
            pos_btn_C.GetComponent<Button>().onClick.AddListener(contar);
        }
        StartCoroutine (playsound ());

	}

	void InstanciarPosD () {
		btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
		GameObject btnIzqSup = Instantiate (btnPrincipal.gameObject, new Vector3 (Screen.width + 100, (Screen.height / 6) * 5, 0), transform.rotation);
		btnIzqSup.transform.SetParent (this.transform);
		pos_btn_D = btnIzqSup.GetComponent<Transform> ();
        if (num_repeticiones == 1)
        {
            pos_btn_D.GetComponent<Button>().onClick.AddListener(contar);
        }
        StartCoroutine (playsound ());

	}

	void InstanciarPosE () {
		//POSICIONES INICIALES
		//btnPrincipal.GetComponentInChildren<Text> ().text = "Izquierda Medio";
		btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
		//btnPrincipal.onClick.AddListener(contar);
		GameObject btnIzqSup = Instantiate (btnPrincipal.gameObject, new Vector3 (-100, Screen.height / 2, 0), transform.rotation);
		btnIzqSup.transform.SetParent (this.transform);
		pos_btn_E = btnIzqSup.GetComponent<Transform> ();
        if (num_repeticiones == 1)
        {
            pos_btn_E.GetComponent<Button>().onClick.AddListener(contar);
        }
        StartCoroutine (playsound ());

	}

	void InstanciarPosF () {
		//POSICIONES INICIALES
		//btnPrincipal.GetComponentInChildren<Text> ().text = "Abajo Medio";
		btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
		//btnPrincipal.onClick.AddListener(contar);
		GameObject btnIzqSup = Instantiate (btnPrincipal.gameObject, new Vector3 (Screen.width / 2, -50, 0), transform.rotation);
		btnIzqSup.transform.SetParent (this.transform);
		pos_btn_F = btnIzqSup.GetComponent<Transform> ();
        if (num_repeticiones == 1)
        {
            pos_btn_F.GetComponent<Button>().onClick.AddListener(contar);
        }
        StartCoroutine (playsound ());

	}

	void InstanciarPosG () {
		//POSICIONES INICIALES
		//btnPrincipal.GetComponentInChildren<Text> ().text = "Derecha Medio";
		btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
		//btnPrincipal.onClick.AddListener(contar);
		GameObject btnIzqSup = Instantiate (btnPrincipal.gameObject, new Vector3 (Screen.width + 100, Screen.height / 2, 0), transform.rotation);
		btnIzqSup.transform.SetParent (this.transform);
		pos_btn_G = btnIzqSup.GetComponent<Transform> ();
        if (num_repeticiones == 1)
        {
            pos_btn_G.GetComponent<Button>().onClick.AddListener(contar);
        }
        StartCoroutine (playsound ());

	}

	void InstanciarPosH () {
		//POSICIONES INICIALES
		//btnPrincipal.GetComponentInChildren<Text> ().text = "Arriba Medio";
		btnPrincipal.GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0, 0, 256, 256), new Vector2 (0.5f, 0.5f));
		//btnPrincipal.onClick.AddListener(contar);
		GameObject btnIzqSup = Instantiate (btnPrincipal.gameObject, new Vector3 (Screen.width / 2, Screen.height + 50, 0), transform.rotation);
		btnIzqSup.transform.SetParent (this.transform);
		pos_btn_H = btnIzqSup.GetComponent<Transform> ();
        if (num_repeticiones == 1)
        {
            pos_btn_H.GetComponent<Button>().onClick.AddListener(contar);
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
        if (num_repeticiones != 1)
        {
            pos_btn_I.GetComponent<Button>().onClick.AddListener(contar);
        }
        StartCoroutine (playsound ());
	}

	IEnumerator playsound () {
		Debug.Log ("reproducuiendo......");
		audioGeneral.clip = audioUbicacion;
		audioGeneral.Play ();
		yield return new WaitForSeconds (audioGeneral.clip.length);
		audioGeneral.clip = audio_personaje_1;
		audioGeneral.Play ();
	}

    void contar()
    {
        estado_personaje++;
        if (estado_personaje == 1)
        {
            Debug.Log("iniciar segundo personaje e inicializar las variables");
            obtenerDatos1(codigoB);

        }else
        {
            Debug.Log("Fin de la retoralimentacion");
            codigoA = 0;
            codigoB = 0;
            btnMsg.SetActive(true);
        }
    }

}