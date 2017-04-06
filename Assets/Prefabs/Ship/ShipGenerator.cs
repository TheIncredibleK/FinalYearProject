using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGenerator : MonoBehaviour {

	//Possible ship parts
	public GameObject[] possible_cockpits = new GameObject[2];
	public GameObject[] possible_bodies = new GameObject[3];
	public GameObject gun;
	public GameObject thruster;
	public GameObject[] possible_inventory_items;
	public GameObject FST;
	public int amount = 0;
	int initial = 5;
	bool creating = false;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < initial; i++) {
			CreateShip ();
		}
		StartCoroutine (WaitAndCreate());
	}

	void Update() {
		//If I am not creating and ship amount is les than 10, make ships until there is 10
		if (creating = false) {
			if (amount < 10) {
				StartCoroutine (WaitAndCreate ());
			}
		}
	}

	//Put ship parts in the right place and spawn them
	void CreateShip() {
		//Include the Finite State Machien as a parent object
		GameObject theParent = Instantiate (FST);
		theParent.AddComponent<Rigidbody> ();
		theParent.GetComponent<Rigidbody> ().useGravity = false;
		theParent.GetComponent<Rigidbody> ().isKinematic = true; 
		theParent.transform.position = new Vector3 (Random.Range (30, 100) * Mathf.Sign (Random.Range (-1, 1)), Random.Range (30, 100) * Mathf.Sign (Random.Range (-1, 1)),
			Random.Range (30, 100) * Mathf.Sign (Random.Range (-1, 1)));

		//Create the body, and choose a prefab randomly
		GameObject body = (GameObject)Instantiate (possible_bodies [Random.Range (0, possible_bodies.Length)], theParent.transform.position, Quaternion.identity);
		//Create a cockpit and choose a prefab randomly
		GameObject cockpit = (GameObject)Instantiate (possible_cockpits [Random.Range (0, possible_cockpits.Length)], new Vector3 (body.transform.position.x, 
			body.transform.position.y, body.transform.position.z), Quaternion.identity);


		//Change position to be halfway out of the ship.
		cockpit.transform.position += body.transform.up * (body.transform.localScale.y / 2);
		cockpit.transform.position -= body.transform.forward * (body.transform.localScale.z / 4);
		body.transform.parent = theParent.transform;
		cockpit.transform.parent = theParent.transform;
		//Set initial speed, this is altered in other ships based on difficult
		theParent.GetComponent<OtherShips> ().speed = 5.0f;

		//Create and move thruster
		GameObject thrust = (GameObject)Instantiate (thruster, body.transform.position, Quaternion.identity);
		thrust.transform.position -= (body.transform.up * body.transform.localScale.y / 2) + (body.transform.up * thrust.transform.localScale.y / 2);
		thrust.transform.parent = theParent.transform;
		theParent.GetComponent<Setcolour> ().randomiseSections = true;
		theParent.GetComponent<Setcolour> ().randomiseColor = true;

		//Creates guns and attaches
		GameObject guns = (GameObject)Instantiate (gun, body.transform.position + body.transform.forward * body.transform.localScale.y / 2, body.transform.rotation);
		guns.transform.parent = theParent.transform;
		//Increases amount of ships
		amount++;

	}
	//Coroutine that handles spawnign ships
	IEnumerator WaitAndCreate() {
		creating = true;
		while (amount < 10) {
			yield return new WaitForSeconds (60.0f);
			CreateShip ();
		}
		creating = false;
	}
		
}
