using System.Collections;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine;

public class DBSQLITE : MonoBehaviour {

    // Use this for initialization
    public AudioSource audioA;
    void Start () {
        //sonido ();
    }

    // Update is called once per frame
    void Update () {

    }

    void sonido () {
        byte[] son = new byte[0];
        string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/sonido_prueba.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection (conn);
        dbconn.Open ();
        IDbCommand dbcmd = dbconn.CreateCommand ();
        string sqlQuery = "Select sonido from sonido where id = 1";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader ();
        while (reader.Read ()) {

            son = (byte[]) reader["sonido"];
            Debug.Log (son.Length);

        }

        WAV sonido = new WAV (son);
        AudioClip audioClip = AudioClip.Create ("testSound", sonido.SampleCount, 1, sonido.Frequency, false, false);
        audioClip.SetData (sonido.LeftChannel, 0);
        audioA.clip = audioClip;
        audioA.Play ();

        reader.Close ();
        reader = null;
        dbcmd.Dispose ();
        dbcmd = null;
        dbconn.Close ();
    }

}