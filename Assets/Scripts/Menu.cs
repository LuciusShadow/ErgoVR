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

	public Toggle toggleMove;
	public Toggle togglePitch;
	public Toggle toggleHandle;
	public Toggle toggleHorizontalHeadMovement;
	public Toggle toggleVerticalHeadMovement;

	public Button endButton;

	Vector3 startPosition;
	// Use this for initialization
	void Start () {
		_moveBike = Bike.GetComponent<MoveBike>();
		_moveBikeAuto = Bike.GetComponent<AutoMove>();

		_bikePitch = BikeBody.GetComponent<BikePitch>();
		_handlebars = Handlebars.GetComponent<Handlebars>();

		toggleMove.isOn = _moveBike.enabled;
		togglePitch.isOn = _bikePitch.enabled;
		toggleHandle.isOn = _handlebars.enabled;

		startPosition = Bike.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		_moveBike.enabled = toggleMove.isOn;
		_moveBikeAuto.enabled = !toggleMove.isOn;
		_bikePitch.enabled = togglePitch.isOn;
		_handlebars.enabled = toggleHandle.isOn;

		if(Input.GetKeyDown(KeyCode.Escape)){
			userMenu.enabled = !userMenu.enabled;
		}

	}

	public void resetButton(){
		Debug.Log ("Spiel wird zurückgesetzt");
		Bike.transform.position = startPosition;
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
