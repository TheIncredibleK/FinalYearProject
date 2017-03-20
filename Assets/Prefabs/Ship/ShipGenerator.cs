using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGenerator : MonoBehaviour {


	public GameObject[] possible_cockpits = new GameObject[2];
	public GameObject[] possible_bodies = new GameObject[3];
	public GameObject gun;
	public GameObject thruster;
	public GameObject[] possible_inventory_items;
	int amount = 5;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < amount; i++) {
			GameObject theParent = new GameObject ();
			theParent.AddComponent<Rigidbody> ();
			theParent.GetComponent<Rigidbody> ().useGravity = false;
			theParent.GetComponent<Rigidbody> ().isKinematic = true; 
			theParent.tag = "Move";
			theParent.transform.position = new Vector3 (Random.Range (30, 100) * Mathf.Sign (Random.Range (-1, 1)), Random.Range (30, 100) * Mathf.Sign (Random.Range (-1, 1)), Random.Range (30, 100) * Mathf.Sign (Random.Range (-1, 1)));
			GameObject body = (GameObject)Instantiate (possible_bodies [Random.Range (0, possible_bodies.Length)], theParent.transform.position, Quaternion.identity);
			GameObject cockpit = (GameObject)Instantiate (possible_cockpits [Random.Range (0, possible_cockpits.Length)], new Vector3 (body.transform.position.x, body.transform.position.y, body.transform.position.z), Quaternion.identity);

			//Change position to be halfway out of the ship.
			cockpit.transform.position += body.transform.up * (body.transform.localScale.y / 2);
			cockpit.transform.position -= body.transform.forward * (body.transform.localScale.z / 4);
			body.transform.parent = theParent.transform;
			cockpit.transform.parent = theParent.transform;
			theParent.AddComponent<Setcolour> ();
			theParent.AddComponent<OtherShips> ();

			//Create and move thruster
			GameObject thrust = (GameObject)Instantiate (thruster, body.transform.position, Quaternion.identity);
			thrust.transform.position -= (body.transform.up * body.transform.localScale.y / 2) + (body.transform.up * thrust.transform.localScale.y / 2);
			thrust.transform.parent = theParent.transform;
			theParent.GetComponent<Setcolour> ().randomiseSections = true;
			theParent.GetComponent<Setcolour> ().randomiseColor = true;
			//theParent.AddComponent<SeekingTarget> ();
			GameObject guns = (GameObject)Instantiate (gun, body.transform.position + body.transform.forward * body.transform.localScale.y / 2, body.transform.rotation);
			guns.transform.parent = theParent.transform;

		}

	}
}
