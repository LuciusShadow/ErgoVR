//Entommen von https://youtu.be/r5U5O7WncHk

/***********************************************************
* Dateiname: AutoSteer.cs
* Autor: Sascha Bach
* letzte Aenderung: 03.08.2015
* Inhalt: enthaelt die Implementierung der Klasse AutoSteer
***********************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***********************************************************
* Klasse: AutoSteer
* Beschreibung: Implementiert autonome Steuerung durch
* abfahren eines in Unity festgelegten Pfades
***********************************************************/
public class AutoSteer : MonoBehaviour {
	System.Collections.Generic.List<Transform> path = new System.Collections.Generic.List<Transform>();
	public Transform body;
	public Transform pathGroup;

	public float maxSteer = 35.0f;
	
	public WheelCollider frontWheel;

	public int currentPathObject;
	public float distanceFromPath = 2;
	public float dir;

	public GameObject obstacles;

	/***********************************************************
	 * Methode: Start
	 * Beschreibung: Wandelt die Path-Gruppe in ein Array um
	 * Parameter: keine
	 * Rückgabewert: keinen
	 ***********************************************************/
	void Start () {
		Transform[] pathObjects = pathGroup.GetComponentsInChildren<Transform>();
		path = new List<Transform>();
		
		foreach(Transform pathObject in pathObjects){
			
			if(pathObject != pathGroup){
				path.Add(pathObject);
			}
		}
	}

	/***********************************************************
	 * Methode: Update
	 * Beschreibung: Sucht nächstes Element des Path-Arrays,
	 * fährt dieses an und fokussiert anschließend das 
	 * darauffolgende Element
	 * Parameter: keine
	 * Rückgabewert: keinen
	 ***********************************************************/
	void Update () {
		//Steuere Hindernisserscheinung
		if(obstacles.activeSelf == true)
			obstacles.SetActive(false);

		//Berechnung des Vektors zum Zielobjekt
		Vector3 steerVector = transform.InverseTransformPoint(new Vector3(path[currentPathObject].position.x, 
		                                                                  transform.position.y, 
		                                                                  path[currentPathObject].position.z));
		float newSteer = -maxSteer * (steerVector.x / steerVector.magnitude);
		dir = steerVector.x / steerVector.magnitude;
		frontWheel.steerAngle = newSteer;
		float rotate = newSteer;
		//Visuelle Lenkbewegung
		//SetEulerAngles(transform.eulerAngles.x, rotate+body.eulerAngles.y, body.eulerAngles.z);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, 
		                                              Quaternion.Euler(transform.eulerAngles.x,
		                                                               rotate+body.eulerAngles.y,
		                 												body.eulerAngles.z),
		                                              40*Time.deltaTime);
		//Wenn nah genug am Objekt, wähle nächstes Pfadobjekt
		if (steerVector.magnitude <= distanceFromPath){
			currentPathObject++;
			if(currentPathObject >= path.Count)
				currentPathObject = 0;
		}
	}

	/***********************************************************
	 * Methode: SetEulerAngles
	 * Beschreibung: Setzt die lokalen Winkel
	 * Parameter: float x, float y, float z
	 * Rückgabewert: keinen
	 ***********************************************************/
	void SetEulerAngles(float x, float y, float z){
		transform.eulerAngles = new Vector3(x,	y,	z);
	}

}
