/***********************************************************
* Dateiname: WheelRotation.cs
* Autor: Sascha Bach
* letzte Aenderung: 03.08.2015
* Inhalt: enthaelt die Implementierung der Klasse WheelRotation
***********************************************************/
using UnityEngine;
using System.Collections;

/***********************************************************
* Klasse: WheelRotation
* Beschreibung: Rotiert die Modelle der Räder entsprechend
* der Geschwindigkeit des Mutterobjekts
***********************************************************/
public class WheelRotation : MonoBehaviour {

	public Transform frontWheel;
	public Transform rearWheel;
	public Transform joint;
	public Transform rPedal;
	public Transform lPedal;

	public Rigidbody bike;

	float velocity;
	/***********************************************************
	 * Methode: Start
	 * Beschreibung: Initialisierung der Variablen
	 * Parameter: keine
	 * Rückgabewert: keiner
	 ***********************************************************/
	void Start () {
		velocity = 0;
	}
	
	/***********************************************************
	 * Methode: Update
	 * Beschreibung: Anwendung der Geschwindigkeit auf 
	 * Modelle des Fahrrad
	 * Parameter: keine
	 * Rückgabewert: keiner
	 ***********************************************************/
	void Update () {
		velocity = -bike.velocity.z;

		frontWheel.Rotate(velocity,0,0);		//Rotation des Vorderrads
		rearWheel.Rotate(velocity,0,0);			//Rotation des Hinterrads
		joint.Rotate(velocity/2,0,0);			//Rotation der PedalJoints
		rPedal.Rotate(-velocity/2,0,0);			//Rotation rechtes Pedal
		lPedal.Rotate(-velocity/2,0,0);			//Rotation linkes Pedal
	}
}
