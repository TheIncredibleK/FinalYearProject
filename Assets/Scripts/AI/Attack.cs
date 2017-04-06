using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

public class Attack : MonoBehaviour {


	public float thresholdAttack;
	public float thresholdEvade;
	bool amIAttacking = true;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		GameObject[] beings = GameObject.FindGameObjectsWithTag ("Attack");
		for (int i = 0; i < beings.Length; i++) {
			Steering (beings[i], beings [i].GetComponent<OtherShips>().GetTarget());
			beings [i].GetComponent<Blackboard> ().GetBoolVar ("goalComplete").Value = CheckIfDead (beings [i]);
		}
	}

	//Steering behaviour for attacking
	void Steering(GameObject ship, Vector3 target) {
		
		//Get distance between ship and target
		float distance = Vector3.Distance (ship.transform.position, target);

		//If not attacking, avoiding
		if (!amIAttacking) {
			//This calls a function which uses the Steering function from Flee
			AvoidTarget (ship, target);
			//Swap to attack after breaching threshold
			if (distance > thresholdEvade) {
				amIAttacking = true;
			}
		} else {
			//Calls function which calls steering on Seek
			ChaseTarget(ship, target);
			//If breach threshold, swap
			if (distance < thresholdAttack) {
				amIAttacking = false;
			}
		}
	}

	void AvoidTarget(GameObject ship, Vector3 target) {
		this.gameObject.GetComponent<Flee> ().Steering (ship, target);
	}

	void ChaseTarget(GameObject ship, Vector3 target) {
		this.gameObject.GetComponent<Seek> ().Steering (ship, target);
			for (int i = 0; i < ship.transform.childCount; i++) {
				if (ship.transform.GetChild (i).name.Contains ("Guns")) {

					ship.transform.GetChild (i).gameObject.GetComponent<Firing>().fire = true;
			}
		}

	}

	bool CheckIfDead(GameObject ship) {
		if (ship.GetComponent<OtherShips> ().target == null) {
			return true;
		}
		return false;
	}
}
