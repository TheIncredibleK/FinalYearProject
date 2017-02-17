using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGenerator : MonoBehaviour {


	public GameObject[] possible_cockpits = new GameObject[2];
	public GameObject[] possible_bodies = new GameObject[3];
	public GameObject thruster;
	// Use this for initialization
	void Start () {
		GameObject body = (GameObject)Instantiate (possible_bodies [Random.Range (0, possible_bodies.Length)], this.transform.position, Quaternion.identity);
		GameObject cockpit =  (GameObject)Instantiate (possible_cockpits [Random.Range (0, possible_cockpits.Length)], new Vector3(body.transform.position.x, body.transform.position.y, body.transform.position.z), Quaternion.identity);
	
		//Change position to be halfway out of the ship.
		cockpit.transform.position += body.transform.up * (body.transform.localScale.y / 2);
		cockpit.transform.position -= body.transform.forward * (body.transform.localScale.z / 4);

		cockpit.transform.parent = body.transform;
		body.AddComponent<Setcolour> ();


		GameObject thrust = (GameObject)Instantiate (thruster, body.transform.position, Quaternion.identity);
		thrust.transform.position -= (body.transform.up * body.transform.localScale.y / 2) + (body.transform.up * thrust.transform.localScale.y / 2);
		thrust.transform.parent = body.transform;
		body.GetComponent<Setcolour> ().randomiseSections = true;
		body.GetComponent<Setcolour> ().randomiseColor = true;
		body.AddComponent<Flight>();
		body.GetComponent<Flight>().speed = Random.Range(10.0f, 30.0f);


	}
}
