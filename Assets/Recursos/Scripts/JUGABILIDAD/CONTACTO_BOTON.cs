using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CONTACTO_BOTON : MonoBehaviour {

	private int valor = 0;
	private bool sumar = true;

	public Text tiempo_boton_Derecha;

	// Use this for initialization
	void Start () {
		tiempo_boton_Derecha.text = ""+1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision other) {
		valor++;
		tiempo_boton_Derecha.text = "" + (valor-2);
		if(valor >= 10){
			gameObject.GetComponent<Renderer>().material.color = Color.red;
		}
		}

	private void OnCollisionStay(Collision other) {			
	
	}
}
