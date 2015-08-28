/***********************************************************
* Dateiname: birdSpawner.cs
* Autor: Eric Heung
* letzte Aenderung: 10.08.2015
* Inhalt: enthaelt die Implementierung der Klasse birdSpawner
***********************************************************/
using UnityEngine;
using System.Collections;

public class birdSpawner : MonoBehaviour {


	public GameObject prefabBird;
	public Vector3 spawnValues;
	public int Count;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public float destroyAfter;

	public Transform player;

	bool spawn;
	void Start ()
	{
		spawn = false;

	}

	void Update()
	{
		Vector3 distanceVector = transform.InverseTransformPoint(new Vector3(player.position.x, 
		                                                                  transform.position.y, 
		                                                                  player.position.z));

		if(distanceVector.magnitude < 100){
			if(spawn == false)StartCoroutine(SpawnWaves());
			spawn = true;
		}
		else{
			spawn = false;
		}

	}

	IEnumerator SpawnWaves ()
	{
			while (true)
			{	

					yield return new WaitForSeconds (startWait);
					for (int i = 0; i < Count; i++)
					{
						Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x),Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
						GameObject bird = Instantiate (prefabBird, transform.position + spawnPosition, this.transform.rotation) as GameObject;
						bird.GetComponent<Rigidbody>().velocity = this.transform.forward * 5 ;
						Destroy (bird,destroyAfter);
						yield return new WaitForSeconds (spawnWait);
					}
					yield return new WaitForSeconds (waveWait);
				
			}
		

	}
}
