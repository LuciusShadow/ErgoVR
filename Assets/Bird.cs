using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject,5.0f);
	}
}
