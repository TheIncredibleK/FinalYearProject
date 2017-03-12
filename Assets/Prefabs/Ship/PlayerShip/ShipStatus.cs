using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipStatus : MonoBehaviour {
	//Status Bars
	public GameObject health_bar;
	public GameObject speed_bar;
	public GameObject power_bar;
	public GameObject magnet_bar;

	//Current Values
	public float speed;
	public float health;
	public float power;
	public float magnet;
	//Max Values
	float max_health;
	float max_speed;
	float max_power;
	float max_mag;
	//Public Variables
	float increaseAmt = 1.0f;

	//List of functions that controls the status's
	//This status's are actually spread across various components due to some poor planning on my part
	//Whilst trying to make the entire code base more componnent based and less specific.

	void Start() {
		Debug.Log ("starting");
		speed = 15.0f;
		magnet = 5.0f;
		GameObject.FindGameObjectWithTag("HandController").GetComponent<FlightController> ().topSpeed = speed;
		Debug.Log (speed);

	}


	public void IncreaseSpeed(GameObject image) {
		//Speeds location is the flight controller
		speed += increaseAmt;
		Debug.Log ("made it to increaspeed :" + speed);
		Debug.Log ("increased speed");
		GameObject.FindGameObjectWithTag("HandController").GetComponent<FlightController> ().topSpeed = speed;
		Debug.Log ("Set speed");
		image.GetComponent<UIBarManager> ().IncreaseSize ();
		Debug.Log ("Increased bar size");
	}

	public void IncreasePower() {

	}

	public void IncreaseHealth() {

	}

	public void IncreaseMagnet(GameObject image ) {
		magnet += increaseAmt;
		image.GetComponent<UIBarManager> ().IncreaseSize ();
	}

	public void Increase(GameObject image, int keyForIncrease) {
		if (keyForIncrease == 1) {
			IncreaseSpeed (image);

		} else if (keyForIncrease == 2) {
			IncreaseMagnet (image);

		} else if (keyForIncrease == 3) {

		}
	}

}