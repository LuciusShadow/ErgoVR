/* 
 * 
 * 
 * 
 * */
using UnityEngine;
using System.Collections;

public class PathScript : MonoBehaviour {
	public Transform[] checkPoints;

	public Vector3 GetPointPos(int id){
		return checkPoints[id].position;
	}
}
