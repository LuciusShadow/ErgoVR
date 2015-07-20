using UnityEngine;
using System.Collections;

public class Handlebars : MonoBehaviour {

	SerialPortScript accelerometer;

	//public GameObject bike;
	Vector3 bikePosition;
	int maxAngle = 45;
	public Transform body;
	public WheelCollider frontWheel;
	Vector3 gyroData;


	// Use this for initialization
	void Start () {
		gyroData = new Vector3(0,0,0);

		//Testing with AccData
		accelerometer = GameObject.Find("DataHub").GetComponent<SerialPortScript>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//bikePosition = bike.transform.position;
		//transform.position = new Vector3(bikePosition.x + 2, bikePosition.y + 1, bikePosition.z);
		//transform.Rotate(this.transform.up, 10 * Time.deltaTime); 
		//Testing
		float rotate = accelerometer.Acceleration.y * maxAngle;
		//transform.RotateAround(this.transform.up, rotate);
		float steer = 20 * Input.GetAxis("Horizontal");
		frontWheel.steerAngle = steer;
		rotate = steer;
		setEulerAngles(transform.eulerAngles.x, rotate+body.eulerAngles.y, transform.eulerAngles.z);

		//Hier Sensordaten auf WheelColliderLenkung anwenden

	}

	void setEulerAngles(float x, float y, float z){
		transform.eulerAngles = new Vector3(x,	y,	z);
	}
}
