using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VRControl : MonoBehaviour {

	public BoxCollider bikeCollider;
	public Transform CamCenter;
	public Transform ovrRig;

	public Toggle xTrackingEnabled;
	public Toggle yTrackingEnabled;

	bool xTracking;
	bool yTracking;

	Vector3 startPos;
	float yFactor = 0;

	void Awake(){
		startPos = bikeCollider.center;
	}

	// Use this for initialization
	void Start () {
		xTracking = xTrackingEnabled.isOn;
		yTracking = yTrackingEnabled.isOn;

	}
	

	// Update is called once per frame
	void Update () {
		xTracking = xTrackingEnabled.isOn;
		yTracking = yTrackingEnabled.isOn;
		//print (OVRManager.display.acceleration);
		Vector3 ovrAcc = new Vector3(0,0,0);
		if(xTracking || yTracking){
			ovrAcc = OVRManager.display.acceleration;
			yFactor = CamCenter.localPosition.y*5;

		}

	
		if(yTracking){
			bikeCollider.center = new Vector3(bikeCollider.center.x, startPos.z + yFactor, bikeCollider.center.z);
		}
		else {
			bikeCollider.center = startPos;
			OVRManager.tracker.isEnabled = false;
		}
	}

}
