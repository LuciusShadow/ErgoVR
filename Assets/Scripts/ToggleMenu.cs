using UnityEngine;
using System.Collections;

public class ToggleMenu : MonoBehaviour {
	public Canvas userMenu;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			userMenu.enabled = !userMenu.enabled;
		}
	}

	
	public void startGameButton(){
		userMenu.enabled = !userMenu.enabled;
	}
}
