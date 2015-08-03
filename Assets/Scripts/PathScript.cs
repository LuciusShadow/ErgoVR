/***********************************************************
* Dateiname: PathScript.cs
* Autor: Sascha Bach
* letzte Aenderung: 03.08.2015
* Inhalt: enthaelt die Implementierung der Klasse PathScript
***********************************************************/
using UnityEngine;
using System.Collections;

/***********************************************************
* Klasse: PathScript
* Beschreibung: Gibt die Positionen der Pfad-Objekte zurück
***********************************************************/
public class PathScript : MonoBehaviour {
	public Transform[] checkPoints;

	/***********************************************************
	 * Methode: GetPointPos
	 * Beschreibung: Gibt Position des Pfad-Checkpoints zurück
	 * Rückgabewert: Position des Checkpoints mit ID
	 ***********************************************************/
	public Vector3 GetPointPos(int id){
		return checkPoints[id].position;
	}
}
