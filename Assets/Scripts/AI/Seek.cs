using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

public class Seek : MonoBehaviour {


	public List<GameObject> targets;

	public float rotationRate;
	public int range;
	public float maxAllowableDistance;
	// Use this for initialization
	void Start () {


	}

	// Update is called once per frame
	void Update () {
		//Get all ships that are seeking
		GameObject[] beings = GameObject.FindGameObjectsWithTag ("Seek");
		for (int i = 0; i < beings.Length; i++) {
			Steering (beings [i], beings [i].GetComponent<OtherShips>().GetTarget());
			FireWhenClose(beings [i], beings [i].GetComponent<OtherShips>().GetTarget());
		}

	}

	//Seek Steering behaviour
	public void Steering(GameObject ship, Vector3 target) {
		//Get speed of ship
		float speed = ship.GetComponent<OtherShips> ().speed;
		//Desired rotation wanted
		Quaternion desiredRotation = Quaternion.LookRotation (target - ship.transform.position);
		//Lerp between current rotation of the ship and the desired by their speed
		ship.transform.rotation = Quaternion.Lerp (ship.transform.rotation, desiredRotation, speed * Time.deltaTime);
		//Fly forwards by speed
		ship.transform.position += ship.transform.forward * Time.deltaTime * speed;
	}

	void FireWhenClose(GameObject ship, Vector3 target) {
		float curDist = Vector3.Distance (ship.transform.position, target);
		if (curDist < maxAllowableDistance) {
			for (int i = 0; i < ship.transform.childCount; i++) {
				if (curDist < 2.0f) {
					ship.GetComponent<OtherShips> ().CreateResourceTarget ();
				}
				if (ship.transform.GetChild (i).name.Contains ("Guns")) {

					ship.transform.GetChild (i).gameObject.GetComponent<Firing>().fire = true;
				}
			}
		}

	}





	IEnumerator FindTarget(float waitTime) {
		yield return new WaitForSeconds (waitTime);
		targets.Add(GameObject.FindGameObjectWithTag ("AstMng").GetComponent<AsteroidGenerator> ().requestRandomAsteroid ());
		//Debug.Log ("Made it out:: x: " + target.transform.position.x + "y: " + target.transform.position.y + " z: " + target.transform.position.z);
	}
}
