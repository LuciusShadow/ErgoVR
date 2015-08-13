
/***********************************************************
* Dateiname: BirdSpawner.cs
* Autor: Eric Heunf
* letzte Aenderung: 12.08.2015
* Inhalt: enthaelt die Implementierung der Klasse BirdSpawner
***********************************************************/

using UnityEngine;
using System.Collections;

/***********************************************************
* Klasse: BirdSpawner
* Beschreibung: Instantiert Wellen von Objekten in bestimmten Intervallen
***********************************************************/

public class BirdSpawner : MonoBehaviour {


	public GameObject prefabBird;	 // instantiertes Objekt
	public Vector3 spawnValues; 	 // Anfangsposition
	public int Count; 				 // Anzahl der instantierten Objekte

	public float startWait;			 // Verzögerung zur ersten Instantierung
	public float spawnWait;			 // Verzögerung zwischen den einzelen Instantierungen
	public float waveWait;			 // Verzögerung zwischen den Wellen
	public float destroyAfter; 		 // Lebensdauer

	public Transform player;		 // Transform des Spielers
	private bool spawn = false;		 // Boolean zur Kontrolle der Aktivität

	void Start(){
		//StartCoroutine(SpawnWaves());
	}
	/***********************************************************
	 * Methode: Update
	 * Beschreibung: Berechnet Distanz zum Spieler und startet SpawnWaves()
	 * Parameter: keine
	 * Rückgabewert: keinen
	 ***********************************************************/
	void Update(){
		Vector3 distanceVector = transform.InverseTransformPoint(new Vector3(player.position.x,transform.position.y,player.position.z));
		// Distanz zwischen Spieler und Spawner
		if(distanceVector.magnitude < 100){
			// Instantierung der Objekte wird aktiviert sobald die Distanz zum Spieler klein genug ist
			if(spawn == false){
				StartCoroutine("SpawnWaves");
			}
			spawn = true;
			print ("start");	
		}
		else{
			// und deaktiviert wenn die Distanz größer wird
			spawn = false;
			StopCoroutine("SpawnWaves");
			print ("stop");
		}
	}

	/***********************************************************
	 * Methode: SpawnWaves
	 * Beschreibung: Instantiert Wellen von Objekten in bestimmten Intervallen
	 * Parameter: keine
	 * Rückgabewert: keiner
	 ***********************************************************/
	IEnumerator SpawnWaves (){
		while (true)
		{	
				
			yield return new WaitForSeconds (startWait);
			// Verzögerung der ersten Welle nach Aktivierung
				for (int i = 0; i < Count; i++){
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x),Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
					// Berechnung des zufälligen Verschiebungsvektor
					GameObject bird = Instantiate (prefabBird, transform.position + spawnPosition, this.transform.rotation) as GameObject;
					// Instantiert Objekt mit mit angepasster Rotation und zufälliger Anfangsposition basierend auf Position des Besitzers und des Verschiebungsvektors
					bird.GetComponent<Rigidbody>().velocity = this.transform.forward * 5 ;
					// Setzt die Geschwindigkeit des neu-instantierten Objektes
					Destroy (bird,destroyAfter);
					// Setzt die Lebensdauer
					yield return new WaitForSeconds (spawnWait);
					// Verzögerung zwischen den Instantierungen
				}
				yield return new WaitForSeconds (waveWait);
				// Verzögerungen zwischen den Wellen
		}	
	}
}
