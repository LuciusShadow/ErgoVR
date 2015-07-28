using UnityEngine;
using System.Collections;

public class VRControl : MonoBehaviour {

	public BoxCollider bikeCollider;
	public Transform CamCenter;

	public bool xTrackingEnabled;
	public bool yTrackingEnabled;

	Vector3 startPos;
	float yFactor = 0;
	// Use this for initialization
	void Start () {
		xTrackingEnabled = true;
		yTrackingEnabled = true;
		startPos = bikeCollider.center;
	}
	
	// Update is called once per frame
	void Update () {
		print (OVRManager.display.acceleration);

		Vector3 ovrAcc = new Vector3(0,0,0);
		if(xTrackingEnabled || yTrackingEnabled){
			ovrAcc = OVRManager.display.acceleration;
			yFactor = CamCenter.localPosition.y*5;

		}


		if(yTrackingEnabled){
			bikeCollider.center = new Vector3(bikeCollider.center.x, startPos.z + yFactor, bikeCollider.center.z);
		}
		else {
			bikeCollider.center = startPos;
			//TODO Kameraposition fixieren. Alle Ancors einzeln oder über Getcomponents
		}
	}

	public void ToggleXTracking(){
		xTrackingEnabled = !xTrackingEnabled;
	}

	public void ToggleYTracking(){
		xTrackingEnabled = !yTrackingEnabled;
	}
}
