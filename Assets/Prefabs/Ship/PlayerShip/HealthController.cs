using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

	//Ships health
	public float health = 0;
	public GameObject healthbar;
	// Use this for initialization
	void Start () {
	}

	//Decrease health amount
	public void Decrease(float amount) {
		Debug.Log ("Decrease by : " + amount);
		health -= amount;
		Debug.Log ("health : " + health);
		NotifyStatus (amount);
	}
	//Increase halth amount
	public void Increase(float amount) {
		health += amount;
		NotifyStatus (amount);
	}

	//Update the status bar based on changed health
	void NotifyStatus(float amount) {
		healthbar.GetComponent<UIBarManager> ().DecreaseSize (amount, GameObject.FindGameObjectWithTag ("Player").GetComponent<ShipStatus> ().max_health);
		Debug.Log ("Changed Health Bar");
	}

	//If I amhit my an enemy bullet, take damage based on it
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "EnemyBullet") {
			if (health <= 0) {
				RemoveRandomInventory ();
			}
			Decrease(other.gameObject.GetComponent<Bullet> ().damage);

			Destroy (other.gameObject);
		}

	}

	//If my health is 0, then start removing from my inventory

	void RemoveRandomInventory() {
		GameObject slots = GameObject.FindGameObjectWithTag ("Slots");

		bool hurt = false;
		for (int i = 0; i < slots.transform.childCount; i++) {
			if (!hurt) {
				GameObject child = slots.transform.GetChild (i).gameObject;
				if (child.GetComponent<Slot> ().amount >= 2) {
					child.GetComponent<Slot> ().Decrease (2);
					hurt = true;
				}
			}
		}
	}
}
