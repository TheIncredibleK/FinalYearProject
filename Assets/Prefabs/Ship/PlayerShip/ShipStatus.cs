using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipStatus : MonoBehaviour {
	//Public Variables

	//Status Bars
	public GameObject health_bar;
	public GameObject speed_bar;
	public GameObject power_bar;
	public GameObject magnet_bar;

	//Current Values
	public float speed;
	public float health;
	float oldHealth;
	public float power;
	public float magnet;
	public float increaseAmt = 1.0f;
	//Max Values
	public float max_health;
	public float max_speed;
	public float max_power;
	public float max_mag;

	//List of functions that controls the status's
	//This status's are actually spread across various components due to some poor planning on my part
	//Whilst trying to make the entire code base more componnent based and less specific.

	void Start() {
		speed = 15.0f;
		magnet = 5.0f;
		health = 100.0f;
		oldHealth = 100.0f;
		GameObject.FindGameObjectWithTag("HandController").GetComponent<FlightController> ().topSpeed = speed;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<HealthController> ().health = health;

	}

	void Update() {
		
		if(health != oldHealth) {
			float change = oldHealth - health;
			oldHealth = health;
			health_bar.GetComponent<UIBarManager> ().DecreaseSize (change, 100.0f);
			}


	}

	//Increase speed and notify the actual thing that controls speed
	public void IncreaseSpeed(GameObject image) {
		//Speeds location is the flight controller
		speed *= 1.2f;
		GameObject.FindGameObjectWithTag("HandController").GetComponent<FlightController> ().topSpeed = speed;
		image.GetComponent<UIBarManager> ().IncreaseSize ();
	}

	//Increase max health
	public void IncreaseHealth(GameObject image) {
		max_health *= 1.2f;
		image.GetComponent<UIBarManager> ().IncreaseSize ();
	}
	//Increase magnetic distance
	public void IncreaseMagnet(GameObject image) {
		magnet  *= 1.2f;
		image.GetComponent<UIBarManager> ().IncreaseSize ();
	}
	//Increase bullet strength

	public void IncreasePower(GameObject image) {
		power  *= 1.2f;
		image.GetComponent<UIBarManager> ().IncreaseSize ();
		GameObject.FindGameObjectWithTag ("Player").transform.FindChild("MyGuns").gameObject.GetComponent<Firing> ().UpdateDamage (power);
	}

	//Pass in the int key corresponding to what needs to be updated
	//It will increase
	public void Increase(GameObject image, int keyForIncrease) {
		if (keyForIncrease == 1) {
			IncreaseSpeed (image);

		} else if (keyForIncrease == 2) {
			IncreaseMagnet (image);

		} else if (keyForIncrease == 3) {
			IncreaseHealth (image);
		} else if (keyForIncrease == 4) {
			IncreasePower (image);
		}
	}

}