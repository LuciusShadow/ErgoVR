/***********************************************************
* Dateiname: Menu.cs
* Autor: Sascha Bach
* letzte Aenderung: 03.08.2015
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
	VRControl vrControl;

	public Toggle toggleMove;
	public Toggle togglePitch;
	public Toggle toggleHandle;

	public Button endButton;

	Transform startPosition;

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
		moveBike = Bike.GetComponent<MoveBike>();
		moveBikeAuto = Bike.GetComponent<AutoMove>();

		bikePitch = BikeBody.GetComponent<BikePitch>();

		handlebars = Handlebars.GetComponent<Handlebars>();
		autoSteer = Handlebars.GetComponent<AutoSteer>();

		vrControl = DataHub.GetComponent<VRControl>();

		toggleMove.isOn = moveBike.enabled;
		togglePitch.isOn = bikePitch.enabled;
		toggleHandle.isOn = handlebars.enabled;

		startPosition = Bike.transform;
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
		Application.LoadLevel(0);
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
		userMenu.enabled = !userMenu.enabled;
	}


}
