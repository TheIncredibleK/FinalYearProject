using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

public class Move : MonoBehaviour {


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
		GameObject[] beings = GameObject.FindGameObjectsWithTag ("Move");
		//If there isn't enough targets, get more
		if (targets.Count < beings.Length) {
			for (int i = 0; i < beings.Length - targets.Count; i++) {
				Debug.Log ("Creating");
				StartCoroutine(FindTarget (0.0f));
			}
		} else if(targets.Count > beings.Length){
			for (int i = 0; i < targets.Count - beings.Length; i++) {
				targets.RemoveAt (targets.Count - 1);
			}
		}


		for (int i = 0; i < beings.Length; i++) {
			Steering (beings [i], targets [i], i);
		}
		
	}

	void Steering(GameObject ship, GameObject target,int myIndex) {
		if (target != null) {
			float speed = 5.0f;
			Quaternion desiredRotation = Quaternion.LookRotation (target.transform.position - ship.transform.position);
			ship.transform.rotation = Quaternion.Slerp (ship.transform.rotation, desiredRotation, .01f);
			Vector3 Reltarget = target.transform.position - ship.transform.position;
			Debug.Log ("Making it out of steering");
			float curDist = Vector3.Distance (ship.transform.position, target.transform.position);
			if(curDist < maxAllowableDistance) {
				
				speed = 2.0f;
				ship.transform.FindChild ("Guns(Clone)").GetComponent<Firing> ().fire = true;
			} 
			if (curDist < maxAllowableDistance / 5) {

				targets.RemoveAt (myIndex);
			}
			ship.transform.position += ship.transform.forward * Time.deltaTime * speed;
			//Debug.Log ("x : " + target.x + " y: " + target.y + " z: " + target.z);
		}
	}





	IEnumerator FindTarget(float waitTime) {
		yield return new WaitForSeconds (waitTime);
		targets.Add(GameObject.FindGameObjectWithTag ("AstMng").GetComponent<AsteroidGenerator> ().requestRandomAsteroid ());
		//Debug.Log ("Made it out:: x: " + target.transform.position.x + "y: " + target.transform.position.y + " z: " + target.transform.position.z);
	}
}
