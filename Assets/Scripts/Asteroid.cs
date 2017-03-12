using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	public int myX;
	public int myZ;

	// Use this for initialization
	void Start () {
		
	}

	public void RespawnSelf() {
		Debug.Log ("Respawnin self");
		GameObject.FindGameObjectWithTag ("AstMng").GetComponent<AsteroidGenerator> ().Respawn (myX, myZ);
		Debug.Log("Leaving rspawnSelf");
	
	}
}
