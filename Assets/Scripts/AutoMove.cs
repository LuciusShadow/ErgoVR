/***********************************************************
* Dateiname: AutoMove.cs
* Autor: Sascha Bach
* letzte Aenderung: 03.08.2015
* Inhalt: enthaelt die Implementierung der Klasse AutoMove
***********************************************************/
using UnityEngine;
using System.Collections;

/***********************************************************
* Klasse: Handlebars
* Beschreibung: Konstante Beschleunigung der WheelCollider
* wenn Sensorgestützte Steuerung inaktiv
***********************************************************/
public class AutoMove : MonoBehaviour {

	public WheelCollider frontWheel;
	public WheelCollider rearWheel;

	public int constTorque;

	
	/***********************************************************
	 * Methode: Update
	 * Beschreibung: Anwendung des definierten Moments auf
	 * WheelCollider
	 * Parameter: keine
	 * Rückgabewert: keinen
	 ***********************************************************/
	void Update () {
		frontWheel.motorTorque = constTorque;
		rearWheel.motorTorque = constTorque;
	}
}
