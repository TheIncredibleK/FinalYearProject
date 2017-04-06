using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

public class Flee : MonoBehaviour {

	float threshold = 50.0f;
	// Use this for initialization
	void Start () {


	}

	// Update is called once per frame
	void Update () {
		//Get all ships that are seeking
		GameObject[] beings = GameObject.FindGameObjectsWithTag ("Flee");
		for (int i = 0; i < beings.Length; i++) {
			Steering (beings [i], beings [i].GetComponent<OtherShips>().GetTarget());
			beings [i].GetComponent<Blackboard> ().GetBoolVar ("goalComplete").Value = CheckIfSafe (beings [i], beings [i].GetComponent<OtherShips> ().GetTarget ());
		}

	}

	//Sterring behaviour for Flee
	public void Steering(GameObject ship, Vector3 target) {
		//Get Speed of ship
		float speed = ship.GetComponent<OtherShips> ().speed;
		//Get rotation looking away from target
		Quaternion desiredRotation = Quaternion.LookRotation (ship.transform.position - target);
		//Lerp to rotation away from target
		ship.transform.rotation = Quaternion.Lerp (ship.transform.rotation, desiredRotation, speed * Time.deltaTime);
		//Consistently move forward
		ship.transform.position += ship.transform.forward * Time.deltaTime * speed;
	}

	bool CheckIfSafe(GameObject ship, Vector3 target) {
		if (Vector3.Distance (ship.transform.position, target) > threshold) {
			return true;
		}
		return false;
	}
}
