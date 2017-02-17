using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEffect : MonoBehaviour {

	public GameObject effect;
	// Use this for initialization
	void Start () {
		GameObject thing = (GameObject)Instantiate (effect, this.transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
