using UnityEngine;
using System.Collections;

public class Handlebars : MonoBehaviour {

	SerialPortScript accelerometer;

	//public GameObject bike;
	Vector3 bikePosition;
	int maxAngle = 10;
	public Transform body;
	public WheelCollider frontWheel;
	public PhoneSensor phone;
	Vector3 gyroData;
	public GameObject obstacles;
	// Use this for initialization
	void Start () {
		gyroData = new Vector3(0,0,0);

		//Testing with AccData
		accelerometer = GameObject.Find("DataHub").GetComponent<SerialPortScript>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		gyroData = phone.Gyodata;
		if(obstacles.activeSelf == false)
			obstacles.SetActive(true);

		//bikePosition = bike.transform.position;
		//transform.position = new Vector3(bikePosition.x + 2, bikePosition.y + 1, bikePosition.z);
		//transform.Rotate(this.transform.up, 10 * Time.deltaTime); 
		//Testing

		//float rotate = accelerometer.Acceleration.y * maxAngle;
		//float steer = 30 * Input.GetAxis("Horizontal");
		float rotate = -gyroData.z * maxAngle;

		frontWheel.steerAngle = transform.localEulerAngles.y;


		transform.Rotate(0,rotate,0);
		//setEulerAngles(transform.eulerAngles.x, rotate+body.eulerAngles.y, body.eulerAngles.z);


	}

	void setEulerAngles(float x, float y, float z){
		transform.eulerAngles = new Vector3(x,	y,	z);
	}
}
