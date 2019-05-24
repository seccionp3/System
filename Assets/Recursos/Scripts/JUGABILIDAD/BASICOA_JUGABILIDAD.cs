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

public class BASICOA_JUGABILIDAD : MonoBehaviour
{

    //Tiempos
    private string tiempo_reaccion, tiempo_cuadrado, tiempo_boton, acierto;
    public Text timerText;
    private float startTime;
    public GameObject HandRight;
    public GameObject HandLeft;
    private bool finalisacion = false;
    private int contador = 0;
    private string id_boton = "";
    private int contadorBoton;
    private string fecha = LOGIN_JUGABILIDAD.fecha;

    public int acierto_bd;
    public Text txtSalir;
    public GameObject btnSalir;
    public Text txtFinal;
    public int intentos_boton_seleccionado = 0;
    private int intentos_fallos = 0;
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

    public AudioClip correcto, intenta_otra, seleciona_otra_vez;

    public GameObject btnMsg;
    public GameObject btn_izquierda;
    private Texture2D texturaIzquierda;
    public GameObject btn_derecha;
    private Texture2D texturaDerecha;
    rgb colorIzq = new rgb();
    rgb colorDer = new rgb();
    private int codigo_detalle_aprendizaje_1 = LOGIN_JUGABILIDAD.codigosBasicoA.ElementAt(0);
    private int codigo_detalle_aprendizaje_2 = LOGIN_JUGABILIDAD.codigosBasicoA.ElementAt(1);
    private int number = 0;

    //Instanciar Clases
    public COORDENADAS coordenadas = new COORDENADAS();
    private float secondsCounter = 1, secondstoCounter = 1;
    private string nombre_usuario = LOGIN_JUGABILIDAD.nombre_usuario_log, seconds;
    private string posicion_x, posicion_y, posicion_z, mano;
    // Use this for initialization
    void Start()
    {
        iniciarReloj();
        coordenadas.iniciarBusqueda();
        tipoMano(HandRight, HandLeft);
        Debug.Log(mano);
        texturaIzquierda = new Texture2D(256, 256);
        texturaDerecha = new Texture2D(256, 256);
        txtcontinuar.text = "Iniciar";
        txtFinal.text = "";
        btnSalir.SetActive(false);
        txtSalir.text = "";
        btnMsg.SetActive(true);
        Debug.Log(codigo_detalle_aprendizaje_1);
        Debug.Log(codigo_detalle_aprendizaje_2);
        imgA = panelA.GetComponent<Image>();
        imgB = panelB.GetComponent<Image>();
        obtenerDatos1(codigo_detalle_aprendizaje_1);
        obtenerDatos2(codigo_detalle_aprendizaje_2);
        btnPrincipal.GetComponentInChildren<Text>().text = "";

    }

    // Update is called once per frame
    void Update()
    {
        procesoReloj();
        coordenadas.procesoBusqueda();
        //posicion_x = posicion_x.ToString ();

        tipoMano(HandRight,HandLeft);
        secondsCounter += Time.deltaTime;
        if (secondsCounter >= secondstoCounter) {
            //Añasdir Campo Mano
            coordenadas.savePosicion(nombre_usuario, posicion_x = COORDENADAS.posicion_x, posicion_y=COORDENADAS.posicion_y, posicion_z = COORDENADAS.posicion_z, codigo_detalle_aprendizaje_1, mano, intentos, fecha);
            secondsCounter = 0;
            number++;
        }

    }

    public void siguienteElemento()
    {

    }

    //colores guardados.
    private void obtenerDatos1(int id)
    {
        Debug.Log("funcion con id " + id);
        byte[] son = new byte[0];
        byte[] imagen_personaje = new byte[0];
        string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/dbdata.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery;

        sqlQuery = "select t2.r_color, t2.g_color, t2.b_color, t3.id_personaje, t4.nombre_ubicacion,  t3.audio_personaje, t3.imagen_personaje from detalle_aprendizaje as t1 inner join color as t2 on t2.id = t1.id_color inner join personaje as t3 on t1.id_personaje = t3.id_personaje inner join ubicacion as t4 on t1.id_ubicacion = t4.id where t1.id_detalle_apre = " + id;
        //"select color.r_color, color.g_color, color.b_color, ubicacion.nombre_ubicacion, tpersonaje.audio_personaje, tpersonaje.imagen_personaje from detalle_aprendizaje  as detalle_aprendizaje inner join color on color.id = id_color inner join ubicacion on ubicacion.id = id_ubicacion inner join personaje as tpersonaje on tpersonaje.id_personaje = detalle_aprendizaje.id_personaje where id_detalle_apre = " + id;
        Debug.Log(sqlQuery);
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            nombre_ubicacion = reader.GetString(4);
            id_personaje_1 = reader.GetInt32(3);
            imagen_personaje = (byte[])reader["imagen_personaje"];
            son = (byte[])reader["audio_personaje"];
            if (nombre_ubicacion == "derecha")
            {
                id_boton_derecha = id_personaje_1;
                colorDer.r = reader.GetInt32(0);
                colorDer.g = reader.GetInt32(1);
                colorDer.b = reader.GetInt32(2);
            }
            else
            {
                id_boton_izquierda = id_personaje_1;
                colorIzq.r = reader.GetInt32(0);
                colorIzq.g = reader.GetInt32(1);
                colorIzq.b = reader.GetInt32(2);
            }
        }
        Debug.Log(colorIzq.r);
        WAV sonido = new WAV(son);
        audio_personaje_1 = AudioClip.Create("personaje_1", sonido.SampleCount, 1, sonido.Frequency, false, false);
        audio_personaje_1.SetData(sonido.LeftChannel, 0);
        if (nombre_ubicacion == "derecha")
        {
            texturaDerecha.LoadImage(imagen_personaje);
        }
        else
        {
            texturaIzquierda.LoadImage(imagen_personaje);
        }
        playsoundOtravez(audio_personaje_1);
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        Debug.Log("Ya salio de la base de datos");
        nombre_ubicacion = "";
        StartCoroutine(playsound());
    }
    private void obtenerDatos2(int id)
    {
        byte[] son = new byte[0];
        byte[] imagen_personaje = new byte[0];
        string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/dbdata.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery;
        sqlQuery = "select t2.r_color, t2.g_color, t2.b_color, t3.id_personaje, t4.nombre_ubicacion,  t3.audio_personaje, t3.imagen_personaje from detalle_aprendizaje as t1 inner join color as t2 on t2.id = t1.id_color inner join personaje as t3 on t1.id_personaje = t3.id_personaje inner join ubicacion as t4 on t1.id_ubicacion = t4.id where t1.id_detalle_apre = " + id;
        Debug.Log(sqlQuery);
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            id_personaje_2 = reader.GetInt32(3);
            nombre_ubicacion = reader.GetString(4);
            son = (byte[])reader["audio_personaje"];
            imagen_personaje = (byte[])reader["imagen_personaje"];
            Debug.Log(id_personaje_2);
            if (nombre_ubicacion == "derecha")
            {
                id_boton_derecha = id_personaje_2;
                colorDer.r = reader.GetInt32(0);
                colorDer.g = reader.GetInt32(1);
                colorDer.b = reader.GetInt32(2);
            }
            else
            {
                id_boton_izquierda = id_personaje_2;
                colorIzq.r = reader.GetInt32(0);
                colorIzq.g = reader.GetInt32(1);
                colorIzq.b = reader.GetInt32(2);
            }
        }
        WAV sonido = new WAV(son);
        audio_personaje_2 = AudioClip.Create("personaje_2", sonido.SampleCount, 1, sonido.Frequency, false, false);
        audio_personaje_2.SetData(sonido.LeftChannel, 0);
        if (nombre_ubicacion == "derecha")
        {
            texturaDerecha.LoadImage(imagen_personaje);
        }
        else
        {
            texturaIzquierda.LoadImage(imagen_personaje);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();

    }
    public void interaccionPanelA()
    {
        //Debug.Log( "Tiempo del Cronometro = " + CRONOMETRO.cuentaAtras );
        imgA.color = new Color(colorIzq.r, colorIzq.g, colorIzq.b);
        // Guardar tiempo uno;
        if (tiempo_cuadrado == null)
        {
            //tiempo_cuadrado = CRONOMETRO.tiempoTranscurrido;

        }

    }
    public void interaccionPanelASalir()
    {
        imgA.color = new Color(255, 255, 255);
        // Guardar tiempo uno;

    }

    public void interccionPanelB()
    {
        imgB.color = new Color(colorDer.r, colorDer.g, colorDer.b);
        //Debug.Log(CRONOMETRO.tiempoTranscurrido);
        if (tiempo_cuadrado == null)
        {


            //tiempo_cuadrado = CRONOMETRO.tiempoTranscurrido;

        }
    }
    public void interccionPanelBSalir()
    {
        imgB.color = new Color(255, 255, 255);
    }

    public void VerPersonaje1()
    {
        //btn_izquierda.GetComponent<Image>().sprite = Sprite.Create(texturaIzquierda, new Rect(0,0,256,256), new Vector2(0.5f,0.5f));
    }

    public void VerPersonaje2()
    {
        //btn_derecha.GetComponent<Image>().sprite = Sprite.Create(texturaDerecha, new Rect(0,0,256,256), new Vector2(0.5f,0.5f));
    }

    public void botonderecho()
    {
        valorContinuar = 0;
        valorIzquierda = 0;
        tocar_boton_izquierdo = true;
        if (tocar_boton_derecho)
        {
            valorDerecha++;
        }
        if (valorDerecha == 1)
        {
            finalisarReloj();
            tiempo_cuadrado = timerText.text;
            Debug.Log(tiempo_cuadrado);
            reinicioReloj();
        }
        if (valorDerecha >= 50)
        {
            tiempo_boton = timerText.text;
            Debug.Log(tiempo_boton);
            //btn_derecha.GetComponent<Renderer>().material.color = Color.red;
            tocar_boton_derecho = false;
            //Indicar Imagen
            InstanciarDer();
            btn_izquierda.SetActive(false);
            btn_derecha.SetActive(false);
            reiniciar();
            btnMsg.SetActive(true);
            txtcontinuar.text = "CONTINUAR ";

            Debug.Log(" ID DERECHA  " + id_boton_derecha);
            if (estado_juego == 1 && intentos > 0)
            {
                //intentos--;
                if (id_personaje_1 == id_boton_derecha)
                {
                    Debug.Log(" ** ACIERTO **  ");
                    acierto = "acierto";
                    audioUbicacion.clip = correcto;
                    audioUbicacion.Play();
                    ACIERTO = 1;
                    intentos_boton_seleccionado++;
                    Debug.Log(" **  intentos para boto **  " + intentos_boton_seleccionado);

                    if (intentos_boton_seleccionado == 1)
                    {
                        id_boton = "Boton Normal";
                        btn_derecha.transform.localScale += new Vector3(0.05F, 0.05F, 0.0F);
                        StartCoroutine(playsoundOtravez(audio_personaje_1));
                    }

                    if (intentos_boton_seleccionado == 2)
                    {
                        id_boton = "Boton Grande";
                        btn_derecha.transform.localScale -= new Vector3(0.05F, 0.05F, 0.0F);
                        StartCoroutine(playsoundOtravez(audio_personaje_1));

                    }
                    if (intentos_boton_seleccionado == 3)
                    {
                        id_boton = "Boton Mediano";
                        btn_derecha.transform.localScale -= new Vector3(0.05F, 0.05F, 0.0F);
                        StartCoroutine(playsoundOtravez(audio_personaje_1));
                        intentos--;
                        contadorBoton++;
                    } else if (contadorBoton == 1) {
                        id_boton = "Boton Pequeño";
                        contadorBoton = 0;
                    }
                }
                else
                {
                    Debug.Log("--  NO ACIERTO -- ");
                    acierto = "no acierto";
                    audioUbicacion.clip = intenta_otra;
                    audioUbicacion.Play();
                    intentos_fallos++;
                    ACIERTO = 0;
                }
                saveAcierto(codigo_detalle_aprendizaje_1, tiempo_reaccion, tiempo_cuadrado, tiempo_boton, acierto, id_boton, fecha);
                Debug.Log("INTENTOS =>> " + intentos);
            }

            if (estado_juego == 2 && intentos > 0)
            {
                //intentos--;
                if (id_personaje_2 == id_boton_derecha)
                {
                    Debug.Log(" ** ACIERTO **  ");
                    acierto = "acierto";
                    audioUbicacion.clip = correcto;
                    audioUbicacion.Play();
                    ACIERTO = 3;
                    intentos_boton_seleccionado++;
                    Debug.Log(" **  intentos para boto **  " + intentos_boton_seleccionado);

                    if (intentos_boton_seleccionado == 1)
                    {
                        id_boton = "Boton Normal";
                        btn_derecha.transform.localScale += new Vector3(0.05F, 0.05F, 0.0F);
                        StartCoroutine(playsoundOtravez(audio_personaje_2));
                    }

                    if (intentos_boton_seleccionado == 2)
                    {
                        id_boton = "Boton Grande";
                        btn_derecha.transform.localScale -= new Vector3(0.05F, 0.05F, 0.0F);
                        StartCoroutine(playsoundOtravez(audio_personaje_2));

                    }
                    if (intentos_boton_seleccionado == 3)
                    {
                        id_boton = "Boton Mediano";
                        btn_derecha.transform.localScale -= new Vector3(0.05F, 0.05F, 0.0F);
                        StartCoroutine(playsoundOtravez(audio_personaje_2));
                        intentos--;
                        contadorBoton++;
                    } else if (contadorBoton == 1) {
                        id_boton = "Boton Pequeño";
                        contadorBoton = 0;
                    }
                }
                else
                {
                    Debug.Log("--  NO ACIERTO -- ");
                    acierto = "no acierto";
                    audioUbicacion.clip = intenta_otra;
                    ACIERTO = 2;
                    intentos_fallos++;
                    audioUbicacion.Play();
                }
                saveAcierto(codigo_detalle_aprendizaje_1, tiempo_reaccion, tiempo_cuadrado, tiempo_boton, acierto, id_boton, fecha);
                Debug.Log("INTENTOS =>> " + intentos);
            }

        }
        tiempo_boton_Derecha.text = "" + valorDerecha;
        tiempo_boton_Izquierda.text = "" + valorIzquierda;
    }

    public void botonIzquierdo()
    {
        valorContinuar = 0;
        valorDerecha = 0;
        tocar_boton_derecho = true;
        if (tocar_boton_izquierdo)
        {
            valorIzquierda++;
        }
        if (valorIzquierda == 1)
        {
            finalisarReloj();
            tiempo_cuadrado = timerText.text;
            Debug.Log(tiempo_cuadrado);
            reinicioReloj();
        }
        if (valorIzquierda >= 50)
        {
            tiempo_boton = timerText.text;
            Debug.Log(tiempo_boton);
            InstanciarIzq();
            //btn_izquierda.GetComponent<Renderer>().material.color = Color.red;
            tocar_boton_izquierdo = false;
            //Ocultar Botones
            btn_izquierda.SetActive(false);
            btn_derecha.SetActive(false);
            reiniciar();
            btnMsg.SetActive(true);
            txtcontinuar.text = "CONTINUAR ";

            Debug.Log(" id izquierda " + id_boton_izquierda);
            if (estado_juego == 1 && intentos > 0)
            {
                //intentos--;
                if (id_personaje_1 == id_boton_izquierda)
                {
                    Debug.Log(" ** ACIERTO **  ");
                    acierto = "acierto";
                    audioUbicacion.clip = correcto;
                    audioUbicacion.Play();
                    ACIERTO = 1;
                    intentos_boton_seleccionado++;
                    Debug.Log(" **  intentos para boto **  " + intentos_boton_seleccionado);

                    if (intentos_boton_seleccionado == 1)
                    {
                        id_boton = "Boton Normal";
                        btn_izquierda.transform.localScale += new Vector3(0.05F, 0.05F, 0.0F);
                        StartCoroutine(playsoundOtravez(audio_personaje_1));
                    }

                    if (intentos_boton_seleccionado == 2)
                    {
                        id_boton = "Boton Grande";
                        btn_izquierda.transform.localScale -= new Vector3(0.05F, 0.05F, 0.0F);
                        StartCoroutine(playsoundOtravez(audio_personaje_1));

                    }
                    if (intentos_boton_seleccionado == 3)
                    {
                        id_boton = "Boton Mediano";
                        btn_izquierda.transform.localScale -= new Vector3(0.05F, 0.05F, 0.0F);
                        StartCoroutine(playsoundOtravez(audio_personaje_1));
                        intentos--;
                        contadorBoton++;
                    } else if (contadorBoton == 1) {
                        id_boton = "Boton Pequeño";
                        contadorBoton = 0;
                    }
                }
                else
                {
                    Debug.Log("--  NO ACIERTO -- ");
                    acierto = "no acierto";
                    audioUbicacion.clip = intenta_otra;
                    audioUbicacion.Play();
                    ACIERTO = 0;
                    intentos_fallos++;
                }
                saveAcierto(codigo_detalle_aprendizaje_1, tiempo_reaccion, tiempo_cuadrado, tiempo_boton, acierto, id_boton, fecha);
                Debug.Log("INTENTOS =>> " + intentos);
            }
            else
            {
                Debug.Log("Se acabaron los Intentos");
            }

            if (estado_juego == 2 && intentos > 0)
            {
                //intentos--;
                if (id_personaje_2 == id_boton_izquierda)
                {
                    Debug.Log(" ** ACIERTO **  ");
                    acierto = "acierto";
                    audioUbicacion.clip = correcto;
                    audioUbicacion.Play();
                    ACIERTO = 3;
                    intentos_boton_seleccionado++;
                    Debug.Log(" **  intentos para boto **  " + intentos_boton_seleccionado);

                    if (intentos_boton_seleccionado == 1)
                    {
                        id_boton = "Boton Normal";
                        btn_izquierda.transform.localScale += new Vector3(0.05F, 0.05F, 0.0F);
                        StartCoroutine(playsoundOtravez(audio_personaje_2));

                    }

                    if (intentos_boton_seleccionado == 2)
                    {
                        id_boton = "Boton Grande";
                        btn_izquierda.transform.localScale -= new Vector3(0.05F, 0.05F, 0.0F);
                        StartCoroutine(playsoundOtravez(audio_personaje_2));

                    }
                    if (intentos_boton_seleccionado == 3)
                    {
                        id_boton = "Boton Mediano";
                        btn_izquierda.transform.localScale -= new Vector3(0.05F, 0.05F, 0.0F);
                        StartCoroutine(playsoundOtravez(audio_personaje_2));
                        intentos--;
                        contadorBoton++;
                    } else if (contadorBoton == 1) {
                        id_boton = "Boton Pequeño";
                        contadorBoton = 0;
                    }
                }
                else
                {
                    Debug.Log("--  NO ACIERTO -- ");
                    acierto = "no acierto";
                    audioUbicacion.clip = intenta_otra;
                    audioUbicacion.Play();
                    ACIERTO = 2;
                    intentos_fallos++;
                }
                saveAcierto(codigo_detalle_aprendizaje_1, tiempo_reaccion, tiempo_cuadrado, tiempo_boton, acierto, id_boton, fecha);
                Debug.Log("INTENTOS =>> " + intentos);
            }
            else
            {
                Debug.Log("Se acabaron los intentos");
            }

        }
        tiempo_boton_Derecha.text = "" + valorDerecha;
        tiempo_boton_Izquierda.text = "" + valorIzquierda;
    }

    public void continuar()
    {
        tiempo_boton_Derecha.text = "";
        tiempo_boton_Izquierda.text = "";
        valorContinuar++;
        txtcontinuar.text = "CONTINUAR " + valorContinuar;
        if (valorContinuar >= 50)
        {
            if (contador == 0)
            {
                finalisarReloj();
                tiempo_reaccion = timerText.text;
                Debug.Log(tiempo_reaccion);
                contador++;
            }
            reinicioReloj();
            iniciarReloj();
            txtcontinuar.text = "";
            btnMsg.SetActive(false);
            btn_izquierda.SetActive(true);
            btn_derecha.SetActive(true);
            if (pos_btn_A != null)
            {
                pos_btn_A.gameObject.SetActive(false);
            }
            if (pos_btn_B != null)
            {
                pos_btn_B.gameObject.SetActive(false);
            }

            if (ACIERTO == 0 && intentos > 0 && intentos_boton_seleccionado != 0)
            {
                StartCoroutine(playsound());
            }

            if (ACIERTO == 1 && intentos > 0 && intentos_boton_seleccionado == 4 && intentos_fallos != 3)
            {
                StartCoroutine(playsound2());
                estado_juego = 2;
                intentos_boton_seleccionado = 0;

                //intentos++;
            }

            if (ACIERTO == 2 && intentos > 0 && intentos_fallos != 3)
            {
                StartCoroutine(playsound2());
                estado_juego = 2;
                intentos_boton_seleccionado = 0;

                //intentos++;
            }
            if (intentos_fallos >= 3)
            {
                BASICOA_REAPRENDIZAJE.codigoA = codigo_detalle_aprendizaje_1;
                BASICOA_REAPRENDIZAJE.codigoB = codigo_detalle_aprendizaje_2;
                Debug.Log("Se acabaron tus intentos");
                btn_izquierda.SetActive(false);
                btn_derecha.SetActive(false);
                txtFinal.text = "Numero de intentos alcanzados, regrese ala seccion de aprendizaje.";
                btnSalir.SetActive(true);
                txtSalir.text = "Continuar";
                valorContinuar = 0;
            }
            Debug.Log("intenos = " + intentos + " acierto = " + ACIERTO + " INTENTO BOTON = " + intentos_boton_seleccionado);
            if (intentos == 1 && ACIERTO == 3 && intentos_boton_seleccionado == 4)
            {
                Debug.Log("Se acabaron tus intentos");
                btn_izquierda.SetActive(false);
                btn_derecha.SetActive(false);
                txtFinal.text = "Fase terminada... presiona en salir";
                btnSalir.SetActive(true);
                txtSalir.text = "Salir ";
                valorContinuar = 0;
            }

        }
    }

    void reiniciar()
    {
        valorDerecha = 0;
        valorIzquierda = 0;
        //btn_izquierda.GetComponent<Renderer>().material.color = Color.gray;
        //btn_derecha.GetComponent<Renderer>().material.color = Color.gray;
        tocar_boton_izquierdo = true;
        tocar_boton_derecho = true;
        tiempo_boton_Derecha.text = "B";
        tiempo_boton_Izquierda.text = "A";
    }

    public void botonSalir()
    {

        valorContinuar++;
        txtSalir.text = "SALIR " + valorContinuar;
        if (valorContinuar >= 50)
        {
            if (intentos_fallos >= 3)
            {
                SceneManager.LoadScene(21);
            }
            else
            {
                SceneManager.LoadScene(15);
            }
            valorContinuar = 0;

        }

    }

    public void InstanciarIzq()
    {
        btnPrincipal.GetComponent<Image>().sprite = Sprite.Create(texturaIzquierda, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));
        GameObject btnIzq = Instantiate(btnPrincipal.gameObject, new Vector3(-0.1f, 0f, 0.1f), transform.rotation);
        btnIzq.transform.SetParent(this.transform);
        btnIzq.transform.localScale = new Vector3(1f, 1f, 0f);
        pos_btn_A = btnIzq.GetComponent<Transform>();
    }
    public void InstanciarDer()
    {
        btnPrincipal.GetComponent<Image>().sprite = Sprite.Create(texturaDerecha, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));
        GameObject btnDer = Instantiate(btnPrincipal.gameObject, new Vector3(0.1f, 0f, 0.1f), transform.rotation);
        btnDer.transform.SetParent(this.transform);
        btnDer.transform.localScale = new Vector3(1f, 1f, 0f);
        pos_btn_B = btnDer.GetComponent<Transform>();
    }

    IEnumerator playsound()
    {
        Debug.Log("reproducuiendo......");
        audioUbicacion.clip = donde_esta;
        audioUbicacion.Play();
        yield return new WaitForSeconds(audioUbicacion.clip.length);
        audioUbicacion.clip = audio_personaje_1;
        audioUbicacion.Play();
        //INICIAR TIEMPO 1
        //TimerStart ();
        iniciarReloj();
        contador = 0;
    }

    IEnumerator playsound2()
    {
        valorContinuar = 0;
        Debug.Log("reproducuiendo......");
        audioUbicacion.clip = donde_esta;
        audioUbicacion.Play();
        yield return new WaitForSeconds(audioUbicacion.clip.length);
        audioUbicacion.clip = audio_personaje_2;
        audioUbicacion.Play();
        txtcontinuar.text = "Iniciar";
        btnMsg.SetActive(true);
        iniciarReloj();
        contador = 0;
    }

    IEnumerator playsoundOtravez(AudioClip audio_personaje)
    {
        Debug.Log("reproducuiendo......");
        audioUbicacion.clip = seleciona_otra_vez;
        audioUbicacion.Play();
        yield return new WaitForSeconds(audioUbicacion.clip.length);
        audioUbicacion.clip = audio_personaje;
        audioUbicacion.Play();
    }

    public void saveAcierto(int id_detalle_aprendizaje, string time1, string time2, string time3, string acierto, string id_boton, string fecha)
    {
        string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/dbdata.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "INSERT INTO detalle_aprendizaje_acierto (tiempo_reaccion,tiempo_cuadro, tiempo_boton, acierto, id_detalle_aprendizaje, id_boton, fecha) Values ('" + time1 + "','" + time2 + "','" + time3 + "' , '" + acierto + "' , '" + id_detalle_aprendizaje + "','" + id_boton + "' ,'" + fecha + "')";
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

    public string tipoMano(GameObject HandRight, GameObject HandLeft) {
        if (HandRight.activeInHierarchy)
        {
            mano = "Mano Derecha";
        }
        else if(HandLeft.activeInHierarchy)
        {
            mano = "Mano Izquierda";
        }
        return mano;
    }

}
