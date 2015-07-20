using UnityEngine;
using System.Collections;

public class BikePitch : MonoBehaviour {

	public float turnSpeed = 100; 
	public float maxRotation = 30; //Maximaler Winkel

	//public WheelCollider frontWheel;

	float pitch;
	public GameObject bike;
	SerialPortScript accelerometer;
	Vector3 bikePosition;
	int turnspeed;

	bool pitchEnabled;
	
	public bool PitchEnabled
	{
		get
		{
			return pitchEnabled;
		}
		
		set
		{
			pitchEnabled = value;
		}
	}

	// Use this for initialization
	void Start () {
		accelerometer = GameObject.Find("DataHub").GetComponent<SerialPortScript>();

		pitch = 0f;

	}
	
	// Update is called once per frame
	void Update () {

		//pitch = accelerometer.Acceleration.x;
		//Match BikeSprite Position with Position of Illusionbike
//		bikePosition = bike.transform.position;
//		transform.position = bikePosition;
		pitch = accelerometer.Acceleration.y;
		pitch = -pitch * maxRotation;
		//Passe Richtung an.
		transform.rotation =  Quaternion.Euler(bike.transform.eulerAngles.x,-bike.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

		//print (pitch);
		if(Mathf.Abs(pitch) <= 30f)
			//transform.RotateAround(rotationPoint.position, transform.forward, pitch);
			setEulerAngles(transform.eulerAngles.x, -180 + bike.transform.rotation.eulerAngles.y, 0 -pitch);
//		else
//			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,90,0),turnSpeed*Time.deltaTime);

		 

		//-------------------

	}

	void setEulerAngles(float x, float y, float z){
		transform.eulerAngles = new Vector3(x,	y,	z);
	}
}

	
