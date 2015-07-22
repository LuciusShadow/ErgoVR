using UnityEngine;
using System.Collections;

public class AutoMove : MonoBehaviour {

	public WheelCollider frontWheel;
	public WheelCollider rearWheel;

	public int constTorque;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		frontWheel.motorTorque = constTorque;
		rearWheel.motorTorque = constTorque;
	}
}
