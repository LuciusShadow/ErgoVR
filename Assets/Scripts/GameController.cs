/***********************************************************
* Dateiname: GameController.cs
* Autor: Sascha Bach
* letzte Aenderung: 13.08.2015
* Inhalt: enthaelt die Implementierung der Klasse GameController
***********************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


/***********************************************************
* Klasse: GameController
* Beschreibung: Steuert HUD-Anzeigen, Highscore und 
* Abbruchbedingungen für das Spiel
***********************************************************/
public class GameController : MonoBehaviour {

	public Text hud;		//HUD-Anzeige für Punkte und Leben
	public Text goText;		//Game Over Anzeige

	public int score;		//Aktueller Punktestand
	public int lab;			//Aktuelle Runde
	public int energy;	//Aktuelle Lebenspunkte

	float time;				//Spielzeit seit Start
	float endTime;			//Benötigte Gesamtzeit

	public AudioClip hitSound; //Sound bei Lebenspunktverlust

	public Toggle toggleLife;
	public Text lifePoints;
	
	bool gameActive = false;//Steuervariable 
	public bool GameActive
	{
		get
		{
			return gameActive;
		}
		
		set
		{
			gameActive = value;
		}
	}
	bool raceStart = false; //Ist wahr sobald Spieler Startlinie überfährt
	
	int scoreFactor = 5;		//Punktesteigerung pro Item

	//Variablen für Highscoreanzeige und Verwaltung
	List<Scores> highscore;
	public Text namesText;
	public Text scoresText;
	public Text userName;
	
	/***********************************************************
	 * Methode: Start
	 * Beschreibung: Initalisiert globale Variablen
	 * Parameter: keine
	 * Rückgabewert: keiner
	 ***********************************************************/
	void Start () {
		highscore = new List<Scores>();
		goText.enabled = false;
		lab = lab + 1;
		score = 0;
		time = 0;
		endTime = 0;
		//Ignoriere Layerkollision zwischen Collider des Fahrrads und Collider des Zielschilds
		Physics.IgnoreLayerCollision(9,10);
		if(lifePoints.text == "")
			energy = 0;
		else
			energy = System.Int32.Parse(lifePoints.text);
	}
	
	/***********************************************************
	 * Methode: Update
	 * Beschreibung: Wird in jedem Frame aufgerufen. Reagiert
	 * auf Abbruchbedingungen, passt Lebensanzeige und Punkte
	 * an
	 * Parameter: keine
	 * Rückgabewert: keinen
	 ***********************************************************/
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
			gameActive = !gameActive;
		//Freeze/Defreeze des Spiels
		if(gameActive){
			Time.timeScale = 1;
			goText.enabled = false;
		}
		else Time.timeScale = 0;

		//HUD Anzeige
		if(raceStart && gameActive){
			if(toggleLife.isOn)
				hud.text = "Score: "+score+" Health: " + energy + " Time: " + (Time.realtimeSinceStartup - time).ToString("0.00");
			else
				hud.text = "Score: "+score+" Time: " + (Time.realtimeSinceStartup - time).ToString("0.00");
		}

		//Game Over Anzeige
		if(energy <= 0 && toggleLife.isOn){
			time = Time.realtimeSinceStartup - time;
			if(!gameActive){
				goText.enabled = true;
				goText.text = "Game Over! \n Zeit: " + endTime.ToString("0.00") + " Sekunden \n Erzielte Punkte: " + score;

			}
		}
			
	}

	/***********************************************************
	 * Methode: OnTriggerEnter
	 * Beschreibung: Steuert die Kollisionsbehandlung
	 * Parameter: Collider other
	 * Rückgabewert: keinen
	 ***********************************************************/
	void OnTriggerEnter(Collider other){
		switch(other.tag){
			case "PickUp": 
				score = score + scoreFactor; //Für jedes PickUp ein Punktezuwachs
				break;
			case "Ziellinie":
				if(raceStart == false){
				print ("ziellinie hit");
					time = Time.realtimeSinceStartup;
					raceStart = true;
					lab = lab - 1;
				if(lab == 0)
					gameActive = false;
				}
				break;
			case "Terrain":
				//Kollisionen mit dem Terrain soll ignoriert werden
				break;
			default: 
				if(toggleLife.isOn)
					energy = energy - 1;
				AudioSource.PlayClipAtPoint(hitSound, new Vector3(0,0,0));
				print (other.name);
				if(energy == 0 && toggleLife.isOn){
					gameActive = false;
					endTime = Time.realtimeSinceStartup - time;
					//Time.timeScale = 0;
					name = userName.text;
					AddScore();
				} 
				break;
		}
	}

	/***********************************************************
	 * Methode: LoadScore
	 * Beschreibung: Lädt die Highscoredaten aus der Prefs-Datei
	 * Parameter: keine
	 * Rückgabewert: keinen
	 ***********************************************************/
	public void LoadScore(){
		highscore = HighScoreManager._instance.GetHighScore();
		namesText.text = "";
		scoresText.text ="";
		foreach(Scores _score in highscore)
		{
			namesText.text = namesText.text + _score.name + "\n";
			scoresText.text = scoresText.text + _score.score + "\n";
		}
	}

	/***********************************************************
	 * Methode: AddScore
	 * Beschreibung: Fügt den Punktestand in die Prefs-Datei
	 * Parameter: keine
	 * Rückgabewert: keinen
	 ***********************************************************/
	void AddScore(){
		//Add Score
		HighScoreManager._instance.SaveHighScore(name,System.Int32.Parse(score.ToString())); 
		LoadScore();
	}

	/***********************************************************
	 * Methode: DeleteScore
	 * Beschreibung: Löscht die HighScore-Daten
	 * Parameter: keine
	 * Rückgabewert: keinen
	 ***********************************************************/
	public void DeleteScore(){
		HighScoreManager._instance.ClearLeaderBoard();
		LoadScore();
	}
}
