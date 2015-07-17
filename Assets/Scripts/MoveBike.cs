using UnityEngine;
using System.Collections;

public class MoveBike : MonoBehaviour {
	SerialPortScript accelerometer;

	public WheelCollider frontWheel;
	public WheelCollider rearWheel;

	Rigidbody rb;

	GameObject bike;
	Transform bBody;

	// Use this for initialization
	void Start () {
		accelerometer = GameObject.Find("DataHub").GetComponent<SerialPortScript>();
		bike = GameObject.Find("Bike");
		rb = bike.GetComponent<Rigidbody>();
		rb.centerOfMass = new Vector3(0, -1f, -1f);

		//bBody = GameObject.Find ("BikeBody").transform;
	}

	// Update is called once per frame
	void FixedUpdate () {

		float weight = 72f;
		float vx = Mathf.Abs(accelerometer.Acceleration.x);
		float vz = Mathf.Abs(accelerometer.Acceleration.z);
		//print (accelerometer.Acceleration.y + " " + accelerometer.Acceleration.z);
		float force = 0f;
		//Example values (Genaue Werte am Fahrrad ausmessen)
		int pedalr = 10;
		int frontTeethr = 15;
		int rearTeethr = 5;
		float torque = 0;
		float v;
		v=vx+vz;
		weight = weight * 10; //weight to Newton

		//vx > 0.16f && vz > 1 || 
		if(v > 1.2f){
			force =  weight * v * pedalr;
		}
		else{
			//torque = torque/2;
		}
		
		//Hinteres Zahnrad * (Kraft / vorderes Zahnrad)
		torque = rearTeethr * (force / frontTeethr);
		//Kompensieren der Masseeinstellungen im Editor
		torque = torque * Time.deltaTime;

		float steer = 20 * Input.GetAxis("Horizontal");
		//if(true){
			bike.transform.eulerAngles = new Vector3(
				bike.transform.eulerAngles.x,
				bike.transform.eulerAngles.y,
				0//bike.transform.eulerAngles.z
				);
		//}
		frontWheel.steerAngle = steer;
		//rearWheel.motorTorque = Input.GetAxis("Vertical") * 30;

		rearWheel.motorTorque = torque;
		
		frontWheel.motorTorque = torque;

		//frontWheel.brakeTorque = 90;

			//frontWheel.brakeTorque = 0;



		}

}
