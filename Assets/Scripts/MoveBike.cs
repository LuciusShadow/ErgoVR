/***********************************************************
* Dateiname: MoveBike.cs
* Autor: Sascha Bach
* letzte Aenderung: 10.08.2015
* Inhalt: enthaelt die Implementierung der Klasse MoveBike
***********************************************************/
using UnityEngine;
using System.Collections;

/***********************************************************
* Klasse: MoveBike
* Beschreibung: Liest die Accelerometerdaten aus
* SerialPortScript und nutzt diese um Drehmoment auf dem
* Hinterrad zu generieren
***********************************************************/
public class MoveBike : MonoBehaviour {
	SerialPortScript accelerometer;		
	public WheelCollider frontWheel;
	public WheelCollider rearWheel;
	public GameObject bike;
	Transform bBody;

	/***********************************************************
	 * Methode: Start
	 * Beschreibung: Referenziert SerialPortScript
	 * Parameter: keine
	 * Rückgabewert: keiner
	 ***********************************************************/
	void Start () {
		accelerometer = GameObject.Find("DataHub").GetComponent<SerialPortScript>();
		//rb.centerOfMass = new Vector3(0, 1f, 1f);

	}

	/***********************************************************
	 * Methode: FixedUpdate
	 * Beschreibung: Berechnet Drehmoment und wendet diesen auf
	 * Räder an
	 * Parameter: keine
	 * Rückgabewert: keinen
	 ***********************************************************/
	void FixedUpdate () {
		
		float weight = 60f;										//Pauschal angenommenes Personengewicht
		weight = weight * 10; 									//Umrechnen in Newton
		float vx = Mathf.Abs(accelerometer.Acceleration.x);		
		float vz = Mathf.Abs(accelerometer.Acceleration.z);
		float force = 0f;
		//Example values in cm (Genaue Werte am Fahrrad ausmessen)
		int pedalr = 10;
		int frontTeethr = 20;
		int rearTeethr = 5;

		float torque = 0;
		float v;												//Variable für Beschleunigung
		v=vx+vz;

		//Berechne v nur wenn Accelerometerdaten Schwelle überschreiten
		if(v > 1.2f){
			force =  weight * v * pedalr;
		}
		//Hinteres Zahnrad * (Kraft / vorderes Zahnrad)
		torque = rearTeethr * (force / frontTeethr);
		//Kompensieren der Masseeinstellungen im Editor
		torque = torque * Time.deltaTime;

		//Umkippen des Rades vermeiden, setze Winkel um z auf Konstant null
		bike.transform.eulerAngles = new Vector3(
			bike.transform.eulerAngles.x,
			bike.transform.eulerAngles.y,
			0//bike.transform.eulerAngles.z
			);

			//Zu Testzwecken kann auch ohne den Sensor mit den Pfeiltasten beschleunigt werden
//			rearWheel.motorTorque = Input.GetAxis("Vertical") * 60;
//			frontWheel.motorTorque = Input.GetAxis("Vertical") * 60;

			//Umsetzung des Drehmoments
			rearWheel.motorTorque = torque;
			frontWheel.motorTorque = torque;
	}
}
