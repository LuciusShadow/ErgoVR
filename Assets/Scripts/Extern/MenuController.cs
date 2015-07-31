//Quelle: http://flamy.weebly.com/offile-high-score-system-for-unity3d.html

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
	
	string name="";
	string scores="";
	List<Scores> highscore;
	
	public Text namesText;
	public Text scoresText;
	// Use this for initialization
	void Start () {
		//EventManager._instance._buttonClick += ButtonClicked;
		
		highscore = new List<Scores>();
		
	}
	
	
	void ButtonClicked(GameObject _obj)
	{
		print("Clicked button:"+_obj.name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI()
	{
		GUILayout.BeginHorizontal();
		GUILayout.Label("Name :");
		name =  GUILayout.TextField(name);
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		GUILayout.Label("Score :");
		scores =  GUILayout.TextField(scores);
		GUILayout.EndHorizontal();
		
		if(GUILayout.Button("Add Score"))
		{
			//Add Score
			HighScoreManager._instance.SaveHighScore(name,System.Int32.Parse(scores));
			highscore = HighScoreManager._instance.GetHighScore(); 
			LoadScore();
		}
		
		if(GUILayout.Button("Get LeaderBoard"))
		{
			highscore = HighScoreManager._instance.GetHighScore();  
			LoadScore();

		}
		
		if(GUILayout.Button("Clear Leaderboard"))
		{
			HighScoreManager._instance.ClearLeaderBoard();  
			LoadScore();
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
}




