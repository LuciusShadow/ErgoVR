using UnityEngine;
using System.Collections;

public class birdSpawner : MonoBehaviour {


	public GameObject prefabBird;
	public Vector3 spawnValues;
	public int Count;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	void Start ()
	{
		StartCoroutine (SpawnWaves ());
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < Count; i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x),Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
				GameObject bird = Instantiate (prefabBird, transform.position + spawnPosition, this.transform.rotation) as GameObject;
				bird.GetComponent<Rigidbody>().velocity = this.transform.forward * 5 ;
				Destroy (bird,5.0f);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
		}
	}
}
