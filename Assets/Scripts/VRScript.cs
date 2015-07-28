using UnityEngine;
using System.Collections;
using VR = UnityEngine.VR;

public class VRScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		print(OVRManager.display.acceleration);
	}
}
