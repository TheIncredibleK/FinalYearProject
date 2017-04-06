using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

	public GameObject target = null;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			GetTarget ();
		}
		this.transform.position = target.transform.position;
		this.transform.LookAt (target.transform);
	}

	void GetTarget() {
		target = GameObject.FindGameObjectsWithTag ("Move") [0];
	}
}
