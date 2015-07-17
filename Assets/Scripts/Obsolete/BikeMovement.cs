using UnityEngine;
using System.Collections;
using System;

public class BikeMovement : MonoBehaviour {

	GameObject player;
	SerialPortScript serialInput;
	Vector3 orientation;
	bool firstValrecieved;
	
	float speedY;
	float speedZ;

	float oldY;
	float oldZ;
	
	bool zerodetect;
	CharacterController controller;
	// Use this for initialization
	void Start () {
	
		firstValrecieved = false;
		player = GameObject.Find("Player");
		serialInput =player.GetComponent<SerialPortScript>();
		orientation = new Vector3(0, 0, 0);
		speedY= 0f;
		speedZ= 0f;

		oldY = 0f;
		oldZ = 0f;


		controller = player.GetComponent<CharacterController>();

	}

	// Update is called once per frame
	void Update () {

		if(orientation != serialInput.Acceleration){
			if(firstValrecieved == false){
				firstValrecieved = true;

			}
			orientation = serialInput.Acceleration;
		}
		else{
			//orientation = new Vector3(0,0,0);
			//rb.velocity = new Vector3(0,0,0);
		}
		//Wenn keine neuen Werte, Beschleunigung auf null

//		Debug.Log("Orientation y und z " + orientation.y + " " + orientation.z);
		if(Math.Abs(orientation.y) > 0.005f && Math.Abs(orientation.z) > 0.005f ){
//			speedY = speedY + (Time.time - startTime)*orientation.y;
//			speedZ = speedZ + (Time.time - startTime)*orientation.z;
			speedY = Math.Abs(orientation.y);
			speedZ = Math.Abs(orientation.z);

			
		}
//		else{
//			speedY = speedY/2;
//			speedZ = speedZ/2;
//			if( speedY < 0.001f && speedZ < 0.001f){
//				speedY = 0f;
//				speedZ = 0f;
//			}
//		}
		if(oldY+oldZ == speedY+speedZ){
			speedY = 0;
			speedZ = 0;

		}

//		if(oldY == speedY && oldZ == speedZ){
//			resetcounter += 1;
//			Debug.Log ("reset: "+resetcounter);
//	
//		}
//		if(resetcounter == 10){
//			speedY = 0f;
//			speedZ = 0f;
//			resetcounter = 0;
//		}
//		Debug.Log("Force: " + (speedY + speedZ));
		//rb.AddForce(transform.forward * (speedY + speedZ)*50, ForceMode.Acceleration);
		float speed = speedY + speedZ;
		if(speed >= 1.01f)
		controller.SimpleMove(transform.forward * speed);
		
				oldY = speedY;
				oldZ = speedZ;
			
		
		

	//	Debug.Log("Speed nachher " + speedY + " " + speedZ);
		//TODO: Maximale Rotation definieren
		//transform.Rotate(new Vector3(0,0, orientation.y));
		//TODO Beschleunigung modifizieren - bei Rotation werden AccWerte kleiner -> Tempo wird gebremst
		//rb.AddForce(transform.forward * (speedY + speedZ)/2, ForceMode.Acceleration);

		/* TODO Korrekte Geschwindigkeit: Integiere Beschleunigungsvektor (frage im System Sekunden ab)
		 * 
		 */
	}
}
