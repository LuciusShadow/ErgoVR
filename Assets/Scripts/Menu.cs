using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
	// Use this for initialization
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
	public GameObject ovrRig;
	// Update is called once per frame
	void Update () {
		moveBike.enabled = toggleMove.isOn;
		moveBikeAuto.enabled = !toggleMove.isOn;
		bikePitch.enabled = togglePitch.isOn;
		handlebars.enabled = toggleHandle.isOn;
		autoSteer.enabled = !toggleHandle.isOn;



		if(Input.GetKeyDown(KeyCode.Escape)){
			userMenu.enabled = !userMenu.enabled;
		}

		if(userMenu.isActiveAndEnabled){
			ovrRig.SetActive(false);
		} else{
			ovrRig.SetActive(true);
		}

	}

	public void resetButton(){
		Debug.Log ("Spiel wird zurückgesetzt");
		Bike.transform.position = startPosition.position;
		Bike.transform.rotation = startPosition.rotation;
		PlayerCam.transform.forward = Bike.transform.forward;
	}

	public void endGameButton(){
		Debug.Log ("Spiel wird geschlossen");
		Application.Quit ();
	}

	public void startGameButton(){
		userMenu.enabled = !userMenu.enabled;
	}


}
