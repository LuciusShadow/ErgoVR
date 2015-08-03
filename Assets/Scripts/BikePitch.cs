﻿/***********************************************************
* Dateiname: BikePitch.cs
* Autor: Sascha Bach
* letzte Aenderung: 03.08.2015
* Inhalt: enthaelt die Implementierung der Klasse BikePitch
***********************************************************/
using UnityEngine;
using System.Collections;

/***********************************************************
* Klasse: BikePitch
* Beschreibung: Berechnet anhand der Accelerometerdaten
* die Fahrradneigung
***********************************************************/
public class BikePitch : MonoBehaviour {

	public float turnSpeed = 100;  //Rotationsgeschwindigkeit
	public float maxRotation = 30; //Maximaler Winkel
	public GameObject bike;

	float pitch;				   
	Vector3 bikePosition;
	int turnspeed;
	SerialPortScript accelerometer;

	bool pitchEnabled;
	
	public bool PitchEnabled
	{
		get
		{
			return pitchEnabled;
		}
		
		set
		{
			pitchEnabled = value;
		}
	}

	/***********************************************************
	 * Methode: Start
	 * Beschreibung: Referenziert SerialPortScript, Inialisierung
	 * Parameter: keine
	 * Rückgabewert: keiner
	 ***********************************************************/
	void Start () {
		accelerometer = GameObject.Find("DataHub").GetComponent<SerialPortScript>();

		pitch = 0f;

	}
	
	/***********************************************************
	 * Methode: Update
	 * Beschreibung: Berechnet Neigung aus Accelerometerdaten
	 * Parameter: keine
	 * Rückgabewert: keinen
	 ***********************************************************/
	void Update () {
		//Berechnung der Neigung
		pitch = accelerometer.Acceleration.y;
		pitch = -pitch * maxRotation;
		//Passe Richtung an.
		//transform.rotation =  Quaternion.Euler(transform.rotation.x,-bike.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

		//Anwendung der Neigung auf bis maximal 30 Grad
		if(Mathf.Abs(pitch) <= 30f)
			//transform.RotateAround(rotationPoint.position, transform.forward, pitch);
			setEulerAngles(transform.eulerAngles.x, -180 + bike.transform.rotation.eulerAngles.y, 0 -pitch);
			//else
			//transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,90,0),turnSpeed*Time.deltaTime);
	}

	void setEulerAngles(float x, float y, float z){
		transform.eulerAngles = new Vector3(x,	y,	z);
	}
}

	
