/***********************************************************
* Dateiname: PickUpPickedUp.cs
* Autor: Sascha Bach
* letzte Aenderung: 03.08.2015
* Inhalt: enthaelt die Implementierung der Klasse PickUpPickedUp
***********************************************************/
using UnityEngine;
using System.Collections;

/***********************************************************
* Klasse: PickUpPickedUp
* Beschreibung: Steuert Verhalten bei Spielerkollision
***********************************************************/
public class PickUpPickedUp : MonoBehaviour {

	/***********************************************************
	 * Methode: OnTriggerEnter
	 * Beschreibung: Bei Kollision mit Spieler wird das Objekt
	 * vom Spielfeld entfernt, um nach Ablauf von 15 Sekunden
	 * wieder zu erscheinen
	 * Parameter: keine
	 * Rückgabewert: keiner
	 ***********************************************************/
	IEnumerator OnTriggerEnter(Collider other){

		if(other.tag == "Player"){

			gameObject.GetComponent<MeshRenderer>().enabled = false;
			yield return new WaitForSeconds(15);
			gameObject.GetComponent<MeshRenderer>().enabled = true;
		}
			 
		

	}
	

}
