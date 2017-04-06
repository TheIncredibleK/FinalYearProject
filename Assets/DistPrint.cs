using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistPrint : MonoBehaviour {

	GameObject target;
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag ("Shop");
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Distance  :  " +Vector3.Distance (target.transform.position, this.transform.position));
	}
}
