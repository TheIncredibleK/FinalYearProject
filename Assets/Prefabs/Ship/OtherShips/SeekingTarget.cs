using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingTarget : MonoBehaviour {

	public float rotationRate;
	GameObject target;
	bool gotTarget = false;
	public int range;
	public float maxAllowableDistance;
	// Use this for initialization
	void Start () {
		StartCoroutine (FindTarget (1.0f));

	}

	void Update() {
		Steering ();
	}


	void Steering() {
		if (target != null) {
			Quaternion desiredRotation = Quaternion.LookRotation (target.transform.position - this.transform.position);
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, desiredRotation, .01f);
			Vector3 Reltarget = target.transform.position - this.transform.position;
			Vector3 steering = Vector3.Normalize (Reltarget - this.GetComponent<Rigidbody> ().velocity) * 4.0f;
			float curDist = Vector3.Distance (this.transform.position, steering);
			if (curDist < maxAllowableDistance) {
				steering *= (curDist / maxAllowableDistance);
			}
			this.GetComponent<Rigidbody> ().velocity = steering;
			//Debug.Log ("x : " + target.x + " y: " + target.y + " z: " + target.z);
		}



	}
		

	IEnumerator FindTarget(float waitTime) {
		yield return new WaitForSeconds (waitTime);
		target = GameObject.FindGameObjectWithTag ("AstMng").GetComponent<AsteroidGenerator> ().requestRandomAsteroid ();
		Debug.Log ("Made it out:: x: " + target.transform.position.x + "y: " + target.transform.position.y + " z: " + target.transform.position.z);
	}
}
