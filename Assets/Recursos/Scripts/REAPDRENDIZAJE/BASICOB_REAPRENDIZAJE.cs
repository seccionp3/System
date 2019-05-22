using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BASICOB_REAPRENDIZAJE : MonoBehaviour
{
    private String nombre_ubicacion;
    private int contador = -1;
    private rgb color_personaje_1 = new rgb();
    private int num_repeticiones, velocidad = 0;
    public static int codigoA, codigoB;
    public RectTransform panelA, panelB;
    private Image imgA, imgB;
    public Button btnPrincipal;
    public AudioSource audioGeneral;
    public AudioClip audio_personaje_1, audio_personaje_2, audioUbicacion;
    private AudioClip a, b;
    private Texture2D tex;
    public GameObject btnMsg;
    private int estado_personaje = 0;
    private int yi, yf;
    private Transform pos_btn_A, pos_btn_B;
    // Start is called before the first frame update
    void Start()
    {
        btnMsg.SetActive(false);
        obtenerDatos1(codigoA);
        imgA = panelA.GetComponent<Image>();
        imgB = panelB.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        if (contador == 0)
        {
            imgA.color = new Color(color_personaje_1.r, color_personaje_1.g, color_personaje_1.b);
            if (pos_btn_A.position.y < ((Screen.height / 2) + yf))
            {
                pos_btn_A.position += new Vector3(0, Time.deltaTime * velocidad, 0f);
            }
            if (pos_btn_A.position.y >= ((Screen.height / 2) + yf))
            {
                if (num_repeticiones > 0)
                {
                 
                    Destroy(pos_btn_A.gameObject);
                    InstanciarIzq();
                    num_repeticiones--;
                }
            }
        }

        if (contador == 1)
        {
            imgB.color = new Color(color_personaje_1.r, color_personaje_1.g, color_personaje_1.b);
            if (pos_btn_B.position.y > ((Screen.height / 2)) + yf)
            {
                pos_btn_B.position += new Vector3(0, -Time.deltaTime * velocidad, 0);
            }
            if (pos_btn_B.position.y <= ((Screen.height / 2) + yf))
            {
                if (num_repeticiones > 0)
                {
                    
                    Destroy(pos_btn_B.gameObject, 1f);
                    InstanciarDer();
                    num_repeticiones--;
                }
            }
        }

    }

    public void regresarEscena()
    {
        SceneManager.LoadScene(14);
    }
    public void InstanciarIzq()
    {
        //POSICIONES INICIALES
        //btnPrincipal.GetComponentInChildren<Text>().text = "ARRIBA";
        btnPrincipal.GetComponentInChildren<Text>().alignment = TextAnchor.UpperCenter;
        btnPrincipal.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));
        //btnIzq = Instantiate(btnPrincipal.gameObject,new Vector3(-100,(Screen.height/2),0f),transform.rotation);
        GameObject btnIzq = Instantiate(btnPrincipal.gameObject, new Vector3((Screen.width / 2), yi, 0), transform.rotation);
        btnIzq.transform.SetParent(this.transform);
        pos_btn_A = btnIzq.GetComponent<Transform>();
        if (num_repeticiones == 1)
        {
            pos_btn_A.GetComponent<Button>().onClick.AddListener(contar);
        }
        StartCoroutine(playsound());
        //audioUbicacion.Play();
        //Debug.Log("Xa = "+ pos_btn_A.position.x + "Ya = " + pos_btn_A.position.y);

    }

    public void InstanciarDer()
    {
        //btnPrincipal.GetComponentInChildren<Text>().text = "ABAJO";
        btnPrincipal.GetComponentInChildren<Text>().alignment = TextAnchor.LowerCenter;
        btnPrincipal.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));
        GameObject btnDer = Instantiate(btnPrincipal.gameObject, new Vector3((Screen.width / 2), Screen.height + yi, 0), transform.rotation);
        btnDer.transform.SetParent(this.transform);
        pos_btn_B = btnDer.GetComponent<Transform>();
        if (num_repeticiones == 1)
        {
            pos_btn_B.GetComponent<Button>().onClick.AddListener(contar);
        }
        StartCoroutine(playsound());
    }



    private void obtenerDatos1(int id)
    {
        byte[] sonido_personaje = new byte[0];
        byte[] audio_ubicacion = new byte[0];
        byte[] imagen_personaje = new byte[0];
        string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/dbdata.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery;
        sqlQuery = "select t1.num_repeticiones, t1.velocidad_detalle_apre, t2.r_color, t2.g_color, t2.b_color, " +
            " t3.audio_personaje, t3.imagen_personaje, " +
            " t4.nombre_ubicacion, t4.audio_ubicacion, t4.pos_y_ini, t4.pos_y_fin " +
            " from detalle_aprendizaje as t1 inner join color as t2 on t2.id = t1.id_color " +
            " inner join personaje as t3 on t1.id_personaje = t3.id_personaje " +
            " inner join ubicacion as t4 on t1.id_ubicacion = t4.id where t1.id_detalle_apre = " + id;
        Debug.Log(sqlQuery);
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {

            num_repeticiones = reader.GetInt32(0);
            velocidad = reader.GetInt32(1);
            color_personaje_1.r = reader.GetInt32(2);
            color_personaje_1.g = reader.GetInt32(3);
            color_personaje_1.b = reader.GetInt32(4);

            nombre_ubicacion = reader.GetString(7);
            yi = reader.GetInt32(9);
            yf = reader.GetInt32(10);
            imagen_personaje = (byte[])reader["imagen_personaje"];
            audio_ubicacion = (byte[])reader["audio_ubicacion"];
            sonido_personaje = (byte[])reader["audio_personaje"];

        }
        Debug.Log("colores" + color_personaje_1.r + color_personaje_1.g + color_personaje_1.b);
        tex = new Texture2D(256, 256);
        tex.LoadImage(imagen_personaje);

        WAV sonido1 = new WAV(sonido_personaje);
        audio_personaje_1 = AudioClip.Create("personaje", sonido1.SampleCount, 1, sonido1.Frequency, false, false);
        audio_personaje_1.SetData(sonido1.LeftChannel, 0);

        WAV sonido2 = new WAV(audio_ubicacion);
        audioUbicacion = AudioClip.Create("ubicacion", sonido2.SampleCount, 1, sonido2.Frequency, false, false);
        audioUbicacion.SetData(sonido2.LeftChannel, 0);

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();

        //imgB.color = new Color (color_personaje_1.r, color_personaje_1.g, color_personaje_1.b);
        inicio_animacion();
        //nombre_ubicacion = "";
        //StartCoroutine (playsound (audio_personaje_1));
    }


    void inicio_animacion()
    {
        num_repeticiones--;
        if (nombre_ubicacion == "arriba")
        {
            contador = 0;
            InstanciarIzq();
        }
        if (nombre_ubicacion == "abajo")
        {
            contador = 1;
            InstanciarDer();
        }

    }

    void contar()
    {
        estado_personaje++;
        if (estado_personaje == 1)
        {
            Debug.Log("iniciar segundo personaje e inicializar las variables");
            obtenerDatos1(codigoB);

        }
        else
        {
            Debug.Log("Fin de la retoralimentacion");
            codigoA = 0;
            codigoB = 0;
            btnMsg.SetActive(true);
        }
    }

    IEnumerator playsound()
    {
        Debug.Log("reproducuiendo......");
        audioGeneral.clip = audioUbicacion;
        audioGeneral.Play();
        yield return new WaitForSeconds(audioGeneral.clip.length);
        audioGeneral.clip = audio_personaje_1;
        audioGeneral.Play();
    }
}
