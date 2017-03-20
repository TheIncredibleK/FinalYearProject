using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

public class OtherShips : MonoBehaviour {

	public List<GameObject> inventory;
	public float health;
	float aggression;
	float damage;
	float speed;
	int difficulty;
	Color myColor;
	public bool dropMyLoad = false;
	// Use this for initialization
	void Start () {
		health = 100.0f;

	}
	
	// Update is called once per frame
	void Update () {
		if (inventory.Count  <= 0) {
			difficulty = (int)Random.Range (0, 4);
			CreateInventory ();
		}
		if (dropMyLoad) {
			dropMyLoad = false;
			DropInventory ();
			this.transform.DetachChildren ();
		}
	}

	//Alter health of enemy
	void ChangeHealth(float change) {
		//Change it then set it on the blackboard for the behaviour machine
		health += change;
		this.GetComponent<Blackboard> ().GetFloatVar ("Health").Value = health;
	}

	//Drop everything in inventory
	void DropInventory() {

		for (int i = 0; i < inventory.Count; i++) {
			Instantiate(inventory[i], this.transform.position + (this.transform.forward * Random.Range(2,5) * Mathf.Sign(Random.Range(-1,1))) + (this.transform.right * Random.Range(2,5) * Mathf.Sign(Random.Range(-1,1))), Quaternion.identity);
		}
	}

	//Start off with some stuff in inventory
	void CreateInventory() {
		int amount = (difficulty + 1) * 5;
		GameObject[] possible_items = GameObject.FindGameObjectWithTag ("ShipMng").GetComponent<ShipGenerator>().possible_inventory_items; 
		for(int i = 0; i < amount; i++) {
			float possiblity = Random.Range (0, 10);
			int item = 0;
			if (possiblity < 1 + difficulty) {
				item = (int)Random.Range (possible_items.Length / 3, possible_items.Length);
			} else {
				item = Random.Range (0, possible_items.Length);
			}

			inventory.Add (possible_items [item]);
		}

	}

	void OnTriggerEnter(Collider other) {
		if (health <= 0) {
			if (other.gameObject.tag == "Bullet") {
				Destroy (other.gameObject);
				Detach ();
				DropInventory ();
				Destroy (this.gameObject);
			}
		} else {
			this.tag = "Flee";
			ChangeHealth (-10.0f);
		}

	}

	void Detach() {
		for (int i = 0; i < this.transform.childCount; i++) {
			this.transform.GetChild (i).gameObject.AddComponent<Rigidbody> ();
			this.transform.GetChild (i).gameObject.GetComponent<Rigidbody> ().useGravity = false;
			this.transform.GetChild (i).GetComponent<Rigidbody> ().velocity = this.transform.forward * Random.Range (-10, 10) + this.transform.up * Random.Range (-10, 10) + this.transform.right * Random.Range (-10, 10);
		}
		this.transform.DetachChildren ();
	}
}
