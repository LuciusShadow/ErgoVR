using UnityEngine;
using System.Collections;

public class PickUpPickedUp : MonoBehaviour {


	IEnumerator OnTriggerEnter(Collider other){

		if(other.tag == "Player"){

			gameObject.SetActive(false);
			yield return new WaitForSeconds(10);
			gameObject.SetActive(true);
		}
			 
		

	}
	

}
