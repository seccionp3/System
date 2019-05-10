using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class APRENDIZAJE : MonoBehaviour {

    // Use this for initialization
    public int contador = 0;
    public float velocidad;
    public Button btnA, btnB, btnC, btnD;
    private Transform movB, movA, movC, movD;

    public int d = 400;

    public RectTransform panelA, panelB, panelC, panelD;
    private Image imgA, imgB, imgC, imgD;

    void Start () {
        imgA = panelA.GetComponent<Image> ();
        imgB = panelB.GetComponent<Image> ();
        imgC = panelC.GetComponent<Image> ();
        imgD = panelD.GetComponent<Image> ();
        getcolor (conf_ini.a, imgA);
        movA = btnA.transform;
        btnB.interactable = false;
        movB = btnB.transform;
        btnC.interactable = false;
        movC = btnC.transform;
        btnD.interactable = false;
        movD = btnD.transform;

    }

    // Update is called once per frame
    void Update () {
        if (contador == 0) {
            Transform movA = btnA.transform;
            if (movA.position.x < 530) {
                movA.position += new Vector3 (Time.deltaTime * velocidad, 0f, 0f);
            }
            if (movA.position.x > 530 && movA.position.y < 530) {
                movA.position += new Vector3 (0f, Time.deltaTime * velocidad, 0f);
            }

            // if(movA.position.x >= 531 && movA.position.y >= 530){

            // 	 movA.Rotate( Vector3.up * velocidad * Time.deltaTime);
            // }

        }

        if (contador == 3) {
            if (movB.position.x < 530) {
                movB.position += new Vector3 (Time.deltaTime * velocidad, 0f, 0f);
            }
            if (movB.position.x > 530 && movB.position.y > 60) {
                movB.position += new Vector3 (0f, -Time.deltaTime * velocidad * 2, 0f);
                if (movB.position.y < 60) {
                    btnB.interactable = true;
                }
            }
            // if(movB.position.x >= 531 && movB.position.y <= 60 ){
            // 	d--;
            // 	 movB.Rotate( Vector3.up * velocidad * Time.deltaTime);
            // 	Debug.Log(d);
            // }
        }

        if (contador == 6) {
            if (movC.position.x > 890) {
                movC.position += new Vector3 (-Time.deltaTime * velocidad, 0f, 0f);
            }
            if (movC.position.x < 890 && movC.position.y > 60) {
                movC.position += new Vector3 (0f, -Time.deltaTime * velocidad * 2, 0f);
                if (movC.position.y < 60) {
                    btnC.interactable = true;
                }
            }
        }

        if (contador == 9) {
            if (movD.position.x > 890) {
                movD.position += new Vector3 (-Time.deltaTime * velocidad, 0f, 0f);
            }
            if (movD.position.x < 890 && movD.position.y < 530) {
                movD.position += new Vector3 (0f, Time.deltaTime * velocidad * 2, 0f);
                btnD.interactable = true;
                if (movD.position.y > 530) {
                    btnD.interactable = true;
                }
            }
        }
    }

    public void click () {

        Debug.Log (contador);

        contador++;
        if (contador == 3) {
            Destroy (btnA.gameObject, 0.1f);
            imgA.color = Color.white;
            getcolor (conf_ini.b, imgB);
        }
        if (contador == 6) {
            Destroy (btnB.gameObject, 0.1f);
            imgB.color = Color.white;
            getcolor (conf_ini.c, imgC);
        }
        if (contador == 9) {
            imgC.color = Color.white;
            Destroy (btnC.gameObject, 0.1f);
            getcolor (conf_ini.d, imgD);
        }
        if (contador == 12) {
            imgD.color = Color.white;
            Destroy (btnD.gameObject, 0.1f);
        }

    }

    void getcolor (string nombre_color, Image panel) {
        rgb data = new rgb ();
        string conn = "URI=file:" + Application.dataPath + "/Recursos/BD/dbdata.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection (conn);
        dbconn.Open ();
        IDbCommand dbcmd = dbconn.CreateCommand ();
        string sqlQuery = "Select * from color where nombre_color = '" + nombre_color + "'";
        Debug.Log (sqlQuery);
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader ();
        while (reader.Read ()) {
            //int id = reader.GetInt32(0);
            int r = reader.GetInt32 (3);
            int g = reader.GetInt32 (5);
            int b = reader.GetInt32 (4);
            data.r = r;
            data.g = g;
            data.b = b;
        }
        panel.color = new Color (data.r, data.g, data.b);

        reader.Close ();
        reader = null;
        dbcmd.Dispose ();
        dbcmd = null;
        dbconn.Close ();
    }

}