using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlachHole : MonoBehaviour {

	public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (this.transform.forward * speed);
	}
}
