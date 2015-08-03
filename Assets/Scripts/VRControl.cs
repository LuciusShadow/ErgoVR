/***********************************************************
* Dateiname: VRControl.cs
* Autor: Sascha Bach
* letzte Aenderung: 03.08.2015
* Inhalt: enthaelt die Implementierung der Klasse VRControl
***********************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/***********************************************************
* Klasse: VRControl
* Beschreibung: Aktiviert bzw. deaktiviert das Tracking
* in X- bzw. Y-Richtung, je nach Checkboxeinstellung im
* Menue
***********************************************************/
public class VRControl : MonoBehaviour {
	
	public BoxCollider bikeCollider;	//Collider der mit Vertikaler Kopfbewegung gesteuert wird
	public Transform CamCenter;			//Referenzpunkt
	public Transform ovrRig;			//VR-CameraRig

	//Referenz auf Checkboxen
	public Toggle xTrackingEnabled;		
	public Toggle yTrackingEnabled;

	bool xTracking;
	bool yTracking;

	Vector3 startPos;
	float yFactor = 0;


	//Getter für aktuelle Beschleunigungsdaten des Head-Up Displays
	public Vector3 OvrAcc
	{
		get
		{
			return OVRManager.display.acceleration;
		}
	}
	//Getter für aktuelle Gyroskopdaten des Head-Up Displays
	public Vector3 OvrGyro
	{
		get
		{
			return OVRManager.display.angularVelocity;
		}
	}

	/***********************************************************
	 * Methode: Awake
	 * Beschreibung: Wird noch vor Start () aufgerufen. Initialisiert
	 * startPosition mit Position des Colliders
	 * Rückgabewert: keiner
	 ***********************************************************/
	void Awake(){
		startPos = bikeCollider.center;
	}

	/***********************************************************
	 * Methode: Start
	 * Beschreibung: Initalisiert Trackingvariablen anhand
	 * der Checkboxen des Menüs
	 * Parameter: keine
	 * Rückgabewert: keiner
	 ***********************************************************/
	void Start () {
		xTracking = xTrackingEnabled.isOn;
		yTracking = yTrackingEnabled.isOn;

			
	}
	

	/***********************************************************
	 * Methode: Update
	 * Beschreibung: Wird in jedem Frame aufgerufen. Aktiviert
	 * bzw. deaktiviert vertikales und horizontales Tracking
	 * Parameter: keine
	 * Rückgabewert: keinen
	 ***********************************************************/
	void Update () {
		xTracking = xTrackingEnabled.isOn;
		yTracking = yTrackingEnabled.isOn;

		Vector3 ovrAcc = new Vector3(0,0,0);
		if(xTracking || yTracking){
			ovrAcc = OVRManager.display.acceleration;
			yFactor = CamCenter.localPosition.y*5;

		}
			
		if(yTracking){
			bikeCollider.center = new Vector3(bikeCollider.center.x, startPos.z + yFactor, bikeCollider.center.z);
		}
		else {
			bikeCollider.center = startPos;
			OVRManager.tracker.isEnabled = false;
		}
	}
}
