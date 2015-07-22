using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BikeSteering : MonoBehaviour {
	public float rotate=15.0f;
	private Quaternion initRot;
	
	void Awake(){
		initRot = transform.localRotation;
	}
	
	void Update(){
		if(Input.anyKey)
		{
			if(Input.GetButton("q")&&transform.localRotation.y >=-0.6f )
			{
				transform.Rotate(0,-rotate,0);
				print (transform.localRotation.y);
			}
			if(Input.GetButton("e")&&transform.localRotation.y <=0.6f )
			{
				transform.Rotate(0,rotate,0);
			}

		}else{
			transform.localRotation = Quaternion.Lerp(transform.localRotation,initRot,0.2f);
		}				
	}
}