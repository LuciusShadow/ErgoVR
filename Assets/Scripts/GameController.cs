using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GUIStyle mystyle;

	public int score;
	public int scoreFactor = 5;
	private int bonus = 7000;

	public int lab;

	public int energy = 3;
	bool gameActive = false;

	float time;
	float endTime;

	// Use this for initialization
	void Start () {
		lab = lab + 1;
		score = 0;
		time = 0f;
		endTime = 0;
		//Ignoriere Layerkollision zwischen Collider des Fahrrads und Collider des Zielschilds
		Physics.IgnoreLayerCollision(9,10);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other){

		switch(other.tag){
			case "PickUp": 
				score = score + scoreFactor; //Für jedes PickUp ein Punktezuwachs
				break;
			case "Ziellinie":
				if(time == 0){
					time = Time.realtimeSinceStartup;
					gameActive = true;
				}
				break;
			default: 
				energy = energy - 1;
				if(energy == 0){
					gameActive = false;
					endTime = Time.realtimeSinceStartup - time;
				}
				break;
		}
	}



	void OnGUI(){

		if(gameActive)
		GUI.Label (new Rect(10,10,400,20), "Score: "+score+" Health: " + energy + " Time: " + (Time.realtimeSinceStartup - time));

		if(energy <= 0){
			time = Time.realtimeSinceStartup - time;

			GUI.Label (new Rect(Screen.width - 600, Screen.height - 250,400,20), 
			           "Game Over! \n Zeit: " + endTime.ToString("0.00") + " Sekunden \n Erzielte Punkte: " + score, mystyle);

			Time.timeScale = 0;
		}
		if(lab == 0){
			GUI.Label (new Rect(Screen.width - 600, Screen.height - 250,600,20), "Bonuspunkte: " + Mathf.Round(bonus/time), mystyle);
			Time.timeScale = 0;
		}
	}
}
