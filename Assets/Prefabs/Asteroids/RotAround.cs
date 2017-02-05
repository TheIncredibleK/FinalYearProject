using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotAround : MonoBehaviour {

	float rotationSpeed;
	public float stdSpeed;
	// Use this for initialization
	void Start () {
		rotationSpeed = Random.Range (0.0f, stdSpeed/this.transform.localScale.x);
	}

	// Update is called once per frame
	void Update () {
		this.transform.Rotate (this.transform.up *rotationSpeed);
		this.transform.RotateAround (new Vector3(0.0f, 0.0f, 0.0f), Vector3.up, rotationSpeed/100.0f * Time.deltaTime);
	}
}
