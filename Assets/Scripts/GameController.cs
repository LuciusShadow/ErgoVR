﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public GUIStyle mystyle;
	public Text hud;

	public Text goText;

	public int score;
	public int scoreFactor = 5;
	private int bonus = 7000;

	public int lab;

	public int energy = 3;
	bool gameActive = false;

	bool raceStart = false;

	float time;
	float endTime;

	string name="";
	string scores="";
	List<Scores> highscore;

	public Text namesText;
	public Text scoresText;
	
	// Use this for initialization
	void Start () {
		highscore = new List<Scores>();

		goText.enabled = false;
		lab = lab + 1;
		score = 0;
		time = 0f;
		endTime = 0;
		//Ignoriere Layerkollision zwischen Collider des Fahrrads und Collider des Zielschilds
		Physics.IgnoreLayerCollision(9,10);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			gameActive = !gameActive;

		}
		if(gameActive){
			Time.timeScale = 1;
			goText.enabled = false;
		}
		else Time.timeScale = 0;

		if(raceStart && gameActive){
			hud.text = "Score: "+score+" Health: " + energy + " Time: " + (Time.realtimeSinceStartup - time).ToString("0.00");
		}

		if(energy <= 0 ){
			time = Time.realtimeSinceStartup - time;
			if(!gameActive){
				goText.enabled = true;
				goText.text = "Game Over! \n Zeit: " + endTime.ToString("0.00") + " Sekunden \n Erzielte Punkte: " + score;

			}
		}
			
	}

	void OnTriggerEnter(Collider other){
		switch(other.tag){
			case "PickUp": 
				score = score + scoreFactor; //Für jedes PickUp ein Punktezuwachs
				break;
			case "Ziellinie":
				if(time == 0){
					time = Time.realtimeSinceStartup;
					raceStart = true;
				}
				break;
			case "Terrain":
				//Ignorieren
				break;
			default: 
				energy = energy - 1;
				print (other.name);
				if(energy == 0){
					gameActive = false;
					endTime = Time.realtimeSinceStartup - time;
					Time.timeScale = 0;
				}
				break;
		}
	}


	void LoadScore(){
		namesText.text = "";
		scoresText.text ="";
		foreach(Scores _score in highscore)
		{
			namesText.text = namesText.text + _score.name + "\n";
			scoresText.text = scoresText.text + _score.score + "\n";
		}
	}

//	void OnGUI(){
////		print ("Race: "+raceStart + " " + "Game: " + gameActive);
//		if(raceStart && gameActive)
//		//GUI.Label (new Rect(10,10,400,20), "Score: "+score+" Health: " + energy + " Time: " + (Time.realtimeSinceStartup - time).ToString("0.00"));
//		hud.GetComponent<Text>().text = "Score: "+score+" Health: " + energy + " Time: " + (Time.realtimeSinceStartup - time).ToString("0.00");
//
//		if(energy <= 0 ){
//			time = Time.realtimeSinceStartup - time;
//			if(!gameActive)
//			GUI.Label (new Rect(Screen.width - 600, Screen.height - 250,400,20), 
//				           "Game Over! \n Zeit: " + endTime.ToString("0.00") + " Sekunden \n Erzielte Punkte: " + score, mystyle);
//
//		}
//
//	}
}
