using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
//Enemy:
//		
/*- Verfolgt Spieler
*- Wird mit jedem Pickup schneller (z. B. + 10)
*- Wird langsamer sobald Spieler getroffen wurde (-20) - 
*/
	float moveSpeed;
	public int maxSpeed = 50;
	public int decSpeed = 20;
	public GameObject bike; //Hierüber Script referenzieren das Collision und Powerup erkennt


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(bike.transform.position);
		transform.position = Vector3.MoveTowards(transform.position,bike.transform.position, Time.deltaTime * moveSpeed);
//
//		if(bikePickUp){
//			moveSpeed = moveSpeed + decSpeed;
//		}
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.collider.CompareTag("bike;")) {
			moveSpeed = moveSpeed - decSpeed;
		}
	}
}
