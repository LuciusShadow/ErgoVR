using UnityEngine;
using System.Collections;

public class PickUpPickedUp : MonoBehaviour {


	IEnumerator OnTriggerEnter(Collider other){

		if(other.tag == "Player"){

			gameObject.GetComponent<MeshRenderer>().enabled = false;
			yield return new WaitForSeconds(15);
			gameObject.GetComponent<MeshRenderer>().enabled = true;
		}
			 
		

	}
	

}
