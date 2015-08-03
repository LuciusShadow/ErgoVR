﻿/***********************************************************
* Dateiname: Handlebars.cs
* Autor: Sascha Bach
* letzte Aenderung: 03.08.2015
* Inhalt: enthaelt die Implementierung der Klasse Menu
***********************************************************/
using UnityEngine;
using System.Collections;

/***********************************************************
* Klasse: Handlebars
* Beschreibung: Wendet Gyroskopdaten auf Modell des Lenkers
* und WheelCollider an
***********************************************************/
public class Handlebars : MonoBehaviour {
	
	Vector3 bikePosition;
	int maxAngle = 10;

	public Transform body;
	public WheelCollider frontWheel;
	public PhoneSensor phone;
	Vector3 gyroData;
	public GameObject obstacles;

	/***********************************************************
	 * Methode: Start
	 * Beschreibung: Initalisierung
	 * Parameter: keine
	 * Rückgabewert: keiner
	 ***********************************************************/
	void Start () {
		gyroData = new Vector3(0,0,0);
	}
	
	/***********************************************************
	 * Methode: FixedUpdate
	 * Beschreibung: Wendet Gyroskopdaten auf Steuerwinkel des
	 * WheelColliders und des Lenkermodells an. Wenn dieses
	 * Model deaktiviert wird, werden Hindernisse entlang des
	 * Pfades deaktiviert
	 * Parameter: keine
	 * Rückgabewert: keinen
	 ***********************************************************/
	void FixedUpdate () {
		gyroData = phone.Gyrodata;		

		//Steuere Hindernisserscheinung
		if(obstacles.activeSelf == false)
			obstacles.SetActive(true);

		//bikePosition = bike.transform.position;
		//transform.position = new Vector3(bikePosition.x + 2, bikePosition.y + 1, bikePosition.z);
		//transform.Rotate(this.transform.up, 10 * Time.deltaTime); 
		//Testing

		#region Manuelle Steuerung
		//float rotate = accelerometer.Acceleration.y * maxAngle;
		//float steer = 30 * Input.GetAxis("Horizontal");
		#endregion

		float rotate = -gyroData.z * maxAngle;
		frontWheel.steerAngle = transform.localEulerAngles.y; //Winkel des Wheelcolliders
		transform.Rotate(0,rotate,0);						  //Winkel des Modells
		//setEulerAngles(transform.eulerAngles.x, rotate+body.eulerAngles.y, body.eulerAngles.z);


	}

//	void setEulerAngles(float x, float y, float z){
//		transform.eulerAngles = new Vector3(x,	y,	z);
//	}
}
