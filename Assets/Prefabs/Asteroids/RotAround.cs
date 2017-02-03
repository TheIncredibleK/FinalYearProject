﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotAround : MonoBehaviour {

	float rotationSpeed;
	public float stdSpeed;
	// Use this for initialization
	void Start () {
		rotationSpeed = Random.Range (0.0f, this.transform.localScale.x * stdSpeed);
	}

	// Update is called once per frame
	void Update () {
		this.transform.Rotate (this.transform.up *rotationSpeed);
		this.transform.RotateAround (Vector3.zero, Vector3.up, rotationSpeed/100.0f * Time.deltaTime);
	}
}
