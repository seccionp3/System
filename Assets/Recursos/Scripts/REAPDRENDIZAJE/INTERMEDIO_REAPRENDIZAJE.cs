using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class INTERMEDIO_REAPRENDIZAJE : MonoBehaviour
{

    private int estado_personaje = 0;
    public static int codigoA, codigoB;
    private String nombre_ubicacion;
    private int contador = -1;

    private int num_repeticiones, velocidad = 0;
    public AudioSource audioGeneral;
    public AudioClip audio_personaje_1, audio_personaje_2, audioUbicacion;

    private rgb color_personaje_1 = new rgb();
    public RectTransform panelA, panelB, panelC, panelD;
    private Image imgA, imgB, imgC, imgD;
    public Button btnPrincipal;

    private int xi, xf, yi, yf;

    private Transform pos_btn_A, pos_btn_B, pos_btn_C, pos_btn_D;



    public GameObject btnMsg;
    private Texture2D tex;
    // Start is called before the first frame update
    void Start()
    {
        btnMsg.SetActive(false);
        obtenerDatos1(codigoA);
        imgA = panelA.GetComponent<Image>();
        imgB = panelB.GetComponent<Image>();
        imgC = panelC.GetComponent<Image>();
        imgD = panelD.GetComponent<Image>();
    }

    void Update()
    {

        if (contador == 0)
        {
            imgA.color = new Color(color_personaje_1.r, color_personaje_1.g, color_personaje_1.b);
            if (pos_btn_A.position.x > ((Screen.width / 2) - xf))
            {
                pos_btn_A.position += new Vector3(-Time.deltaTime * velocidad, 0f, 0f);
            }
            if (pos_btn_A.position.x < ((Screen.width / 2) - xf) && pos_btn_A.position.y < (Screen.height + yf))
            {
                pos_btn_A.position += new Vector3(0f, Time.deltaTime * velocidad, 0f);
                if (pos_btn_A.position.y > (Screen.height + yf) && num_repeticiones > 0)
                {
                  
                    Destroy(pos_btn_A.gameObject);
                    InstanciarIzqSuperior();
                    num_repeticiones--;
                }
            }
        }

        if (contador == 1)
        {
            imgB.color = new Color(color_personaje_1.r, color_personaje_1.g, color_personaje_1.b);
            if (pos_btn_B.position.x > ((Screen.width / 2) - xf))
            {
                pos_btn_B.position += new Vector3(-Time.deltaTime * velocidad, 0f, 0f);
            }
            if (pos_btn_B.position.x < ((Screen.width / 2) - xf) && pos_btn_B.position.y > yf)
            {
                pos_btn_B.position += new Vector3(0f, -Time.deltaTime * velocidad, 0f);
                if (pos_btn_B.position.y < yf && num_repeticiones > 0)
                {
                
                    Destroy(pos_btn_B.gameObject);
                    InstanciarIzqInferior();
                    num_repeticiones--;
                }
            }
        }

        if (contador == 2)
        {
            imgC.color = new Color(color_personaje_1.r, color_personaje_1.g, color_personaje_1.b);
            if (pos_btn_C.position.x < ((Screen.width / 2) - xf))
            {
                pos_btn_C.position += new Vector3(Time.deltaTime * velocidad, 0f, 0f);
            }
            if (pos_btn_C.position.x > ((Screen.width / 2) - xf) && pos_btn_C.position.y > yf)
            {
                pos_btn_C.position += new Vector3(0f, -Time.deltaTime * velocidad, 0f);
                if (pos_btn_C.position.y < yf && num_repeticiones > 0)
                {
               
                    Destroy(pos_btn_C.gameObject);
                    InstanciarDerInferior();
                    num_repeticiones--;
                }
            }
        }

        if (contador == 3)
        {
            imgD.color = new Color(color_personaje_1.r, color_personaje_1.g, color_personaje_1.b);
            if (pos_btn_D.position.x < ((Screen.width / 2) - xf))
            {
                pos_btn_D.position += new Vector3(Time.deltaTime * velocidad, 0f, 0f);
            }
            if (pos_btn_D.position.x > ((Screen.width / 2) - xf) && pos_btn_D.position.y < (Screen.height + yf))
            {
                pos_btn_D.position += new Vector3(0f, Time.deltaTime * velocidad, 0f);
                if (pos_btn_D.position.y > (Screen.height + yf) && num_repeticiones > 0)
                {
                
                    Destroy(pos_btn_D.gameObject);
                    InstanciarDerSuperior();
                    num_repeticiones--;
                }
            }
        }

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
            " t4.nombre_ubicacion, t4.audio_ubicacion, t4.pos_x_ini , t4.pos_x_fin , t4.pos_y_ini , t4.pos_y_fin" +
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

            xi = reader.GetInt32(9);
            xf = reader.GetInt32(10);
            yi = reader.GetInt32(11);
            yf = reader.GetInt32(12);

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
        if (nombre_ubicacion == "izquierda arriba")
        {
            contador = 0;
            InstanciarIzqSuperior();
        }
        if (nombre_ubicacion == "izquierda abajo")
        {
            contador = 1;
            InstanciarIzqInferior();
        }
        if (nombre_ubicacion == "derecha abajo")
        {
            contador = 2;
            InstanciarDerInferior();
        }
        if (nombre_ubicacion == "derecha arriba")
        {
            contador = 3;
            InstanciarDerSuperior();
        }
 
    }
    public void regresarEscena()
    {
        SceneManager.LoadScene(9);
    }
    public void InstanciarIzqSuperior()
    {

        //POSICIONES INICIALES


        //btnPrincipal.GetComponentInChildren<Text>().text = "Izquierda Arriba";
        btnPrincipal.GetComponentInChildren<Text>().alignment = TextAnchor.UpperLeft;
        btnPrincipal.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));
        //GameObject btnIzqSup = Instantiate(btnPrincipal.gameObject,new Vector3(-100 ,(Screen.height/2) + 25  ,0),transform.rotation);
        GameObject btnIzqSup = Instantiate(btnPrincipal.gameObject, new Vector3(xi, (Screen.height / 2) + yi, 0), transform.rotation);
        btnIzqSup.transform.SetParent(this.transform);
        pos_btn_A = btnIzqSup.GetComponent<Transform>();
        if (num_repeticiones == 1)
        {
            pos_btn_A.GetComponent<Button>().onClick.AddListener(contar);
        }
        StartCoroutine(playsound());

    }

    public void InstanciarIzqInferior()
    {

        //POSICIONES INICIALES
        //btnPrincipal.GetComponentInChildren<Text>().text = "Izquierda Abajo";
        btnPrincipal.GetComponentInChildren<Text>().alignment = TextAnchor.LowerLeft;
        btnPrincipal.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));
        //GameObject btnIzqInf = Instantiate(btnPrincipal.gameObject,new Vector3( -100, (Screen.height/2) - 25 ,0),transform.rotation);
        GameObject btnIzqInf = Instantiate(btnPrincipal.gameObject, new Vector3(xi, (Screen.height / 2) + yi, 0), transform.rotation);

        btnIzqInf.transform.SetParent(this.transform);
        pos_btn_B = btnIzqInf.GetComponent<Transform>();
        if (num_repeticiones == 1)
        {
            pos_btn_B.GetComponent<Button>().onClick.AddListener(contar);
        }
        StartCoroutine(playsound());
        //Debug.Log("Xa = "+ pos_btn_A.position.x + "Ya = " + pos_btn_A.position.y);

    }

    public void InstanciarDerInferior()
    {

        btnPrincipal.GetComponentInChildren<Text>().text = "Derecha Abajo";
        btnPrincipal.GetComponentInChildren<Text>().alignment = TextAnchor.UpperRight;
        btnPrincipal.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));
        //GameObject btnDerInf = Instantiate(btnPrincipal.gameObject,new Vector3( Screen.width + 100 , (Screen.height/2) - 25  ,0),transform.rotation);
        GameObject btnDerInf = Instantiate(btnPrincipal.gameObject, new Vector3(Screen.width + xi, (Screen.height / 2) + yi, 0), transform.rotation);
        btnDerInf.transform.SetParent(this.transform);
        pos_btn_C = btnDerInf.GetComponent<Transform>();

        if (num_repeticiones == 1)
        {
            pos_btn_C.GetComponent<Button>().onClick.AddListener(contar);
        }
        StartCoroutine(playsound());
    }
    public void InstanciarDerSuperior()
    {

        //btnPrincipal.GetComponentInChildren<Text>().text = "Derecha Arriba";
        btnPrincipal.GetComponentInChildren<Text>().alignment = TextAnchor.UpperRight;
        btnPrincipal.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));
        // GameObject btnDerSup = Instantiate(btnPrincipal.gameObject,new Vector3( Screen.width + 100 , (Screen.height/2) + 25  ,0),transform.rotation);
        GameObject btnDerSup = Instantiate(btnPrincipal.gameObject, new Vector3(Screen.width + xi, (Screen.height / 2) + yi, 0), transform.rotation);
        btnDerSup.transform.SetParent(this.transform);
        pos_btn_D = btnDerSup.GetComponent<Transform>();
        if (num_repeticiones == 1)
        {
            pos_btn_D.GetComponent<Button>().onClick.AddListener(contar);
        }
        StartCoroutine(playsound());
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

}
