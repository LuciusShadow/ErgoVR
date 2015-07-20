using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GUIStyle mystyle;

	public int score;
	public int scoreFactor = 5;

	public float delay = 1f; //delay in seconds
	public float interval =2f; 

	public int energy = 1;


	// Use this for initialization
	void Start () {

		score = 0;
		InvokeRepeating("IncreaseScore",delay,interval);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision){
		if(collision.collider.tag != "Pickup")
			energy-=1; //If hit by, lose one life
		else {
			score += 5;
		}
	}

	void IncreaseScore()
	{
//		if(controller.velocity.x > 0)
//		score+=scoreFactor;
	}

	void OnGUI(){


		GUI.Label (new Rect(10,10,400,20), "Score: "+score+" Health: "+energy);
		if(energy <= 0){
			GUI.Label (new Rect(Screen.width - 600, Screen.height - 250,400,20), "Game Over!", mystyle);
			Time.timeScale = 0;
		}
	}
}
