using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {
	public Canvas userMenu;

    public GameObject Bike;
	public GameObject BikeBody;
	public GameObject Handlebars;
	public Camera PlayerCam;

	MoveBike _moveBike;
	AutoMove _moveBikeAuto;
	BikePitch _bikePitch;
	Handlebars _handlebars;
	AutoSteer _autoSteer;

	public Toggle toggleMove;
	public Toggle togglePitch;
	public Toggle toggleHandle;
	public Toggle toggleHorizontalHeadMovement;
	public Toggle toggleVerticalHeadMovement;

	public Button endButton;

	Transform startPosition;
	// Use this for initialization
	void Start () {
		_moveBike = Bike.GetComponent<MoveBike>();
		_moveBikeAuto = Bike.GetComponent<AutoMove>();

		_bikePitch = BikeBody.GetComponent<BikePitch>();

		_handlebars = Handlebars.GetComponent<Handlebars>();
		_autoSteer = Handlebars.GetComponent<AutoSteer>();

		toggleMove.isOn = _moveBike.enabled;
		togglePitch.isOn = _bikePitch.enabled;
		toggleHandle.isOn = _handlebars.enabled;

		startPosition = Bike.transform;
	}
	public GameObject ovrRig;
	// Update is called once per frame
	void Update () {
		_moveBike.enabled = toggleMove.isOn;
		_moveBikeAuto.enabled = !toggleMove.isOn;
		_bikePitch.enabled = togglePitch.isOn;
		_handlebars.enabled = toggleHandle.isOn;
		_autoSteer.enabled = !toggleHandle.isOn;

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
