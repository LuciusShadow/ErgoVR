//Entommen von https://youtu.be/r5U5O7WncHk
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoSteer : MonoBehaviour {
	public System.Collections.Generic.List<Transform> path = new System.Collections.Generic.List<Transform>();
//	List<Transform> path;
	//Transform[] path;
	public Transform pathGroup;

	public float maxSteer = 30.0f;
	
	public WheelCollider frontWheel;

	int currentPathObject;
	public float dir;

	// Use this for initialization
	void Start () {
		GetPath ();
	}
	void GetPath(){
		Transform[] pathObjects = pathGroup.GetComponentsInChildren<Transform>();
		path = new List<Transform>();

		foreach(Transform pathObject in pathObjects){

			if(pathObject != pathGroup){
				print (pathObject.name);
				path.Add(pathObject);
			}
				
		
		}

	}

	// Update is called once per frame
	void Update () {
		GetSteer();
	}

	void GetSteer(){
		Vector3 steerVector = transform.InverseTransformPoint(new Vector3(path[currentPathObject].position.x, transform.position.y, path[currentPathObject].position.z));
		float newSteer = -maxSteer * (steerVector.x / steerVector.magnitude);
		dir = steerVector.x / steerVector.magnitude;
		frontWheel.steerAngle = newSteer;

	}
}
