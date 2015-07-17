using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GUIStyle mystyle;

	public int score;
	public int scoreFactor = 5;

	public float delay = 1f; //delay in seconds
	public float interval =2f; 

	public int energy = 1;

	public CharacterController controller;

//	public GameObject explosion;
//
//	public AudioClip boom;


	// Use this for initialization
	void Start () {

		score = 0;
		InvokeRepeating("IncreaseScore",delay,interval);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision){

//		GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
//		Destroy(expl, 3); // delete the explosion after 3 seconds
//		AudioSource.PlayClipAtPoint(boom, transform.position);
		energy-=1; //If hit, lose one life
	}

	void IncreaseScore()
	{
		if(controller.velocity.x > 0)
		score+=scoreFactor;
	}

	void OnGUI(){


		GUI.Label (new Rect(10,10,400,20), "Score: "+score+" Health: "+energy+"  Vel:"+controller.velocity.magnitude);
		if(energy <= 0){
			GUI.Label (new Rect(Screen.width - 600, Screen.height - 250,400,20), "Game Over!", mystyle);
			Time.timeScale = 0;
		}
	}
}
