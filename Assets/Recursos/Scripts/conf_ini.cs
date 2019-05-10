using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class conf_ini : MonoBehaviour {

    // Use this for initialization

    public Dropdown color1, color2, color3, color4, color5, color6, color7, color8, color9;
    public Text txtTitulo, txtcol3, txtcol4, txtcol5, txtcol6, txtcol7, txtcol8, txtcol9;
    public Dropdown velocidad;
    public Dropdown repeticiones;

    public static float velocidad_boton = 100f;
    public static int num_repeticiones;

    public static string a, b, c, d, e, f, g, h, i;
    void Start () {
        //velocidad_boton = 0;
        txtTitulo.text = Selec_level.nombre_nivel;
        cargarColores ();
        if (Selec_level.nivel <= 2) {
            color3.gameObject.SetActive (false);
            color4.gameObject.SetActive (false);
            color5.gameObject.SetActive (false);
            color6.gameObject.SetActive (false);
            color7.gameObject.SetActive (false);
            color8.gameObject.SetActive (false);
            color9.gameObject.SetActive (false);
            txtcol3.gameObject.SetActive (false);
            txtcol4.gameObject.SetActive (false);
            txtcol5.gameObject.SetActive (false);
            txtcol6.gameObject.SetActive (false);
            txtcol7.gameObject.SetActive (false);
            txtcol8.gameObject.SetActive (false);
            txtcol9.gameObject.SetActive (false);
        }
        if (Selec_level.nivel == 3) {
            color5.gameObject.SetActive (false);
            color6.gameObject.SetActive (false);
            color7.gameObject.SetActive (false);
            color8.gameObject.SetActive (false);
            color9.gameObject.SetActive (false);
            txtcol5.gameObject.SetActive (false);
            txtcol6.gameObject.SetActive (false);
            txtcol7.gameObject.SetActive (false);
            txtcol8.gameObject.SetActive (false);
            txtcol9.gameObject.SetActive (false);
        }
    }

    // Update is called once per frame
    void Update () {

    }

    public void cargarColores () {

        List<string> colores = new List<string> ();

        string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/dbdata.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection (conn);
        dbconn.Open ();
        IDbCommand dbcmd = dbconn.CreateCommand ();
        string sqlQuery = "Select * from color";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader ();
        while (reader.Read ()) {
            string color = reader.GetString (1);
            colores.Add (color);
        }

        color2.AddOptions (colores);
        color1.AddOptions (colores);
        color3.AddOptions (colores);
        color4.AddOptions (colores);
        color5.AddOptions (colores);
        color6.AddOptions (colores);
        color7.AddOptions (colores);
        color8.AddOptions (colores);
        color9.AddOptions (colores);

        reader.Close ();
        reader = null;
        dbcmd.Dispose ();
        dbcmd = null;
        dbconn.Close ();
    }

    public void datos () {
        a = color1.options[color1.value].text;
        b = color2.options[color2.value].text;
        c = color3.options[color3.value].text;
        d = color4.options[color4.value].text;
        e = color5.options[color5.value].text;
        f = color6.options[color6.value].text;
        g = color7.options[color7.value].text;
        h = color8.options[color8.value].text;
        i = color9.options[color9.value].text;
        num_repeticiones = repeticiones.value + 2;

        Debug.Log (a + " ; " + b + " ; " + c + " ; " + d + " ; " + e + " ; " + f);

        if (Selec_level.nombre_nivel == "BASICO A") {
            SceneManager.LoadScene (3);
        }
        if (Selec_level.nombre_nivel == "BASICO B") {
            SceneManager.LoadScene (4);
        }
        if (Selec_level.nombre_nivel == "INTERMEDIO") {
            SceneManager.LoadScene (5);
        }
        if (Selec_level.nombre_nivel == "AVANZADO") {
            SceneManager.LoadScene (6);
        }

    }

    public void velocidad_ini (int index) {
        Debug.Log (index);
        if (index == 0) {
            velocidad_boton = 100f;
        }
        if (index == 1) {
            velocidad_boton = 120f;
        }
        if (index == 2) {
            velocidad_boton = 170f;
        }
        if (index == 3) {
            velocidad_boton = 250f;
        }

    }

    public void regresar () {
        SceneManager.LoadScene (1);
    }

}