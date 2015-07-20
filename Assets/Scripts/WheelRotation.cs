using UnityEngine;
using System.Collections;

public class WheelRotation : MonoBehaviour {

	public Transform frontWheel;
	public Transform rearWheel;
	public Transform joint;
	public Transform rPedal;
	public Transform lPedal;

	public Rigidbody bike;

	float velocity;
	// Use this for initialization
	void Start () {
		velocity = 0;
	}
	
	// Update is called once per frame
	void Update () {
		velocity = -bike.velocity.sqrMagnitude/2;

		frontWheel.Rotate(velocity,0,0);
		rearWheel.Rotate(velocity,0,0);
		joint.Rotate(velocity/4,0,0);
		rPedal.Rotate(-velocity/4,0,0);
		lPedal.Rotate(-velocity/4,0,0);
	}
}
