/***********************************************************
* Dateiname: Menu.cs
* Autor: Sascha Bach
* letzte Aenderung: 22.08.2015
* Inhalt: enthaelt die Implementierung der Klasse Menu
***********************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/***********************************************************
* Klasse: Menu
* Beschreibung: Steuert die Aktivierung bzw. Deaktivierung
* von Modulen anhand der Menueeingaben
***********************************************************/
public class Menu : MonoBehaviour {
	public Canvas userMenu;

	Vector3 startPos;
	Vector3 startRot;
    public GameObject Bike;
	public GameObject BikeBody;
	public GameObject Handlebars;
	public GameObject DataHub;
	public Camera PlayerCam;

	MoveBike moveBike;
	AutoMove moveBikeAuto;
	BikePitch bikePitch;
	Handlebars handlebars;
	AutoSteer autoSteer;


	public Toggle toggleMove;
	public Toggle togglePitch;
	public Toggle toggleHandle;
	public Toggle lifeOption;
	public Button endButton;
	
	public GameObject lifeCount;
	public GameObject ovrRig;

	public GameObject stones;
	public GameObject autoPickUps;
	public GameObject regularPickUps;
	public GameObject birds;

	/***********************************************************
	 * Methode: Start
	 * Beschreibung: Initialisierung der Skriptreferenzen und
	 * Checkboxstatus
	 * Parameter: keine
	 * Rückgabewert: keiner
	 ***********************************************************/
	void Start () {
		startPos = Bike.transform.position;
		startRot = Bike.transform.eulerAngles;
		moveBike = Bike.GetComponent<MoveBike>();
		moveBikeAuto = Bike.GetComponent<AutoMove>();

		bikePitch = BikeBody.GetComponent<BikePitch>();

		handlebars = Handlebars.GetComponent<Handlebars>();
		autoSteer = Handlebars.GetComponent<AutoSteer>();


		toggleMove.isOn = moveBike.enabled;
		togglePitch.isOn = bikePitch.enabled;
		toggleHandle.isOn = handlebars.enabled;


	}

	/***********************************************************
	 * Methode: Update
	 * Beschreibung: Skripte werden je nach Checkboxeinstellung
	 * Deaktiviert oder Aktiviert
	 * Parameter: keine
	 * Rückgabewert: keinen
	 ***********************************************************/
	void Update () {
		//Wende Checkbox auf Skript an (Aktiv oder Inaktiv)
		moveBike.enabled = toggleMove.isOn;
		moveBikeAuto.enabled = !toggleMove.isOn;
		bikePitch.enabled = togglePitch.isOn;
		handlebars.enabled = toggleHandle.isOn;
		autoSteer.enabled = !toggleHandle.isOn;
		lifeCount.SetActive(lifeOption.isOn);
		//Steuerung der Objekte
		stones.SetActive(handlebars.enabled);
		autoPickUps.SetActive(autoSteer.enabled);
		regularPickUps.SetActive(handlebars.enabled);
		birds.SetActive(moveBike.enabled);
		//Escape Taste um ins Menü zu gelangen
		if(Input.GetKeyDown(KeyCode.Escape)){
			userMenu.enabled = !userMenu.enabled;
		}
		//Wenn Menü aktiv deaktiviere VR-Ansicht für Menüanzeige
		if(userMenu.isActiveAndEnabled){
			ovrRig.SetActive(false);
		} else{
			ovrRig.SetActive(true);
		}



	}

	/***********************************************************
	 * Methode: ResetButton
	 * Beschreibung: Startet das Spiel neu
	 * Parameter: keine
	 * Rückgabewert: keinen
	 ***********************************************************/
	public void ResetButton(){
		//Application.LoadLevel(Application.loadedLevel);
		Bike.transform.position = startPos;
		Bike.transform.eulerAngles = startRot;
		Bike.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
		autoSteer.currentPathObject = 0;
		Start ();


	}

	/***********************************************************
	 * Methode: EndGameButton
	 * Beschreibung: Beendet das Spiel
	 * Parameter: keine
	 * Rückgabewert: keinen
	 ***********************************************************/
	public void EndGameButton(){
		Debug.Log ("Spiel wird geschlossen");
		Application.Quit ();
	}

	/***********************************************************
	 * Methode: StartGameButton
	 * Beschreibung: Startet das Spiel durch Schließen des
	 * Menues
	 * Parameter: keine
	 * Rückgabewert: keinen
	 ***********************************************************/
	public void StartGameButton(){
		userMenu.enabled = false;
		Time.timeScale = 1;
		Bike.GetComponent<GameController>().GameActive = true;
	}


}
