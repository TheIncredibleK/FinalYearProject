using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAllChildren : MonoBehaviour {

	float[] speeds;
	// Use this for initialization
	void Start () {
		speeds = new float[this.transform.childCount];
		for (int i = 0; i < speeds.Length; i++) {
			speeds [i] = Random.Range (0, 4);
			if (i % 2 == 0) {
				speeds [i] *= -1;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < speeds.Length; i++) {
			this.transform.GetChild (i).transform.Rotate (Vector3.up, speeds [i]);
		}
		this.transform.Rotate (this.transform.forward, .3f * Time.deltaTime);
	}
}
