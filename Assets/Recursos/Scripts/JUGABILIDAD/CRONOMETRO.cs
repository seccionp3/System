using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRONOMETRO : MonoBehaviour {

	public static string tiempoTranscurrido;
	public static float startTime;
	public  static float stopTime;
	public static float timerTime;
	public static bool  isRunning = false;

	// Use this for initialization
	void Start () {
		TimerReset ();
	}
	// Update is called once per frame
	void Update () {
		timerTime = stopTime + (Time.time - startTime);
		int minutesInt = (int) timerTime / 60;
		int secondsInt = (int) timerTime % 60;
		int seconds100Int = (int) (Mathf.Floor ((timerTime - (secondsInt + minutesInt * 60)) * 100));

		if (isRunning) {
			//timerMinutes.text = (minutesInt < 10) ? "0" + minutesInt : minutesInt.ToString ();
			//timerSeconds.text = (secondsInt < 10) ? "0" + secondsInt : secondsInt.ToString ();
			//timerSeconds100.text = (seconds100Int < 10) ? "0" + seconds100Int : seconds100Int.ToString ();
			// if(minutesInt < 10){
			// 	Debug.Log("Minutos = 0"  );
			// }else{
			// 	Debug.Log("Minutos = " + minutesInt.ToString () );
			// }
			// if(secondsInt < 10){
			// 	Debug.Log("Segundos = 0"  );
			// }else{
			// 	Debug.Log("Segundos = " + secondsInt.ToString () );
			// }
			// if(seconds100Int < 10){
			// 	Debug.Log("Milisegundos = 0"  );
			// }else{
			// 	Debug.Log("Milisegundos = " + seconds100Int.ToString ());
			// }

			tiempoTranscurrido = string.Format ("{00}:{01}:{02}", minutesInt.ToString(), secondsInt.ToString(), seconds100Int.ToString());
		}

	}

	public static void TimerStart () {
		if (!isRunning) {
			Debug.Log("START");
			isRunning = true;
			startTime = Time.time;
		}
	}

	public static void TimerStop () {
		if (isRunning) {
			Debug.Log ("STOP");
			isRunning = false;
			stopTime = timerTime;
		}
	}

	public static void TimerReset () {
		Debug.Log ("RESET");
		stopTime = 0;
		isRunning = false;
		//timerMinutes.text = timerSeconds.text = timerSeconds100.text = "00";
	}

}