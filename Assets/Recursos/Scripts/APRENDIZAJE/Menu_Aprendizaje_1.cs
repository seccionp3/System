using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Aprendizaje_1 : MonoBehaviour {
    public Dropdown usuarios, personaje1, personaje2, color1, color2;
    public Dropdown repeticiones;
    Dictionary<string, int> listUsuarios = new Dictionary<string, int> ();
    Dictionary<string, int> listColores = new Dictionary<string, int> ();
    Dictionary<string, int> listPersonaje = new Dictionary<string, int> ();

    public static int cod_user, cod_aprendizaje, cod_color_1, cod_color_2, cod_personaje_1, cod_personaje_2, num_repeticiones, velocidad_data;
    public static string colora, colorb, nombre_user;

    public Text txtMensaje;

    public static int cod_user_inicializado = 0;

    public static string fecha;
    void Start () {
        fecha = System.DateTime.Now.ToString ("dd/MM/yyyy");
        velocidad_data = 100;
        cargarUsuarios ();
        cargarColores ();
        cargarPersonajes ();
        if (cod_user != 0) {
            Debug.Log ("Valor ya inicializados anteriormente");
            cod_user_inicializado = cod_user;
        }
    }

    void Update () {

    }

    public void cargarColores () {
        string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/dbdata.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection (conn);
        dbconn.Open ();
        IDbCommand dbcmd = dbconn.CreateCommand ();
        string sqlQuery = "Select * from color";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader ();
        while (reader.Read ()) {
            int id = reader.GetInt32 (0);
            string color = reader.GetString (1);
            listColores.Add (color, id);
        }
        color1.AddOptions (listColores.Keys.ToList ());
        color2.AddOptions (listColores.Keys.ToList ());
        reader.Close ();
        reader = null;
        dbcmd.Dispose ();
        dbcmd = null;
        dbconn.Close ();
    }

    public void cargarUsuarios () {
        string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/dbdata.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection (conn);
        dbconn.Open ();
        IDbCommand dbcmd = dbconn.CreateCommand ();
        string sqlQuery = "Select * from usuario";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader ();
        while (reader.Read ()) {
            int id = reader.GetInt32 (0);
            string user = reader.GetString (1);
            listUsuarios.Add (user, id);
        }
        usuarios.AddOptions (listUsuarios.Keys.ToList ());
        reader.Close ();
        reader = null;
        dbcmd.Dispose ();
        dbcmd = null;
        dbconn.Close ();
    }

    public void cargarPersonajes () {
        string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/dbdata.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection (conn);
        dbconn.Open ();
        IDbCommand dbcmd = dbconn.CreateCommand ();
        string sqlQuery = "Select * from personaje";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader ();
        while (reader.Read ()) {
            int id = reader.GetInt32 (0);
            string nombre = reader.GetString (1);
            listPersonaje.Add (nombre, id);
        }
        personaje1.AddOptions (listPersonaje.Keys.ToList ());
        personaje2.AddOptions (listPersonaje.Keys.ToList ());
        reader.Close ();
        reader = null;
        dbcmd.Dispose ();
        dbcmd = null;
        dbconn.Close ();
    }

    public void verId () {
        //Repeticiones estaticas, +2 porque parte de 0.
        num_repeticiones = repeticiones.value + 2;

        string name = usuarios.options[usuarios.value].text;
        int id = listUsuarios[name];
        Debug.Log ("USUARIO => clave = " + name + " valor =  " + id);
        nombre_user = name;
        cod_user = id;

        name = color1.options[color1.value].text;
        id = listColores[name];
        Debug.Log ("Color 1 => clave = " + name + " valor =  " + id);
        colora = name;
        cod_color_1 = id;

        name = color2.options[color2.value].text;
        id = listColores[name];
        Debug.Log ("Color 2 => clave = " + name + " valor =  " + id);
        colorb = name;
        cod_color_2 = id;

        name = personaje1.options[personaje1.value].text;
        id = listPersonaje[name];
        Debug.Log ("Personaje 1 => clave = " + name + " valor =  " + id);
        cod_personaje_1 = id;

        name = personaje2.options[personaje2.value].text;
        id = listPersonaje[name];
        Debug.Log ("Personaje 2 => clave = " + name + " valor =  " + id);
        cod_personaje_2 = id;


        if(cod_color_1 == cod_color_2){
            txtMensaje.text = "Seleccione Diferentes Colores y Personajes";
        }
        if(cod_personaje_1 == cod_personaje_2){
            txtMensaje.tag = "Selecciones Diferentes Colores y Personajes";
        }
        if(cod_color_1 != cod_color_2 && cod_personaje_1 != cod_personaje_2){
            if( cod_user_inicializado == 0 ){
                saveAprendizaje (fecha, cod_user);
            }else{
                cod_user = cod_user_inicializado;
            }
            SceneManager.LoadScene (9);
        }
        

        

    }

    public void velocidad_ini (int index) {
        if (index == 0) {
            velocidad_data = 100;
        }
        if (index == 1) {
            velocidad_data = 130;
        }
        if (index == 2) {
            velocidad_data = 170;
        }
        if (index == 3) {
            velocidad_data = 220;
        }

    }

    public void saveAprendizaje (string fecha, int id_usurio) {
        string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/dbdata.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection (conn);
        dbconn.Open ();
        IDbCommand dbcmd = dbconn.CreateCommand ();
        string sqlQuery = "INSERT INTO aprendizaje (fecha_aprendizaje, id_usuario) Values ('" + fecha + "','" + id_usurio + "')";
        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteReader ();
        Debug.Log ("Datos Guardados Corectamente!..");
        dbcmd.Dispose ();
        dbcmd = null;
        dbconn.Close ();
        dbconn = null;
        obtenerIdAprendizaje ();
    }

    void obtenerIdAprendizaje () {
        string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/dbdata.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection (conn);
        dbconn.Open ();
        IDbCommand dbcmd = dbconn.CreateCommand ();
        string sqlQuery = " SELECT * from SQLITE_SEQUENCE where name = 'aprendizaje' ;";
        Debug.Log (sqlQuery);
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader ();
        while (reader.Read ()) {
            cod_aprendizaje = reader.GetInt32 (1);
        }
        Debug.Log ("Codigo de aprendizaje para guardar detalle_aprendizaje => " + cod_aprendizaje);
        reader.Close ();
        reader = null;
        dbcmd.Dispose ();
        dbcmd = null;
        dbconn.Close ();
    }

    public void regresarMenu () {
        SceneManager.LoadScene (0);
    }

}