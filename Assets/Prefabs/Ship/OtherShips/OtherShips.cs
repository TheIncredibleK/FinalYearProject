using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

public class OtherShips : MonoBehaviour {
	//Class for other ships

	//Inventory of ship
	public List<GameObject> inventory;
	// Ships values to control it's difficulty, strengthe tc
	public float health;
	float aggression;
	float damage;
	public float speed;
	public int difficulty;
	//Target to be used for steering behaviours
	public GameObject target = null;
	//Vector to be used for steering behaviours to prevent Null pointer bug
	Vector3 target_;
	public bool dropMyLoad = false;
	// Use this for initialization
	void Start () {
		health = 100.0f;

	}
	
	// Update is called once per frame
	void Update () {
		//If there is no target, find a new one
		if (target == null) {
			CreateResourceTarget ();
		}

		//If there's no inventory, create one
		//Called here due to inventory setup needing to be called
		if (inventory.Count  <= 0) {
			difficulty = (int)Random.Range (0, 4);
			CreateInventory ();
			//Set other values based on difficulty
			damage = Random.Range (2, 6) * difficulty;
			aggression = Random.Range (10 * difficulty, 100);
			speed = 5 * 1 + Random.Range (1.0f, 1.5f) * difficulty;
			this.GetComponent<Blackboard> ().GetFloatVar ("Aggression").Value = aggression;

		}
		//Used for testing
	//	if (dropMyLoad) {
	//		Detach ();
	///		dropMyLoad = false;
	//		DropInventory ();
	//		this.transform.DetachChildren ();
	//	}
	}

	//Sets speed of current ship
	void SetSpeed() {
		this.GetComponent<Flight> ().speed = speed;
	}
	//Alter health of this ship on blackboard
	void ChangeHealth(float change) {
		//Change it then set it on the blackboard for the behaviour machine
		health += change;
		this.GetComponent<Blackboard> ().GetFloatVar ("Health").Value = health;
		this.GetComponent<Blackboard> ().GetFloatVar ("EnterHealth").Value = health;
	}
	//Return Vector target
	public Vector3 GetTarget() {
		return target_;
	}

	//Drop everything in inventory
	void DropInventory() {

		for (int i = 0; i < inventory.Count; i++) {
			Instantiate(inventory[i], this.transform.position + (this.transform.forward * Random.Range(2,5) * Mathf.Sign(Random.Range(-1,1))) + (this.transform.right * Random.Range(2,5) * Mathf.Sign(Random.Range(-1,1))), Quaternion.identity);
		}
	}

	//Start off with some stuff in inventory
	//More rare items and bigger inventory for more difficult ships
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

	//If the other object is a bullet
	//And I don't own that bullet
	//If I am dead, drop my inventory and explore
	//Else, decrease my inventory and set my target to my attacker
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Contains("Bullet")) {
			if (other.gameObject.GetComponent<Bullet> ().myOwner != this.gameObject.GetInstanceID ()) {
				Debug.Log ("My ID : " + this.GetInstanceID () + " It's Owner " + other.gameObject.GetComponent<Bullet> ().myOwner);
				if (health <= 0) {
					Detach ();
					DropInventory ();
					Destroy (this.gameObject);
				} else {
					ChangeHealth (-other.gameObject.GetComponent<Bullet> ().damage);
					target = other.gameObject.GetComponent<Bullet>().shipShotFrom;
				} 
			}
			Destroy (other.gameObject);
		}

	}

	//Detach all children, and set some of them up to be collected
	//Drop my invenotry, and notify the ship generator one has died
	void Detach() {
		for (int i = 0; i < this.transform.childCount; i++) {
			GameObject currentChild = this.transform.GetChild (i).gameObject;
			currentChild.AddComponent<Rigidbody> ();

			if (currentChild.name.Contains ("Thruster")) {
				Debug.Log ("Created Collectable Thrusters for :" + currentChild.name);
				currentChild.AddComponent<Item> ();
				currentChild.GetComponent<Item> ().myName = "Thruster";
				currentChild.GetComponent<Item> ().Setup ();

			} else if (currentChild.name.Contains ("Guns")) {
				Debug.Log ("Created Collectable Guns");
				currentChild.AddComponent<Item> ();
				currentChild.GetComponent<Item> ().myName = "Powercells";
				currentChild.GetComponent<Item> ().Setup ();
			} 

			currentChild.GetComponent<Rigidbody> ().useGravity = false;
			currentChild.GetComponent<Rigidbody> ().velocity = this.transform.forward * Random.Range (-2, 2) + this.transform.up * Random.Range (-2, 2) + this.transform.right * Random.Range (-2, 2);
		}
		this.transform.DetachChildren ();
		GameObject.FindGameObjectWithTag ("ShipMng").GetComponent<ShipGenerator> ().amount -= 1;
	}

	//Set up what target is a resource
	public void CreateResourceTarget() {
		target = GameObject.FindGameObjectWithTag ("AstMng").GetComponent<AsteroidGenerator> ().requestRandomAsteroid ();
		target_ = target.transform.position;
	}
}
