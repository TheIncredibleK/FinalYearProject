using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour {

	float speed = 10.0f;
	GameObject newFlee;
	// Use this for initialization
	void Start () {
		newFlee = new GameObject ();
		newFlee.transform.position = new Vector3 (Random.Range (-200, 200), Random.Range (-200, 200), Random.Range (-200, 200));
	}

	void Update() {
		GameObject[] beings = GameObject.FindGameObjectsWithTag ("Flee");
		for( int i = 0; i < beings.Length; i++) {
			Fleeing (beings [i]);
		}

	}

	// Update is called once per frame
	void Fleeing(GameObject ship) {
		Quaternion desiredRotation = Quaternion.LookRotation (newFlee.transform.position - ship.transform.position);
		ship.transform.rotation = Quaternion.Slerp (ship.transform.rotation, desiredRotation, .01f);
		ship.transform.position += ship.transform.forward * Time.deltaTime * speed;

		float r = Random.Range (0, 100);
		if (r < 5) {
			newFlee.transform.position = new Vector3 (Random.Range (-200, 200), Random.Range (-200, 200), Random.Range (-200, 200));

		}
	}
}
