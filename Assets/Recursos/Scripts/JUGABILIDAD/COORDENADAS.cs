using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using Mono.Data.Sqlite;
using System.Data;
using System.Linq;

public class COORDENADAS : MonoBehaviour
{
    LeapProvider provider;
    private string nombre_usuario = LOGIN_JUGABILIDAD.nombre_usuario_log;
    private int nombre_nivel = LOGIN_JUGABILIDAD.codigosBasicoA.ElementAt(0);
    public string posicion_x, posicion_y, posicion_z;
    // Start is called before the first frame update
    void Start()
    {
        iniciarBusqueda();
    }

    // Update is called once per frame
    void Update()
    {
        procesoBusqueda();
        savePosicion(nombre_usuario, posicion_x, posicion_y, posicion_z, nombre_nivel);
    }

    public void iniciarBusqueda() {
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
    }

    public void procesoBusqueda() {
        Frame frame = provider.CurrentFrame;
        Hand hand = frame.Hands[0];
        Vector position = hand.PalmPosition;
        Vector direction = hand.Direction;
        posicion_x = position.x.ToString();
        posicion_y = position.y.ToString();
        posicion_z = position.z.ToString();
        Debug.Log("La posicion de la mano es:" + position + "La direccion de la mano es:" + direction);
    }

    public void savePosicion(string nombre_usuario, string posicion_x, string posicion_y, string posicion_z, int nombre_nivel)
    {
        string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/dbdata.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "INSERT INTO posicion (posicion_x,posicion_y, posicion_z,nombre_nivel, nombre_usuario) Values ('" + posicion_x  + "','" + posicion_y + "','" + posicion_z + "' , '" + nombre_nivel + "' , '" + nombre_usuario + "' )";
        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteReader();
        Debug.Log("Datos Guardados Corectamente!..");
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}
