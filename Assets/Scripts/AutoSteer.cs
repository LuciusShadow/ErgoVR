//Entommen von https://youtu.be/r5U5O7WncHk
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoSteer : MonoBehaviour {
	public System.Collections.Generic.List<Transform> path = new System.Collections.Generic.List<Transform>();
	public Transform body;
	public Transform pathGroup;

	public float maxSteer = 30.0f;
	
	public WheelCollider frontWheel;

	public int currentPathObject;
	public float distanceFromPath = 2;
	public float dir;

	public GameObject obstacles;
	// Use this for initialization
	void Start () {
		Transform[] pathObjects = pathGroup.GetComponentsInChildren<Transform>();
		path = new List<Transform>();
		
		foreach(Transform pathObject in pathObjects){
			
			if(pathObject != pathGroup){
				path.Add(pathObject);
			}
			
			
		}
	}

	// Update is called once per frame
	void Update () {
		if(obstacles.activeSelf == true)
			obstacles.SetActive(false);

		Vector3 steerVector = transform.InverseTransformPoint(new Vector3(path[currentPathObject].position.x, 
		                                                                  transform.position.y, 
		                                                                  path[currentPathObject].position.z));
		float newSteer = -maxSteer * (steerVector.x / steerVector.magnitude);
		dir = steerVector.x / steerVector.magnitude;
		frontWheel.steerAngle = newSteer;
		float rotate = newSteer;
		//Visuelle Lenkbewegung
		setEulerAngles(transform.eulerAngles.x, rotate+body.eulerAngles.y, body.eulerAngles.z);

		if (steerVector.magnitude <= distanceFromPath){
			currentPathObject++;
			if(currentPathObject >= path.Count)
				currentPathObject = 0;
		}
	}

	void setEulerAngles(float x, float y, float z){
		transform.eulerAngles = new Vector3(x,	y,	z);
	}

}
